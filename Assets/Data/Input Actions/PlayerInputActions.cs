//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Data/Input Actions/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""General"",
            ""id"": ""f0f903d7-0145-4520-977b-5b3cc07f40db"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""458d7415-9a09-4503-9234-1f433d3f3420"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""e2c49ef5-be25-4dc4-b9c0-052b449361bb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""4de4133d-74a1-4352-a38e-b764b5a2646b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""bafe6a11-0ab8-468d-968e-d724a0c94a5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""4f37c872-3579-46b3-9bee-d1fe793eea68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7d6b8b38-face-4437-ba45-64ff0aad4b17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDrag"",
                    ""type"": ""Value"",
                    ""id"": ""ed0d563b-b401-4246-a078-53d6bee392fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseHold"",
                    ""type"": ""Button"",
                    ""id"": ""1c0f7a47-3e75-441d-be5c-8567ab45dcc6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4bb3f390-e61c-40da-8a45-c8832bf0249e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""1b9c004a-93c5-4879-bda1-0e8fbe0097d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerIndex"",
                    ""type"": ""Button"",
                    ""id"": ""78aeae61-9987-4b5b-b5d7-1d9be58438b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""70c810de-261a-4b0c-8c0a-a9596b1e414b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""39519dc8-6e30-4703-97c4-69430ed2d71d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""20d4a5d7-02ed-4327-ad7d-e2f5a65ad2ed"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e844c7b5-744f-4540-b801-7cdbae5e65d0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""43085fc1-b8c3-4ca7-9246-daf4d384d0d8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c2966016-506a-42cf-9d7f-094bd3f1e494"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e78b6e5-abac-4d73-800f-fc50c0a44faf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""323183e7-3d2f-429e-a671-ef89bb9c9396"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""124fa33d-d6c9-40b3-b418-179a5c1bb8b8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fab1f648-8dc7-4ad1-8332-d1b6d2bdc8f0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""932bd2cf-4a16-43c7-ace8-047a82b2888a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52940e18-1245-47ba-884e-0d249cdec942"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94e68291-5b3d-4004-a536-192ae418cf51"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76ac157d-3df4-470b-8e90-7ac0c49debf3"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8d29ecf-51cf-4ba7-9596-eef3f8d87c76"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f086e071-5692-4e15-b23e-6b492b503e1e"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5135ca6d-6940-4a1d-8626-10ac4a36cb56"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b4dc730-e7ef-4f0e-8aec-cb14e1aab53f"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7adc4780-afcb-41c8-aaae-36bd927fbf02"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""78984037-8ebe-4bae-8534-c0579e79811a"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""7158591d-f30d-4c4f-880c-7363d09f5d97"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""ca801f16-e9b6-421f-b867-54ebe9ed9dc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""4331ea47-f5c4-4c69-9486-ca435dc25efc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""ebca5758-3ab5-4a29-8a22-0b3751b4ddab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""74177cdd-860b-413e-bea9-4907b69a76e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fa0739d6-7c1c-4ca9-ab45-0eb2c1b12dd3"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""040de3ec-bb44-41e9-8ad0-bd81fc31d7c1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74a5f9b0-3829-4862-bdff-8ca4618f6fdb"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d2cdc91-599c-473d-bc64-8a63c2be4a90"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96e63c4a-d54d-4516-9845-9af0eb648c35"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""605aa53e-4636-4e58-a677-859b80c882a8"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_Move = m_General.FindAction("Move", throwIfNotFound: true);
        m_General_MousePosition = m_General.FindAction("MousePosition", throwIfNotFound: true);
        m_General_MouseClick = m_General.FindAction("MouseClick", throwIfNotFound: true);
        m_General_Inventory = m_General.FindAction("Inventory", throwIfNotFound: true);
        m_General_Escape = m_General.FindAction("Escape", throwIfNotFound: true);
        m_General_Interact = m_General.FindAction("Interact", throwIfNotFound: true);
        m_General_MouseDrag = m_General.FindAction("MouseDrag", throwIfNotFound: true);
        m_General_MouseHold = m_General.FindAction("MouseHold", throwIfNotFound: true);
        m_General_Jump = m_General.FindAction("Jump", throwIfNotFound: true);
        m_General_Aim = m_General.FindAction("Aim", throwIfNotFound: true);
        m_General_PlayerIndex = m_General.FindAction("PlayerIndex", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_MousePosition = m_UI.FindAction("MousePosition", throwIfNotFound: true);
        m_UI_MouseClick = m_UI.FindAction("MouseClick", throwIfNotFound: true);
        m_UI_Inventory = m_UI.FindAction("Inventory", throwIfNotFound: true);
        m_UI_Escape = m_UI.FindAction("Escape", throwIfNotFound: true);
        m_UI_Accept = m_UI.FindAction("Accept", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_Move;
    private readonly InputAction m_General_MousePosition;
    private readonly InputAction m_General_MouseClick;
    private readonly InputAction m_General_Inventory;
    private readonly InputAction m_General_Escape;
    private readonly InputAction m_General_Interact;
    private readonly InputAction m_General_MouseDrag;
    private readonly InputAction m_General_MouseHold;
    private readonly InputAction m_General_Jump;
    private readonly InputAction m_General_Aim;
    private readonly InputAction m_General_PlayerIndex;
    public struct GeneralActions
    {
        private @PlayerInputActions m_Wrapper;
        public GeneralActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_General_Move;
        public InputAction @MousePosition => m_Wrapper.m_General_MousePosition;
        public InputAction @MouseClick => m_Wrapper.m_General_MouseClick;
        public InputAction @Inventory => m_Wrapper.m_General_Inventory;
        public InputAction @Escape => m_Wrapper.m_General_Escape;
        public InputAction @Interact => m_Wrapper.m_General_Interact;
        public InputAction @MouseDrag => m_Wrapper.m_General_MouseDrag;
        public InputAction @MouseHold => m_Wrapper.m_General_MouseHold;
        public InputAction @Jump => m_Wrapper.m_General_Jump;
        public InputAction @Aim => m_Wrapper.m_General_Aim;
        public InputAction @PlayerIndex => m_Wrapper.m_General_PlayerIndex;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMove;
                @MousePosition.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMousePosition;
                @MouseClick.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseClick;
                @Inventory.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInventory;
                @Escape.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnEscape;
                @Interact.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @MouseDrag.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseDrag;
                @MouseDrag.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseDrag;
                @MouseDrag.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseDrag;
                @MouseHold.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseHold;
                @MouseHold.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseHold;
                @MouseHold.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnMouseHold;
                @Jump.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnAim;
                @PlayerIndex.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPlayerIndex;
                @PlayerIndex.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPlayerIndex;
                @PlayerIndex.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPlayerIndex;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @MouseDrag.started += instance.OnMouseDrag;
                @MouseDrag.performed += instance.OnMouseDrag;
                @MouseDrag.canceled += instance.OnMouseDrag;
                @MouseHold.started += instance.OnMouseHold;
                @MouseHold.performed += instance.OnMouseHold;
                @MouseHold.canceled += instance.OnMouseHold;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @PlayerIndex.started += instance.OnPlayerIndex;
                @PlayerIndex.performed += instance.OnPlayerIndex;
                @PlayerIndex.canceled += instance.OnPlayerIndex;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_MousePosition;
    private readonly InputAction m_UI_MouseClick;
    private readonly InputAction m_UI_Inventory;
    private readonly InputAction m_UI_Escape;
    private readonly InputAction m_UI_Accept;
    public struct UIActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_UI_MousePosition;
        public InputAction @MouseClick => m_Wrapper.m_UI_MouseClick;
        public InputAction @Inventory => m_Wrapper.m_UI_Inventory;
        public InputAction @Escape => m_Wrapper.m_UI_Escape;
        public InputAction @Accept => m_Wrapper.m_UI_Accept;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                @MouseClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMouseClick;
                @Inventory.started -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnInventory;
                @Escape.started -= m_Wrapper.m_UIActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnEscape;
                @Accept.started -= m_Wrapper.m_UIActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnAccept;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IGeneralActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMouseDrag(InputAction.CallbackContext context);
        void OnMouseHold(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnPlayerIndex(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
    }
}
