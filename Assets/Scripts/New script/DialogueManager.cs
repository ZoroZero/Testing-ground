using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Dialog text and entity name ui
    public GameObject dialogMenu;
    public Text dialogText;
    public Text entityName;

    // Varable to check if in dialogue
    public bool isTalking = false;

    // Instance of dialog manager
    public static DialogueManager instance = null;

    // Dialog
    private Queue<string> sentences;

    // Awake function
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        sentences = new Queue<string>();
        dialogMenu = GameObject.Find("Dialogue");
        dialogText = GameObject.Find("DialogueText").GetComponent<Text>();
        entityName = GameObject.Find("EntityName").GetComponent<Text>();
        dialogMenu.SetActive(false);
    }



    private void Start()
    {
        
    }

    // Display dialog
    public void startDialogue(Dialogue dialogue)
    {
        isTalking = true;
        dialogMenu.SetActive(true);

        entityName.text = dialogue.name + " :";

        // Clear sentences
        sentences.Clear();

        // enqueue all sentences in dialogue
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();
    }

    // write next sentece when we hit continue
    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            endDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
    }


    // Make letter swoop in
    IEnumerator typeSentence(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence)
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    // End the dialog
    public void endDialog()
    {
        dialogMenu.SetActive(false);
        isTalking = false;
    }

    
}
