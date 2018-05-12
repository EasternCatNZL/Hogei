using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour {

    //sound effects
    public AudioSource[] sfxArray = new AudioSource[0];

    private void OnEnable()
    {
        MusicManager.sfxVolChangeEvent += ChangeSfxVol;
        MusicManager.sfxMuteEvent += ToggleSfxMuteOn;
        MusicManager.sfxUnmuteEvent += ToggelSfxMuteOff;
    }

    private void OnDisable()
    {
        MusicManager.sfxVolChangeEvent -= ChangeSfxVol;
        MusicManager.sfxMuteEvent -= ToggleSfxMuteOn;
        MusicManager.sfxUnmuteEvent -= ToggelSfxMuteOff;
    }

    // Use this for initialization
    void Start () {
        ChangeSfxVol();
	}

    private void Awake()
    {
        ChangeSfxVol();
    }

    // Update is called once per frame
    void Update () {
		
	}

    //change vol of sfx
    private void ChangeSfxVol()
    {
        //for all sfx
        for(int i = 0; i < sfxArray.Length; i++)
        {
            //change vol
            sfxArray[i].volume = MusicManager.GetSfxVol();
        }
    }

    //change mute of sfx
    private void ToggleSfxMuteOn()
    {
        //for all sfx
        for (int i = 0; i < sfxArray.Length; i++)
        {
            //change vol
            sfxArray[i].mute = true;
        }
    }

    private void ToggelSfxMuteOff()
    {
        //for all sfx
        for (int i = 0; i < sfxArray.Length; i++)
        {
            //change vol
            sfxArray[i].mute = false;
        }
    }
}
