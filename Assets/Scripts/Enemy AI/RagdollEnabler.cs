using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.Helper;

public class RagdollEnabler : MonoBehaviour
{
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
    private Rigidbody[] rb;
    private CharacterJoint[] joints;
    [SerializeField]
    private Collider[] colliders;
    private float fadeOutDelay = 3f;

    private void Awake()
    {
        rb = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        joints = ragdollRoot.GetComponentsInChildren<CharacterJoint>();
        colliders = ragdollRoot.GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        if (startRagdoll)
        {
            EnableRagdoll();
        }
        else
        {
            EnableAnimator();
        }
    }

    public void EnableRagdoll()
    {
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
            float cv = enemyMat.GetComponent<Renderer>().material.GetFloat("_ClippingValue");
            enemyMat.GetComponent<Renderer>().material.SetFloat("_ClippingValue", cv + Time.deltaTime);
            yield return null;
        }

        enemy.GetComponent<Health>().DestroyGameObject();
    }
}