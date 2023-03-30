using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string newGameScene;
    [SerializeField] GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Player_Pos_X"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ExitButton()
    {
        Debug.Log("We have just exit");
    }
}
