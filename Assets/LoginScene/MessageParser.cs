using LoginUI;
using System.Threading.Tasks;
using UnityEngine;
using NoGameFoundClient;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MessageParser : MonoBehaviour
{

    public Animator infoLogAnimator;
    public Text infoLogText;

    private ServerConnection serverConnection;

    private LoginLogic loginLogic;
    private InformationLogic informationLogic;
    private RegisterLogin registerLogin;
    private MainMenuLogic mainMenuLogic;
    private GameCreationLogic gameCreationLogic;
    private InvitationLogic invitationLogic;
    private JoinAGameLogic joinAGameLogic;




    bool invitationError = false;

    // Start is called before the first frame update
    void Start()
    {

        GameObject infoLogObject = GameObject.Find("InfoLog");
        infoLogObject.GetComponent<Canvas>().enabled = false;
        infoLogAnimator = infoLogObject.GetComponent<Animator>();
        

        GameObject loginWindow = GameObject.Find("LoginWindow");
        loginLogic = loginWindow.GetComponent<LoginLogic>();
        GameObject informationWindow = GameObject.Find("InformationWindow");
        informationLogic = informationWindow.GetComponent<InformationLogic>();
        GameObject registerWindow = GameObject.Find("RegisterWindow");
        registerLogin = registerWindow.GetComponent<RegisterLogin>();
        GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
        mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
        GameObject gameCreationWindow = GameObject.Find("GameCreationWindow");
        gameCreationLogic = gameCreationWindow.GetComponent<GameCreationLogic>();
        GameObject invitationWindow = GameObject.Find("InvitationWindow");
        invitationLogic= invitationWindow.GetComponent<InvitationLogic>();
        GameObject joinAGameWindow = GameObject.Find("JoinAGameWindow");
        joinAGameLogic= joinAGameWindow.GetComponent<JoinAGameLogic>();
        connectToServer();
    }

    async private void listenForServer()
    {
        bool run = true;
        while (run)
        {
            string serverResponse = await Task.Run(() => serverConnection.ListenForMessage());


            string[] commands = serverResponse.Split('~');

            int i = 0;
            while (commands[i] != "")
            {
                int prefix = int.Parse(commands[i].Split('/')[0]);
               Debug.Log(serverResponse);
                switch (prefix)
                {
                    case 0:
                        break;
                    case 1:
                        if (commands[i] == "1/0")
                        {
                            globalData.userName = loginLogic.username.text;
                            if (loginLogic.credentialsError)
                            {
                                loginLogic.errorAnimator.SetBool("open", false);
                            }

                            mainMenuLogic.mainMenuCanvas.enabled = true;
                            loginLogic.loginCanvas.enabled = false;
                        }
                        else
                        {
                            loginLogic.credentialsError = true;
                            loginLogic.errorAnimator.SetBool("open", true);
                            loginLogic.error.text = "Incorrect user or password";
                        }
                        break;
                    case 2:
                        if (commands[i].Equals("2/0"))
                        {
                            registerLogin.registerWindow.enabled = false;
                            loginLogic.loginCanvas.enabled = true;
                            if (registerLogin.userError) loginLogic.errorAnimator.SetBool("open", false);

                        }

                        else
                        {
                            loginLogic.errorAnimator.SetBool("open", true);

                            loginLogic.error.text = "Register Error: User already exists";
                            Debug.Log("Register Error: User already exists");
                            registerLogin.userError = true;
                        }
                        break;
                    case 3:
                        informationLogic.AgeText.text = commands[i].Replace("3/", "");
                        break;
                    case 4:
                        informationLogic.EmailText.text = commands[i].Replace("4/", "");
                        break;
                    case 5:
                        if (commands[i] == "5/0")
                        {
                            Debug.Log("Change spam successful!");
                            if (informationLogic.canvasError) loginLogic.errorAnimator.SetBool("open", false);
                        }
                        else
                        {
                            loginLogic.errorAnimator.SetBool("open", true);

                            loginLogic.error.text = "Change spam error";
                            Debug.Log("Change spam error");

                        }
                        break;
                    case 6:
                        informationLogic.spam.isOn = commands[i].Equals("6/1");
                        break;
                    case 7:
                        informationLogic.connectedUsersNotificationListen(commands[i]);
                        gameCreationLogic.connectedUsersNotificationListen(commands[i]);
                        break;
                    case 8:
                        gameCreationLogic.gameNumber = int.Parse(commands[i].Split('/')[1]);
                        break;
                    case 9:
                        if(commands[i].Equals("9/0"))
                        {
                            if(invitationError)
                            {
                                loginLogic.errorAnimator.SetBool("open",false);
                            }
                            infoLogAnimator.SetBool("open", true);
                            infoLogText.text = "Invitation sent!";
                            this.closeInfoLog();
                        }
                        else
                        {
                            loginLogic.errorAnimator.SetBool("open", true);
                            loginLogic.error.text = "Error sending invitation";
                            invitationError = true;
                        }
                        
                        break;
                    case 10:
                        
                        string user = commands[i].Split('/')[1].Split(',')[0];
                        int gameNumber = int.Parse(commands[i].Split('/')[1].Split(',')[1]);
                        invitationLogic.gameNumber = gameNumber;
                        gameCreationLogic.gameNumber = gameNumber;
                        Debug.Log("gamenumber: ");
                        Debug.Log(invitationLogic.gameNumber);
                        
                        invitationLogic.message.text = user + " has invited you to a game!";
                        invitationLogic.animator.SetBool("open", !invitationLogic.animator.GetBool("open"));
						
                        break;
                    case 11:
                        gameCreationLogic.gamePlayersNotificationListen(commands[i]);
                        globalData.gameUserList = commands[i].Replace("11/", "").Split(',');
                        break;
                    
                    case 12:
                        if (invitationLogic.gameNumber != -1)
                        {
                            serverConnection.SendMessage("13/");
                            int level = int.Parse(commands[i].Split(',')[1]);
                            switch (level)
                             {
                                case 1:
                                    SceneManager.LoadScene("GameScenePython", LoadSceneMode.Single);
                                    run = false;
                                    break;
                                case 2:
                                    SceneManager.LoadScene("GameSceneMatlab", LoadSceneMode.Single);
                                    run = false;
                                    break;

                            }
                         
                        }
                        break;
                    
                    case 13:
                        joinAGameLogic.gamesNotificationUpdate(commands[i]);
                        break;
                    
                    case 14:
                        if (commands[i].Equals("14/0"))
                        {
                            gameCreationLogic.gameCreationCanvas.enabled = true;
                            joinAGameLogic.mainCanvas.enabled = false;
                        }
                        else
                        {
                            Debug.Log("Error entering the game");
                        }
                        break;
                    
                    //confirmation of exiting a game
                    case 15:
                        if (commands[i].Equals("15/0"))
                        {
                            infoLogAnimator.SetBool("open", true);
                            infoLogText.text = "Exited game!";
                        }
                        else
                        {
                            loginLogic.errorAnimator.SetBool("open", true);

                            Debug.Log("Game exit Error");
                            loginLogic.error.text = "Game exit error Error";
                        }
                        
                        break;
                }
                
                



                i++;
            }
        }
    }

    private async Task closeInfoLog()
    {
        await Task.Delay(5000);
        infoLogAnimator.SetBool("open", false);
    }

    async private void connectToServer()
    {
        serverConnection = ServerConnection.getInstance();
        if (!serverConnection.isConnected())
        {
            serverConnection.setConnected(true);

            int connectionSuccess = await Task.Run(() =>
            {
                return serverConnection.ConnectToServer();
            });


            if (connectionSuccess == 0)
            {
                Debug.Log("Connection with server successfull!");
                loginLogic.loginButton.enabled = true;
                loginLogic.registerButton.enabled = true;
                //Listen forever
                listenForServer();
            }

            else if (connectionSuccess == -1)
            {

                loginLogic.errorAnimator.SetBool("open", true);

                Debug.Log("Connection Error");
                loginLogic.error.text = "Connection Error";
            }

        }
    }
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        serverConnection.SendMessage("0/");
    }

    //executed when window is closed
    void OnDestroy()
    {
        Debug.Log("Destroyed...");
    }

}
