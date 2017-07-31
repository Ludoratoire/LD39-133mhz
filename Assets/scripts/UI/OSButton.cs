using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSButton : MonoBehaviour {

    public OSButtonID ID;

    public TaskBar taskbar;

    public Sprite inactiveImage;
    public Sprite activeImage;

    protected Image _buttonImageComp;
    protected Button _buttonComp;

    private bool _active = false;

    void Start () {
        _buttonImageComp = GetComponent<Image>();
        _buttonComp = GetComponent<Button>();
        _buttonComp.onClick.AddListener(Active);
	}
	
	public void Active() {
        taskbar.Active(ID);
        if(_active) {
            _active = false;
            _buttonImageComp.sprite = inactiveImage;
        }
        else {
            _active = true;
            _buttonImageComp.sprite = activeImage;
        }
    }

    public void InActive() {
        _active = false;
        _buttonImageComp.sprite = inactiveImage;
    }

}

public enum OSButtonID {
    Start,
    Game,
    Console
}
