using StationDefense.Game;
using UnityEngine;

namespace StationDefense.Components
{
    public class RotatedAnimation : MonoBehaviour
    {
        public GameObject rotator;
        public float speed = 2.0f;


        void Update()
        {
            if (GameManager.instanceExists)
            {
                if (GameManager.instance.State != GameState.Game) return;
            }
            rotator.transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
