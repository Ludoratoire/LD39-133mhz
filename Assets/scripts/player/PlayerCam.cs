using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {

    public GameObject target;

    public ParallaxLayer[] parallaxLayers;

    void LateUpdate () {
        var delta = target.transform.position.x - gameObject.transform.position.x;
        gameObject.transform.position = new Vector3(target.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        foreach(var layer in parallaxLayers) {
            layer.MoveLayer(delta);
        }
	}
}
