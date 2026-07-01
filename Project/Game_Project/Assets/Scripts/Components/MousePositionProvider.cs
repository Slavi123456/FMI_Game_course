using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

class MousePositionProvider : MonoBehaviour {

    public Vector2 GetPosition() {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = 0f;
        return worldPos;
    }
}