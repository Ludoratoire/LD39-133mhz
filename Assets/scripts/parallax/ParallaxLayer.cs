using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {

    public float speed;
    public GameObject sprite;
    public GameObject nextSprite;
    public GameObject previousSprite;

    public void MoveLayer(float pixelNumber) {
        var currentPos = sprite.transform.position;

        var move = -speed * pixelNumber * Time.fixedDeltaTime;
        sprite.transform.position = new Vector2(move + currentPos.x, currentPos.y);

        currentPos = nextSprite.transform.position;
        nextSprite.transform.position = new Vector2(move + currentPos.x, currentPos.y);

        if (nextSprite.transform.localPosition.x <= 0) { 
            previousSprite.transform.position = new Vector2(12 + nextSprite.transform.position.x, sprite.transform.position.y);
            var temp = nextSprite;
            nextSprite = previousSprite;
            previousSprite = sprite;
            sprite = temp;
        }

        currentPos = previousSprite.transform.position;
        previousSprite.transform.position = new Vector2(move + currentPos.x, currentPos.y);

        if (previousSprite.transform.localPosition.x >= 0) {
            nextSprite.transform.position = new Vector2(12 - previousSprite.transform.position.x, sprite.transform.position.y);
            var temp = previousSprite;
            previousSprite = nextSprite;
            nextSprite = sprite;
            sprite = temp;
        }

    }
}
