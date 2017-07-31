using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    private Boolean _isWalking;

    private Boolean _isJumping;

    private Boolean _isIdle;

    private CanWalk _canWalk;
    
    private CanJump _canJump;
    
    private Animator _animator;

    public float fallLimit = -10f;
    

    // Use this for initialization
    void Start()
    {
        _canWalk = GetComponent<CanWalk>();
        _canJump = GetComponent<CanJump>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Boolean _isIdlePrev = _isIdle;
        Boolean _isWalkingPrev = _isWalking;
        Boolean _isJumpingPrev = _isJumping;

        _isWalking = !_canWalk.IsMoveDirection(CanWalk.MoveDirection.None);
        _isJumping = !_canJump.IsTouchingGround();
        _isIdle = !_isWalking && !_isJumping;

        // Trigger playerIdle animation ?
        if (!_isIdlePrev && _isIdle)
        {
            _animator.SetTrigger("playerIdle");
        }
        
        // Trigger playerWalking animation ?
        if (!_isWalkingPrev && _isWalking)
        {
            _animator.SetTrigger("playerWalking");
        }
        
        // Trigger playerJumping animation ?
        if (!_isJumpingPrev && _isJumping)
        {
            _animator.SetTrigger("playerJump");
        }

        if(gameObject.transform.position.y < fallLimit) {
            GameManager.Instance.ResetPlayerPos();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ennemies")) {
            var mgr = GameManager.Instance;
            mgr.life--;
            if(mgr.life <= 0) {
                mgr.score = 0;
                GameManager.Instance.ResetPlayerPos();
            }
        }
    }

}