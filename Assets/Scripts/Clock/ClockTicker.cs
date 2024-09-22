using UnityEngine;

namespace Clock
{
    public class ClockTicker : MonoBehaviour
    {
        [SerializeField] private TimeKeeper timeKeeper;
    
        private void FixedUpdate()
        {
            timeKeeper.CurrentTime = timeKeeper.CurrentTime.AddSeconds(Time.fixedDeltaTime);
        }
    }
}