using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Security.Cryptography;

[RequireComponent(typeof(ClonePathRecorderComp))]
[RequireComponent(typeof(ClonePathFollower))]
[RequireComponent(typeof(CloneCombatComponent))]
[RequireComponent(typeof(LifetimeComponent))]
[RequireComponent(typeof(PathSamplerComponent))]
public class AbilityShadowCloneController : MonoBehaviour
{
    private ClonePathRecorderComp pathRecorder;
    private ClonePathFollower pathFollower;
    private CloneCombatComponent combatComp;
    private LifetimeComponent lifetimeComp;
    private PathSamplerComponent pathSampler;

    public UnityAction onFinished;
    private void Awake()
    {
        pathRecorder = GetComponent<ClonePathRecorderComp>();
        pathFollower = GetComponent<ClonePathFollower>();
        combatComp = GetComponent<CloneCombatComponent>();
        lifetimeComp = GetComponent<LifetimeComponent>();
        lifetimeComp.OnLifetimeFinished += HandleLifetimeEnded;
        pathSampler = GetComponent<PathSamplerComponent>();
    }

    private void Update()
    {
        pathSampler.Tick();

        combatComp.Tick();
        if (!combatComp.HasTarget)
        {
            pathFollower.Follow();
        }
    }
    private void HandleLifetimeEnded() {
        //Debug.Log("Shadow Clone ability deactivated");
        pathRecorder.Clear();       
        onFinished?.Invoke();
        Destroy(gameObject);
    }
}
