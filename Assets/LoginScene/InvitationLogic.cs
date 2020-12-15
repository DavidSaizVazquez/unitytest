using System.Collections;
using System.Collections.Generic;
using LoginUI;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;

public class InvitationLogic : MonoBehaviour
{

    public Canvas invitationCanvas;
    public Button joinBtn;
    public Button declineBtn;

    public Text message;
    public Animator animator;

    public int gameNumber;

    private ServerConnection serverConnection;

    private GameCreationLogic gameCreationLogic;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameCreationWindow = GameObject.Find("GameCreationWindow");
        gameCreationLogic = gameCreationWindow.GetComponent<GameCreationLogic>();
        serverConnection = ServerConnection.getInstance();
        invitationCanvas.enabled = false;
        joinBtn.onClick.AddListener(joinClick);
        declineBtn.onClick.AddListener(declineClick);

        animator = invitationCanvas.GetComponent<Animator>();
    }


    private void joinClick()
    {
        serverConnection.SendMessage("10/" + gameNumber);
        gameCreationLogic.gameCreationCanvas.enabled = true;
        animator.SetBool("open", false);
    }

    private void declineClick()
    {
        serverConnection.SendMessage("10/-1");
        animator.SetBool("open", false);
    }
  
}
