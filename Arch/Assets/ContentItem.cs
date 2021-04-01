using Microsoft.Xna.Framework.Content.Pipeline;

namespace Arch.Assets
{
    public class ContentItem<T> : ContentItem
    {
        public ContentItem(T data)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
