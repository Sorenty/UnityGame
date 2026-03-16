using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoatChange : MonoBehaviour
{
    public KeyBoard_Simple control;
    public Sprite[] BoatStatus;
    public SpriteRenderer BOAT;
    public Text txt;
    int boat_status;
    public GameObject WinTextPanel;
    private bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        boat_status = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D boat)
    {
        if (control.tree >= 10 && control.tree < 20)
        {
            BOAT.sprite = BoatStatus[1];
 
        }
        else if ((control.tree >= 20) && (control.lestva >= 10))
        {
            BOAT.sprite = BoatStatus[2];
        }
    }
    private void OnCollisionStay2D(Collision2D boat)
    {
        if (!hasWon && BOAT.sprite == BoatStatus[2])
        {
            hasWon = true;
            if (WinTextPanel != null)
                WinTextPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
