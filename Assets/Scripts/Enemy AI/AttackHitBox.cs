using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public GameObject hitBox;
    public Transform attackArea;

    private GameObject box;

    public void Attack()
    {
        box = Instantiate(hitBox, attackArea.position, attackArea.rotation);
    }

    public void DestroyBox()
    {
        Destroy(box);
    }



}
