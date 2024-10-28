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

        foreach (Transform child in transform)
        {
            enemiesInZone.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !on)
        {
            fogArea.SetActive(true); // Activate fog area if needed
            on = true;
            
            // Optional: Play battle music or any additional logic
            Destroy(fightAreaCollider); // Disable the collider after entering
        }
    }
}
