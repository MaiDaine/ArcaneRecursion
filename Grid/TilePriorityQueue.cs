using System.Collections.Generic;

namespace ArcaneRecursion
{
    public class TilePriorityQueue
    {
        public int Count { get; private set; } = 0;
        public Tile[] Tiles { get { return _list.ToArray(); } }

        private readonly List<Tile> _list = new List<Tile>();
        private int _minimum = int.MaxValue;

        public void Enqueue(Tile tile)
        {
            Count++;
            int priority = tile.SearchData.SearchPriority;
            if (priority < _minimum)
                _minimum = priority;
            while (priority >= _list.Count)
                _list.Add(null);
            tile.SearchData.NextWithSamePriority = _list[priority];
            _list[priority] = tile;
        }

        public Tile Dequeue()
        {
            Count--;
            for (; _minimum < _list.Count; _minimum++)
            {
                Tile tile = _list[_minimum];
                if (tile != null)
                {
                    _list[_minimum] = tile.SearchData.NextWithSamePriority;
                    return tile;
                }
            }
            return null;
        }

        public void Change(Tile tile, int oldPriority)
        {
            Tile current = _list[oldPriority];
            Tile next = current.SearchData.NextWithSamePriority;
            if (current == tile)
            {
                _list[oldPriority] = next;
            }
            else
            {
                while (next != tile)
                {
                    current = next;
                    next = current.SearchData.NextWithSamePriority;
                }
                current.SearchData.NextWithSamePriority = tile.SearchData.NextWithSamePriority;
            }
            Enqueue(tile);
            Count--;
        }

        public void Clear()
        {
            _list.Clear();
            Count = 0;
            _minimum = int.MaxValue;
        }
    }
}