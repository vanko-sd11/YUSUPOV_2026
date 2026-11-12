using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyRebinding : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private string _actionName;
    [SerializeField] private int _bindingIndex;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private void Start()
    {
        UpdateButtonText();
    }

    public void StartRebinding()
    {
        InputAction action = _playerInput.GetAction(_actionName);

        _buttonText.text = "Нажмите на клавишу ";
        action.Disable();

        _rebindingOperation = action.PerformInteractiveRebinding(_bindingIndex).WithCancelingThrough("<Keyboard>/escape").OnComplete(operation =>
        {
            action.Enable();
            UpdateButtonText();

            _playerInput.SaveKeyBindings();

            operation.Dispose();
        }).OnCancel(operation =>
        {
            action.Enable();
            UpdateButtonText();

            operation.Dispose();
        });

        _rebindingOperation.Start();

    }
    private void UpdateButtonText()
    {
        InputAction action = _playerInput.GetAction(_actionName);

        if (action != null)
        {
            _buttonText.text = action.GetBindingDisplayString(_bindingIndex);
        }
    }
}
