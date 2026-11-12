using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _pickupRadius = 5f;

    private void OnEnable()
    {
        _playerInput.Controls.Player.Pickup.started += TryPickup;
    }
    private void OnDisable()
    {
        _playerInput.Controls.Player.Pickup.started -= TryPickup;
    }

    private void TryPickup(InputAction.CallbackContext context)
    {
        Collider[] items = Physics.OverlapSphere(transform.position, _pickupRadius);
        foreach (Collider item in items)
        {
            if(item.TryGetComponent(out Item component))
            {
                component.Pickup();
                break;
            }
        }
    }
}
