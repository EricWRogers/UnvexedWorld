using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public GameObject hitBox;

    public void TurnOn()
    {
        hitBox.SetActive(true);
    }

    public void TurnOff()
    {
        hitBox.SetActive(false);
    }
}
