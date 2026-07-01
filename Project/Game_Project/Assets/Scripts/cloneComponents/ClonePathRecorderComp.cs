using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClonePathRecorderComp : MonoBehaviour
{
    public Queue<Vector2> PathPoints { get; } = new();
    public UnityAction<Vector2> OnPointAdded;
    public UnityAction OnPointRemoved;
    public UnityAction OnPathCleared;
    public bool HasPoints => PathPoints.Count > 0;


    public void Clear() {
        PathPoints.Clear();
        OnPathCleared?.Invoke();
    }
    public Vector2 CurrentPoint() { 
        return PathPoints.Peek();
    }
    public void RemoveCurrentPoint()
    {
        PathPoints.Dequeue();
        OnPointRemoved?.Invoke();
    }
    public void AddPoint(Vector2 point) {
        //GameLogger.Log(this, $"adding {point}");
        PathPoints.Enqueue(point);
        OnPointAdded?.Invoke(point);
    }
}
