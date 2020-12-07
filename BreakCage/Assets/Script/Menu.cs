using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool LoadMenu = false;

    public GameObject MenuUI;


    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            

            if (LoadMenu)
            {
                Resume();
            } else
            {
                pause();
            }
        }
    }

    void pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0f;
        LoadMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        LoadMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
