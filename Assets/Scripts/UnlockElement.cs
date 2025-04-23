using UnityEngine;

public class UnlockElement : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<SpellCraft>().UnlockElement(SpellCraft.Aspect.scavenge);
            Destroy(transform.parent.gameObject);
        }
    }
}
