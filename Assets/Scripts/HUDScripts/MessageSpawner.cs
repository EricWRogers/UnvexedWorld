using System.Collections;
using System.Collections.Generic;
//using Scripts.System.MessageSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.HUDScripts.MessageSystem
{
    public class MessageSpawner : MonoBehaviour
    {
        public GameObject floatingMessagePrefab;
        public void ApplyDamage(GameObject damageSource)
        {
            IDamageDealer damageDealer = damageSource.GetComponent<IDamageDealer>();
            if (damageDealer != null)
            {
                int damage = damageDealer.GetDamage();
                Debug.Log("Damage dealt: " + damage);
                //ShowFloatingMessage(damage);
            }
            else
            {
                Debug.Log("No IDamageDealer found on the damage source.");
            }
        }

        public void ShowFloatingMessage(SuperPupSystems.Helper.HealthChangedObject healthChangedObject)
        {
            int damage = -healthChangedObject.delta;
            GameObject message = Instantiate(floatingMessagePrefab, transform.position + (Vector3.up * -0.5f), Quaternion.identity);
            FloatingMessage floatingMsg = message.GetComponent<FloatingMessage>();
            if (floatingMsg != null)
            {
                floatingMsg.SetMessage(damage);
            }
        }
    }
}
