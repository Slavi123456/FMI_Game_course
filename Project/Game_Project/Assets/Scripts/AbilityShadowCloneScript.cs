using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityShadowCloneScript : MonoBehaviour
{
    private Queue<Vector2> mousePathPoints = new Queue<Vector2>();
    private Queue<GameObject> pointsVisual = new Queue<GameObject>();

    private float pointMaxTimer = 0.3f;
    private float pointTimer = 0f;
    private float abilityTimer = 0.0f;
    
    public float maxTimer;
    public float speed;
    public GameObject pointPrefab;
    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        handleTrajectoryTimer();
        checkForPoints();
        //timer for cooldown
    }
    void handleTrajectoryTimer()
    {
        if (pointMaxTimer <= pointTimer)
        {
            pointTimer = 0;
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f;

            //Instantiate(shadowPrefab, worldPos, Quaternion.identity);
            mousePathPoints.Enqueue(new Vector2(worldPos.x, worldPos.y));


            GameObject v = Instantiate(pointPrefab, worldPos, Quaternion.identity);
            pointsVisual.Enqueue(v);
            //Debug.Log("Saved point " + worldPos.x + " " + worldPos.y);
        }
        pointTimer += Time.deltaTime;
    }
    void checkForPoints() {
        if (mousePathPoints.Count > 0)
        {
            Vector2 target = mousePathPoints.Peek();

            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                mousePathPoints.Dequeue();

                GameObject point = pointsVisual.Dequeue();
                Destroy(point);
            }
        }
    }
}
