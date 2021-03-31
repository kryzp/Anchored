using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;

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
