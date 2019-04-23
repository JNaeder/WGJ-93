using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using FMODUnity;

public class LevelManager : MonoBehaviour
{

    public GameObject startMenu, creditsMenu;
    public GameObject creditMenuFirstButton, startMenuFirstButton;


    [FMODUnity.EventRef]
    public string uiButtonHover, uiButtonClick;

    EventSystem eS;

    private void Start()
    {
        eS = FindObjectOfType<EventSystem>();
    }

    public void LoadLevel(int lvlNum) {
        SceneManager.LoadScene(lvlNum);
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();

    }

    public void ShowCreditsScreen() {
        startMenu.SetActive(false);
        creditsMenu.SetActive(true);
        eS.SetSelectedGameObject(creditMenuFirstButton);
    }

    public void BackToStartMenu() {
        startMenu.SetActive(true);
        creditsMenu.SetActive(false);
        eS.SetSelectedGameObject(startMenuFirstButton);

    }

    public void PlayButtonHoverSound() {
        FMODUnity.RuntimeManager.PlayOneShot(uiButtonHover);
    }

    public void PlayButtonClickSound() {
        FMODUnity.RuntimeManager.PlayOneShot(uiButtonClick);
      }
}
