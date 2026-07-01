using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(XpComponent))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(InputProvider))]
[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(PlayerAnimationComponent))]
public class Player : MonoBehaviour
{

    //public enum PlayerState {
    //    Normal,
    //    UsingAbility
    //}

    public XpComponent XP { get; private set; }
    public HealthComponent Health { get; private set; }
    private InputProvider Input;
    private MovementComponent Movement;
    private PlayerShadowSkillScript skill;
    private PlayerAnimationComponent playerAnimation;
    //public PlayerState state { get; set;  }
    

    private void Awake() {
        skill = GetComponent<PlayerShadowSkillScript>();
        XP = GetComponent<XpComponent>();
        Health = GetComponent<HealthComponent>();
        Health.OnDeath += HandleDeath;
        Input = GetComponent<InputProvider>();
        Movement = GetComponent<MovementComponent>();
        playerAnimation = GetComponent<PlayerAnimationComponent>();
    }

    void Start()
    {
        GameLogger.Log(this, $"{gameObject} skill is activated with \"E\" ");
    }
    void Update()
    {
        if (Input.isAbilityActivated)
        {
            skill.handleShadowClone(this);
        }
    }
    private void FixedUpdate()
    {
        Movement.MoveDirection(Input.moveDirection);
        playerAnimation.UpdateMovementAnim(Input.moveDirection);
    }
    private void HandleDeath(){
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
    }
}
