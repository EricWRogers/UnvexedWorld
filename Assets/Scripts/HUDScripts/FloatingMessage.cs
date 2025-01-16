using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.HUDScripts.MessageSystem
{
    public class FloatingMessage : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private TMP_Text _damageValue;

        public float InitialYVelocity = 7f;
        public float InitialXVelocityRange = 3f;
        public float LifeTime = 0.8f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _damageValue = transform.Find("Canvas/DamageValue").GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _rigidbody.linearVelocity = 
                new Vector2(Random.Range(-InitialXVelocityRange, InitialXVelocityRange), InitialYVelocity);
            Destroy(gameObject, LifeTime);
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }

        // Keep the method public and it can still accept a string, since TMP_Text needs a string
        public void SetMessage(int damageAmount)
        {
            _damageValue.SetText(damageAmount.ToString());
        }
    }
}
