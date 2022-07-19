using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<GameObject> icons;
    private GameObject currIcon;

    private bool doneOnce = false;
    public bool dialogueDone1 = false;
    public bool dialogueDone2 = false;
    public bool dialogueDone3 = false;
    public bool dialogueDone4 = false;

    private void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        icons = new Queue<GameObject>();
    }

    private void Update()
    {
         Debug.Log(sentences.Count);
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        sentences.Clear();
        names.Clear();
        icons.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach (GameObject icon in dialogue.icons)
        {
            icons.Enqueue(icon);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
           return;
        }
        if (doneOnce)
        {
            currIcon.SetActive(false);
        }

        GameObject icon = icons.Dequeue();
        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        StopAllCoroutines();
        nameText.text = name;
        icon.SetActive(true);
        currIcon = icon;
        StartCoroutine(TypeSentence(sentence));
        doneOnce = true;
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach  (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        if (dialogueDone1 == false)
        {
            dialogueDone1 = true;
            return;
        }
        if (dialogueDone2 == false)
        {
            dialogueDone2 = true;
            return;
        }
        if (dialogueDone3 == false)
        {
            dialogueDone3 = true;
            return;
        }
        if (dialogueDone4 == false)
        {
            dialogueDone4 = true;
            return;
        }
    }
}
