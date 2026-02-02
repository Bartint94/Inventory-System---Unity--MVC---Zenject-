using UnityEngine;

public class Timer 
{
    float duration;
    public float elapsed;
    public bool isFinished;
    
    public Timer(float duration)
    {
        this.duration = duration;
    }
    public void Update()
    {
        elapsed += Time.deltaTime;
        //Debug.Log($"elspsed = {elapsed}, duration = {duration}");
        if (elapsed >= duration)
        {
            isFinished = true;
        }
        else
        {
            isFinished = false;
        }
    }
    public float GetPercentage()
    {
        if (isFinished)
        { return 1f; }
        else
            return elapsed / duration;
    }
    public void Restart()
    {
        elapsed = 0f;
    }
}
