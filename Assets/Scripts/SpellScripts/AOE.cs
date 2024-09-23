using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;

[RequireComponent(typeof(Spell))]
public class AOE : MonoBehaviour
{
    public UnityEvent<GameObject> hitTarget;
    public float duration = 10f;
    public int damage = 5;
    public Timer timer;
    public Spell spell;
    // Start is called before the first frame update
     private void Awake()
        {
            if (hitTarget == null)
            {
                hitTarget = new UnityEvent<GameObject>();
            }
        }
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        if (timer.timeout == null)
                timer.timeout = new UnityEvent();
        timer.countDownTime = duration;
        timer.autoRestart = true;
        timer.autoStart = true;
        timer.timeout.AddListener(EndAOE);
        spell = gameObject.GetComponent<Spell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndAOE()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider target)
    {
        if(target.gameObject.tag == "Enemy" || target.gameObject.tag =="GroundEnemy")
        {
            Debug.Log("AOE Hit" + target.gameObject.name + "");
            hitTarget.Invoke(target.gameObject);
            target.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
        }
    }

}
