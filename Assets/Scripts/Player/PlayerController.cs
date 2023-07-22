using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Class Authored by Robbie Beaumont

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1.0f;
    public PlayerControls playerControls;

    private Vector2 _playerDirection = Vector2.zero;
    private bool _attackPressed = false;
    private bool _defendPressed = false;
    private InputAction _move;
    private InputAction _attack;
    private InputAction _defend;

    public bool GetAttackPressed() => _attackPressed;
    public bool GetDefendPressed() => _defendPressed;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _move = playerControls.Player.Move;
        _attack = playerControls.Player.Attack;
        _defend = playerControls.Player.Defend;
        _move.Enable();
        _attack.Enable();
        _defend.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
        _attack.Disable();
        _defend.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _playerDirection = _move.ReadValue<Vector2>();
        if (_attack.ReadValue<float>() > 0)
        {
            _attackPressed = true;
        }
        else
        {
            _attackPressed = false;
        }

        if (_defend.ReadValue<float>() > 0)
        {
            _defendPressed = true;
        }
        else 
        {
            _defendPressed = false;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_playerDirection.x * moveSpeed, rb.velocity.y, _playerDirection.y * moveSpeed);
    }
}
