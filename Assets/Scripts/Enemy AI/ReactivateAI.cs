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

    //Raycast logic to slow down the knockback
    public float wallDetectDistance = 1.0f;
    public float wallSlowdownFactor = 0.5f;
    public LayerMask wallLayer;
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
            if (enemy.GetComponent<GruntStateMachine>() != null)
            {
                gruntStateMachine = enemy.GetComponent<GruntStateMachine>();
            }
            else if (enemy.GetComponent<AgroGruntStateMachine>() != null)
            {
                agroGruntStateMachine = enemy.GetComponent<AgroGruntStateMachine>();
            }
            else if (enemy.GetComponent<RangeGruntStateMachine>() != null)
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

        SlowDown();
    }

    public void SelfDestruct()
    {
        if (Mathf.Abs(knockbackRB.linearVelocity.y) > speedAmount)
        {
            knockbackTimer.StartTimer(0.1f, false);
            return;
        }
        enemy.transform.parent = null;
        if (enemy.GetComponent<GruntStateMachine>() != null)
        {
            enemy.transform.parent = gruntStateMachine.ogParent.transform;
            gruntStateMachine.enabled = true;
            gruntStateMachine.ChangeState(nameof(InRangeState));
        }
        else if (enemy.GetComponent<AgroGruntStateMachine>() != null)
        {
            enemy.transform.parent = agroGruntStateMachine.ogParent.transform;
            agroGruntStateMachine.enabled = true;
            agroGruntStateMachine.ChangeState(nameof(ChargeState));
        }
        else if (enemy.GetComponent<RangeGruntStateMachine>() != null)
        {
            enemy.transform.parent = rangeGruntStateMachine.ogParent.transform;
            rangeGruntStateMachine.enabled = true;
            rangeGruntStateMachine.ChangeState(nameof(ChargeState));
        }
        if (enemy.GetComponent<Health>().currentHealth > 0)
        {
            enemyCollider.enabled = true;
            agent.enabled = true;
            rb.WakeUp();
        }
        else
        {
            enemyCollider.enabled = false;
            agent.enabled = false;
            rb.Sleep();
        }
        Destroy(gameObject);
    }

    void SlowDown()
    {
        Vector3 origin = transform.position;
        float angleStep = 360f / 8;

        for (int i = 0; i < 8; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

            if (Physics.Raycast(origin, direction, out RaycastHit hit, wallDetectDistance, wallLayer))
            {
                Debug.DrawRay(origin, direction * wallDetectDistance, Color.red);

                knockbackRB.linearVelocity *= wallSlowdownFactor;
                break; 
            }
            else
            {
                Debug.DrawRay(origin, direction * wallDetectDistance, Color.green);
            }
        }
    }
}
