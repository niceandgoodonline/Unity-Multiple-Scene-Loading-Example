using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Transform _t;

    private MainInputActions mainInput;
    private InputAction inputMove;

    private Rigidbody _rb;

    private Vector2 _v2Move;
    private float _moveSpeed, _moveMulti, _moveX, _moveZ, _baseY, _currentY;

    private void Awake()
    {
        mainInput = new MainInputActions();
        _t        = this.transform;
        InitRigidBody();
    }

    private void InitRigidBody()
    {
        if (_rb == null) 
        {
            try
            {
                _rb = this.gameObject.GetComponent<Rigidbody>();
            }
            catch
            {
                _rb = new Rigidbody();
            }
        }
    }

    private void OnEnable()
    {
        inputMove = mainInput.Basic.Move;
        inputMove.Enable();
        SetStandardVariables();
        SubscribeToEvents(true);
    }

    private void SetStandardVariables()
    {
        _moveSpeed         = 1000f;
        _moveMulti         = 1f;
        _baseY             = 0.5f;
        _currentY          = _baseY;
    }

    private void SubscribeToEvents(bool state)
    {
        if(state) {}
        else {}
    }

    private void UpdateMove()
    {
        _v2Move      = inputMove.ReadValue<Vector2>();
        _moveX       = _v2Move.x * _moveSpeed * Time.fixedDeltaTime * _moveMulti;
        _moveZ       = _v2Move.y * _moveSpeed * Time.fixedDeltaTime * _moveMulti;
        _rb.velocity = new Vector3(_moveX, _rb.velocity.y, _moveZ);
    }

    private void FixedUpdate()
    {
        UpdateMove();
    }
}
