using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;

namespace LoginUI
{
    public class MainMenuLogic : MonoBehaviour
    {

        public Button createGameBtn;
        public Button joinGameBtn;
        public Button userInfoBtn;

        public Canvas mainMenuCanvas;

        private InformationLogic informationLogic;
        private LoginLogic loginLogic;
        private GameCreationLogic gameCreationLogic;
        private JoinAGameLogic joinAGameLogic;

        private ServerConnection serverConnection;


        //Debugging invitation
        private InvitationLogic invitationLogic;


        // Start is called before the first frame update
        void Start()
        {
            
            mainMenuCanvas.enabled = false;
            GameObject informationWindow = GameObject.Find("InformationWindow");
            informationLogic = informationWindow.GetComponent<InformationLogic>();
            GameObject loginWindow = GameObject.Find("LoginWindow");
            loginLogic = loginWindow.GetComponent<LoginLogic>();
            GameObject gameCreationWindow = GameObject.Find("GameCreationWindow");
            gameCreationLogic = gameCreationWindow.GetComponent<GameCreationLogic>();

            GameObject invitationWindow = GameObject.Find("InvitationWindow");
            invitationLogic = invitationWindow.GetComponent<InvitationLogic>();
            GameObject joinAGameWindow = GameObject.Find("JoinAGameWindow");
            joinAGameLogic= joinAGameWindow.GetComponent<JoinAGameLogic>();

            serverConnection = ServerConnection.getInstance();
            createGameBtn.onClick.AddListener(CreateGameClick);
            joinGameBtn.onClick.AddListener(JoinGameClick);
            userInfoBtn.onClick.AddListener(UserInfoClick);
            
           gameCreationLogic.playButtonCanvas.enabled = false;
           joinAGameLogic.mainCanvas.enabled = false;


        }

        private void CreateGameClick()
        {
            serverConnection.SendMessage("8/0");
            
            gameCreationLogic.playButtonCanvas.enabled = true; 
            gameCreationLogic.gameCreationCanvas.enabled = true;
            mainMenuCanvas.enabled = false;


        }

        private void JoinGameClick()
        {
            joinAGameLogic.mainCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
            
        }

        private void UserInfoClick()
        {
            serverConnection.SendMessage("6/" + loginLogic.username.text);
            informationLogic.informationCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
        }

    }
}
