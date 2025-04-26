using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public GameObject hitBox;
    public Transform attackArea;

    public bool isBoss;
    public GameObject armCharge;
    public GameObject armSlam;
    public GameObject legStomp;

    private GameObject box;

    public void Awake()
    {
        if(armSlam == null)
        {
            armSlam = GameObject.Find("BossArmSlam");
        }
        else
        {
            return;
        }
        if(legStomp == null)
        {
            legStomp = GameObject.Find("BossLegStomp");
        }
        else
        {
            return;
        }
        if(armCharge == null)
        {
            armCharge = GameObject.Find("BossArmCharge");
        }
        else
        {
            return;
        }

        armCharge.SetActive(false);
        armSlam.SetActive(false);
        legStomp.SetActive(false);
    }

    public void Attack()
    {
        box = Instantiate(hitBox, attackArea.position, attackArea.rotation);
    }

    public void BossSlamAttack()
    {
        armSlam.SetActive(true);
    }

    public void TurnOffSlamAttack()
    {
        armSlam.SetActive(false);
    }

    public void BossChargeAttack()
    {
        armCharge.SetActive(true);
    }

    public void TurnOffChargeAttack()
    {
        armCharge.SetActive(false);
    }

    public void BossStompAttack()
    {
        legStomp.SetActive(true);
    }

    public void TurnOffStompAttack()
    {
        legStomp.SetActive(false);
    }

    public void DestroyBox()
    {
        Destroy(box);
    }



}
