using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LoginUI;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;
using UnityEngine.EventSystems;

public class JoinAGameLogic : MonoBehaviour
{
    

    public Canvas mainCanvas;
    public GameObject textTemplate;
    public Button joinButton;
    public Button mainMenuButton;

    private MainMenuLogic mainMenuLogic;
    
    private ServerConnection serverConnection;
    void Start()
    {
        GameObject mainMenuWindow = GameObject.Find("MainMenuWindow");
        mainMenuLogic = mainMenuWindow.GetComponent<MainMenuLogic>();
        serverConnection = ServerConnection.getInstance();
        joinButton.onClick.AddListener(JoinButtonClick);
        mainMenuButton.onClick.AddListener(MainMenuButtonClick);
    }

    private void MainMenuButtonClick()
    {
        mainCanvas.enabled = false;
        mainMenuLogic.mainMenuCanvas.enabled = true;
    }
    private void JoinButtonClick()

    {
        Debug.Log("join button");
        Transform selected = textTemplate.transform.parent.Find("selected");

        if (selected != null)
        {
            string game = selected.gameObject.GetComponent<Text>().text;
            serverConnection.SendMessage("14/"+game);
        }
           
    }
    public void gamesNotificationUpdate(string serverResponse)
        {
            List<string> names = serverResponse.Replace("13/", "").Split(',').ToList<string>();
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

                    newText.name = "connectedUser";

                    newText.SetActive(true);
                    newText.GetComponent<Text>().text = name;
                    newText.transform.SetParent(textTemplate.transform.parent, false);
                }
            }
        }


        public void addEventTriggers(GameObject newText)
        {

            newText.AddComponent(typeof(EventTrigger));
            EventTrigger trigger = newText.GetComponent<EventTrigger>();
            EventTrigger.Entry entry;

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { onClick(newText); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { makeGrey(newText); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((eventData) => { makeBlack(newText); });
            trigger.triggers.Add(entry);

        }

        public void onClick(GameObject gameObject)
        {

            if (gameObject.name != "selected")
            {
                unSelect();
                gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
                gameObject.GetComponent<Text>().fontSize = 17;
                gameObject.name = "selected";

                joinButton.enabled = true;
            }
            else
            {
                unSelect();

                joinButton.enabled = false;
            }

        }

        public void unSelect()
        {
            Transform selected = textTemplate.transform.parent.Find("selected");

            if (selected != null)
            {
                selected.gameObject.GetComponent<Text>().fontSize = 14;
                selected.gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
                selected.gameObject.name = "connectedUser";
            };


        }

        public void makeGrey(GameObject go)
        {
            go.GetComponent<Text>().color = Color.grey;
        }

        public void makeBlack(GameObject go)
        {
            go.GetComponent<Text>().color = Color.black;
        }


}
