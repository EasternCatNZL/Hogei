using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [System.Serializable]
    public struct MusicFile
    {
        string name;
        int id;
        AudioClip clip;
    }

    [Header("Background Music Files")]
    public MusicFile[] musicFiles = new MusicFile[0];

    [Header("Sound control vars")]
    [Tooltip("Sound effects")]
    public float sfxVol = 1.0f;

    [Header("Background sound")]
    [Tooltip("The bgm volume")]
    public float bgmVol = 1.0f;

    [Header("Master volume")]
    [Tooltip("Master volume")]
    public float masterVol = 1.0f;

    //control vars
    private AudioSource bgm; //audiosource used for background music

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //returns volume of sfx to required components
    public float GetSfxVol()
    {
        float vol = 1.0f;
        vol = masterVol - (1.0f - sfxVol);
        return vol;
    }
}
