using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singelton

    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public HologramMananger hologram;
    public MainMenu mainMenu;
    public PauseMenu pauseMenu;

    public Countdown countdown;

    /// <summary>
    /// Enables or disables the hologram.
    /// </summary>
    /// <param name="active">True if the the hud should be displayed; otherwise false.</param>
    public void ShowHologram(bool active)
    {
        hologram.ShowForceFieldBar(active);
        hologram.ShowHealthBar(active);
    }

    /// <summary>
    /// Enables or disables the main menu based on the given value.
    /// </summary>
    /// <param name="active">True if the the main menu should be displayed; otherwise false.</param>
    public void ShowMainMenu(bool active) => mainMenu.gameObject.SetActive(active);

    /// <summary>
    /// Enables or disables the pause menu based on the given value.
    /// </summary>
    /// <param name="active">True if the the pause menu should be displayed; otherwise false.</param>
    public void ShowPauseMenu(bool active) => pauseMenu.gameObject.SetActive(active);

    public IEnumerator StartCountdown(float time, Action callback) => countdown.StartCountdown(time, callback);

    public void StopCountdown() => countdown.StopCountdown();

}