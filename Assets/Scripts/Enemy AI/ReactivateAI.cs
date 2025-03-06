using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;


public class ReactivateAI : MonoBehaviour
{
    public float speedAmount = 0.5f;
    private GruntStateMachine gruntStateMachine;
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
            enemy = transform.GetChild(0).gameObject;
        }
        else
        {
            enemy = null;
        }

        if (enemy != null)
        {
            gruntStateMachine = enemy.GetComponent<GruntStateMachine>();
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
        Debug.Log("The Y velocity: " + Mathf.Abs(knockbackRB.linearVelocity.y));
    }

    public void SelfDestruct()
    {
        if(Mathf.Abs(knockbackRB.linearVelocity.y) > speedAmount)
        {
            Debug.Log("Pile of Papers");
            knockbackTimer.StartTimer(0.1f, false);
            return;
        }
        enemy.transform.parent = null;
        enemy.transform.parent = gruntStateMachine.ogParent.transform;
        enemyCollider.enabled = true;
        gruntStateMachine.enabled = true;
        gruntStateMachine.ChangeState(nameof(InRangeState));
        agent.enabled = true;
        rb.WakeUp();
        Destroy(gameObject, 1.0f);
    }
}
