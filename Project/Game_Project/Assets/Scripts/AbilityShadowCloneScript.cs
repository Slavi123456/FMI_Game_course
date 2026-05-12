using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using static UnityEngine.GraphicsBuffer;

public class AbilityShadowCloneScript : MonoBehaviour
{
    private Queue<Vector2> mousePathPoints = new Queue<Vector2>();
    private Queue<GameObject> pointsVisual = new Queue<GameObject>();
    private HashSet<Enemy> recentAttackedEnemies = new HashSet<Enemy>();

    private bool canAttack = true;
    private CircleCollider2D circleCollider; //used only for the visual and the radius
    private float pointTimer = 0f;
    private Enemy currAttackedEnemy = null;
    private float distClosestEnemy = Mathf.Infinity;
    private CloneState cloneState;
    private Player player = null;

    private enum CloneState {
        FollowingPath,
        Attacking
    };

    public float pointMaxTimer = 0.3f;
    public float maxDuration = 6f;
    public float speed;
    public float enemyRemoveTaimer = 1.5f;
    public float enemyAttackTaimer = 1.0f;
    public float attackTickRate = 0.3f;
    public float cloneDamage = 10.0f;

    public GameObject pointPrefab;
    public UnityAction onFinished;

    int enemyLayerMask;
    private void Start()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        circleCollider = GetComponent<CircleCollider2D>();
        cloneState = CloneState.FollowingPath;
        StartCoroutine(AbilityDuration());

    }
    private void Update()
    {
        if (cloneState != CloneState.Attacking)
        {
            handleTrajectoryTimer();
            checkForPoints();
            checkForEnemies();
        }
        else {
            attackEnemy();
        }

    }
    private IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(maxDuration);

        clearTrajectoryPoints();
        //Debug.Log("Shadow Clone ability deactivated");
        onFinished?.Invoke();
        Destroy(gameObject);
    }
    private void clearTrajectoryPoints() {
        foreach (GameObject point in pointsVisual)
        {
            Destroy(point);
        }
        mousePathPoints.Clear();
    }
    private void handleTrajectoryTimer()
    {
        if (pointMaxTimer <= pointTimer)
        {
            pointTimer = 0;
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f;

            mousePathPoints.Enqueue(new Vector2(worldPos.x, worldPos.y));


            GameObject v = Instantiate(pointPrefab, worldPos, Quaternion.identity);
            pointsVisual.Enqueue(v);
            //Debug.Log("Saved point " + worldPos.x + " " + worldPos.y);
        }
        pointTimer += Time.deltaTime;
    }
    private void checkForPoints()
    {
        if (mousePathPoints.Count > 0)
        {
            Vector2 target = mousePathPoints.Peek();

            moveShadowCloneToPoint(target);

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                mousePathPoints.Dequeue();

                GameObject point = pointsVisual.Dequeue();
                Destroy(point);
            }
        }
    }
    private void checkForEnemies() {
        if (!canAttack) return;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayerMask);
        foreach (Collider2D hit in hits) {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;
            if (recentAttackedEnemies.Contains(enemy)) continue;
            //Debug.Log("enemy: " + enemy);
            float dist = Vector2.Distance(transform.position, enemy.transform.position);

            if (dist < distClosestEnemy) {
                distClosestEnemy = dist;
                currAttackedEnemy = enemy;
            }
        }
        if (currAttackedEnemy != null)
        {
            recentAttackedEnemies.Add(currAttackedEnemy);
            cloneState = CloneState.Attacking;
            StartCoroutine(AttackEnemy(currAttackedEnemy));
        }
    }
    private void moveShadowCloneToPoint(Vector2 point) {
        transform.position = Vector2.MoveTowards(
           transform.position,
           point,
           speed * Time.deltaTime
       );
    }
    private void attackEnemy() {
        canAttack = false;
        if (currAttackedEnemy != null) 
            moveShadowCloneToPoint(currAttackedEnemy.transform.position);
        clearTrajectoryPoints();
    }
    private IEnumerator AttackEnemy(Enemy enemy) {
        float timer = 0f;

        enemy.onEnemyDeath += HandleEnemyDeath;

        while (timer < enemyAttackTaimer)
        {
            if (enemy == null || enemy.isDead) break;

            enemy.TakeDamage(cloneDamage);
            //Debug.Log("Shadow clone attack");
            yield return new WaitForSeconds(attackTickRate);

            timer += attackTickRate;
        }
        canAttack = true;
        cloneState = CloneState.FollowingPath;
        distClosestEnemy = Mathf.Infinity;
        currAttackedEnemy = null;
        if (enemy != null) {
            enemy.onEnemyDeath -= HandleEnemyDeath;
            StartCoroutine(RemoveEnemy(enemy));
        }
    }
    private IEnumerator RemoveEnemy(Enemy enemy) {
        yield return new WaitForSeconds(enemyRemoveTaimer);
        if (enemy != null)
            recentAttackedEnemies.Remove(enemy);
    }
    private void HandleEnemyDeath(Enemy enemy)
    {
        if (enemy == null)
            return;
        player.AddXP(enemy.xp);
        recentAttackedEnemies.Remove(enemy);
    }
    public void SetPlayer(Player player) { 
        if (player == null) return;
        this.player = player;
    }
}
