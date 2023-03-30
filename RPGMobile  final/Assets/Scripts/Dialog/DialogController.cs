using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogText, nameText;
    [SerializeField] GameObject dialogBox, nameBox;
    [SerializeField] string[] dialogSentences;
    [SerializeField] int currentSentence;

    private bool dialogJustStarted;//tiene traccia del fatto che il dialogo è mai partito o è la prima volta

    public static DialogController dialogControllerInstance;

    private string questToMark;
    private bool markTheQuestComplete;
    private bool shouldMarkQuest;

    // Start is called before the first frame update
    void Start()
    {
        dialogControllerInstance = this;
        dialogText.text = dialogSentences[currentSentence];
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(!dialogJustStarted)
                {
                    currentSentence++;

                    if(currentSentence >= dialogSentences.Length)
                    {
                        dialogBox.SetActive(false);
                        GameManager.gameManagerInstance.dialogBoxOpened = false;

                        if(shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if(markTheQuestComplete)
                            {
                                QuestManager.instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.MarkQuestInComplete(questToMark);
                            }
                        }
                    }
                    else
                    {
                        CheckForName();
                        dialogText.text = dialogSentences[currentSentence];
                    }
                }
                else
                {
                    dialogJustStarted = false;
                }
            }
        }
    }

    public void ActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markTheQuestComplete = markComplete;
        shouldMarkQuest = true;
    }

    public void ActivateDialog(string[] newSentenceToUse)
    {
        dialogSentences = newSentenceToUse;
        currentSentence = 0;

        CheckForName();

        dialogText.text = dialogSentences[currentSentence];
        dialogBox.SetActive(true);
        dialogJustStarted = true;
        GameManager.gameManagerInstance.dialogBoxOpened = true;

    }


    void CheckForName()
    {
        if (dialogSentences[currentSentence].StartsWith("#"))
        {
            nameText.text = dialogSentences[currentSentence].Replace("#", "");
            currentSentence++;
        }
    }

    //La seguente funzione, IsDialogBoxActive, è stata creata perchè dialogBox è serializefield e quindi inaccessibile
    //al posto di renderla public si preferisce usare questa funzione perchè è più "safe"
    public bool IsDialogBoxActive()
    {
        return dialogBox.activeInHierarchy;
    }
}
