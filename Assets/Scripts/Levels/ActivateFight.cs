using System.Collections.Generic;
using UnityEngine;

public class ActivateFight : MonoBehaviour
{
    public GameObject fogArea; // Optional fog area if you want it to appear
    private bool on = false;
    [SerializeField]
    private List<GameObject> enemiesInZone = new List<GameObject>();
    private MeshCollider fightAreaCollider;
    private HUDManager hudManager;

    private void Start()
    {
        fightAreaCollider = GetComponent<MeshCollider>();

        hudManager = GameObject.FindObjectOfType<HUDManager>();

        foreach (Transform child in transform)
        {
            enemiesInZone.Add(child.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(on && transform.childCount == 1)
        {
            ActivateFight[] argoZone = FindObjectsOfType<ActivateFight>();

            int count = 0;

            foreach(ActivateFight ae in argoZone)
            {
                if(ae.on == true)
                {
                    count++;
                }
            }

            if(count <= 1)
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
        if (other.CompareTag("Player") && !on)
        {
            fogArea.SetActive(true); // Activate fog area if needed
            on = true;
            AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

            if(battleMusic.isPlaying == false)
            {
                backgroundMusic.volume = 0.2f;
                battleMusic.Play();
            }

            hudManager.ShowHUD();
            
            // Optional: Play battle music or any additional logic
            Destroy(fightAreaCollider); // Disable the collider after entering
        }
    }
}
