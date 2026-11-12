using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _interactDistance = 5f;

    private void OnEnable()
    {
        _playerInput.Controls.Player.Interact.started += TryInteract;
        _playerInput.Controls.Player.Attack.started += TryAttack;
    }
    private void OnDisable()
    {
        _playerInput.Controls.Player.Interact.started -= TryInteract;
        _playerInput.Controls.Player.Attack.started -= TryAttack;
    }

    private void TryInteract(InputAction.CallbackContext context)
    {
        UseObject("Interact");
    }
    private void TryAttack(InputAction.CallbackContext context)
    {
        UseObject("Attack");
    }

    private void UseObject(string methodName)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance))
        {
            hit.collider.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
        }
    }
}