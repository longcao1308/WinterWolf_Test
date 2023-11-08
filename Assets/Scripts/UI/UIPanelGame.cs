﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelGame : MonoBehaviour,IMenu
{
    public Text LevelConditionView;

    [SerializeField] private Button btnPause;

    [SerializeField] Button _buttonRestart;

    private UIMainManager m_mngr;

    private void Awake()
    {
        btnPause.onClick.AddListener(OnClickPause);
        _buttonRestart.onClick.AddListener(OnClickRestart);
    }

    private void OnClickPause()
    {
        m_mngr.ShowPauseMenu();
    }

    private void OnClickRestart()
    {
        m_mngr.RestartLevel();
    }

    public void Setup(UIMainManager mngr)
    {
        m_mngr = mngr;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
