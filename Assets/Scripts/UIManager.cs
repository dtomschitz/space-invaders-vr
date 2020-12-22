using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
/// <summary>
/// Class HUDManager is used to centralize all management classes of visual
/// relevant informations the player could see in the game. From here the
/// different menus or the hud could be displayed.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Singelton

    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public HUDManager hud;
    public Canvas shopCanvas;
    public Canvas gameOverCanvas;
    public PauseMenu pauseMenu;

    void Start()
    {
        if (hud == null) throw new NullReferenceException("HUDManager class cannot be null");
        if (shopCanvas == null) throw new NullReferenceException("Shop canavas cannot be null");
        if (gameOverCanvas == null) throw new NullReferenceException("GameOver canvas cannot be null");
        if (pauseMenu == null) throw new NullReferenceException("PauseMenu class cannot be null");
    }

    /// <summary>
    /// Sets the Hud active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the hud should be displayed; otherwise false.</param>
    public void SetHUDActive(bool active) => SetHUDActive(active, true, true, true, true, true);

    /// <summary>
    /// Sets the Hud active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the hud should be displayed; otherwise false.</param>
    /// <param name="showHelp">True if the the help should be displayed; otherwise false.</param>
    public void SetHUDActive(bool active, bool showHelp) => SetHUDActive(active, true, true, showHelp);

    /// <summary>
    /// Sets the Hud active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the hud should be displayed; otherwise false.</param>
    /// <param name="showWaveCountdown">True if the the countdown for the next wave should be displayed; otherwise false.</param>
    /// <param name="showWaveSkipText">True if the the skip text for the next wave should be displayed; otherwise false.</param>
    public void SetHUDActive(bool active, bool showWaveCountdown, bool showWaveSkipText)
    {
        SetHUDActive(active, showWaveCountdown, showWaveSkipText, true, true, true);
    }

    /// <summary>
    /// Sets the Hud active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the hud should be displayed; otherwise false.</param>
    /// <param name="showWaveCountdown">True if the the countdown for the next wave should be displayed; otherwise false.</param>
    /// <param name="showWaveSkipText">True if the the skip text for the next wave should be displayed; otherwise false.</param>
    /// <param name="showHealthBar">True if the the health bar should be displayed; otherwise false.</param>
    /// <param name="showManaBar">True if the the mana bar should be displayed; otherwise false.</param>
    /// <param name="showHelp">True if the the help should be displayed; otherwise false.</param>
    public void SetHUDActive(bool active, bool showWaveCountdown = true, bool showWaveSkipText = true, bool showHealthBar = true, bool showManaBar = true, bool showHelp = true)
    {
        hud.gameObject.SetActive(active);

        hud.waveInfo.SetSkipTextActive(showWaveSkipText);
        hud.waveInfo.SetSkipCountdownActive(showWaveCountdown);

        hud.healthBar.gameObject.SetActive(showHealthBar);
        hud.manaBar.gameObject.SetActive(showManaBar);

        hud.hotbar.SetLeftBumperActive(showHelp);
        hud.hotbar.SetRightBumperActive(showHelp);
    }

    /// <summary>
    /// Sets the Shop active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the shop should be displayed; otherwise false.</param>
    public void SetShopActive(bool active) => shopCanvas.gameObject.SetActive(active);

    /// <summary>
    /// Sets the pause menu active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the pause menu should be displayed; otherwise false.</param>
    public void SetPauseMenuActive(bool active) => pauseMenu.gameObject.SetActive(active);

    /// <summary>
    /// Sets the GameOver menu active or not based on the given value.
    /// </summary>
    /// <param name="active">True if the the game over menu should be displayed; otherwise false.</param>
    public void SetGameOverMenuActive(bool active) => gameOverCanvas.gameObject.SetActive(active);

    */