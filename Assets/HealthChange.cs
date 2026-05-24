using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthChange : MonoBehaviour
{
    public PlayerStats stats;
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
        if (stats == null) return;

        int hp = stats.health;

        if (hp >= 200)
            Spr.sprite = healStatus[0];
        else if (hp >= 150)
            Spr.sprite = healStatus[1];
        else if (hp >= 100)
            Spr.sprite = healStatus[2];
        else if (hp >= 50)
            Spr.sprite = healStatus[3];
        else if (hp > 0)
            Spr.sprite = healStatus[4];
        else if (!isDead)
        {
            Debug.Log("Персонаж умер");
            Spr.sprite = healStatus[5];

            isDead = true;
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}