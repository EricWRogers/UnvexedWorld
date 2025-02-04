using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOrbs : MonoBehaviour
{

    public int points = 0;

    public void opaqueToTransparent()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Debug.Log("You picked up a point");
            points++;
            gameObject.GetComponent<Animator>().Play("OrbCollectAnim");

        }
    }

}
