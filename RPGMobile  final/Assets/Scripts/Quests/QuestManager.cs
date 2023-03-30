using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questsName;
    [SerializeField] bool[] questMarkersCompleted;

    public static QuestManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkersCompleted = new bool[questsName.Length];
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.S))
        //{
        //    Debug.Log("saved");
        //    SaveQuestData();
        //}

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    Debug.Log("loaded");
        //    LoadQuestData();
        //}
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questsName.Length; i++)
        {
            if (questsName[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogWarning("Quest: " + questToFind + "does not exist");
        return 0;//always leave the first quest blank so if it doesn't find questToFind it will return a null string
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);

        if(questNumberToCheck != 0)//bacause in zero we don't have any return
        {
            return questMarkersCompleted[questNumberToCheck];
        }
        return false;
    }

    public void UpdateQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsOfType<QuestObject>();

        if(questObjects.Length > 0)
        {
            foreach(QuestObject questObject in questObjects)
            {
                questObject.CheckForCompletion();
            }
        }
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = true;
        UpdateQuestObjects();
    }

    public void MarkQuestInComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = false;
        UpdateQuestObjects();
    }

    public void SaveQuestData()
    {
        for(int i = 0; i < questsName.Length; i++)
        {
            if (questMarkersCompleted[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questsName[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questsName[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questsName.Length; i++)
        {
            int valueToSet = 0;
            string keyToUse = "QuestMarker_" + questsName[i];

            if (PlayerPrefs.HasKey(keyToUse))
            {
                valueToSet = PlayerPrefs.GetInt(keyToUse);
            }

            if(valueToSet == 0)
            {
                questMarkersCompleted[i] = false;
            }
            else
            {
                questMarkersCompleted[i] = true;

            }
        }
    }
}
