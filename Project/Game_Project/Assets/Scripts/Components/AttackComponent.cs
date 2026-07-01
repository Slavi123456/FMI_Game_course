using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(StatsComponent))]
public class AttackComponent : MonoBehaviour
{
    private StatsComponent stats;

    private void Awake()
    {
        stats = GetComponent<StatsComponent>(); 
    }
    public void Attack(HealthComponent target) {
        target.TakeDamage(stats.damage);
    }
}
