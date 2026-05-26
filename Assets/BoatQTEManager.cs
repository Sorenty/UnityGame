using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoatQTEManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject qteRoot;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button[] buttons;

    [Header("QTE Settings")]
    [SerializeField] private int stepsToWin = 4;
    [SerializeField] private float stepTimeLimit = 1.25f;

    [Header("Text")]
    [SerializeField] private string actionName = "Repair boat";

    [Header("Button Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;

    [Header("Random Position")]
    [SerializeField] private float offsetRange = 100f;

    public UnityEvent onSuccess;
    public UnityEvent onFail;

    private Image[] buttonImages;
    private RectTransform[] buttonRects;
    private Vector2[] basePositions;

    private int currentStep = 0;
    private int targetIndex = -1;
    private float timer = 0f;
    private bool running = false;

    void Awake()
    {
        if (buttons == null || buttons.Length == 0)
            return;

        buttonImages = new Image[buttons.Length];
        buttonRects = new RectTransform[buttons.Length];
        basePositions = new Vector2[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int capturedIndex = i;

            buttonImages[i] = buttons[i].GetComponent<Image>();
            buttonRects[i] = buttons[i].GetComponent<RectTransform>();
            basePositions[i] = buttonRects[i].anchoredPosition;

            buttons[i].onClick.AddListener(() => OnButtonPressed(capturedIndex));
            buttons[i].gameObject.SetActive(false);
        }

        HideQTE();
    }

    void Update()
    {
        if (!running) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            FailQTE();
        }
    }

    public void SetActionName(string newActionName)
    {
        actionName = newActionName;
        UpdateInstructionUI();
    }

    public void BeginQTE()
    {
        if (running || qteRoot == null || buttons == null || buttons.Length == 0)
            return;

        running = true;
        currentStep = 0;
        qteRoot.SetActive(true);

        PickNextTarget();
        UpdateInstructionUI();
        UpdateTimerUI();
    }

    private void PickNextTarget()
    {
        targetIndex = Random.Range(0, buttons.Length);
        timer = stepTimeLimit;

        for (int i = 0; i < buttons.Length; i++)
        {
            bool isTarget = (i == targetIndex);

            buttons[i].gameObject.SetActive(isTarget);

            if (buttonImages[i] != null)
                buttonImages[i].color = isTarget ? highlightColor : normalColor;

            if (buttonRects[i] != null)
            {
                if (isTarget)
                {
                    Vector2 randomOffset = new Vector2(
                        Random.Range(-offsetRange, offsetRange),
                        Random.Range(-offsetRange, offsetRange)
                    );

                    buttonRects[i].anchoredPosition = basePositions[i] + randomOffset;
                }
                else
                {
                    buttonRects[i].anchoredPosition = basePositions[i];
                }
            }
        }
    }

    private void OnButtonPressed(int index)
    {
        if (!running) return;

        if (index != targetIndex)
        {
            FailQTE();
            return;
        }

        currentStep++;

        if (currentStep >= stepsToWin)
        {
            CompleteQTE();
        }
        else
        {
            PickNextTarget();
            UpdateInstructionUI();
        }
    }

    private void UpdateInstructionUI()
    {
        if (instructionText != null)
            instructionText.text = $"{actionName} {currentStep + 1}/{stepsToWin}";
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.CeilToInt(timer)}";
    }

    private void CompleteQTE()
    {
        running = false;
        HideQTE();
        onSuccess?.Invoke();
    }

    private void FailQTE()
    {
        running = false;
        HideQTE();
        onFail?.Invoke();
    }

    private void HideQTE()
    {
        if (qteRoot != null)
            qteRoot.SetActive(false);

        ResetButtonColors();
    }

    private void ResetButtonColors()
    {
        if (buttonImages == null) return;

        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttons[i] != null)
                buttons[i].gameObject.SetActive(false);

            if (buttonImages[i] != null)
                buttonImages[i].color = normalColor;

            if (buttonRects[i] != null)
                buttonRects[i].anchoredPosition = basePositions[i];
        }
    }
}