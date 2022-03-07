using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    public Slider playerProgressBar;
    public Slider aiProgressBar;
    public TMP_Text timerText;
    public TMP_Text headerText;

    public Button miniGameButton;
    public TMP_Text buttonText;

    public delegate void MiniGameButtonClick();

    public MiniGameButtonClick OnMiniGameButtonClick;

    public void MiniGameButtonClicked()
    {
        if (OnMiniGameButtonClick != null) OnMiniGameButtonClick();
    }

    public void ShowButton(string buttonText)
    {
        this.buttonText.text = buttonText;
        miniGameButton.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        miniGameButton.gameObject.SetActive(false);
    }

    public void ShowHeader(string headerText)
    {
        this.headerText.text = headerText;
        this.headerText.gameObject.SetActive(true);
    }

    public void HideHeader()
    {
        headerText.gameObject.SetActive(false);
    }

    public void SetTimerText(float timeRemaining)
    {
        if (timeRemaining > 0f)
        {
            timerText.text = timeRemaining.ToString("00");
        }
        else
        {
            timerText.text = "0";
        }
    }

    public void SetPlayerBar(float playerValue)
    {
        playerProgressBar.value = playerValue;
    }

    public void SetAiBar(float aiValue)
    {
        aiProgressBar.value = aiValue;
    }
}
