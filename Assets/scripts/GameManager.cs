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

    public int powerAvailable = 133;
    public List<GameTask> taskList;

	void Start () {
        if (_sInstance == null)
            _sInstance = this;
        else {
            Destroy(this);
            return;
        }

        taskList = new List<GameTask>();
        taskList.Add(new GravityTask());
        taskList.Add(new LuminosityTask());
    }
	
	void Update () {
		
	}

    // Tasks

    public GameTask GetTask(string name) {
        return taskList.Find(x => x.name == name);
    }
}
