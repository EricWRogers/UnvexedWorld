using UnityEngine;
using TMPro;

public class WalletHUD : MonoBehaviour
{

    public TMP_Text count;

    public void CountUp(int _amountGained, int _totalCoins)
    {
        if(count!=null){
        count.text = "Points: " + _totalCoins.ToString();
        }
        else
        {
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //At end of level save coin value to player pref, at start of level assign coin value
}
