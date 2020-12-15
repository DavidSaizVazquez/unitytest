using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;
using System.Linq;
using UnityEngine.EventSystems;

namespace LoginUI
{

    public class InformationLogic : MonoBehaviour
    {
        public Text EmailText;
        public Text AgeText;
        public Button loadEmail;
        public Button loadAge;
        public Toggle spam;
        public Button modifySpam;
        public Canvas informationCanvas;
        public Button mainMenuBtn;


        public GameObject textTemplate;

        public bool canvasError = false;


        private ServerConnection serverConnection;
        GameObject LoginWindow;
        LoginLogic loginLogic;
        private MainMenuLogic mainMenuLogic;

        // Start is called before the first frame update
        void Start()
        {
            GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
            mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
            LoginWindow = GameObject.Find("LoginWindow");
            loginLogic = LoginWindow.GetComponent<LoginLogic>();
            serverConnection = ServerConnection.getInstance();
        
            informationCanvas.enabled = false;

            loadEmail.onClick.AddListener(EmailLoadOnClick);
            loadAge.onClick.AddListener(AgeLoadOnClick);
            modifySpam.onClick.AddListener(SpamModifyOnClick);
            mainMenuBtn.onClick.AddListener(MainMenuClick);
 

        

        }

        private void MainMenuClick()
        {
            informationCanvas.enabled = false;
            mainMenuLogic.mainMenuCanvas.enabled = true;

        }

        public void EmailLoadOnClick()
        {
            serverConnection.SendMessage("4/" + loginLogic.username.text);
        }

        public void AgeLoadOnClick()
        {
            serverConnection.SendMessage("3/" + loginLogic.username.text);
        }

        public void SpamModifyOnClick()
        {
            serverConnection.SendMessage("5/" + loginLogic.username.text + "," + (spam.isOn?1:0));
        }

        public void connectedUsersNotificationListen(string serverResponse)
        {
                List<string> names = serverResponse.Replace("7/", "").Split(',').ToList<string>();
                Transform[] allChildren = textTemplate.transform.parent.GetComponentsInChildren<Transform>();

                foreach (Transform child in allChildren)
                {
                    if (!(child.gameObject.name == "Text" || child.gameObject.name == "Content")) Destroy(child.gameObject);
                }

                foreach (string name in names)
                {
                    if (name != "")
                    {
                        GameObject newText = Instantiate(textTemplate) as GameObject;

                        addEventTriggers(newText);

                        newText.name="connectedUser";

                        newText.SetActive(true);
                        newText.GetComponent<Text>().text = name;
                        newText.transform.SetParent(textTemplate.transform.parent, false);
                    }
                }
        }


        public void addEventTriggers(GameObject newText){

            newText.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = newText.GetComponent<EventTrigger>();
            EventTrigger.Entry entry;

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener( (eventData) => { onClick(newText);});
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener( (eventData) => { makeGrey(newText);});
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener( (eventData) => { makeBlack(newText);});
            trigger.triggers.Add(entry);

        }

        public void onClick(GameObject gameObject){

            if(gameObject.name!="selected"){
            unSelect();
            gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
            gameObject.GetComponent<Text>().fontSize = 17;
            gameObject.name="selected";
            } else {
                unSelect();
            }

        }

        public void unSelect(){
            Transform selected = textTemplate.transform.parent.Find("selected");

            if (selected!=null) 
            {
                selected.gameObject.GetComponent<Text>().fontSize = 14;
                selected.gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                selected.gameObject.name="connectedUser";
            };
            

        }

        public void makeGrey(GameObject go){
            go.GetComponent<Text>().color = Color.grey;
        }

        public void makeBlack(GameObject go){
            go.GetComponent<Text>().color = Color.black;
        }
     
     
}
}
