using TMPro;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public Transform spawnPointOne;
    public Transform spawnPointTwo;
    public bool hasSpawnedSecondLocation;

    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(GameManager.Instance.hasKeyOrb == false)
        {
            Debug.Log("PosOne");
            player.position = spawnPointOne.position;
            player.rotation = spawnPointOne.rotation;
        }else
        {
            Debug.Log("PosTwo");
            player.position = spawnPointTwo.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.Instance.hasKeyOrb == true && !hasSpawnedSecondLocation)
        {
            hasSpawnedSecondLocation = true;
            player.position = spawnPointTwo.position;
        }
    }
}
