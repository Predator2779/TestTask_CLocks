using System;
using UnityEngine;

public class ClockTicker : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    
    private void FixedUpdate()
    {
        timeKeeper.CurrentTime = timeKeeper.CurrentTime.AddSeconds(Time.fixedDeltaTime); /// а минуты, часы?
    }
}