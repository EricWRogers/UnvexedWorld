using System.Collections;
using System.Collections.Generic;
using Scripts.System.MessageSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Systems.MessageSystem
{
    public class MessageSpawner : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _initialPosition;

        [SerializeField]
        private GameObject _messagePrefab;

        public void SpawnMessage(string msg)
        {
            var msgObj = Instantiate(_messagePrefab, GetSpawnPosition(), Quaternion.identity);
            var inGameMessage = msgObj.GetComponent<FloatingMessage>();

            // Set the message text to the damage value
            if (inGameMessage != null)
            {
                inGameMessage.SetMessage(msg);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            return transform.position + (Vector3)_initialPosition;
        }
    }
}
