using UnityEngine;

namespace ArcaneRecursion
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] protected CombatGrid _grid;
        [SerializeField] protected GameEvent _unitTurnEnd;

        protected UnitController _currentUnit = null;
        protected CombatCursor _cursor;

        protected virtual void Awake()
        {
            _cursor = new CombatCursor(_grid);
        }

        public virtual void UnitTurn(CombatEntity unit)
        {
            _currentUnit = unit.GameObject.GetComponent<UnitController>();
            CombatUIController.Instance.UnitSkillPanelControler.SetUnitPannel(_currentUnit);
            _currentUnit.StartTurn();
            _currentUnit.Status.OnStartTurn();
        }

        public virtual void UnitActionEnd()
        {
            _currentUnit.Status.OnEndTurn();
            _unitTurnEnd.Raise();
        }
    }
}