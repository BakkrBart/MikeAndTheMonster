using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class startCutscene : MonoBehaviour
{
    public DialogueController diaController;
    public PlayableDirector director;
    public PlayableDirector director2;
    public PlayableDirector director3;
    public PlayableDirector director4;
    private bool dialogue1Checked = false;
    private bool dialogue2Checked = false;
    private bool dialogue3Checked = false;
    private bool dialogue4Checked = false;

    void Update()
    {
        if (diaController.dialogueDone1 && !dialogue1Checked)
        {
            director.Play();
            dialogue1Checked = true;
        }
        if (diaController.dialogueDone2 && !dialogue2Checked)
        {
            director2.Play();
            dialogue2Checked = true;
        }
        if (diaController.dialogueDone3 && !dialogue3Checked)
        {
            director3.Play();
            dialogue3Checked = true;
        }
        if (diaController.dialogueDone4 && !dialogue4Checked)
        {
            director4.Play();
            dialogue4Checked = true;
        }

    }
}
