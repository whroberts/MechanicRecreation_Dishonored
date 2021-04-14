using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] CharacterController _controller = null;
    [SerializeField] float _speed = 12f;
    [SerializeField] float _sprintBoost = 5f;

    [Header("Gravity")]
    [SerializeField] Transform _groundCheck = null;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _groundDistance = 0.4f;
    [SerializeField] float _jumpHeight = 2f;
    [SerializeField] LayerMask _groundMask;

    [Header("Sound")]
    [SerializeField] AudioClip _jumpSound = null;

    AudioSource _audioSource;

    Vector3 _velocity;
    bool _isGrounded;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        Gravity();
    }
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);

        Sprinting();
    }
    void Gravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _audioSource.PlayOneShot(_jumpSound, 0.5f);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void Sprinting()
    {
        if (Input.GetButtonDown("Sprint") && _isGrounded)
        {
            _speed += _sprintBoost;
        } else if (Input.GetButtonUp("Sprint") && _isGrounded)
        {
            _speed -= _sprintBoost;
        }
    }
}
