using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuButton : MonoBehaviour {

    public Sprite inactiveImage;
    public Sprite activeImage;

    protected Image buttonImage;

    public void Start() {
        buttonImage = GetComponent<Image>();
    }

    public void MouseOn() {
        buttonImage.sprite = activeImage;
    }

    public void MouseOut() {
        buttonImage.sprite = inactiveImage;
    }

}
