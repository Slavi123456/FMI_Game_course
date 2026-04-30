using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Transform positionB;
    public float speed = 2f;

    private Vector3 positionA;
    private float t = 0f;
    private bool movingToB = true;

    void Setup() { 
        positionA = transform.position;
    }
    void Update()
    {
        handleMovement();
    }

    void handleMovement()
    {
        if (positionB == null)
            return;

        t += Time.deltaTime * speed;

        if (t >= 1f)
        {
            t = 0f;
            movingToB = !movingToB; 
        }

        if (movingToB)
            transform.position = Vector3.Lerp(positionA, positionB.position, t);
        else
            transform.position = Vector3.Lerp(positionB.position, positionA, t);
    }
}
