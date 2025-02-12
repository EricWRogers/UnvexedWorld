using UnityEngine;

public class BasicRanged : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<ProjectileSpell>().activate.AddListener(Activate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        gameObject.GetComponentInParent<AttackUpdater>().player.GetComponent<MeleeRangedAttack>().activeProjectile = null;
    }
}
