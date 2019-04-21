using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{

    public bool isPaused = false;


    public GameObject mainStuff, pauseMenuStuff, controlsStuff, mainPauseMenuStuff;
    public GameObject pauseFirstButton, controlsFirstButton;

    EventSystem eS;
    // Start is called before the first frame update
    void Start()
    {
        eS = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            isPaused = !isPaused;
            if (isPaused)
            {
                eS.SetSelectedGameObject(pauseFirstButton);
                Time.timeScale = 0;
                mainStuff.SetActive(false);
                pauseMenuStuff.SetActive(true);
                
            }
            else {
                Time.timeScale = 1;
                mainStuff.SetActive(true);
                pauseMenuStuff.SetActive(false);
            }

        }


    }

    public void GoToControlsScreen() {
        mainPauseMenuStuff.SetActive(false);
        controlsStuff.SetActive(true);
        eS.SetSelectedGameObject(controlsFirstButton);
    }


    public void BackToMainMenu() {
        mainPauseMenuStuff.SetActive(true);
        controlsStuff.SetActive(false);
        eS.SetSelectedGameObject(pauseFirstButton);

    }


    public void UnPause() {
        isPaused = false;
        Time.timeScale = 1;
        mainStuff.SetActive(true);
        pauseMenuStuff.SetActive(false);
        controlsStuff.SetActive(false);
        mainPauseMenuStuff.SetActive(true);
    }
}
