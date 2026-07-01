using System.Collections;
using UnityEngine;
using UnityEngine.Events;

class TaimerComponent : MonoBehaviour {
    public UnityAction OnFinished;

    public float duration;

    public void StartTimer()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(duration);

        OnFinished?.Invoke();
    }
}