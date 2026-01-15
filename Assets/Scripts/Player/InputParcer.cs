using UnityEngine;
using UnityEngine.InputSystem;

using Framework.Command;
using Tool.FileSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CameraController))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private FileEditor fileEditor;
        [SerializeField] private CommandSystem commandSystem;
        
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
            commandSystem = CommandSystem.Instance;
        }

        private void Init() => _inputActionAsset = _playerInput.actions;

        private void AddListeners()
        {
            _inputActionAsset["ResetPosition"].performed += ResetAction;
            _inputActionAsset["Save"].performed += SaveAction;
            _inputActionAsset["Undo"].performed += UndoAction;
            _inputActionAsset["Redo"].performed += RedoAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["ResetPosition"].performed -= ResetAction;
            _inputActionAsset["Save"].performed += SaveAction;
            _inputActionAsset["Undo"].performed -= UndoAction;
            _inputActionAsset["Redo"].performed -= RedoAction;
        }
        
        #region Context

        private void ResetAction(InputAction.CallbackContext context) => cameraController.ResetPosition();
        
        private void SaveAction(InputAction.CallbackContext context) => fileEditor.SaveTilemapData();

        private void UndoAction(InputAction.CallbackContext context) => commandSystem.Undo();

        private void RedoAction(InputAction.CallbackContext context) => commandSystem.Redo();

        #endregion
    }
}