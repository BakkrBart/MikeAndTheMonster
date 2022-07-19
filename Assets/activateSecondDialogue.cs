using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateSecondDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;

    private void OnEnable()
    {
        trigger.TriggerDialogue();
    }
}
