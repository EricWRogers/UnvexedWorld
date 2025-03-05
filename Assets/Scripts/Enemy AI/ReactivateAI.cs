using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;

public class ReactivateAI : MonoBehaviour
{
    private GruntStateMachine gruntStateMachine;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private CapsuleCollider enemyCollider;
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
    }

    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }

    public void SelfDestruct()
    {
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
