using UnityEngine;

namespace StationDefense.UI
{
    public class Billboard : MonoBehaviour
    {
        private Transform _camera;

        void Start()
        {
            _camera = Camera.main.transform;
        }


        void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.forward);
        }
    }
}
