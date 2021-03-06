﻿using System;
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
    public float recoveryTime = 2f;

    private float _nextDamageTime = 0;
    private AudioSource _source;

    // Use this for initialization
    void Start()
    {
        _canWalk = GetComponent<CanWalk>();
        _canJump = GetComponent<CanJump>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
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
            _source.Play();
        }

        if(gameObject.transform.position.y < fallLimit) {
            ReceiveDamage();
            if(Time.timeScale != 0)
                GameManager.Instance.ResetPlayerPos();
        }
    }

    public void OnKillEnnemy()
    {
        GetComponent<Animator>().SetTrigger("playerHit");
    }

    public void ReceiveDamage() {
        var currentTime = Time.realtimeSinceStartup;
        if(currentTime > _nextDamageTime) {
            var mgr = GameManager.Instance;
            mgr.life--;
            mgr.nextKillPoint = 1;
            if (mgr.life <= 0) {
                mgr.Lose();
            }
            _nextDamageTime = currentTime + recoveryTime;
        }

    }
}