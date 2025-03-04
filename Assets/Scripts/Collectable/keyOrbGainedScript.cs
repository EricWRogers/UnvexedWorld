using UnityEngine;
using UnityEngine.SceneManagement;

public class keyOrbGainedScript : MonoBehaviour
{
    public bool HasKeyOrb = false;
    public static keyOrbGainedScript instance;

    void Awake()
    {

        if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        GameObject.DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


}
