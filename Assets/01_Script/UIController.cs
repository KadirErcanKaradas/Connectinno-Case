using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject NavigatorBarPanel;
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject ReadyCooKPanel;
    [SerializeField] private GameObject TapToStartButton;
    [SerializeField] private GameObject BackButton,TresureButton;
    [SerializeField] private GameObject PopUpImage,notEnoughMoneyText,giftCoin;
    [SerializeField] private GameObject ToggleSound,ToggleMusic,ToggleVibrate;
    public TMP_Text moneyText,levelText,levelInGameText;
    public int moneyCount = 0;
    public bool isNotEnoughMoney=false;

    public GameObject timer;
    public float timerCount;

    public AudioClip loseClip;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt(("Money"), 0);
            moneyText.text = PlayerPrefs.GetInt("Money").ToString();
        }
        else
        {
            moneyText.text = PlayerPrefs.GetInt("Money").ToString();
        }
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
            levelText.text = "Level " + PlayerPrefs.GetInt("Level");
            levelInGameText.text="Level "+ PlayerPrefs.GetInt("Level");
        }
        else
        {
            levelText.text = "Level " + PlayerPrefs.GetInt("Level");
            levelInGameText.text="Level "+ PlayerPrefs.GetInt("Level");
        }

        if (PlayerPrefs.GetInt("Level")%4==0)
        {
            TresureButton.SetActive(true);
        }
        else
        {
            TresureButton.SetActive(false);
        }
        giftCoin.SetActive(false);
    }
    public void TapToStart()
    {
        GameController.Instance.SetGameStage(GameStage.Started);
        TapToStartButton.SetActive(false);
        MainMenuPanel.SetActive(false);
        NavigatorBarPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        InGamePanel.SetActive(true);
        ReadyCooKPanel.SetActive(true);
    }

    private void Update()
    {
        ShowWinPanel();
        ShowLosePanel();
        
    }

    public void PressMainMenuButton()
    {
        WinPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        NavigatorBarPanel.SetActive(true);
        TapToStartButton.SetActive(true);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void PressShopButton()
    {
        MainMenuPanel.SetActive(false);
        NavigatorBarPanel.SetActive(true);
        ShopPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void PressSettingsButton()
    {
        MainMenuPanel.SetActive(false);
        NavigatorBarPanel.SetActive(false);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void PressBackButton()
    {
        MainMenuPanel.SetActive(true);
        NavigatorBarPanel.SetActive(true);
        ShopPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void PressNextLevelButton()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        levelText.text = PlayerPrefs.GetInt("Level").ToString();
        levelInGameText.text = PlayerPrefs.GetInt("Level").ToString();
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    

    public void ShowWinPanel()
    {
        if (GameController.Instance.GameStage==GameStage.Win)
        {
            StartCoroutine(WinPanelCor());
            GameController.Instance.SetGameStage(GameStage.Loaded);
        }
    }

    public void ShowLosePanel()
    {
        if (GameController.Instance.GameStage == GameStage.Fail)
        {
            StartCoroutine(LosePanelCor());
            GameController.Instance.SetGameStage(GameStage.Loaded);
        }
    }
    public IEnumerator WinPanelCor()
    {
        yield return new WaitForSeconds(1f);
        WinPanel.SetActive(true);
    }
    public IEnumerator LosePanelCor()
    {
        PopUpImage.transform.localScale = Vector3.one/10;
        SoundManager.Instance.PlayLoseSound(loseClip);
        yield return new WaitForSeconds(1f);
        LosePanel.SetActive(true);
        PopUpImage.transform.DOScale(Vector3.one, 1f);
    }

    public void TryAgainButton()
    {
        levelText.text = "Level " + PlayerPrefs.GetInt("Level");
        levelInGameText.text = "Level " + PlayerPrefs.GetInt("Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExtraSecondsButton()
    {
        if (PlayerPrefs.GetInt("Money")>30)
        {
            timerCount = timer.GetComponent<Timer>().TimeLeft;
            timerCount += 30;
            timer.GetComponent<Timer>().TimerOn = true;
            timer.GetComponent<Timer>().ExtraTime = true;
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 30);
            moneyText.text = PlayerPrefs.GetInt("Money").ToString();
            GameController.Instance.SetGameStage(GameStage.Started);
            LosePanel.SetActive(false);
        }
        else
        {
            notEnoughMoneyText.SetActive(true);
            notEnoughMoneyText.transform.DOScale(Vector3.one, 1f).OnComplete(() =>
            {
                notEnoughMoneyText.transform.DOScale(Vector3.one / 100, 1f);
            });
        }
    }

    public void PressTresureButton()
    {
        PlayerPrefs.SetInt("Money",PlayerPrefs.GetInt("Money")+15);
        moneyText.text = PlayerPrefs.GetInt("Money").ToString();
        giftCoin.SetActive(true);
        giftCoin.transform.DOScale(Vector3.one, 1f).OnComplete(() =>
        {
            giftCoin.transform.DOScale(Vector3.one / 100, 1f);
        });
        TresureButton.SetActive(false);
    }

    public void ChangeSoundVolume()
    {
        if (ToggleSound.GetComponent<Toggle>().isOn)
        {
            SoundManager.Instance.PlaySound();
        }else if(!ToggleSound.GetComponent<Toggle>().isOn)
        {
            SoundManager.Instance.StopSound();
        }
    }
    public void ChangeMusicVolume()
    {
        if (ToggleMusic.GetComponent<Toggle>().isOn)
        {
            SoundManager.Instance.PlayMusic();
        }else if(!ToggleMusic.GetComponent<Toggle>().isOn)
        {
            SoundManager.Instance.StopMusic();
        }
    }

    public void OnOffVibrate()
    {
        if (ToggleVibrate.GetComponent<Toggle>().isOn)
        {
            GameController.Instance.isVibrate = true;
        }
        else
        {
            GameController.Instance.isVibrate = false;
        }
    }
}
