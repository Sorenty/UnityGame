using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoatChange : MonoBehaviour
{
    public KeyBoard_Simple control;

    public Sprite[] BoatStatus;
    public SpriteRenderer BOAT;

    public GameObject WinTextPanel;

    [Header("QTE")]
    public BoatQTEManager qteManager;

    private bool hasWon = false;
    private bool qteStarted = false;

    // 0 = сломана
    // 1 = первая стадия
    // 2 = полностью починена
    private int boatStage = 0;

    void Start()
    {
        boatStage = 0;
    }

    private void OnCollisionEnter2D(Collision2D boat)
    {
        Debug.Log("Boat collision enter");

        // Если лодка полностью готова → переход на новую сцену
        if (boatStage >= 2)
        {
            Debug.Log("Loading scene 2...");
            SceneManager.LoadScene("2");
            return;
        }

        if (qteStarted)
            return;

        // ---------- ПЕРВАЯ СТАДИЯ ----------
        if (boatStage == 0 && control.tree >= 10)
        {
            qteStarted = true;

            Debug.Log("Starting QTE for stage 1");

            if (qteManager != null)
            {
                qteManager.BeginQTE();
            }
            else
            {
                Debug.LogError("qteManager is NULL");
            }

            return;
        }

        // ---------- ВТОРАЯ СТАДИЯ ----------
        if (boatStage == 1 &&
            control.tree >= 20 &&
            control.lestva >= 10)
        {
            qteStarted = true;

            Debug.Log("Starting QTE for final stage");

            if (qteManager != null)
            {
                qteManager.BeginQTE();
            }
            else
            {
                Debug.LogError("qteManager is NULL");
            }
        }
    }

    // УСПЕШНОЕ QTE
    public void FinishRepair()
    {
        // ---------- СТАДИЯ 1 ----------
        if (boatStage == 0 && control.tree >= 10)
        {
            BOAT.sprite = BoatStatus[1];
            boatStage = 1;

            Debug.Log("Boat repaired stage 1");
        }

        // ---------- ФИНАЛ ----------
        else if (boatStage == 1 &&
                 control.tree >= 20 &&
                 control.lestva >= 10)
        {
            BOAT.sprite = BoatStatus[2];
            boatStage = 2;

            Debug.Log("Boat fully repaired");
        }

        qteStarted = false;
    }

    // ПРОВАЛ QTE
    public void FailRepair()
    {
        Debug.Log("QTE FAILED");

        qteStarted = false;
    }
}