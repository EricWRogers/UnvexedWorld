using System.Collections;
using UnityEngine;

public class CorruptionDissolve : MonoBehaviour
{
    public EnemyFinder enemies;

    public float fadeOutDelay = 3f;

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
        while (time < 1)
        {
            time += Time.deltaTime;
            float cv = gameObject.GetComponent<Renderer>().material.GetFloat("_Clipping_Value");
            gameObject.GetComponent<Renderer>().material.SetFloat("_Clipping_Value", cv + Time.deltaTime);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
