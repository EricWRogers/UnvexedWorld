using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemySlash()
    {
        ParticleManager.Instance.SpawnEnemySlash(gameObject.transform);
    }
}
