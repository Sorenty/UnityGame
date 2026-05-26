using System.Collections;
using UnityEngine;

public class ChestQTETrigger : MonoBehaviour
{
    [Header("QTE")]
    public BoatQTEManager qteManager;

    [Header("UI")]
    public GameObject congratulationPanel;

    [Header("Player")]
    public MonoBehaviour playerMovementScript;
    public Rigidbody2D playerRb;

    private bool qteStarted = false;
    private bool chestOpened = false;

    void Start()
    {
        if (congratulationPanel != null)
            congratulationPanel.SetActive(false);

        if (qteManager != null)
        {
            qteManager.SetActionName("Lockpicking chest");
            qteManager.onSuccess.AddListener(OnQTESuccess);
            qteManager.onFail.AddListener(OnQTEFail);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (chestOpened || qteStarted)
            return;

        if (other.CompareTag("Hero"))
        {
            qteStarted = true;

            if (qteManager != null)
                qteManager.BeginQTE();
        }
    }

    private void OnQTESuccess()
    {
        chestOpened = true;
        qteStarted = false;

        FreezePlayer(true);

        if (congratulationPanel != null)
            congratulationPanel.SetActive(true);

        StartCoroutine(HideCongratulationAfterDelay(10f));
    }

    private void OnQTEFail()
    {
        qteStarted = false;
    }

    private IEnumerator HideCongratulationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (congratulationPanel != null)
            congratulationPanel.SetActive(false);

        FreezePlayer(false);
    }

    private void FreezePlayer(bool freeze)
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = !freeze;

        if (playerRb != null)
        {
            if (freeze)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
                playerRb.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                playerRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}