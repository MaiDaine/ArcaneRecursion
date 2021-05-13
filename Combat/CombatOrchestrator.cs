using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    //DUMMY 
    public class CombatOrchestrator : MonoBehaviour
    {
        [SerializeField] private CombatLoader loader;
        [SerializeField] private CombatPlayerController playerController;
        [SerializeField] private CombatController aIController;
        [SerializeField] private Transform turnOrderUIBar;

        private CombatTurnController turnController;

        private void Awake()
        {
            turnController = new CombatTurnController(this, turnOrderUIBar);
        }

        public void OnMapLoaded()
        {
            loader.Init();
            List<ICombatTurnEntity> merged = new List<ICombatTurnEntity>(
              loader.PlayerUnits.Count
              + loader.EnemyUnits.Count
              + loader.MapEntities.Count
            );
            merged.AddRange(loader.PlayerUnits);
            merged.AddRange(loader.EnemyUnits);
            merged.AddRange(loader.MapEntities);
            Destroy(loader);
            turnController.InitCombat(merged);
        }

        public void OnTurnStart(ICombatTurnEntity entity)
        {
            string team = entity.Team == 0 ? "Map" : (entity.Team == 1 ? "Player" : "Enemy");
            Debug.Log("TURN START: " + team + " {" + entity.Id + "}");

            switch (entity.Team)
            {
                case 0:
                    Debug.Log("MAP EVENT");
                    break;
                case 1:
                    playerController.PlayerTurn = true;
                    playerController.UnitTurn(entity as CombatEntity);
                    break;
                case 2:
                    aIController.UnitTurn(entity as CombatEntity);
                    break;
                default:
                    break;
            }
        }

        public void OnTurnEnd()
        {
            turnController.OnUnitTurnEnd();
        }

        public void OnCombatEnd()
        {
            Debug.Log("CombatEnd");
        }
    }
}