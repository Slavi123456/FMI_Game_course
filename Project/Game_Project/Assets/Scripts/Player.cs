using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum PlayerState { 
        Normal,
        UsingAbility
    }
    public InputActionReference button;
    
    private PlayerMovementScript move;
    private PlayerShadowSkillScript skill;
    private PlayerState state;
    private bool isInvisible = false;
    private int level = 0;

    public int health = 4;
    public float invisibleTaimer = 2.0f;
    public int currentXP = 0;
    public int currentXPthreshold = 20;
    void Start()
    {
        move = GetComponent<PlayerMovementScript>();
        skill = GetComponent<PlayerShadowSkillScript>();
        Debug.Log("Player skill is activated with \"E\" ");
    }
    // Update is called once per frame
    void Update()
    {
        if (button == null)
        {
            return;
        }
        if (button.action.WasPressedThisFrame() && state == PlayerState.Normal)
        {
            move.stopMovement();
            state = PlayerState.UsingAbility;
            skill.handleShadowClone(this);
            return;
        }
        else if (state == PlayerState.Normal)
        {
            move.handleMovement();
        }
    }

    public void SetState(PlayerState newState) {
        state = newState;
    }
    public void TakeDamage() {
        if (isInvisible)
        {
            return;
        }
        health -= 1;

        if (health <= 0) {
            Debug.Log("Player died");
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        isInvisible = true;
        Debug.Log("Player health: " + health);
        StartCoroutine(InvisibleTaimer());
    }
    private IEnumerator InvisibleTaimer() {
        yield return new WaitForSeconds(invisibleTaimer);

        isInvisible = false;
    }

    public void AddXP(int xp) {
        this.currentXP += xp;
        Debug.Log($"Player current xp: {currentXP}");

        if (this.currentXP >= this.currentXPthreshold) { 
            this.level += 1;
            Debug.Log($"Player LEVEL UP: {this.level}");
        }
    }
}
