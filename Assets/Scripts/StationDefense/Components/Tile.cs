using UnityEngine;

namespace StationDefense.Components
{
    public class Tile : MonoBehaviour
    {
        private float _tileSize = 2.0f;

        public bool Used { get; set; }

        private void Awake()
        {
            BoxCollider m_Collider = GetComponent<BoxCollider>();

            if (m_Collider != null)
            {
                _tileSize = m_Collider.size.x;
            }

            Used = false;
        }

        public float GetTileSize()
        {
            return _tileSize;
        }

        public Vector2Int GetGridPosition()
        {
            return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / _tileSize),
                Mathf.RoundToInt(transform.position.z / _tileSize)
            );
        }

    }
}
