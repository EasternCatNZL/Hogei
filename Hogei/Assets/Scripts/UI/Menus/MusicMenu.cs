using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMenu : MonoBehaviour {

    [Header("Menu components")]
    [Tooltip("Ref to master vol control")]
    public Slider masterVolSlider;
    [Tooltip("Ref to bgm slider")]
    public Slider bgmVolSlider;
    [Tooltip("Ref to sfx slider")]
    public Slider sfxVolSlider;

    [Tooltip("Ref to master mute toggle")]
    public Toggle masterMuteToggle;
    [Tooltip("Ref to bgm mute toggle")]
    public Toggle bgmMuteToggle;
    [Tooltip("Ref to sfx mute toggle")]
    public Toggle sfxMuteToggle;

    [Header("Script refs")]
    public MusicManager musicMan;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Update value from sliders
    public void UpdateMasterVol()
    {
        //set master vol of music manager using value from slider
        musicMan.SetMasterVol(masterVolSlider.value);
    }

    public void UpdateBgmVol()
    {
        //set bgm vol of music manager using value from slider
        musicMan.UpdateBgmValue(bgmVolSlider.value);
    }

    public void UpdateSfxVol()
    {
        //set sfx vol of music manager using value from slider
        musicMan.UpdateSfxValue(sfxVolSlider.value);
    }

    public void UpdateMasterMute()
    {
        //set master mute to value of toggle
        musicMan.UpdateMasterMute(masterMuteToggle.isOn);
    }

    public void UpdateBgmMute()
    {
        //set bgm mute to value of toggle
        musicMan.UpdateBgmMute(bgmMuteToggle.isOn);
    }

    public void UpdateSfxMute()
    {
        //set sfx mute to value of toggle
        musicMan.UpdateSfxMute(sfxMuteToggle.isOn);
    }
}
