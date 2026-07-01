using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public UnityAction OnDeath;
    public float Health { get; private set; }
    
    private bool canTakeDamage = true;
    
    public float maxHealth = 4;
    public float invincibleTimer = 2.0f;

    public void Awake()
    {
        Health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) return;
        
        Health -= damage;
        GameLogger.Log(this, $"{gameObject.name} health: {Health}");
        
        if (Health <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(InvincibleTaimer());
    }
    private IEnumerator InvincibleTaimer()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(invincibleTimer);
        
        canTakeDamage = true;
    }

    private void Die() {
        GameLogger.Log(this, $"{gameObject.name} died");

        OnDeath?.Invoke();
    }
}
