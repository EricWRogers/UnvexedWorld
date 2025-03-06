using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class NextLevel : MonoBehaviour
{
    private Transform player;
    [SerializeField] Animator transitionAnim;

    public float doorDistance = 1.5f;
    public string nextScene;
    public UnityEvent nextLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (nextLevel == null)
        {
            nextLevel = new UnityEvent();
        }
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        NextScene(nextScene);
        transitionAnim.SetTrigger("Start");
    }

    public void SceneTransition()
    {
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(keyOrbGainedScript.instance.HasKeyOrb == false && Vector3.Distance(transform.position, player.position) < doorDistance)
        {
            nextLevel.Invoke();
        }
    }

    public void NextScene(string sceneName)
    {
        Debug.Log("Change Scene has been called" + sceneName);
        nextScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}
