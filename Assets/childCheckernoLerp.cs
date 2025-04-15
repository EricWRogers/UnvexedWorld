using System.Linq;
using UnityEngine;

public class childCheckernoLerp : MonoBehaviour
{
    
      bool callOnce = false;

      public bool theText;

      public bool theMusic;

      public bool end = false;

       public GameObject theThing;

      

       public EnemyFinder eFinder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eFinder = GetComponent<EnemyFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0 && theText == true)
        {

            theThing.SetActive(true);
            
        }

      

        // if(eFinder != null) 
        // {
        //     if(eFinder.nearbyEnemies.Count == 0 && theMusic == true && end == false )
        //     {
        //         end = true;
        //         GameManager.Instance.battleOn = false;
        //     }
        // }
    }
}
