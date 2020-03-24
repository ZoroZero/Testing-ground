using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void triggerDialog()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
}
