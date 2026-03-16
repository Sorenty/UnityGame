using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;


public class HealthChange : MonoBehaviour
{
    public KeyBoard_Simple control;
    public Sprite[] healStatus;
    public GameObject GameOverPanel;
    private bool isDead = false;

    Image Spr;
    // Start is called before the first frame update
    void Start()
    {
        Spr = GetComponent<Image>();
        Spr.sprite = healStatus[2];
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (control.health >= 200)
        {
            Spr.sprite = healStatus[0];
        }
        else if (control.health <= 199 && control.health >= 150)
        {
            Spr.sprite = healStatus[1];
        }
        else if (control.health <= 149 && control.health >= 100)
        {
            Spr.sprite = healStatus[2];
        }
        else if (control.health <= 99 && control.health >= 50)
        {
            Spr.sprite = healStatus[3];
        }
        else if (control.health <= 49 && control.health >= 1)
        {
            Spr.sprite = healStatus[4];
        }
        else if (control.health <= 0 && !isDead)
        {
            UnityEngine.Debug.Log("Персонаж умер");
            UnityEngine.Debug.Log("GameOverPanel: " + GameOverPanel);
            Spr.sprite = healStatus[5];
            isDead = true;
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f; 
        }

    }

}
