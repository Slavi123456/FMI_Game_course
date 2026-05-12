using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShadowSkillScript : MonoBehaviour
{
    public GameObject shadowPrefab;
    public float maxTimer = 10.0f;
    
    private bool canBeActivated = true;
   
    public void handleShadowClone(Player player) {
        if (canBeActivated)
        {
            canBeActivated = false;
            //Debug.Log("Activate shadow clone");
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f;
            
            GameObject clone = Instantiate(shadowPrefab, worldPos, Quaternion.identity);
            AbilityShadowCloneScript abilityScript = clone.GetComponent<AbilityShadowCloneScript>();
            abilityScript.SetPlayer(player);
            abilityScript.onFinished = () => {
                player.SetState(Player.PlayerState.Normal);
            };
            
            StartCoroutine(RunAbility(player));
            return;
        }
        Debug.Log("Shadow clone ability is not ready");
        player.SetState(Player.PlayerState.Normal);
    }

    IEnumerator RunAbility(Player player) { 
        yield return new WaitForSeconds(maxTimer);

        Debug.Log("Ability is active");
        canBeActivated = true;
    }
}
