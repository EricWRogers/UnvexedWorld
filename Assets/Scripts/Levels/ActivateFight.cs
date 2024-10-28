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
        if (on && enemiesInZone.Count == 0) // Check if there are no enemies left
        {
            HUDManager hudManager = FindObjectOfType<HUDManager>();
            hudManager.HideHUD(); // Hide HUD when the fight is over

            // Optional: Handle any additional end-of-fight logic here, like playing victory music

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !on)
        {
            fogArea.SetActive(true);
            on = true;

            // Play battle music
            AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

            if (!battleMusic.isPlaying)
            {
                backgroundMusic.volume = 0.2f;
                battleMusic.Play();
            }

            Destroy(fightAreaCollider);
        }
    }
}
