using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransition : MonoBehaviour
{
    public bool on = false;

      [SerializeField]
    private List<GameObject> groundEnemies = new List<GameObject>();
   
    
    // Start is called before the first frame update
    private void Start()
    {
        groundEnemies.Clear();  // Clear the current list of enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void FixedUpdate()
    {
        if (groundEnemies.Count == 0)
        {
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();
            battleMusic.Stop();
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && on == false)
        {
            on = true;
            
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

            if (battleMusic.isPlaying == false)
            {
                
                battleMusic.Play();
            }
        }
    }
}
