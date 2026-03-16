using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject textPanel;

    void OnMouseDown()
    {
        if (textPanel != null)
        {
            textPanel.SetActive(!textPanel.activeSelf);
        }
    }
}