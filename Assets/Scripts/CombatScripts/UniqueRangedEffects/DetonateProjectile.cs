using UnityEngine;

public class DetonateProjectile : MonoBehaviour
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
        Instantiate(ParticleManager.Instance.AOE, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
