using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAlpha : MonoBehaviour
{
    public Color color;
    public float alphaVal;
    public Renderer leftHand;
    public Renderer rightHand;
    // Start is called before the first frame update
    void Start()
    {
        //leftHand = GetComponentsInChildren<PunchScript>()[0].gameObject.transform.parent.gameObject.GetComponent<Renderer>();
        //rightHand = GetComponentsInChildren<PunchScript>()[3].gameObject.transform.parent.gameObject.GetComponent<Renderer>();
        color = leftHand.material.color;

    }

    // Update is called once per frame
    void Update()
    {
        color.a = alphaVal;
        leftHand.material.color = color;
        rightHand.material.color = color;
    }
}
