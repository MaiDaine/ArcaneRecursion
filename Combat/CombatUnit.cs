using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    [Serializable]
    public class CombatUnit
    {
        public GameObject Prefab;
        public Sprite Icone;
        public UnitStats Stats;
        public List<string> InnateSkills;
        public List<ClassBuild> Build;
    }
}