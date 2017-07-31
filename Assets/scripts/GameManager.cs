using AlpacaSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject startPos;
    public Camera gameCamera;
    public int powerAvailable = 133;
    public int speedFactor = 100;
	public int life = 3;
    public int score = 0;
    public List<GameTask> taskList;
    public GameObject LevelModel;
    public Text bigMessage;
    public GameObject restartButton;
    public int nextKillPoint = 1;

    protected AudioSource _audioSource;
    protected RetroPixel _retroPixel;
    private GameObject _levelInstance;

	void Start () {
        if (_sInstance == null)
            _sInstance = this;
        else {
            Destroy(this);
            return;
        }

        _audioSource = GetComponent<AudioSource>();

        LevelModel.SetActive(false);
        _levelInstance = Object.Instantiate(LevelModel);
        _levelInstance.SetActive(true);

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

    public void ResetPlayerPos() {
        foreach (var c in player.GetComponents<Collider2D>()) {
            c.enabled = true;
        }
        player.transform.position = startPos.transform.position;
    }

    public void ResetLevel() {
        GameObject.Destroy(_levelInstance);
        _levelInstance = Object.Instantiate(LevelModel);
        _levelInstance.SetActive(true);
        ResetPlayerPos();
        bigMessage.enabled = false;
        restartButton.SetActive(false);
        score = 0;
        life = 3;
        Time.timeScale = 1;
    }

    public void Lose() {
        _audioSource.Play();
        bigMessage.text = "YOU LOSE !";
        bigMessage.enabled = true;
        restartButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void Victory() {
        bigMessage.text = "YOU WIN !";
        bigMessage.enabled = true;
        restartButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void KillScoreUp() {
        score += nextKillPoint;
        nextKillPoint++;
    }
}
