using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public Collider col;
    public EnemyFinder enemies;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(enemies != null)
            {
                Debug.Log("Enemies: " + enemies.nearbyEnemies.Count);
                if(enemies.openDoor)
                {
                    Debug.Log("Player Unlocks on Door");
                    col.enabled = false;
                    anim.SetBool("IsOpen", true);
                }
            }
            else
            {
                Debug.Log("Player Unlocks on Door");
                col.enabled = false;
                anim.SetBool("IsOpen", true);
            }
        }
    }
}
