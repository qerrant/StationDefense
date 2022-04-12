using StationDefense.Components;
using System.Collections.Generic;
using UnityEngine;

namespace StationDefense.Path
{
    [DisallowMultipleComponent]
    public class PathManager : PathExplorer
    {
        public GameObject tileBranch;
        public Tile finishTile;
        private Dictionary<Vector2Int, List<Tile>> _paths = new Dictionary<Vector2Int, List<Tile>>();
        private Dictionary<Vector2Int, Tile> _grid = new Dictionary<Vector2Int, Tile>();

        public override List<Tile> GetPath(Tile startTile)
        {
            if (_paths.ContainsKey(startTile.GetGridPosition()))
            {
                return _paths[startTile.GetGridPosition()];
            }

            List<Vector2Int> list = Explore(startTile.GetGridPosition(), finishTile.GetGridPosition(), ref _grid);

            if (list != null && list.Count > 0)
            {
                List<Tile> path;

                GridToTile(ref list, out path);

                AddPath(startTile.GetGridPosition(), path);

                return path;
            } else return new List<Tile>();
        }

        private void AddPath(Vector2Int gridPosition, List<Tile> path)
        {
            if (_paths.ContainsKey(gridPosition)) return;
            _paths.Add(gridPosition, path);
        }

        public override bool UpdatePath(Vector2Int gridPosition)
        {
            if (_grid == null || _grid.Count < 1)
            {
                FillGrid();
            }

            if (_paths.ContainsKey(gridPosition))
            {
                List<Tile> oldPath = new List<Tile>(_paths[gridPosition]);
            
                _paths.Remove(gridPosition);
                List<Tile> newPath = GetPath(_grid[gridPosition]);

                if (newPath == null || newPath.Count < 1)
                {
                    _paths.Add(gridPosition, oldPath);
                    return false;
                }

                return true;
            }

            GetPath(_grid[gridPosition]);

            return true;
        }

        private void GridToTile(ref List<Vector2Int> list, out List<Tile> path)
        {
            path = new List<Tile>();

            foreach (Vector2Int position in list)
            {
                path.Add(_grid[position]);
            }

            path.Add(finishTile);
        }

        private void FillGrid()
        {
            foreach (Tile tile in tileBranch.GetComponentsInChildren<Tile>())
            {
                _grid.Add(tile.GetGridPosition(), tile);
            }
        }
    }
}
