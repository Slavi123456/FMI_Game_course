using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isDead { get; private set; }
    public float health = 50.0f;
    public UnityAction<Enemy> onEnemyDeath;
    public void TakeDamage(float damage) {
        if (isDead) return; 
        if (damage < 0.0) return;

        health -= damage;
        if(health <= 0) {
            KillEnemy();    
        }

    }

    public void KillEnemy() {
        onEnemyDeath?.Invoke(this);
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
