/*
    Here I will put functions related to the actionMaps themselves.
    Like switching actionMaps from walking to swimming ..etc.
*/

using System;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager
    {
        public static PlayerInputActions inputActions = new PlayerInputActions();
        public static event Action<InputActionMap> actionMapChange; // event if actionMap changed

        public static void ToggleActionMap(InputActionMap actionMap)
        {
            if(actionMap.enabled)
                return;

            inputActions.Disable();
            actionMapChange?.Invoke(actionMap);
            actionMap.Enable();
        }
    }
}
