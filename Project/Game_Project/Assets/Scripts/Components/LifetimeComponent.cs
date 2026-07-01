using System.Collections;
using UnityEngine;
using UnityEngine.Events;

class LifetimeComponent : MonoBehaviour {

    public UnityAction OnLifetimeFinished;
    public float lifetimeDuration; 
    private void Start()
    {
        StartCoroutine(LifetimeTimer());
    }

    private IEnumerator LifetimeTimer() {
        yield return new WaitForSeconds(lifetimeDuration);

        OnLifetimeFinished?.Invoke();
    }
}