using System.Collections;
using UnityEngine;

public class CorruptionDissolve : MonoBehaviour
{
    public EnemyFinder enemies;

    public float fadeOutDelay = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(enemies == null)
            enemies = gameObject.GetComponentInParent<EnemyFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.nearbyEnemies.Count == 0)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutDelay);

        float time = 0;
        float cv = gameObject.GetComponent<Renderer>().material.GetFloat("_Clipping_Value");
        while (time < 3)
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_Clipping_Value", cv + Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(fadeOutDelay);

        gameObject.SetActive(false);
    }
}
