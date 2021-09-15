using Unity.Entities;

[GenerateAuthoringComponent]
[System.Serializable]
public struct TimerData : IComponentData
{
    /// <summary>
    /// The starting time for the timer.
    /// </summary>
    [UnityEngine.Tooltip("The starting value for the timer in seconds")]
    public float time;
    
    /// <summary>
    /// The time left on the timer.
    /// </summary>
    //[UnityEngine.HideInInspector]
    public float value;

    /// <summary>
    /// Resets the timer.
    /// </summary>
    /// <returns></returns>
    public TimerData ResetTimer()
    {
        value = time;
        return this;
    }
}
