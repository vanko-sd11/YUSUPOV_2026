using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private DataStorage _dataStorage;

    private PlayerControls _controls;

    public PlayerControls Controls => _controls;

    private void Awake()
    {
        _controls = new PlayerControls();

        string json = _dataStorage.LoadKeyBindings();
        if (!string.IsNullOrEmpty(json))
        {
            _controls.asset.LoadBindingOverridesFromJson(json);
        }
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public InputAction GetAction(string actionName)
    {
        return _controls.asset.FindAction("Player/" + actionName);
    }

    public void SaveKeyBindings()
    {
        string json = _controls.asset.SaveBindingOverridesAsJson();
        _dataStorage.SaveKeyBindings(json);
    }
}