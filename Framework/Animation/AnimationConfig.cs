using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Framework/Animation")]
    public class AnimationConfig : ScriptableObject
    {
        public AnimationData[] data;

        public AnimationData GetData(string name)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].name == name)
                    return data[i];
            }
            return null;
        }
    }
}