using System.Collections;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private AudioClip npcVoice;
    private bool didDialogueStart;
    private int lineIndex;//Que linea de dialogo se muestra
    [SerializeField] private float typingTime;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = npcVoice;
    }
    void Awake()
    {
        dialogueMark.SetActive(false);
        dialoguePanel.SetActive(false);
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<DialogueDetector>().currentNPC != this)
        {
            return; // 🚫 No soy el NPC más cercano, ignoro el diálogo
        }
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {//Adelantar linea
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }
    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
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
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char car in dialogueLines[lineIndex])
        {
            dialogueText.text += car;//Concatenar caracteres;
            audioSource.Play();
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            // 🔁 Por si te alejas en medio del diálogo (opcional)
            if (didDialogueStart)
            {
                dialoguePanel.SetActive(false);
                Time.timeScale = 1f;
                didDialogueStart = false;
            }
        }
    }
}
