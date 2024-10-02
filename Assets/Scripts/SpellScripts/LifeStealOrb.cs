using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class LifeStealOrb : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //Make particle look in random direction and move fast
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(player.transform);
        transform.position += transform.forward * 10f * Time.deltaTime;
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
