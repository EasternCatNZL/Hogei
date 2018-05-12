using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEventListener : MonoBehaviour {

    [Header("Sfx sources")]
    public AudioSource[] sfxArray = new AudioSource[0];

    [Header("Tags")]
    public string audioTag = "Audio";

    //script refs
    MusicManager musicMan;

	// Use this for initialization
	void Start () {
        musicMan = GameObject.FindGameObjectWithTag(audioTag).GetComponent<MusicManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //event subscrib
    private void OnEnable()
    {
        MusicManager.sfxMuteEvent += MuteSfx;
        MusicManager.sfxUnmuteEvent += UnmuteSfx;
        MusicManager.sfxVolChangeEvent += ChangeSfxVol;
    }

    private void OnDisable()
    {
        MusicManager.sfxMuteEvent -= MuteSfx;
        MusicManager.sfxUnmuteEvent -= UnmuteSfx;
        MusicManager.sfxVolChangeEvent -= ChangeSfxVol;
    }

    //When volume changes occur
    private void ChangeSfxVol()
    {
        //for all sources
        for(int i = 0; i < sfxArray.Length; i++)
        {
            //sfxArray[i].volume = musicMan.sfxVol;
        }
    }

    //When mute is called
    private void MuteSfx()
    {
        //for all sources
        for (int i = 0; i < sfxArray.Length; i++)
        {
            sfxArray[i].mute = true;
        }
    }

    //When unmute is called
    private void UnmuteSfx()
    {
        //for all sources
        for (int i = 0; i < sfxArray.Length; i++)
        {
            sfxArray[i].mute = false;
        }
    }
}
