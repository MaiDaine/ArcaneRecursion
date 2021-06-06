using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    public class CombatTurnController
    {
        public const int UNITDISPLAYMAX = 10;
        public static CombatTurnController Instance = null;
        public List<Tuple<int, ICombatTurnEntity>> UnitTurnOrder { get; } = new List<Tuple<int, ICombatTurnEntity>>();
        public List<ICombatTurnEntity> Entities = new List<ICombatTurnEntity>();

        private readonly CombatOrchestrator _orchestrator;
        private readonly Transform _turnOrderUIBar;
        private ICombatTurnEntity _currentEntity;
        private int _turnTime;

        public CombatTurnController(CombatOrchestrator orchestrator, Transform turnOrderUIBar)
        {
            _orchestrator = orchestrator;
            _turnOrderUIBar = turnOrderUIBar;
            Instance = this;
        }

        private bool CheckEndCombat()
        {
            List<int> activeTeam = new List<int>();

            foreach (ICombatTurnEntity e in Entities)
                if (e.Team != 0 && !activeTeam.Contains(e.Team))
                    activeTeam.Add(e.Team);
            return activeTeam.Count == 1;
        }

        public void InitCombat(List<ICombatTurnEntity> entities)
        {
            Entities = entities;
            _turnTime = 0;
            RefreshTurnOrder();
            _currentEntity = Entities[0];
            _orchestrator.OnTurnStart(_currentEntity);
        }

        public void OnUnitTurnEnd()
        {
            _turnTime += Entities[0].CombatSpeed - _currentEntity.CombatSpeed;
            UnitTurnOrder.RemoveAt(0);
            CheckEndCombat();
            if (UnitTurnOrder.Count <= UNITDISPLAYMAX)
                RefreshTurnOrder();
            else
                for (int i = 0; i < UNITDISPLAYMAX; i++)
                    _turnOrderUIBar.Find(i.ToString()).GetComponent<Image>().sprite = UnitTurnOrder[i].Item2.Icone;
            _currentEntity = UnitTurnOrder[0].Item2;
            _orchestrator.OnTurnStart(_currentEntity);
        }

        public void RefreshTurnOrder()
        {
            UnitTurnOrder.Clear();
            int timeValue = _turnTime + Entities[0].CombatSpeed;
            for (int i = 0; i < UNITDISPLAYMAX; i++)
            {
                UnitTurnOrder.Add(new Tuple<int, ICombatTurnEntity>(timeValue, Entities[0]));
                timeValue += Entities[0].CombatSpeed;
            }
            for (int i = 1; i < Entities.Count; i++)
                InsertUnit(Entities[i]);
            for (int i = 0; i < UNITDISPLAYMAX; i++)
                _turnOrderUIBar.Find(i.ToString()).GetComponent<Image>().sprite = UnitTurnOrder[i].Item2.Icone;
        }

        public void InsertUnit(ICombatTurnEntity unit)
        {
            int timeValue = _turnTime + unit.CombatSpeed;
            int currentSearchIndex = 0;

        Search:
            while (currentSearchIndex < UnitTurnOrder.Count
                    && timeValue >= UnitTurnOrder[currentSearchIndex].Item1)
                currentSearchIndex++;

            if (currentSearchIndex >= UnitTurnOrder.Count)
                return;

            UnitTurnOrder.Insert(currentSearchIndex, new Tuple<int, ICombatTurnEntity>(timeValue, unit));
            timeValue += unit.CombatSpeed;
            currentSearchIndex++;

            if (currentSearchIndex < UnitTurnOrder.Count)
                goto Search;
        }

        public void RemoveUnit(ICombatTurnEntity unit)
        {
            Entities.Remove(unit);
            if (CheckEndCombat())
                _orchestrator.OnCombatEnd();
            else
                RefreshTurnOrder();
        }

        public void UpdateUnit(ICombatTurnEntity unit)
        {
            int index = Entities.FindIndex(e => e == unit);

            Entities[index] = unit;
            RefreshTurnOrder();
        }

        public void ChangeUnitOrder(ICombatTurnEntity unit, int value)
        {
            //TODO
            Debug.Log("Update " + unit.Id + " =>" + value);
        }
    }
}