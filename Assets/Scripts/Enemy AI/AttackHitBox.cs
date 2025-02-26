using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public GameObject hitBox;
    public float timeLeft;

    public void Attack()
    {
        StartCoroutine(Attacking());
        Debug.Log("Hit Box Attack FUCK YOU");
    }

    private IEnumerator Attacking()
    {
        GameObject obj = Instantiate(hitBox, transform.position, Quaternion.identity);
        Destroy(obj, timeLeft);
        yield return new WaitForSeconds(timeLeft);
    }
}
