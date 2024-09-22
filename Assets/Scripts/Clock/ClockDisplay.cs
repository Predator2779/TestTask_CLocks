using System;
using UnityEngine;

namespace Clock
{
    public abstract class ClockDisplay : MonoBehaviour
    {
        public bool CanDisplayed { get; set; } = true;

        [SerializeField] private TimeKeeper timeKeeper;
    
        private void FixedUpdate()
        {
            if (CanDisplayed) DisplayTime(timeKeeper.CurrentTime);
        }

        public abstract void DisplayTime(DateTime time);
    }
}