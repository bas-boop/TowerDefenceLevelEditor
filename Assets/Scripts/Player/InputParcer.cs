using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CameraController))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;
        
        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void Update()
        {
            float scrollInput = _inputActionAsset["Scroll"].ReadValue<float>();
            cameraController.Zoom(-scrollInput);
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _inputActionAsset["Move"].ReadValue<Vector2>();
            cameraController.Move(moveInput);
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();

        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Init() => _inputActionAsset = _playerInput.actions;

        private void AddListeners()
        {
            _inputActionAsset["ResetPosition"].performed += ResetAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["ResetPosition"].performed -= ResetAction;
        }
        
        #region Context

        private void ResetAction(InputAction.CallbackContext context) => cameraController.ResetPosition();

        #endregion
    }
}