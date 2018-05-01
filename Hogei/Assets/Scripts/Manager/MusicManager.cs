using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    [System.Serializable]
    public struct MusicFile
    {
        string name;
        int id;
        AudioClip clip;
    }

    public struct AudioSourceSettings
    {
        public float Pitch;
        public float Volume;
        public float SpatialBlend;
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

    //event stuff
    public delegate void MusicDelegate();
    public static event MusicDelegate sfxVolChangeEvent;
    public static event MusicDelegate sfxMuteEvent;
    public static event MusicDelegate sfxUnmuteEvent;

    //control vars
    private bool isMuted = false; //checks if all is muted
    private bool isBgmMuted = false; //checks if bgm is muted
    private bool isSfxMuted = false; //checks if sfx is muted

    private AudioSource bgm; //audiosource used for background music
    private AudioClip CurrentBGM;

	// Use this for initialization
	void Awake () {
		if(bgm == null)
        {
            bgm = gameObject.AddComponent<AudioSource>();
            bgm.volume = bgmVol;
        }
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // Update is called once per frame
    void Update () {
		
	}

    //Set vol of bgm
    public void SetMasterVol(float value)
    {
        //change the val of master vol
        masterVol = value;
        //change corresponding values
        bgmVol = GetBgmVol();
        sfxVol = GetSfxVol();

        //change volume of all audio sources
        AdjustVolume();
    }

    //Adjust volume of various components
    private void AdjustVolume()
    {
        SetBgmVol();
        sfxVolChangeEvent();
    }

    //adjust mute state of audio sources
    private void AdjustMuteState()
    {
        //if master muted, mute everything
        if (isMuted)
        {
            //mute the bgm
            bgm.mute = true;
            //send out mute event
            sfxMuteEvent();
        }
        //else master is not muted
        else
        {
            //if bgm is muted
            if (isBgmMuted)
            {
                bgm.mute = true;
            }
            else
            {
                bgm.mute = false;
            }

            SfxEvents();
        }
    }

    //mute events for sfx
    private void SfxEvents()
    {
        //Send out corresponding sfx events based on mute state
        if (isSfxMuted)
        {
            sfxMuteEvent();
        }
        else
        {
            sfxUnmuteEvent();
        }
    }

    //returns volume of bgm
    private float GetBgmVol()
    {
        float vol = 1.0f;
        vol = masterVol - (1.0f - bgmVol);
        return vol;
    }

    //set the volume of bgm
    private void SetBgmVol()
    {
        //set the vol
        bgm.volume = GetBgmVol();
    }

    //returns volume of sfx to required components
    public float GetSfxVol()
    {
        float vol = 1.0f;
        vol = masterVol - (1.0f - sfxVol);
        return vol;
    }

    private void GetSceneBGM()
    {
        CurrentBGM = SceneHandler.GetSceneHandler().BackgroundMusic;
    }

    //to receive values from music menu
    public void UpdateBgmValue(float value)
    {
        bgmVol = value;
        SetBgmVol();
    }

    public void UpdateSfxValue(float value)
    {
        sfxVol = value;
        sfxVolChangeEvent();
    }

    public void UpdateMasterMute(bool choice)
    {
        isMuted = choice;
        AdjustMuteState();
    }

    public void UpdateBgmMute(bool choice)
    {
        isBgmMuted = choice;
        bgm.mute = isBgmMuted;
    }

    public void UpdateSfxMute(bool choice)
    {
        isSfxMuted = choice;
        SfxEvents();
    }

    public static AudioSource PlaySoundAtLocation(AudioClip _Clip, Vector3 _Location)
    {
        if (_Clip == null) return null;
        GameObject _Obj = new GameObject("AudioAtLocation");
        AudioSource Source = _Obj.AddComponent<AudioSource>();
        Source.clip = _Clip;
        Source.Play();
        Destroy(_Obj, Source.clip.length);
        return Source;
    }

    public static AudioSource PlaySoundAtLocation(AudioClip _Clip, Vector3 _Location, float _Pitch)
    {
        if (_Clip == null) return null;
        GameObject _Obj = new GameObject("AudioAtLocation");
        AudioSource Source = _Obj.AddComponent<AudioSource>();
        Source.clip = _Clip;
        Source.pitch = _Pitch;
        Source.Play();
        Destroy(_Obj, Source.clip.length);
        return Source;
    }

    public static AudioSource PlaySoundAtLocation(AudioClip _Clip, Vector3 _Location, float _Pitch, float _Volume)
    {
        if (_Clip == null) return null;
        GameObject _Obj = new GameObject("AudioAtLocation");
        AudioSource Source = _Obj.AddComponent<AudioSource>();
        Source.clip = _Clip;
        Source.pitch = _Pitch;
        Source.volume = _Volume;
        Source.Play();
        Destroy(_Obj, Source.clip.length);
        return Source;
    }

    public static AudioSource PlaySoundAtLocation(AudioClip _Clip, Vector3 _Location, AudioSourceSettings _Settings)
    {
        if (_Clip == null) return null;
        GameObject _Obj = new GameObject("AudioAtLocation");
        AudioSource Source = _Obj.AddComponent<AudioSource>();
        Source.clip = _Clip;
        Source.pitch = _Settings.Pitch;
        Source.volume = _Settings.Volume;
        Source.spatialBlend = _Settings.SpatialBlend;
        Source.Play();
        Destroy(_Obj, Source.clip.length);
        return Source;
    }

    private void OnSceneLoad(Scene _Scene, LoadSceneMode _Mode)
    {
        GetSceneBGM();
        bgm.clip = CurrentBGM;
        bgm.Play();
    }
}
