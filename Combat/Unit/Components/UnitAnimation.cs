using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationConfig _config;
        private Animator _animator;
        private string _previousState = null;

        public void PlayAnimation(UnitAnimationType type)
        {
            string state = type.ToString();
            AnimationData data = _config.GetData(state);

            if (data != null)
            {
                if (_previousState != null)
                    _animator.SetBool(_previousState, false);
                _animator.SetBool(state, true);
                _previousState = state;
            }
        }

        public void PlayIndexedAnimation(UnitAnimationType type, int index = -1)
        {
            string state = type.ToString();
            AnimationData data = _config.GetData(state);

            if (data != null)
            {
                if (_previousState != null)
                    _animator.SetBool(_previousState, false);
                if (index == -1)
                    index = UnityEngine.Random.Range(0, data.size);
                _animator.SetInteger(state + "Index", index);
                _animator.SetBool(state, true);
                _previousState = state;
            }
        }

        #region MonoBehavior LifeCycle
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        #endregion /* MonoBehavior LifeCycle */
    }
}