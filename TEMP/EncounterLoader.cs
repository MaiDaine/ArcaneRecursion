using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public class EncounterLoader : MonoBehaviour
    {
        public CombatLoaderData Loader;
        public GameObject Prefab;

        private void Start()
        {
            //public List<Tuple<GameObject, CombatUnit>> playerUnits;
            //public List<Tuple<GameObject, CombatUnit>> enemyUnits;
            //public List<Tuple<GameObject, ICombatTurnEntity>> mapEntities;
            //loader.playerUnits = new List<Tuple<GameObject, CombatEntity>>();
            //loader.playerUnits.Add(new Tuple<GameObject, CombatEntity>(prefab, new CombatEntity(1, 0, 10)));
            //loader.enemyUnits = new List<Tuple<GameObject, CombatEntity>>();
            //loader.enemyUnits.Add(new Tuple<GameObject, CombatEntity>(prefab, new CombatEntity(2, 1, 10)));
            //loader.mapEntities = new List<Tuple<GameObject, ICombatTurnEntity>>();
        }
    }
}