using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.Helper;
using System.Linq;


public class RagdollEnabler : MonoBehaviour
{
    public float power = 100f;
    public bool isGrunt;
    public Vector3 center;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform ragdollRoot;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private bool startRagdoll = false;
    [SerializeField]
    private Rigidbody enemiesRigidbody;
    [SerializeField]
    private Collider enemiesCollider;
    [SerializeField]
    private GameObject enemyMat;
    [SerializeField]
    private Rigidbody[] rb;
    [SerializeField]
    private GameObject enemyKnockBack;
    private CharacterJoint[] joints;
    [SerializeField]
    private Collider[] colliders;
    private float fadeOutDelay = 3f;

    // private void Awake()
    // {
    //     rb = ragdollRoot.GetComponentsInChildren<Rigidbody>();
    //     joints = ragdollRoot.GetComponentsInChildren<CharacterJoint>();
    //     colliders = ragdollRoot.GetComponentsInChildren<Collider>();
    // }

    private void Start()
    {
        rb = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        joints = ragdollRoot.GetComponentsInChildren<CharacterJoint>();
        colliders = ragdollRoot.GetComponentsInChildren<Collider>();

        if (startRagdoll)
        {
            EnableRagdoll();
        }
        else
        {
            EnableAnimator();
        }

        PowerAmount();
    }

    public void EnableRagdoll()
    {
        PowerAmount();
        if(power == 0)
        {
            power = 20;
        }
        animator.enabled = false;
        agent.enabled = false;
        enemiesRigidbody.Sleep();
        enemiesCollider.enabled = false;
        foreach (CharacterJoint joint in joints)
        {
            joint.enableCollision = true;
        }
        foreach (Rigidbody rigidbody in rb)
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(center * power, ForceMode.Impulse);
        }
        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }
        StartCoroutine(FadeOut());
    }

    public void DisableAllRigidbodies()
    {
        foreach (Rigidbody rigidbody in rb)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
        agent.enabled = true;
        foreach (CharacterJoint joint in joints)
        {
            joint.enableCollision = false;
        }
        foreach (Rigidbody rigidbody in rb)
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutDelay);

        DisableAllRigidbodies();

        float time = 0;
        while (time < 1)
        {
            transform.position += Vector3.down * Time.deltaTime;
            time += Time.deltaTime;
            //float cv = enemyMat.GetComponent<Renderer>().material.GetFloat("_ClippingValue");
            //enemyMat.GetComponent<Renderer>().material.SetFloat("_ClippingValue", cv + Time.deltaTime);
            yield return null;
        }

        enemy.GetComponent<Health>().DestroyGameObject();
    }

    public void PowerAmount()
    {
        if(isGrunt)
        {
            center = enemyKnockBack.GetComponent<GruntStateMachine>().knockBack.dir;
            power = enemyKnockBack.GetComponent<GruntStateMachine>().knockBack.power * 4;
        }
        else
        {
            center = enemyKnockBack.GetComponent<MeleeStateMachine>().knockBack.dir;
            power = enemyKnockBack.GetComponent<MeleeStateMachine>().knockBack.power;
        }
    }
}