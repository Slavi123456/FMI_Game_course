using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TaimerComponent))]
[RequireComponent(typeof(MousePositionProvider))]
public class PlayerShadowSkillScript : MonoBehaviour
{
    public GameObject shadowPrefab;
    
    private bool canBeActivated = true;
    private TaimerComponent abilityCooldown;
    private MousePositionProvider mousePositionRecorder;

    private void Awake()
    {
        abilityCooldown = GetComponent<TaimerComponent>();
        abilityCooldown.OnFinished += OnCooldownFinished;
        mousePositionRecorder = GetComponent<MousePositionProvider>();
    }
    public void handleShadowClone(Player player) {
        if (canBeActivated)
        {
            canBeActivated = false;

            GameObject clone = Instantiate(shadowPrefab, mousePositionRecorder.GetPosition(), Quaternion.identity);
            clone.GetComponent<CloneCombatComponent>().SetOwner(player);

            //AbilityShadowCloneController abilityScript = clone.GetComponent<AbilityShadowCloneController>();
            ////abilityScript.SetPlayer(player);
            //abilityScript.onFinished = () => {
            //    player.state = Player.PlayerState.Normal;
            //};

            abilityCooldown.StartTimer();
            return;
        }
        Debug.Log("Shadow clone ability is not ready");
    }

    public void OnCooldownFinished() {
        canBeActivated = true;
    }
}
