using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelpAndCreditsManager : MonoBehaviour
{

    public List<GameObject> HelpImages;
    public List<GameObject> CreditImages;

    private bool CanShow = true;

    private int HelpIndex = 0;
    private int CreditIndex = 0;

    private bool ShowingHelp = false;
    private bool ShowingCredits = false;

    public float HelpExitDirection = -1f;
    public float CreditExitDirection = -1f;

    public WhatCanIDO PlayerSettings;
    private float Delay = 1f;
    private float LastTime = 0f;

    void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        //Move all the images above the canvas
        float CanvasHeight = GetComponent<RectTransform>().rect.height;
        foreach (GameObject _Obj in HelpImages)
        {
            _Obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, CanvasHeight, 0f);
        }
        foreach (GameObject _Obj in CreditImages)
        {
            _Obj.GetComponent<RectTransform>().localPosition = new Vector3(0f, CanvasHeight, 0f);
        }
    }

    void Update()
    {
        CanShow = true;
        if (PlayerSettings.useController)
        {
            ControllerUpdate();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (ShowingHelp) NextHelpScreen();
            else if (ShowingCredits) NextCreditScreen();
        }
    }

    public void ControllerUpdate()
    {
        if (Luminosity.IO.InputManager.GetButton("CSelect"))
        {
            if (CanShow && Time.time - LastTime > Delay)
            {
                LastTime = Time.time;
                print("Boop");
                if (ShowingHelp) NextHelpScreen();
                else if (ShowingCredits) NextCreditScreen();
                CanShow = false;
            }
        }
    }

    public void ShowHelpScreen()
    {
        if (!ShowingHelp && !ShowingCredits)
        {
            ShowingHelp = true;
            HelpExitDirection = HelpImages[0].transform.localPosition.y;
            NextHelpScreen();
        }

    }

    void NextHelpScreen()
    {
        if (HelpIndex < HelpImages.Count)
        {
            HelpImages[HelpIndex].transform.DOLocalMoveY(0f, 1f);
            HelpIndex++;
        }
        else
        {
            ShowingHelp = false;
            HelpIndex = 0;
            float CanvasHeight = GetComponent<RectTransform>().rect.height;
            foreach (GameObject _Obj in HelpImages)
            {
                _Obj.transform.DOComplete();
                _Obj.GetComponent<RectTransform>().transform.DOLocalMoveY(HelpExitDirection * -1f, 1f);
            }
        }
    }

    public void ShowCreditScreen()
    {
        if (!ShowingCredits && !ShowingHelp)
        {
            ShowingCredits = true;
            CreditExitDirection = CreditImages[0].transform.localPosition.y;
            NextCreditScreen();
        }

    }

    void NextCreditScreen()
    {
        ShowingCredits = true;
        if (CreditIndex < CreditImages.Count)
        {
            CreditImages[CreditIndex].transform.DOLocalMoveY(0f, 1f);
            CreditIndex++;
        }
        else
        {
            ShowingCredits = false;
            CreditIndex = 0;
            float CanvasHeight = GetComponent<RectTransform>().rect.height;
            foreach (GameObject _Obj in CreditImages)
            {
                _Obj.transform.DOComplete();
                _Obj.GetComponent<RectTransform>().transform.DOLocalMoveY(CreditExitDirection * -1f, 1f);
            }
        }
    }
}
