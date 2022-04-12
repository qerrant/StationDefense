using StationDefense.Components;
using StationDefense.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace StationDefense.Path
{
    public class PathExplorer : Singleton<PathExplorer>
    {
        protected Queue<Vector2Int> _queue = new Queue<Vector2Int>();
        protected Dictionary<Vector2Int, bool> _visited = new Dictionary<Vector2Int, bool>();
        protected Dictionary<Vector2Int, bool> _used = new Dictionary<Vector2Int, bool>();
        protected Dictionary<Vector2Int, Vector2Int> _linkedList = new Dictionary<Vector2Int, Vector2Int>();
        protected Vector2Int _currentGridPosition;

        protected Vector2Int[] _directions = {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        protected List<Vector2Int> Explore(Vector2Int startTile, Vector2Int finishTile, ref Dictionary<Vector2Int, Tile> grid)
        {
            Clear();

            _currentGridPosition = startTile;

            FillUsedGridPositions(ref grid);
            _used[finishTile] = false;

            _queue.Enqueue(startTile);
            _visited.Add(startTile, true);

            while (_queue.Count > 0 && _currentGridPosition != finishTile)
            {
                _currentGridPosition = _queue.Dequeue();
                EnumerateDirections();
            }

            return ExtractPath(finishTile);
        }

        protected List<Vector2Int> ExtractPath(Vector2Int finishTile)
        {
            List<Vector2Int> list = new List<Vector2Int>();
            Vector2Int position = finishTile;

            while (_linkedList.ContainsKey(position))
            {
                list.Add(_linkedList[position]);
                position = _linkedList[position];
            }

            list.Reverse();

            return list;
        }

        protected void Clear()
        {
            _queue.Clear();
            _visited.Clear();
            _used.Clear();
            _linkedList.Clear();
        }

        protected void FillUsedGridPositions(ref Dictionary<Vector2Int, Tile> grid)
        {
            foreach (KeyValuePair<Vector2Int, Tile> item in grid)
            {
                _used.Add(item.Key, item.Value.Used);
            }
        }

        protected void EnumerateDirections()
        {
            foreach (Vector2Int direction in _directions)
            {
                Vector2Int neighborGridPosition = _currentGridPosition + direction;
                bool isNotUsed = _used.ContainsKey(neighborGridPosition) && !_used[neighborGridPosition];
                bool isNotVisited = !_visited.ContainsKey(neighborGridPosition);

                if (isNotUsed && isNotVisited)
                {
                    AddNeighborToQueue(ref neighborGridPosition);
                }
            }
        }

        protected void AddNeighborToQueue(ref Vector2Int gridPosition)
        {
            _queue.Enqueue(gridPosition);
            _visited.Add(gridPosition, true);
            _linkedList.Add(gridPosition, _currentGridPosition);
        }

        public virtual List<Tile> GetPath(Tile startTile)
        {
            return new List<Tile>();
        }

        public virtual bool UpdatePath(Vector2Int gridPosition)
        {
            return false;
        }
    }
}
