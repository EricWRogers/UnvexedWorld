using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFight : MonoBehaviour
{
    public GameObject fogArea;
    public bool on = false;

    [SerializeField]
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private MeshCollider fightAreaCollider;

    private void Start()
    {
        fightAreaCollider = GetComponent<MeshCollider>();

        foreach (Transform child in transform)
        {
            enemiesInZone.Add(child.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (on && transform.childCount == 1)
        {
            ActivateFight[] argoZone = FindObjectsOfType<ActivateFight>();

            int count = 0;

            foreach(ActivateFight ae in argoZone)
            {
                if (ae.on == true)
                    count++;
            }

            if (count <= 1)
            {
                AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
                AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

                backgroundMusic.volume = 1.0f;
                battleMusic.Stop();
            }

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && on == false)
        {
            fogArea.SetActive(true);

            on = true;
            AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

            if (battleMusic.isPlaying == false)
            {
                backgroundMusic.volume = 0.2f;
                battleMusic.Play();
            }

            Destroy(fightAreaCollider);
        }
    }
}
