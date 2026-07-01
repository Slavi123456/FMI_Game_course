using System.Collections.Generic;
using UnityEngine;

class ClonePathVisualizer : MonoBehaviour {

    private Queue<GameObject> pointsVisual = new Queue<GameObject>();
    private ClonePathRecorderComp comp;

    public GameObject pointPrefab;

    private void Awake()
    {
        comp = GetComponent<ClonePathRecorderComp>();
        comp.OnPointAdded += OnPointAddedHandler;
        comp.OnPointRemoved += OnPointRemoveHandler;
        comp.OnPathCleared += clearVisualPointsHandler;
    }
    public void OnPointAddedHandler(Vector2 position) {
        GameObject v = Instantiate(pointPrefab, position, Quaternion.identity);
        pointsVisual.Enqueue(v);
    }

    public void OnPointRemoveHandler()
    {
        GameObject point = pointsVisual.Dequeue();
        Destroy(point);
    }
    public void clearVisualPointsHandler() {
        foreach (GameObject point in pointsVisual)
        {
            Destroy(point);
        }
    }
}