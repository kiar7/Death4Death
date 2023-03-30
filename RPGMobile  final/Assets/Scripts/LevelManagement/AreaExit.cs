using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string transitionName;
    [SerializeField] AreaEnter areaEnter;

    //Start is called before the first frame update
    private void Start()
    {
        areaEnter.transitionName = transitionName;
    }

    //if collide with the player change the scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player.playerInstance.transitionName = transitionName;
            MenuManager.menuInstance.FadeImage();
            StartCoroutine(LoadSceneCoroutine());
        }
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
