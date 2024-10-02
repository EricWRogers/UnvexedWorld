using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class LifeStealOrb : MonoBehaviour
{
    public GameObject player;

    public float targetTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        transform.position += transform.forward * 25f * Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            gameObject.transform.LookAt(player.transform);
            transform.position += transform.forward * 10f * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Is Player");
            player.GetComponent<Health>().Heal(1);
            Destroy(gameObject);
        }
    }

}
