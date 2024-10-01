using System.Collections;
using System.Collections.Generic;
using Scripts.System.MessageSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace MessageSystem
{
    public class MessageSpawner : MonoBehaviour
    {
        public GameObject floatingMessagePrefab;
        public GameObject enemy;

        public void SpawnMessage()
        {
            GameObject newMessage = Instantiate(floatingMessagePrefab, enemy.transform.position, Quaternion.identity);
            FloatingMessage messageScript = newMessage.GetComponent<FloatingMessage>();
            if (messageScript != null)
            {
                messageScript.SetMessage();
            }
        }
    }
}
