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
            Debug.Log("Activate shadow clone");
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f;

            Instantiate(shadowPrefab, worldPos, Quaternion.identity);
        }
    }

    IEnumerator RunAbility(Player player) { 
        yield return new WaitForSeconds(maxTimer);

        Debug.Log("Ability is active");
        canBeActivated = true;
        player.SetState(Player.PlayerState.Normal);
    }
}
