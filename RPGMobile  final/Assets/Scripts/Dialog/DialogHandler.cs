using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    [SerializeField] bool shouldActivateQuest;
    [SerializeField] string questToMark;
    [SerializeField] bool markAsComplete;

    public string[] senteces;

    private bool canActiveBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActiveBox && Input.GetMouseButtonUp(0) && !DialogController.dialogControllerInstance.IsDialogBoxActive())
        {
            DialogController.dialogControllerInstance.ActivateDialog(senteces);

            if(shouldActivateQuest)
            {
                DialogController.dialogControllerInstance.ActivateQuestAtEnd(questToMark, markAsComplete);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canActiveBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canActiveBox = false;
        }
    }
}
