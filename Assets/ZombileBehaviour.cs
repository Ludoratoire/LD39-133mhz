using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZombileBehaviour : MonoBehaviour
{
    private Transform _transform;
    private EnnemyWalk _ennemyWalk;

    public enum EnnemyType
    {
        Zombile
    }

    public EnnemyType currentType;
    private GameObject _player;

    // Use this for initialization
    void Start()
    {
        _transform = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _ennemyWalk = GetComponent<EnnemyWalk>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Adapter la direction du Zombie en fonction de celle du joueur
    // Uniquement si le joueur est au même niveau que le zombie (y)
    // Et si il n'y a aucun obstacle entre le joueur et le zombie
    // Et si le joueur est à une distance inférieure à 300px (champ de vision)
    private void FollowPlayer()
    {
        Boolean samePlatform = Mathf.Abs(_player.transform.position.y - _transform.position.y) < 10;
        if (!samePlatform)
        {
            return;
        }
        
        if (PlayerInSight(Vector2.left))
        {
            _ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Left);
        }
        if (PlayerInSight(Vector2.right))
        {
            _ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Right);
        }
    }

    Boolean PlayerInSight(Vector2 direction)
    {
        RaycastHit2D[] raycast = Physics2D.RaycastAll(_transform.position, direction, 300);

        foreach (RaycastHit2D hit in raycast)
        {
            if (hit.rigidbody == null || hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Plateforms"))
            {
                return false;
            }

            if (hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
        }
        
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Boolean collideEnnemies = collision.gameObject.layer == LayerMask.NameToLayer("Ennemies");
        Boolean collidePlatforms = collision.gameObject.layer == LayerMask.NameToLayer("Plateforms");
        if (collideEnnemies || collidePlatforms)
        {
//			Debug.Log("ZombileBehaviour.OnTriggerEnter2D : " + collider2D.gameObject.name);
            _ennemyWalk.SwitchDirection();
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == LayerMask.NameToLayer("EnnemyWalls"))
        {
//			Debug.Log("ZombileBehaviour.OnTriggerEnter2D : " + collider2D.gameObject.name);
            _ennemyWalk.SwitchDirection();
        }
    }

    public void Kill()
    {
        DestroyObject(gameObject);
    }
}