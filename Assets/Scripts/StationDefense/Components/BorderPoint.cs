using StationDefense.Components;
using UnityEngine;

public class BorderPoint : MonoBehaviour
{
    void Start()
    {
        Tile ground = GetComponentInParent<Tile>();
        if (ground != null)
        {
            ground.Used = true;
        }
    }
}
