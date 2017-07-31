using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanKillEnnemy : MonoBehaviour
{
    public float bounceForce = 5f;
    public Collider2D killCollider;
    public LayerMask layerToKill;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Contact avec un ennemi
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemies"))
        {
            if (collision.otherCollider != killCollider)
                return;

            var collider = killCollider;
            var colliderCenter = collider.bounds.center;
            var colliderHalfWidth = collider.bounds.extents.x;
            var colliderHalfHeight = collider.bounds.extents.y;

            bool zombieKill = false;
            var centerHits = Physics2D.RaycastAll(colliderCenter, Vector2.down, 20f);
            foreach(var centerHit in centerHits) {
                if(centerHit.rigidbody != null && centerHit.rigidbody.gameObject.tag == "zombile") {
                    ZombileBehaviour zombileBehaviour = centerHit.rigidbody.gameObject.GetComponent<ZombileBehaviour>();
                    if (zombileBehaviour != null) {
                        zombieKill = true;
                        centerHit.collider.enabled = false;
                        GameObject.Destroy(centerHit.rigidbody.gameObject);
                        GetComponent<PlayerBehavior>().OnKillEnnemy();

                        // Faire rebondir le joueur
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                        break;
                    }
                }
            }

            if (!zombieKill) {
                var leftHits = Physics2D.RaycastAll(new Vector2(colliderCenter.x - colliderHalfWidth, colliderCenter.y), Vector2.down, 20f);
                foreach(var leftHit in leftHits) {
                    if (leftHit.rigidbody != null && leftHit.rigidbody.gameObject.tag == "zombile") {
                        ZombileBehaviour zombileBehaviour = leftHit.rigidbody.gameObject.GetComponent<ZombileBehaviour>();
                        if (zombileBehaviour != null) {
                            zombieKill = true;
                            leftHit.collider.enabled = false;
                            GameObject.Destroy(leftHit.rigidbody.gameObject);
                            GetComponent<PlayerBehavior>().OnKillEnnemy();

                            // Faire rebondir le joueur
                            GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                            break;
                        }
                    }
                }
            }

            if (!zombieKill) {
                var rightHits = Physics2D.RaycastAll(new Vector2(colliderCenter.x + colliderHalfWidth, colliderCenter.y), Vector2.down, 20f);
                foreach (var rightHit in rightHits) {
                    if (rightHit.rigidbody != null && rightHit.rigidbody.gameObject.tag == "zombile") {
                        ZombileBehaviour zombileBehaviour = rightHit.rigidbody.gameObject.GetComponent<ZombileBehaviour>();
                        if (zombileBehaviour != null) {
                            zombieKill = true;
                            rightHit.collider.enabled = false;
                            GameObject.Destroy(rightHit.rigidbody.gameObject);
                            GetComponent<PlayerBehavior>().OnKillEnnemy();

                            // Faire rebondir le joueur
                            GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                            break;
                        }
                    }
                }
            }

            ////			Debug.Log("CanKillEnnemy.OnCollisionEnter2D");
            //List<RaycastHit2D> hitsBottom = new List<RaycastHit2D>(Physics2D.RaycastAll(transform.position, Vector2.down));
            //hitsBottom.AddRange(Physics2D.RaycastAll(
            //    new Vector3(transform.position.x, transform.position.y - 16, transform.position.z), Vector2.down));
            //hitsBottom.AddRange(Physics2D.RaycastAll(
            //    new Vector3(transform.position.x, transform.position.y + 16, transform.position.z), Vector2.down));

            //foreach (RaycastHit2D raycastHit in hitsBottom)
            //{
            //    // Un ennemi est touché par le bas
            //    if (raycastHit.rigidbody != null &&
            //        raycastHit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Ennemies"))
            //    {
            //        ZombileBehaviour zombileBehaviour = raycastHit.rigidbody.gameObject.GetComponent<ZombileBehaviour>();
            //        if (zombileBehaviour != null)
            //        {
            //            zombieKill = true;
            //            raycastHit.collider.enabled = false;
            //            GameObject.Destroy(raycastHit.rigidbody.gameObject);
            //            GetComponent<PlayerBehavior>().OnKillEnnemy();
                        
            //            // Faire rebondir le joueur
            //            GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            //        }
            //    }
            //}

            // If no killed ennemy from above then we received a hit
            if(!zombieKill) {
                GetComponent<PlayerBehavior>().ReceiveDamage();
            }
        }
    }
}