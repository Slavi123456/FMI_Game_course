using UnityEngine;

public static class GameLogger 
{
    public static void Log(Object sender, string message) {
        Debug.Log($"[{sender.name}] {message}");
    }
}
