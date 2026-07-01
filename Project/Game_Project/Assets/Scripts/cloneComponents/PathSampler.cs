using UnityEngine;

[RequireComponent(typeof(MousePositionProvider))]
[RequireComponent(typeof(ClonePathRecorderComp))]
public class PathSamplerComponent : MonoBehaviour
{
    private float sampleRate = .3f;

    private float timer;

    private MousePositionProvider mouse;
    private ClonePathRecorderComp recorder;

    void Awake()
    {
        mouse = GetComponent<MousePositionProvider>();
        recorder = GetComponent<ClonePathRecorderComp>();
    }

    public void Tick()
    {
        timer += Time.deltaTime;

        if (timer < sampleRate)
            return;

        timer = 0;

        recorder.AddPoint(mouse.GetPosition());
    }
}