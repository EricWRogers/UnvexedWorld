using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public EnemyFinder enemies;
    public float timeOff = 1f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
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
