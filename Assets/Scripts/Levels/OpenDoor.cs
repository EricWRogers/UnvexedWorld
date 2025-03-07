using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public EnemyFinder enemies;
    public float openDoorDistance = 2.5f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.position) < openDoorDistance)
        {
            if(enemies != null)
            {
                //Debug.Log("Enemies: " + enemies.nearbyEnemies.Count);
                if(enemies.openDoor)
                {
                    //Debug.Log("Player Unlocks on Door");
                    anim.SetBool("IsOpen", true);
                }
            }
            else
            {
                //Debug.Log("Player Unlocks on Door");
                anim.SetBool("IsOpen", true);
            }
        }
    }
}
