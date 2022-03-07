using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public float maxScore = 10f;
    public float maxTime = 30f;
    public MiniGameUI miniGameUI;
    public FlowerManager flowerManager;
    public HummingbirdAgent player;
    public HummingbirdAgent aiPlayer;

    private float gameStartTime;
    
    public enum GameStates
    {
        Default,
        GameStart,
        GamePlaying,
        GameEnd
    }

    public GameStates currentState = GameStates.Default;

    public float TimeRemaining
    {
        get
        {
            if (currentState == GameStates.GamePlaying)
            {
                float timeRemaining = maxTime - (Time.time - gameStartTime);
                return Mathf.Max(0f, timeRemaining);
            }
            else
            {
                return 0f;
            }
        }
    }

    public void ButtonClicked()
    {
        if (currentState == GameStates.GameStart)
        {
            //start game
            PlayingSetUp();
        }else if (currentState == GameStates.GameEnd)
        {
            //goto main menu
            StartMenuSetUp();
        }
    }

    private void Start()
    {
        miniGameUI.OnMiniGameButtonClick += ButtonClicked;
        StartMenuSetUp();
    }

    private void OnDestroy()
    {
        miniGameUI.OnMiniGameButtonClick -= ButtonClicked;
    }

    private void StartMenuSetUp()
    {
        currentState = GameStates.GameStart;
        
        miniGameUI.ShowHeader("Hummingbird\nGame");
        miniGameUI.ShowButton("Start");
        
        aiPlayer.agentCamera.gameObject.SetActive(false);
        
        flowerManager.ResetFlowers();
        
        player.OnEpisodeBegin();
        aiPlayer.OnEpisodeBegin();
        
        player.FreezeAgent();
        aiPlayer.FreezeAgent();
    }

    private void PlayingSetUp()
    {
        currentState = GameStates.GamePlaying;
        gameStartTime = Time.time;
        miniGameUI.HideHeader();
        miniGameUI.HideButton();
        player.UnfreezeAgent();
        aiPlayer.UnfreezeAgent();

    }

    private void EndSetUp()
    {
        currentState = GameStates.GameEnd;
        player.FreezeAgent();
        aiPlayer.FreezeAgent();

        miniGameUI.ShowHeader(player.NectarObtained >= aiPlayer.NectarObtained ? "You win :)" : "ai wins :(");

        miniGameUI.ShowButton("Restart");
    }

    private void Update()
    {
        if (currentState == GameStates.GamePlaying)
        {
            if (TimeRemaining <= 0f || player.NectarObtained >= maxScore || aiPlayer.NectarObtained >= maxScore)
            {
                EndSetUp();
            }
            
            miniGameUI.SetTimerText(TimeRemaining);
            miniGameUI.SetPlayerBar(player.NectarObtained/maxScore);
            miniGameUI.SetAiBar(aiPlayer.NectarObtained / maxScore);
        }else if (currentState == GameStates.GameEnd)
        {
            miniGameUI.SetTimerText(TimeRemaining);
        }
        else
        {
            miniGameUI.SetTimerText(-1f);
            miniGameUI.SetPlayerBar(0f);
            miniGameUI.SetAiBar(0f);
        }
    }
}
