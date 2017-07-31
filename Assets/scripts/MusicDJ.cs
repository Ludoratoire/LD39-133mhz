using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDJ : MonoBehaviour {

    public AudioClip[] musics;
    public float timeBetweenSongs = 2f;

    private int _currentMusic = 0;
    private AudioSource _source;
    private float _nextSongAt;

	void Start () {

        _source = GetComponent<AudioSource>();
        var clip = musics[_currentMusic];
        _source.clip = clip;
        _source.Play();
        _nextSongAt = Time.realtimeSinceStartup + clip.length + timeBetweenSongs;
        _currentMusic++;
        if (_currentMusic >= musics.Length)
            _currentMusic = 0;
	}
	
	void Update () {
        var current = Time.realtimeSinceStartup;
        if(current > _nextSongAt) {
            var clip = musics[_currentMusic];
            _source.clip = clip;
            _source.Play();
            _nextSongAt = Time.realtimeSinceStartup + clip.length + timeBetweenSongs;
            _currentMusic++;
            if (_currentMusic >= musics.Length)
                _currentMusic = 0;
        }
	}
}
