using System;
using UnityEngine;

public class ZombileBehaviour : MonoBehaviour
{
    private Transform _transform;
    private EnnemyWalk _ennemyWalk;
    private Animator _animator;
    private Boolean _fighting = false;
    public int seePlayerY;
    public int fightVelocity;
    public int visionRange;

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
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -50)
            GameObject.Destroy(gameObject);
    }


    // Adapter la direction du Zombie en fonction de celle du joueur
    // Uniquement si le joueur est au même niveau que le zombie (y)
    // Et si il n'y a aucun obstacle entre le joueur et le zombie
    // Et si le joueur est à une distance inférieure à 300px (champ de vision)
    private void FollowPlayer()
    {
        Boolean samePlatform = Mathf.Abs(_player.transform.position.y - _transform.position.y) < seePlayerY;
        if (!samePlatform) 
        {
            return;
        }
        
        if (PlayerInSight(Vector2.left))
        {
            _animator.SetTrigger("zombileFight");
            _ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Left);
            _ennemyWalk.unitsPerSecond = fightVelocity;
            _fighting = true;
            return;
        }
        
        if (PlayerInSight(Vector2.right))
        {
            _animator.SetTrigger("zombileFight");
            _ennemyWalk.SetDirection(EnnemyWalk.MoveDirection.Right);
            _ennemyWalk.unitsPerSecond = fightVelocity;
            _fighting = true;
            return;
        }
        
        _fighting = false;
        _ennemyWalk.unitsPerSecond = 1;
        _animator.SetTrigger("zombileWalk");
    }

    Boolean PlayerInSight(Vector2 direction)
    {
        RaycastHit2D[] raycast = Physics2D.RaycastAll(_transform.position, direction, visionRange);

        foreach (RaycastHit2D hit in raycast)
        {
            if (hit.collider == null)
            {
                continue;
            }
            
           // les zombies ne voient pas a travers les plateformes
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Plateforms"))
            {
                return false;
            }

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
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

    // Detection des murs invisbles pour les ennemis
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        //if (_fighting) {
        //    return;
        //}
        
        if (collider2D.gameObject.layer == LayerMask.NameToLayer("EnnemyWalls"))
        {
//			Debug.Log("ZombileBehaviour.OnTriggerEnter2D : " + collider2D.gameObject.name);
            _ennemyWalk.SwitchDirection();
        }
    }

    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }
}