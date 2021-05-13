using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    [Serializable]
    public class ClassBuild
    {
        public ClassNames Name;
        public bool[] AvailableSkills = new bool[6];
        [HideInInspector] public List<CombatSkillObject> Skills;
    }
}