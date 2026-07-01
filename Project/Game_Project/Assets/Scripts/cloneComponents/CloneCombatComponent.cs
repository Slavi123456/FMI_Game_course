using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(AttackComponent))]
class CloneCombatComponent: MonoBehaviour{

    private HashSet<Enemy> recentAttackedEnemies = new HashSet<Enemy>();
    private HashSet<Enemy> enemiesToAttack = new HashSet<Enemy>();
    
    private float distClosestEnemy = Mathf.Infinity;
    private int enemyLayerMask;
    private CircleCollider2D circleCollider; //used only for the visual and the radius
    
    private MovementComponent movementComponent;
    private AttackComponent attackComponent;
    private Enemy currAttackedEnemy = null;
    private XpComponent ownerXP;
    private bool isAttacking;
    public bool HasTarget => currAttackedEnemy != null;

    public int maxEnemyAttacksPerAttack = 3;
    public float attackTickRate = 0.3f;
    public float enemyRemoveTaimer = 1.5f;
    public float attackRangeThreshold = 0.2f;
    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        movementComponent = GetComponent<MovementComponent>();
        attackComponent = GetComponent<AttackComponent>();
    }
    public void SetOwner(Player player)
    {
        ownerXP = player.XP;
    }
    public void Tick() {
        if (currAttackedEnemy == null)
        {
            checkForEnemies();
            chooseEnemyToAttack();
        }

        if (currAttackedEnemy != null)
        {
            chaseEnemy();

            if (InAttackRange() && !isAttacking)
            {
                StartCoroutine(attackEnemy(this.currAttackedEnemy));
            }
        }
    }
    private bool InAttackRange() {
        return (Vector2.Distance((Vector2)currAttackedEnemy.transform.position, (Vector2)gameObject.transform.position)) < attackRangeThreshold;
    }
    private void checkForEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayerMask);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;
            if (recentAttackedEnemies.Contains(enemy)) continue;
            //Debug.Log("enemy: " + enemy);
            enemiesToAttack.Add(enemy);
            //float dist = Vector2.Distance(transform.position, enemy.transform.position);

            //if (dist < distClosestEnemy)
            //{
            //    distClosestEnemy = dist;
            //    currAttackedEnemy = enemy;
            //    //currentMoveTarget = enemy.transform.position;
            //}
        }
        //if (currAttackedEnemy != null)
        //{
        //    recentAttackedEnemies.Add(currAttackedEnemy);
        //    cloneState = CloneState.Attacking;
        //    StartCoroutine(AttackEnemy(currAttackedEnemy));
        //}
    }

    private void chooseEnemyToAttack() {
        foreach (Enemy enemy in this.enemiesToAttack) {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);

            if (dist < distClosestEnemy)
            {
                distClosestEnemy = dist;
                currAttackedEnemy = enemy;
                //currentMoveTarget = enemy.transform.position;
            }
        }
        enemiesToAttack.Clear();
    }

    private void chaseEnemy()
    {
        Vector2 direction =
           (currAttackedEnemy.transform.position - transform.position).normalized;

        movementComponent.MoveDirection(direction);
        //canAttack = false;
        //if (currAttackedEnemy != null)
        //{
        //    Vector2 direction =
        //    (currAttackedEnemy.transform.position - transform.position).normalized;

        //    movementComponent.MoveDirection(direction);
        //}
        //clearTrajectoryPoints();
    }
    private IEnumerator attackEnemy(Enemy enemy)
    {
        isAttacking = true;
        int attackCount = 0;

        enemy.onEnemyDeath += HandleEnemyDeath;
        this.recentAttackedEnemies.Add(enemy);

        for (int i = 0; i < this.maxEnemyAttacksPerAttack; i++)
        {
            if (enemy == null || enemy.isDead) break;
            attackComponent.Attack(enemy.Health);

            yield return new WaitForSeconds(this.attackTickRate);

            //enemy.Health.TakeDamage(statsComponent.damage);
            ////Debug.Log("Shadow clone attack");
            //yield return new WaitForSeconds(attackTickRate);

            //timer += attackTickRate;
        }

        FinishAttack(enemy);
        isAttacking = false;
    }
    private void FinishAttack(Enemy enemy) {
        distClosestEnemy = Mathf.Infinity;
        currAttackedEnemy = null;
        if (enemy != null)
        {
            enemy.onEnemyDeath -= HandleEnemyDeath;
            StartCoroutine(RemoveEnemy(enemy));
        }
    }
    private IEnumerator RemoveEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(enemyRemoveTaimer);
        if (enemy != null)
            recentAttackedEnemies.Remove(enemy);
    }
    private void HandleEnemyDeath(Enemy enemy)
    {
        if (enemy == null)
            return;
        ownerXP.AddXP(enemy.xp);
        recentAttackedEnemies.Remove(enemy);
    }
}