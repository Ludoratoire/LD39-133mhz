using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour {

    public float maxHeight;
    public float minHeight;
    public float speed;

    protected int direction = 1; // 1 go up, -1 go down

	void FixedUpdate () {
        var move = speed * direction * Time.fixedDeltaTime;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + move);
        if (gameObject.transform.localPosition.y >= maxHeight)
            direction = -1;
        else if (gameObject.transform.localPosition.y <= minHeight)
            direction = 1;
	}
}
