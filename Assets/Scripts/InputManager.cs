using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public enum InputType {
        UI,
        Player
    }

    public class InputManager : MonoBehaviour, GameControls.IPlayerActions, GameControls.IUIActions
    {
        public event UnityAction<Vector2> MovementEvent = delegate { };
        public event UnityAction<bool> RollEvent = delegate { };
        public event UnityAction<bool> AttackEvent = delegate {  }; 
        // Menu Events
        public event UnityAction<Vector2> NavigateEvent = delegate { };
        public event UnityAction<bool> SelectEvent = delegate {  }; 

        private GameControls _actions;

        private void OnEnable()
        {
            if (_actions == null)
            {
                _actions = new GameControls();
                _actions.Player.SetCallbacks(this);
                _actions.UI.SetCallbacks(this);
            }

            SetInputType(InputType.Player);
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void DisableAllInput()
        {
            _actions.Player.Disable();
        }

        public void SetInputType(InputType type)
        {
            if (_actions.Player.enabled) return;

            
            if (type == InputType.Player) {
                _actions.UI.Disable();
                _actions.Player.Enable();
            } else
            {
                _actions.UI.Enable();
                _actions.Player.Disable();
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnRoll(InputAction.CallbackContext context)
        {
            if (context.performed || context.canceled)
            {
                RollEvent?.Invoke(context.ReadValueAsButton());
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke(context.performed);
        }
        public void OnNavigate(InputAction.CallbackContext context)
        {
            NavigateEvent?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnSelect(InputAction.CallbackContext context)
        {
            AttackEvent?.Invoke(context.performed);
        }

    }
}