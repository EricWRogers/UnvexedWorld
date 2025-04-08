using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance { get; private set; } = null;

    public GameObject[] attackPrefabs;
    public GameObject[] rangeAttackPrefabs;
    public GameObject[] superAttackPrefabs;
    public GameObject[] sunderCrystalPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
