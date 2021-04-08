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

    private void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        icons = new Queue<GameObject>();
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
    }
}
