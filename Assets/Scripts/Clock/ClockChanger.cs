using System;
using UnityEngine;

namespace Clock
{
    public class ClockChanger : MonoBehaviour
    {
        [SerializeField] protected TimeKeeper timeKeeper;

        public Action OnTimeChanged;
    }
}