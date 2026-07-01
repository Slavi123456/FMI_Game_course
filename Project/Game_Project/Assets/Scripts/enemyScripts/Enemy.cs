using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AttackComponent))]
public class Enemy : MonoBehaviour
{
    public bool isDead { get; private set; }
    public int xp = 10;
    public UnityAction<Enemy> onEnemyDeath;

    private AttackComponent attack;
    public HealthComponent Health { get; private set; }
    private void Awake()
    {
       Health = GetComponent<HealthComponent>();
       Health.OnDeath += this.OnDeath;
       attack = GetComponent<AttackComponent>();

    }
    public void OnDeath() {
        onEnemyDeath?.Invoke(this);
        //Debug.Log("Enemy died");
        Destroy(gameObject);
    }

    //This could be made into AttackComponent
    private void OnTriggerStay2D(Collider2D other)
    {
        
        //This line could be changed to HealthComponent
        //for the enemies to attack everything
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            attack.Attack(player.Health);
        }
    }
}
