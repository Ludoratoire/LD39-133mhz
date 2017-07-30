using AlpacaSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _sInstance;
    public static GameManager Instance {
        get {
            return _sInstance;
        }
        private set {
            _sInstance = value;
        }
    }

    public GameObject player;
    public Camera gameCamera;
    public int powerAvailable = 133;
    public int speedFactor = 100;
	public int life = 3;
    public int score = 0;
    public List<GameTask> taskList;

    protected RetroPixel _retroPixel;

	void Start () {
        if (_sInstance == null)
            _sInstance = this;
        else {
            Destroy(this);
            return;
        }

        _retroPixel = gameCamera.GetComponent<RetroPixel>();
        _retroPixel.enabled = false;

        taskList = new List<GameTask>();
        taskList.Add(new GravityTask());
        taskList.Add(new LuminosityTask());
        taskList.Add(new ResolutionTask());
        taskList.Add(new JumpTask());
        taskList.Add(new MobilityTask());
        taskList.Add(new CollisionTask());
        taskList.Add(new ScrollingTask());
    }
	
    // Tasks

    public GameTask GetTask(string name) {
        return taskList.Find(x => x.name == name);
    }

    // Retro Pixel
    public void SetRetroFactor(int factor) {
        if (factor == 100)
            _retroPixel.enabled = false;
        else
            _retroPixel.enabled = true;

        _retroPixel.horizontalResolution = (int) (1280 * factor / 100);
        _retroPixel.verticalResolution = (int) (768 * factor / 100);
    }
}
