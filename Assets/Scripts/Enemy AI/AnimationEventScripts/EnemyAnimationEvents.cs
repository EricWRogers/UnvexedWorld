using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public Transform slam;
    public Transform roar;
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

    public void DoTheRoar()
    {
        AudioManager.instance.PlayBossRoarSound();
        ParticleManager.Instance.SpawnBossRoar(roar);
    }

    public void SpawnBossStomp()
    {
        ParticleManager.Instance.SpawnBossStomp(gameObject.transform);
    }

    public void SpawnBossSlam()
    {
        ParticleManager.Instance.SpawnBossSlam(slam);
    }

    public void SpawnBossCharge()
    {
        ParticleManager.Instance.SpawnBossCharge(gameObject.transform);
    }

    public void DestroyBossCharge()
    {
        ParticleManager.Instance.DestroyBossCharge();
    }
}
