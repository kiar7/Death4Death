using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnter : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
         if (transitionName == Player.playerInstance.transitionName)//put the player in the area enter position
        {
            Player.playerInstance.transform.position = transform.position;
        }
    }
}
