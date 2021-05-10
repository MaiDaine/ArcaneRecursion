// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Combat"",
            ""id"": ""a4f1f220-8d8d-4b15-bda1-308ebedfbc10"",
            ""actions"": [
                {
                    ""name"": ""LClick"",
                    ""type"": ""Button"",
                    ""id"": ""baf942b6-c9c1-4ed3-a23a-951a349a1423"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DebugMod"",
                    ""type"": ""Button"",
                    ""id"": ""c7c60f71-c6da-436c-a75c-a126de54bbf8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""d9148e73-1423-4b4b-9506-961fba8fccdd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5bc1a128-4e83-4e25-8bb5-aa6c6f99c78f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default (Keyboard + Mouse)"",
                    ""action"": ""LClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""491d84f1-c520-43f7-99bb-771cec02da8b"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default (Keyboard + Mouse)"",
                    ""action"": ""DebugMod"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e4d4394-f0cd-4fa4-ad3e-004157c5712f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default (Keyboard + Mouse)"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default (Keyboard + Mouse)"",
            ""bindingGroup"": ""Default (Keyboard + Mouse)"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_LClick = m_Combat.FindAction("LClick", throwIfNotFound: true);
        m_Combat_DebugMod = m_Combat.FindAction("DebugMod", throwIfNotFound: true);
        m_Combat_Cancel = m_Combat.FindAction("Cancel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_LClick;
    private readonly InputAction m_Combat_DebugMod;
    private readonly InputAction m_Combat_Cancel;
    public struct CombatActions
    {
        private @Controls m_Wrapper;
        public CombatActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LClick => m_Wrapper.m_Combat_LClick;
        public InputAction @DebugMod => m_Wrapper.m_Combat_DebugMod;
        public InputAction @Cancel => m_Wrapper.m_Combat_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @LClick.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnLClick;
                @LClick.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnLClick;
                @LClick.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnLClick;
                @DebugMod.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnDebugMod;
                @DebugMod.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnDebugMod;
                @DebugMod.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnDebugMod;
                @Cancel.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LClick.started += instance.OnLClick;
                @LClick.performed += instance.OnLClick;
                @LClick.canceled += instance.OnLClick;
                @DebugMod.started += instance.OnDebugMod;
                @DebugMod.performed += instance.OnDebugMod;
                @DebugMod.canceled += instance.OnDebugMod;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    private int m_DefaultKeyboardMouseSchemeIndex = -1;
    public InputControlScheme DefaultKeyboardMouseScheme
    {
        get
        {
            if (m_DefaultKeyboardMouseSchemeIndex == -1) m_DefaultKeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Default (Keyboard + Mouse)");
            return asset.controlSchemes[m_DefaultKeyboardMouseSchemeIndex];
        }
    }
    public interface ICombatActions
    {
        void OnLClick(InputAction.CallbackContext context);
        void OnDebugMod(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
}
