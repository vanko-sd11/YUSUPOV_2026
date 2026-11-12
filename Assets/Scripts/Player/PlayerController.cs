using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterCotroller;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject _craftPanel;

    [SerializeField] private float _rotationSpeed = 80f;
    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private float _minZoom = 2f;
    [SerializeField] private float _maxZoom = 10f;

    [SerializeField] private float _interactionDistance = 3f;
    [SerializeField] private float _pickupRadius = 3f;

    private PlayerControls _playerControls;
    [SerializeField] private DataStorage _dataStorage;
    public PlayerControls Controls => _playerControls; 
    [SerializeField] private Animator _animator;

    private bool _isRunning;
    private bool _isWalking;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        if (_characterCotroller == null)
        {
            Debug.Log("Компонент CharacterController не назначен");
            _characterCotroller = GetComponent<CharacterController>();
        }
        
        _craftPanel.SetActive(false);
        _playerControls = new PlayerControls();

        string json = _dataStorage.LoadKeyBindings();

        if (!string.IsNullOrEmpty(json))
        {
            _playerControls.asset.LoadBindingOverridesFromJson(json);
        }

        if(_camera == null)
        {
            Debug.Log("Компонент Camera не назначен");
            return;
        }
    }
    private void OnEnable()
    {
        _playerControls.Enable();

        _playerControls.Player.Craft.started += TryCraft;
        _playerControls.Player.Run.started += OnRunStarted;
        _playerControls.Player.Run.canceled += OnRunCancelled;
        _playerControls.Player.Move.started += OnWalkStarted;
        _playerControls.Player.Move.canceled += OnWalkCancelled;
        _playerControls.Player.Interact.started += TryInteract;
        _playerControls.Player.Attack.started += TryAttack;
        _playerControls.Player.Pickup.started += TryPickup;

        
    }
    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Craft.started -= TryCraft;
        _playerControls.Player.Run.started -= OnRunStarted;
        _playerControls.Player.Run.canceled -= OnRunCancelled;
        _playerControls.Player.Move.started -= OnWalkStarted;
        _playerControls.Player.Move.canceled -= OnWalkCancelled;
        _playerControls.Player.Interact.started -= TryInteract;
        _playerControls.Player.Attack.started -= TryAttack;
        _playerControls.Player.Pickup.started -= TryPickup;
    }

    private void TryCraft(InputAction.CallbackContext context)
    {
        _craftPanel.SetActive(!_craftPanel.activeSelf);
    }
    
    private void Update()
    {
        Move();
        ControlCamera();
        
    }
    private void OnRunStarted(InputAction.CallbackContext context)
    {
        _isRunning = true;
        _animator.SetBool("IsRun", true);
    }

    private void OnRunCancelled(InputAction.CallbackContext context)
    {
        _isRunning = false;
        _animator.SetBool("IsRun", false);
    }
    private void OnWalkStarted(InputAction.CallbackContext context)
    {
        _isWalking = true;
        _animator.SetBool("IsWalk", true);
    }

    private void OnWalkCancelled(InputAction.CallbackContext context)
    {
        _isWalking = false;
        _animator.SetBool("IsWalk", false);
    }

    public void Move()
    {
        Vector2 move = _playerControls.Player.Move.ReadValue<Vector2>();
        float speed = _speed;

        if (_isRunning)
        {
            speed *= 3f;
        }

        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;
        moveDirection.Normalize();
        _characterCotroller.Move(moveDirection * speed * Time.deltaTime);

       
    }
    private void ControlCamera() 
    {

        if (_playerControls.Player.RotateLeft.IsPressed())
        {
            transform.Rotate(Vector3.up, -_rotationSpeed * Time.deltaTime);

        }
        if (_playerControls.Player.RotateRight.IsPressed())
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

        }

        float zoom = _playerControls.Player.Zoom.ReadValue<Vector2>().y;
        Vector3 cameraPosition = _camera.localPosition;

        cameraPosition.y += zoom * _zoomSpeed;

        cameraPosition.y = Mathf.Clamp(cameraPosition.y, _minZoom, _maxZoom);

        _camera.localPosition = cameraPosition;
    }

    private void TryInteract(InputAction.CallbackContext context)
    {
        UseObject("Interact");
    }
    private void TryAttack(InputAction.CallbackContext context)
    {
        UseObject("Attack");
    }

    private void UseObject(string method)
    {
        Camera camera = _camera.GetComponent<Camera>();

        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out RaycastHit hit, _interactionDistance))
        {
            hit.collider.SendMessage(method, SendMessageOptions.DontRequireReceiver);
        }

    }

    private void TryPickup(InputAction.CallbackContext context)
    {
        Collider[] items = Physics.OverlapSphere(transform.position, _pickupRadius);

        foreach (Collider item in items)
        {
            if (item.TryGetComponent<Item>(out Item itemComponent))
            {
                itemComponent.Pickup();
                break;
            }
        }
    }

    public InputAction GetAction(string actionName)
    {

        return _playerControls.asset.FindAction("Player/" + actionName);
    }
    public void SaveKeyBindings()
    {
        string json = _playerControls.asset.SaveBindingOverridesAsJson();

        _dataStorage.SaveKeyBindings(json);
    }
}
