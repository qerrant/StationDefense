using StationDefense.Game;
using UnityEngine;

namespace StationDefense.Components
{
    public class Pan : MonoBehaviour
    {
        public Camera cam;
        public float floorZ = 0.0f;
        public Transform quad;
        public float stepZoom = 1.0f;
        public float minZoom = 6.0f;
        public float maxZoom = 16.0f;
        private Vector3 _touchStart;
        private GameState _state;

        private void Awake()
        {
            GameManager.OnGameStateChange += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnStateChanged;
        }

        private void OnStateChanged(GameState state)
        {
            _state = state;
        }


        void Update()
        {
            if (_state != GameState.Game) return;

            if (Input.GetMouseButtonDown(0))
            {
                _touchStart = GetWorldPosition(floorZ);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = _touchStart - GetWorldPosition(floorZ);
                Camera.main.transform.position += direction;
                float x = Mathf.Clamp(Camera.main.transform.position.x,
                    quad.GetComponent<BoxCollider>().bounds.min.x,
                    quad.GetComponent<BoxCollider>().bounds.max.x);
                float y = Camera.main.transform.position.y;
                float z = Mathf.Clamp(Camera.main.transform.position.z,
                    quad.GetComponent<BoxCollider>().bounds.min.z,
                    quad.GetComponent<BoxCollider>().bounds.max.z);
                Camera.main.transform.position = new Vector3(x, y, z);
            }
        }

        private Vector3 GetWorldPosition(float y)
        {
            Ray mousePosition = cam.ScreenPointToRay(Input.mousePosition);
            Plane floor = new Plane(Vector3.up, new Vector3(0, y, 0));
            float distance;
            floor.Raycast(mousePosition, out distance);
            return mousePosition.GetPoint(distance);
        }

        public void ZoomIn()
        {
            if (_state != GameState.Game) return;
            Vector3 pos = Camera.main.transform.position;
            float newY = Mathf.Clamp(pos.y - stepZoom, minZoom, maxZoom);
            Camera.main.transform.position = new Vector3(pos.x, newY, pos.z);
        }

        public void ZoomOut()
        {
            if (_state != GameState.Game) return;
            Vector3 pos = Camera.main.transform.position;
            float newY = Mathf.Clamp(pos.y + stepZoom, minZoom, maxZoom);
            Camera.main.transform.position = new Vector3(pos.x, newY, pos.z);
        }
    }    
}
