using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueZone : MonoBehaviour
{

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private bool didDialogueStart;
    private int lineIndex;//Que linea de dialogo se muestra
    private float typingTime = 0.05f;
    private bool alreadyTriggered = false;
    void Awake()
    {
        dialoguePanel.SetActive(false);
    }
    void Update()
    {
        // Si el diálogo ya empezó y estamos mostrando alguna línea, detecta la tecla Q
        if (didDialogueStart && Input.GetKeyDown(KeyCode.Q))
        {
            if (dialogueText.text == dialogueLines[lineIndex])
            {
                // Si ya está completa la línea, pasamos a la siguiente
                NextDialogueLine();
            }
            else
            {
                // Si no está completa, mostramos la línea completa
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyTriggered)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
                alreadyTriggered = true;
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char car in dialogueLines[lineIndex])
        {
            dialogueText.text += car;//Concatenar caracteres;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
}
