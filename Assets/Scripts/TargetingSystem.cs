using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SuperPupSystems.Helper;

public class TargetingSystem : MonoBehaviour
{
    public float range = 20.0f;
    public string targetTag;
    public bool targetPriority = false;
    public GameObject FindTarget()
    {
        
        GameObject target = null;

        List<GameObject> posibleTargets = GameObject.FindGameObjectsWithTag(targetTag).ToList<GameObject>();

        float closetDistance = float.MaxValue;
        foreach(GameObject pt in posibleTargets)
        {
            float distance = Vector3.Distance(pt.transform.position, transform.position);
            if(pt.GetComponent<Health>()){
                if(distance < range && pt.GetComponent<Health>().currentHealth > 0)
                {
                    if (distance < closetDistance)
                    {
                        closetDistance = distance;
                        target = pt;
                    }
                }
            }
        }

        return target;
    }

    public GameObject TargetExcluding(List<GameObject> excluded, float range = float.MaxValue)
    {
        GameObject target = null;

        List<GameObject> posibleTargets = GameObject.FindGameObjectsWithTag(targetTag).ToList<GameObject>();

        float closetDistance = range;
        foreach(GameObject pt in posibleTargets)
        {
            if(!(excluded.Contains(pt)))
            {
                float distance = Vector3.Distance(pt.transform.position, transform.position);
                if(pt.GetComponent<Health>()){
                    if(distance < range && pt.GetComponent<Health>().currentHealth > 0)
                    {
                        if (distance < closetDistance)
                        {
                            closetDistance = distance;
                            target = pt;
                        }
                    }
                }
            }
        }

        return target;
    }

    //Find the Target with the lowest health
    public GameObject FindTargetPriority()
    {
        GameObject target = null;

        if (targetPriority)
        {
            List<GameObject> possibleHealTargets = GameObject.FindGameObjectsWithTag(targetTag).ToList();

            float lowestHealth = float.MaxValue;
            foreach (GameObject pt in possibleHealTargets)
            {
                //Should skip healer
                if (pt == gameObject)
                {
                    continue;
                }

                Health health = pt.GetComponent<Health>();
                if (health != null && health.currentHealth < lowestHealth)
                {
                    lowestHealth = health.currentHealth;
                    target = pt;
                }
            }
        }

        return target;
    }
}
