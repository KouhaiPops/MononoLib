using Microsoft.Xna.Framework;

using MonoTest.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Collision
{
    /// <summary>
    /// A spatial hash for static elements
    /// It's used in handling UI events and propegations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StaticSpatialHash<T> where T : IElement
    {
        private Dictionary<int, HashSet<T>> elements = new();
        public int CellSize { get; private set; }
        public int MaxSize { get; private set; }
        private int width;
        private float conversionFactor;

        public StaticSpatialHash()
        {
            Initialize(50);
        }
        public void Initialize(int cellSize, int maxSize = 20000)
        {
            if(cellSize <= 0)
            {
                throw new InvalidOperationException("Canno't create a spatial hash map with cellsize smaller or equal to zero.");
            }
            CellSize = cellSize;
            MaxSize = maxSize;
            width = MaxSize / cellSize;
            conversionFactor = 1f / CellSize;
        }

        public void AddElement(T element)
        {
            var position = element.Transform.Position;
            AddElement(element, position);
            AddElement(element, position + element.Transform.Size);
        }

        private void AddElement(T element, Vector2 position)
        {
            var gridCell = (int)(position.X * conversionFactor + position.Y * conversionFactor * width);
            if (elements.TryGetValue(gridCell, out var hashSet))
            {
                hashSet.Add(element);
            }
            else
            {
                elements[gridCell] = new HashSet<T>() { element };
            }
        }

        public void RemoveElement(T element)
        {
            var position = element.Transform.Position;
            var gridCell = (int)(position.X * conversionFactor + position.Y * conversionFactor * width);
            if (elements.TryGetValue(gridCell, out var hashSet))
            {
                hashSet.Remove(element);
            }
        }

        public T[] GetNeighbours(T element)
        {
            var position = element.Transform.Position;
            return GetPoint(position);
            
        }
        public T[] GetPoint(Vector2 position)
        {
            var gridCell = (int)(position.X * conversionFactor + position.Y * conversionFactor * width);
            if (elements.TryGetValue(gridCell, out var hashSet))
            {
                return hashSet.ToArray();
            }
            else
            {
                return Array.Empty<T>();
            }
        }
    }
}
