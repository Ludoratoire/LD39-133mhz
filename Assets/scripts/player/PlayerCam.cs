using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {

    public GameObject target;

    private Vector3 _delta;

	void Start () {
        _delta = gameObject.transform.position - target.transform.position;
	}

    void LateUpdate () {
        gameObject.transform.position = new Vector3(target.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}
}
