using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _normalSpeed;
    [SerializeField] float _fastSpeed;
    [SerializeField] float _movementTime;
    [SerializeField] float _rotationAmount;
    [SerializeField] Vector3 _zoomAmount;
    [SerializeField] float _maximumZoomIn;
    [SerializeField] float _maximumZoomOut;

    private Vector3 _newPosition;
    private Quaternion _newRotation;
    private Vector3 _newZoom;

    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;

    private void Start()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
        _newZoom = _cameraTransform.localPosition;
    }

    private void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    private void HandleMouseInput()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            _newZoom += Input.mouseScrollDelta.y * _zoomAmount * 30;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);
                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
            }
        }
    }

    private void HandleMovementInput()
    {
        float movementSpeed = _normalSpeed;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = _fastSpeed;
        }

        // Movement.
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _newPosition += (transform.forward * movementSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _newPosition += (transform.right * movementSpeed);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _newPosition += (transform.forward * -movementSpeed);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _newPosition += (transform.right * -movementSpeed);
        }

        // Rotation.
        if(Input.GetKey(KeyCode.Q))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * _rotationAmount);
        }
        if(Input.GetKey(KeyCode.E))
        {
            _newRotation *= Quaternion.Euler(Vector3.up * -_rotationAmount);
        }

        // Zooming.
        if(Input.GetKey(KeyCode.R))
        {
            _newZoom += _zoomAmount;
        }
        if(Input.GetKey(KeyCode.F))
        {
            _newZoom -= _zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * _movementTime);
        
        if(_newZoom.z > -_maximumZoomIn)
        {
            _newZoom.z = -_maximumZoomIn;
        }

        if (_newZoom.z < -_maximumZoomOut)
        {
            _newZoom.z = -_maximumZoomOut;
        }

        if (_newZoom.y < _maximumZoomIn)
        {
            _newZoom.y = _maximumZoomIn;
        }

        if (_newZoom.y > _maximumZoomOut)
        {
            _newZoom.y = _maximumZoomOut;
        }

        _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _newZoom, Time.deltaTime * _movementTime);
    }

    public void SnapMove(Vector3 position)
    {
        _newPosition = position;
    }
}
