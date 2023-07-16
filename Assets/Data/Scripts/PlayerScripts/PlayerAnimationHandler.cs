using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;

    [SerializeField] [Range(0.01f,1f)] private float animationBlend = 0.05f;

    public Animator PlayerAnimator {get{return _animator;}}

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Run Movement
        Vector2 movementVelocity;
        movementVelocity.x = _playerMovement.PlayerVelocity.x;
        movementVelocity.y = Mathf.Clamp(_playerMovement.PlayerVelocity.magnitude,0f,0.51f);

        _animator.SetFloat("Blend_x",movementVelocity.x, animationBlend, Time.deltaTime);
        _animator.SetFloat("Blend_y",movementVelocity.y, animationBlend, Time.deltaTime);
    }
}
