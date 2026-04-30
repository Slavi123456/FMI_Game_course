using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShadowSkillScript : MonoBehaviour
{

    public InputActionReference button;
    public GameObject shadowPrefab;
    public float dotsDistThreshold = 5f;


    private bool isAbilityTriggered = false;
    private bool isAbilityActive = false;
    private bool canBeActivated = true;
    private float maxTimer = 3.0f;
    private float timer = 0f;

 
    // Update is called once per frame
    void Update()
    {
        handleAbilityTimer();
        handleShadowClone();
    }
    void handleAbilityTimer()
    {
        if (canBeActivated) return;
        if (maxTimer <= timer)
        {
            timer = 0;
            resetAbility();
        }
        timer += Time.deltaTime;
    }
    
    void resetAbility()
    {
        Debug.Log("Ability is active");
        canBeActivated = true;
    }
    void handleShadowClone() {
        if (button == null) {
            return;
        }
        bool isActive = button.action.ReadValue<float>() >= 1f;
        if (isActive && canBeActivated)
        {
            canBeActivated = false;
            isAbilityActive = true;
            Debug.Log("Activate shadow clone");
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f;

            Instantiate(shadowPrefab, worldPos, Quaternion.identity);
        }
    }
}
