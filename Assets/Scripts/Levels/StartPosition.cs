using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public Transform spawnPointOne;
    public Transform spawnPointTwo;

    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(keyOrbGainedScript.instance.HasKeyOrb == false)
        {
            player.position = spawnPointOne.position;
            player.rotation = spawnPointOne.rotation;
        }else
        {
            player.position = spawnPointTwo.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
