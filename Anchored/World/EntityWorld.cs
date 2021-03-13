using Anchored.Streams;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anchored.World
{
	public class EntityWorld
	{
		private List<Entity> entities = new List<Entity>();
		private List<Int32> entitiesFree = new List<Int32>();
		private List<Int32> entitiesDestroying = new List<Int32>();

		private List<IUpdatable> updatables = new List<IUpdatable>();
		private List<IDrawable> renderables = new List<IDrawable>();

		private Dictionary<Type, List<Component>> components = new Dictionary<Type, List<Component>>();
		private Dictionary<Type, List<Int32>> componentsFree = new Dictionary<Type, List<Int32>>();
		private List<SimpleComponentHandle> componentsInitializing = new List<SimpleComponentHandle>();
		private List<SimpleComponentHandle> componentsDestroying = new List<SimpleComponentHandle>();

		private UInt16 uniqueID;

		public List<Entity> Entities => entities;
		public Dictionary<Type, List<Component>> Components => components;

		public EntityWorld()
		{
			uniqueID = 0;
		}

		~EntityWorld()
		{
			entitiesDestroying.Clear();
			entities.Clear();
			entitiesFree.Clear();
			updatables.Clear();
			componentsInitializing.Clear();
			foreach (var componentList in components.Values) componentList.Clear();
			foreach (var componentList in componentsFree.Values) componentList.Clear();
			uniqueID = 0;
		}

		public void Update()
		{
			// Init Components
			{
				for (int ii = 0; ii < componentsInitializing.Count; ii++)
				{
					if (!componentsInitializing[ii])
						continue;

					componentsInitializing[ii].Get().Init();
				}

				componentsInitializing.Clear();
			}

			// Update Components
			{
				updatables.Clear();

				foreach (var componentList in components.Values)
				{
					if (componentList.Any())
					{
						foreach (var component in componentList)
						{
							if (component is IUpdatable)
							{
								if (component.Enabled)
								{
									updatables.Add((IUpdatable)component);
								}
							}
						}
					}
				}

				// TODO(kmp): order the updatables by update order here!

				foreach (var it in updatables)
					it.Update();
			}

			ResolveRemoving();
		}

		public void Draw(SpriteBatch sb)
		{
			// Render Components
			{
				renderables.Clear();

				foreach (var componentList in components.Values)
				{
					if (componentList.Any())
					{
						foreach (var component in componentList)
						{
							if (component is IDrawable)
							{
								if (component.Enabled)
								{
									renderables.Add((IDrawable)component);
								}
							}
						}
					}
				}

				// TODO(kmp): order the renderables by render order here!

				foreach (var it in renderables)
					it.Draw(sb);
			}
		}

		public void ResolveRemoving()
		{
			for (int ii = 0; ii < componentsDestroying.Count; ii++)
			{
				if (!componentsDestroying[ii])
					continue;

				var destroying = componentsDestroying[ii].Get();
				var en_components = destroying.Entity.Components;
				for (int jj = 0; jj < en_components.Count; jj++)
				{
					if (en_components[jj].Index == destroying.Index && en_components[jj].Type == destroying.Type)
					{
						destroying.Destroyed();
						en_components.Remove(en_components[jj]);
						break;
					}
				}
			}
		}

		public Entity AddEntity(string name, Vector2? position = null, float rotation = 0f, Vector2? scale = null)
		{
			int slot = -1;

			if (entitiesFree.Count > 0)
			{
				slot = entitiesFree[0];
				entitiesFree.RemoveAt(entitiesFree.Count-1);
			}
			else
			{
				slot = entities.Count;
				entities.Resize(entities.Count+1);
			}

			uniqueID += 1;

			if (uniqueID == 0)
				uniqueID = 1;

			var entity = entities[slot];
			entity.Transform.Origin = Vector2.Zero;
			entity.Transform.Position = position ?? Vector2.Zero;
			entity.Transform.RotationDegrees = rotation;
			entity.Transform.Scale = scale ?? Vector2.One;
			entity.ID = uniqueID;
			entity.Index = (UInt16)slot;
			entity.World = this;
			entity.Name = name;

			return entity;
		}

		public void DestroyEntity(Entity entity)
		{
			if (entity.IsValid())
				entitiesDestroying.Add(entity.Index);
		}

		public Entity GetEntity(Entity entity)
		{
			return entities[entity.Index];
		}

		public void ForeachEntity(Action<Entity> func)
		{
			foreach (var entity in entities)
			{
				if (entity.ID != 0)
				{
					func(entity);
				}
			}
		}

		public bool ValidEntity(Entity entity)
		{
			if (entity.ID > 0 &&
				entity.Index >= 0 &&
				entity.Index < entities.Count &&
				entities[entity.Index].ID == entity.ID)
			{
				return true;
			}

			return false;
		}

		public T AddComponent<T>(Entity entity, T component) where T : Component, new()
		{
			int slot = -1;

			if (!componentsFree.ContainsKey(typeof(T)))
				componentsFree.Add(typeof(T), new List<int>());

			if (componentsFree[typeof(T)].Count > 0)
			{
				slot = componentsFree[typeof(T)][0];
				componentsFree[typeof(T)].RemoveAt(componentsFree[typeof(T)].Count-1);
			}
			else if (!components.ContainsKey(typeof(T)))
			{
				components.Add(typeof(T), new List<Component>());
			}

			var list = components[typeof(T)];

			if (slot < 0)
			{
				slot = list.Count;
				list.Resize(slot+1);
			}

			uniqueID += 1;

			if (uniqueID == 0)
				uniqueID = 1;

			entity.Components.Add(new SimpleComponentHandle(this, typeof(T), list[slot].Index, list[slot].ID));
			componentsInitializing.Add(new SimpleComponentHandle(this, typeof(T), list[slot].Index, list[slot].ID));

			T comp = new T();
			comp = component;
			comp.ID = uniqueID;
			comp.Index = (UInt16)slot;
			comp.Entity = entity;
			comp.Enabled = true;
			comp.Type = typeof(T);

			list[slot] = comp;
			return (T)comp;
		}

		public void RemoveComponent<T>(Entity entity, SimpleComponentHandle component) where T : Component
		{
			componentsDestroying.Add(component);
		}

		public void RemoveComponent<T>(Entity entity, T component) where T : Component
		{
			componentsDestroying.Add(new SimpleComponentHandle(this, typeof(T), component.Index, component.ID));
		}

		public void RemoveComponent<T>(Entity entity) where T : Component
		{
			componentsDestroying.Add(new SimpleComponentHandle(this, typeof(T), 0, components[typeof(T)][0].ID));
		}

		public T GetComponent<T>(SimpleComponentHandle component) where T : Component
		{
			return (T)components[typeof(T)][component.Index];
		}

		public T GetComponent<T>() where T : Component
		{
			return (T)components[typeof(T)][0];
		}

		public bool ValidComponent<T>(T component) where T : Component
		{
			if (components.ContainsKey(typeof(T)))
			{
				var list = components[typeof(T)];
				if (component.ID > 0 &&
					component.Index >= 0 &&
					component.Index < list.Count &&
					list[component.Index].ID == component.ID)
				{
					return true;
				}
			}

			return false;
		}

		public bool ValidComponent(Component component, Type type)
		{
			if (components.ContainsKey(type))
			{
				var list = components[type];
				if (component.ID > 0 &&
					component.Index >= 0 &&
					component.Index < list.Count &&
					list[component.Index].ID == component.ID)
				{
					return true;
				}
			}

			return false;
		}

		public void DestroyComponent<T>(T component) where T : Component
		{
			if (component.IsValid())
				componentsDestroying.Add(new SimpleComponentHandle(this, typeof(T), component.Index, component.ID));
		}

		public int FindComponent<T>(ref List<T> populate, int capacity, Masks mask = 0) where T : Component
		{
			int index = 0;

			if (capacity > 0 && components[typeof(T)] != null)
			{
				var list = components[typeof(T)];
				foreach (var it in list)
				{
					if (it.ID > 0 && (it.Mask & mask) != 0)
					{
						populate[index] = (T)it;
						index += 1;
						if (index >= capacity)
							break;
					}
				}
			}

			return index;
		}

		public int FindComponent<T>(ref List<T> populate, Masks mask = 0) where T : Component
		{
			int index = 0;

			if (components[typeof(T)] != null)
			{
				var list = components[typeof(T)];
				foreach (var it in list)
				{
					if (it.ID > 0 && (it.Mask & mask) != 0)
					{
						populate.Add((T)it);
						index++;
					}
				}
			}

			return index;
		}

		public void ForeachComponent<T>(Masks mask, Action<T> func, bool onlyEnabled = true) where T : Component
		{
			if (components[typeof(T)] != null)
			{
				var list = components[typeof(T)];
				foreach (var it in list)
				{
					if (it.ID > 0 && (it.Mask & mask) != 0 && (it.Enabled || !onlyEnabled))
					{
						func((T)it);
					}
				}
			}
		}

		public void ForeachComponentStoppable<T>(Masks mask, Func<T, bool> func, bool onlyEnabled = true) where T : Component
		{
			if (components[typeof(T)] != null)
			{
				var list = components[typeof(T)];
				foreach (var it in list)
				{
					if (it.ID > 0 && (it.Mask & mask) != 0 && (it.Enabled || !onlyEnabled))
					{
						if (!func((T)it))
							break;
					}
				}
			}
		}

		public void Save(FileWriter stream)
		{
			foreach (var componentList in components.Values)
			{
				foreach (var component in componentList)
				{
					component.Save(stream);
				}
			}
		}

		public void Load(FileReader stream)
		{
			foreach (var componentList in components.Values)
			{
				foreach (var component in componentList)
				{
					component.Load(stream);
				}
			}
		}
	}
}
