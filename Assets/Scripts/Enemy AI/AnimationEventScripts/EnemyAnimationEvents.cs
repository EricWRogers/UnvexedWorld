using UnityEngine;
using SuperPupSystems.Helper;

public class EnemyAnimationEvents : MonoBehaviour
{
    public Health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponentInParent<Health>();
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
    }

    public void SpawnBossStomp()
    {
        ParticleManager.Instance.SpawnBossStomp(gameObject.transform);
    }

    public void SpawnBossSlam()
    {
        ParticleManager.Instance.SpawnBossSlam(gameObject.transform);
    }

    public void SpawnBossCharge()
    {
        ParticleManager.Instance.SpawnBossCharge(gameObject.transform);
    }

    public void DestroyBossCharge()
    {
        ParticleManager.Instance.DestroyBossCharge();
    }

    public void Houdini()
    {
        gameObject.GetComponentInParent<Health>().DestroyGameObject();
    }
}
