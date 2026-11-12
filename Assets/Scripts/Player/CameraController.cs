using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _player;
    [SerializeField] private CinemachineCamera _cinemachineCamera;

    [SerializeField] private float _rotationSpeed = 80f;
    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private float _minFov = 20f;
    [SerializeField] private float _maxFov = 60f;

    private void Update()
    {
        Rotate();
        Zoom();
    }

    private void Rotate()
    {
        if (_playerInput.Controls.Player.RotateLeft.IsPressed())
        {
            _player.Rotate(Vector3.up, -_rotationSpeed * Time.deltaTime);
        }

        if (_playerInput.Controls.Player.RotateRight.IsPressed())
        {
            _player.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        }
    }

    private void Zoom()
    {
        float zoom = _playerInput.Controls.Player.Zoom.ReadValue<Vector2>().y;

        LensSettings lens = _cinemachineCamera.Lens;
        lens.FieldOfView -= zoom * _zoomSpeed;
        lens.FieldOfView = Mathf.Clamp(lens.FieldOfView, _minFov, _maxFov);
        _cinemachineCamera.Lens = lens;
    }
}