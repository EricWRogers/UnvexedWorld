using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public Transform center;
    public Transform selectobject;

    public GameObject RadialMenuRoot;

    bool isRadialMenuActive;

    public Text HighlightedMove;
    public Text selectMove;

    public string[] MoveSelect;
    public Transform[] itemSlots;
    public Transform min, max;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isRadialMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isRadialMenuActive = !isRadialMenuActive;
            if (isRadialMenuActive)
            {
                RadialMenuRoot.SetActive(true);
            }
            else
            {
                RadialMenuRoot.SetActive(false);
            }
        }

        if (isRadialMenuActive)
        {
            if(Vector3.Distance(Input.mousePosition,center.position)< Vector3.Distance(max.position, center.position)&& Vector3.Distance(Input.mousePosition, center.position) > Vector3.Distance(min.position, center.position))
            {
                //Formula for calculating angle
                Vector2 delta = center.position - Input.mousePosition;
                float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                angle += 100;

                int currentMove = 0;
                for (int i = 0; i < 360; i += 45)
                {
                    if (angle >= i && angle < i + 45)
                    {
                        selectobject.eulerAngles = new Vector3(0, 0, i);
                        HighlightedMove.text = MoveSelect[currentMove];


                        foreach (Transform t in itemSlots)
                        {
                            t.transform.localScale = new Vector3(1, 1, 1);
                        }

                        itemSlots[currentMove].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);


                        if (Input.GetMouseButtonDown(0))
                        {
                            selectMove.text = MoveSelect[currentMove];
                            isRadialMenuActive = false;
                            RadialMenuRoot.SetActive(false);
                        }
                    }
                    currentMove++;
                }
            }
        }

        else
        {
            HighlightedMove.text = "None";
            foreach (Transform t in itemSlots)
            {
                t.transform.localScale = new Vector3(1, 1, 1);
            }

            if (Input.GetMouseButtonDown(0))
            {
                selectMove.text = "None";
                isRadialMenuActive = false;
                RadialMenuRoot.SetActive(false);
            }
        }
    }
}
