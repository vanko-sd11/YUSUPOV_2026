using UnityEngine;
using UnityEngine.InputSystem;

public class CraftMenuController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _craftPanel;

    private void Awake()
    {
        _craftPanel.SetActive(false);
    }
    private void OnEnable()
    {
        _playerInput.Controls.Player.Craft.started += CraftPanel;
    }
    private void OnDisable()
    {
        _playerInput.Controls.Player.Craft.started -= CraftPanel;
    }

    private void CraftPanel(InputAction.CallbackContext context)
    {
        _craftPanel.SetActive(!_craftPanel.activeSelf);
    }
}
