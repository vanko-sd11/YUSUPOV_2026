using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterCotroller;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Animator _animator;

    private bool _isRunning;
    private bool _isWalking;

    private void Awake()
    {
 
        if (_characterCotroller == null)
        {
            Debug.Log("Компонент CharacterController не назначен");
            _characterCotroller = GetComponent<CharacterController>();
        }
    }
    private void OnEnable()
    {
        _playerInput.Controls.Player.Run.started += OnRunStarted;
        _playerInput.Controls.Player.Run.canceled += OnRunCancelled;
        _playerInput.Controls.Player.Move.started += OnWalkStarted;
        _playerInput.Controls.Player.Move.canceled += OnWalkCancelled;
    }

    private void OnDisable()
    {
        _playerInput.Controls.Player.Run.started -= OnRunStarted;
        _playerInput.Controls.Player.Run.canceled -= OnRunCancelled;
        _playerInput.Controls.Player.Move.started -= OnWalkStarted;
        _playerInput.Controls.Player.Move.canceled -= OnWalkCancelled;
    }
    private void Update()
    {
        Move();
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
        Vector2 move = _playerInput.Controls.Player.Move.ReadValue<Vector2>();
        float speed = _speed;

        if (_isRunning)
        {
            speed *= 3f;
        }
        bool isMoving = move.sqrMagnitude > 0.01f;
        _animator.SetBool("IsWalk", isMoving && !_isRunning);
        _animator.SetBool("IsRun", isMoving && _isRunning);

        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;
        moveDirection.Normalize();
        _characterCotroller.Move(moveDirection * speed * Time.deltaTime);

       
    }
    
}
