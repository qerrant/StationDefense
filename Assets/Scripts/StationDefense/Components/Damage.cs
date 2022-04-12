using UnityEngine;
using UnityEngine.Events;

namespace StationDefense.Components
{
    [RequireComponent(typeof(Collider))]
    public class Damage : MonoBehaviour
    {
        public LayerMask friendLayer;
        public UnityEvent<float> getDamage;

        private void OnTriggerEnter(Collider other)
        {
            if ((friendLayer.value & (1 << other.transform.gameObject.layer)) > 0) return;
            Damager damager = other.gameObject.GetComponent<Damager>();
            if (damager == null) return;
            for (int i = 0; i < getDamage.GetPersistentEventCount(); i++)
            {
                ((MonoBehaviour)getDamage.GetPersistentTarget(i)).SendMessage(getDamage.GetPersistentMethodName(i), damager.GetDamage());
            }            
        }       
    }    
}
