using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float speed = 0.1f;
    [SerializeField] GameObject inputPanel;

     public Joystick joystick;

    public static Player playerInstance;
    public string transitionName;
    public Vector3 moveVector;
    public bool deactivateMovement;


    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;


    // Start is called before the first frame update
    void Start()
    {
        if (playerInstance != null && playerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            playerInstance = this;
        }

        joystick = FindObjectOfType<FloatingJoystick>();
       

        CheckIfLoading();

        moveVector = new Vector3(0, 0, 0);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement, verticalMovement;
        horizontalMovement = joystick.Horizontal;
        verticalMovement = joystick.Vertical;

        if(deactivateMovement)
        {
            playerRigidbody.velocity = Vector2.zero;//(0f, 0f)
        }
        else
        {
            playerRigidbody.velocity = new Vector2(horizontalMovement, verticalMovement) * speed;
            playerAnimator.SetFloat("movementX", joystick.Horizontal);
            playerAnimator.SetFloat("movementY", joystick.Vertical);
        }

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if(!deactivateMovement)
            {
                playerAnimator.SetFloat("lastX", joystick.Horizontal);
                playerAnimator.SetFloat("lastY", joystick.Vertical);
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftEdge.x, topRightEdge.x),
            Mathf.Clamp(transform.position.y, bottomLeftEdge.y, topRightEdge.y),
            Mathf.Clamp(transform.position.z, bottomLeftEdge.z, topRightEdge.z)
            );
    }
     
    public void SetLimit(Vector3 bottomEdgeToSet, Vector3 topEdgeToSet)
    {
        bottomLeftEdge = bottomEdgeToSet;
        topRightEdge = topEdgeToSet;
    }


    public void CheckIfLoading()
    {
        if (GameManager.gameManagerInstance.onLoading)
        {
            inputPanel.gameObject.SetActive(false);
        }
        else
        {
            inputPanel.gameObject.SetActive(true);
        }
    }

}//fine Player

        

        
