using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct StatusModifierVariable
    {
        public int FlatValue;
        public int PercentValue;

        public void Reset()
        {
            FlatValue = 0;
            PercentValue = 100;
        }
    }
}