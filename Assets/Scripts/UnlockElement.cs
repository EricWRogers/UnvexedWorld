using UnityEngine;

public class UnlockElement : MonoBehaviour
{

    public bool unlockSplendor = false;

     public bool unlockSunder = false;
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(unlockSplendor == false && unlockSunder == false){
            col.GetComponent<SpellCraft>().UnlockElement(SpellCraft.Aspect.scavenge);
            Destroy(transform.parent.gameObject);
            }
            
            if(unlockSplendor == true)
            {
                col.GetComponent<SpellCraft>().UnlockElement(SpellCraft.Aspect.splendor);
                 Destroy(transform.parent.gameObject);
            }

             if(unlockSunder == true)
            {
                col.GetComponent<SpellCraft>().UnlockElement(SpellCraft.Aspect.sunder);
                 Destroy(transform.parent.gameObject);
            }
        }
    }
}
