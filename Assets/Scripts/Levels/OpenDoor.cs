using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public EnemyFinder enemies;
    public OrbOpenDoor openDoor;
    public float openDoorDistance = 2.5f;
    private Transform player;
    [SerializeField]
    private Collider doorCol;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        doorCol = GetComponentInParent<Collider>();
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.position) < openDoorDistance && openDoor == null)
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
            doorCol.enabled = false;
        }
        else if(openDoor != null)
        {
            if(openDoor.openDoor == true)
            {
                anim.SetBool("IsOpen", true);
                doorCol.enabled = false;
            }    
        }
    }
}
