using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] float _duration = .5f;

    private Vector3 _originalScale;

    void Start()
    {
        _originalScale = transform.localScale;

        StartCoroutine(ScaleDown());

        Destroy(gameObject, _duration);
    }

    /// <summary>
    /// Scales down the object.
    /// </summary>
    private IEnumerator ScaleDown()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < _duration)
        {
            float proportion = elapsedTime / _duration;

            transform.localScale = Vector3.Lerp(_originalScale, Vector3.zero, proportion);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = Vector3.zero;
    }
}
