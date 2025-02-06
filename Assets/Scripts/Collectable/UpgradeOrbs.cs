using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Manager;

public class UpgradeOrbs : MonoBehaviour
{

    private AudioManager audioManager;

    //public WalletManager walletManager;
    public void opaqueToTransparent()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void orbSound()
    {
        //audioManager.PlayOrbSound();
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
            gameObject.GetComponent<Animator>().Play("OrbCollectAnim");
            WalletManager.instance.Earn(1);
            //walletManager.Earn();

        }
    }

}
