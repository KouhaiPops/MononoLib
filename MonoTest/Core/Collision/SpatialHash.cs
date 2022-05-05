using MonoTest.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Core.Collision
{
    public sealed class SpatialHash
    {
        private Dictionary<int, HashSet<IElement>> elements = new();
        public int CellSize { get; private set; }
        public int MaxSize { get; private set; }
        private int width;
        public void Initialize(int cellSize, int maxSize = 20000)
        {
            if(cellSize <= 0)
            {
                throw new InvalidOperationException("Canno't create a spatial hash map with cellsize smaller or equal to zero.");
            }
            CellSize = cellSize;
            MaxSize = maxSize;
            width = MaxSize / cellSize;
        }

        public void AddElement(IElement element)
        {
            var position = element.Transform.Position;
            //var gridCell = position.X
        }

    }
}
