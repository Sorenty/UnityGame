using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject textPanel;
    public TMP_Text dialogueText;

    [TextArea(2, 4)]
    public string[] phrases =
    {
        "Привет, странник! Да, я говорящий кот.",
        "Розовые цветки могут тебя лечить!",
        "Ты можешь добыть материалы, нажав ЛКМ на дерево или камень, но тебе нужен инструмент.",
        "Ты можешь передвигаться как на A, D, SPACE, так и на стрелочки влево и вправо.",
        "Тебе нужно починить лодку на краю острова, чтобы покинуть его."
    };

    void OnMouseDown()
    {
        if (textPanel == null || dialogueText == null || phrases == null || phrases.Length == 0)
            return;

        if (textPanel.activeSelf)
        {
            textPanel.SetActive(false);
            return;
        }

        int index = Random.Range(0, phrases.Length);
        dialogueText.text = phrases[index];
        textPanel.SetActive(true);
    }
}