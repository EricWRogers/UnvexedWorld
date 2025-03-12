using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;


public class ReactivateAI : MonoBehaviour
{
    public float speedAmount = 0.5f;
    private GruntStateMachine gruntStateMachine;
    private AgroGruntStateMachine agroGruntStateMachine;
    private RangeGruntStateMachine rangeGruntStateMachine;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Rigidbody knockbackRB;
    private CapsuleCollider enemyCollider;
    private Timer knockbackTimer;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.childCount > 0)
        {
            enemy = transform.GetChild(1).gameObject;
            enemy.transform.localPosition = Vector3.zero;
        }
        else
        {
            enemy = null;
        }

        if (enemy != null)
        {
            if(enemy.GetComponent<GruntStateMachine>() != null)
            {
                gruntStateMachine = enemy.GetComponent<GruntStateMachine>();
            }
            if(enemy.GetComponent<AgroGruntStateMachine>() != null)
            {
                agroGruntStateMachine = enemy.GetComponent<AgroGruntStateMachine>();
            }
            if(enemy.GetComponent<RangeGruntStateMachine>() != null)
            {
                rangeGruntStateMachine = enemy.GetComponent<RangeGruntStateMachine>();
            }
            agent = enemy.GetComponent<NavMeshAgent>(); 
            rb = enemy.GetComponent<Rigidbody>(); 
            enemyCollider = enemy.GetComponent<CapsuleCollider>();
        } 

        knockbackRB = GetComponent<Rigidbody>();
        knockbackTimer = GetComponent<Timer>();
    }

    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }

    public void SelfDestruct()
    {
        if(Mathf.Abs(knockbackRB.linearVelocity.y) > speedAmount)
        {
            knockbackTimer.StartTimer(0.1f, false);
            return;
        }
        enemy.transform.parent = null;
        if(enemy.GetComponent<GruntStateMachine>() != null)
        {
            enemy.transform.parent = gruntStateMachine.ogParent.transform;
            gruntStateMachine.enabled = true;
            gruntStateMachine.ChangeState(nameof(InRangeState));
        }
        if(enemy.GetComponent<AgroGruntStateMachine>() != null)
        {
            enemy.transform.parent = agroGruntStateMachine.ogParent.transform;
            agroGruntStateMachine.enabled = true;
            agroGruntStateMachine.ChangeState(nameof(ChargeState));
        }
        if(enemy.GetComponent<RangeGruntStateMachine>() != null)
        {
            enemy.transform.parent = rangeGruntStateMachine.ogParent.transform;
            rangeGruntStateMachine.enabled = true;
            rangeGruntStateMachine.ChangeState(nameof(ChargeState));
        }
        if(enemy.GetComponent<Health>().currentHealth > 0)
        {
            enemyCollider.enabled = true;
            agent.enabled = true;
            rb.WakeUp();
        }else
        {
            enemyCollider.enabled = false;
            agent.enabled = false;
            rb.Sleep();
        }
        Destroy(gameObject);
    }
}
