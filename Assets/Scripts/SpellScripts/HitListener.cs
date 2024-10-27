using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitListener : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameObject.GetComponent<AOE>().hitTarget.AddListener(delegate{player.GetComponent<SpellCraft>().SetMain(SpellCraft.Aspect.splendor);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
