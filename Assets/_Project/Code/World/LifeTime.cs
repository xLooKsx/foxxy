using System.Collections;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float lifetime;

    void OnEnable()
    {
        StartCoroutine(nameof(LifeTimeCorroutine));
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator LifeTimeCorroutine()
    {
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    }
}
