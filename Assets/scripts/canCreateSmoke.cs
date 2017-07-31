using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canCreateSmoke : MonoBehaviour {

	public float cooldown;
	private float _nextSmoke;
    private float _stopSmokeAt;
	public ParticleSystem particleSys;

    public BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {

		this.cooldown = 4;
		this._nextSmoke = Time.realtimeSinceStartup + cooldown;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(Time.realtimeSinceStartup > _stopSmokeAt) {
            boxCollider.enabled = false;
        }

		if (Time.realtimeSinceStartup > _nextSmoke) {

			this.particleSys.Play ();
            this._stopSmokeAt = Time.realtimeSinceStartup + this.particleSys.main.duration;
			this._nextSmoke = Time.realtimeSinceStartup + this.cooldown;
            boxCollider.enabled = true;

		}

	}

}
