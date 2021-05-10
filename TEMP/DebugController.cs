using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Readability", "RCS1001", Justification = "<Pending>", Scope = "NamespaceAndDescendants", Target = "ArcaneRecursion")]
[assembly: SuppressMessage("Readability", "RCS1003", Justification = "<Pending>", Scope = "NamespaceAndDescendants", Target = "ArcaneRecursion")]
[assembly: SuppressMessage("Readability", "RCS1049", Justification = "<Pending>", Scope = "NamespaceAndDescendants", Target = "ArcaneRecursion")]

namespace ArcaneRecursion
{
    public class DebugController : MonoBehaviour
    {
        public static DebugController Instance;

        public CombatPlayerController PlayerController;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void EndTurn(InputAction.CallbackContext context)
        {
            if (context.performed)
                PlayerController.UnitActionEnd();
        }
    }
}