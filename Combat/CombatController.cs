using UnityEngine;

namespace ArcaneRecursion
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] protected GameEvent _unitTurnEnd;

        protected CombatGrid _grid;
        protected UnitController _currentUnit = null;
        protected CombatCursor _cursor;

        public virtual void UnitTurn(CombatEntity unit)
        {
            _currentUnit = unit.GameObject.GetComponent<UnitController>();
            CombatUIController.Instance.UnitSkillPanelControler.SetUnitPannel(_currentUnit);
            _currentUnit.StartTurn();
        }

        public virtual void UnitActionEnd()
        {
            _currentUnit.Status.OnEndTurn();
            _unitTurnEnd.Raise();
        }

        #region MonoBehaviorLifeCycle
        protected virtual void Awake()
        {
            _cursor = new CombatCursor();
        }
        protected virtual void Start()
        {
            _grid = CombatGrid.Instance;
        }
        #endregion /* MonoBehaviorLifeCycle */
    }
}