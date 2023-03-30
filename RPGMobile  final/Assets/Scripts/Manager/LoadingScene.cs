using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] float waitToLoadTime;

    // Start is called before the first frame update
    void Start()
    {
       if(waitToLoadTime > 0)
        {
            MenuManager.menuInstance.FadeImage();
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        GameManager.gameManagerInstance.LoadData();
        QuestManager.instance.LoadQuestData();
    }
}
