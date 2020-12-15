using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NoGameFoundClient;
using UnityEditor;

public class Game_MessageParser : MonoBehaviour
{
    public GameObject npcPrefab;
    public Vector2 position;
    
    Dictionary<string, GameObject> npcs = new Dictionary<string, GameObject>();
    
    ServerConnection serverConnection = ServerConnection.getInstance();
    
    // Start is called before the first frame update
    //Starts all npc players
    void OnLevelWasLoaded()
    {
        Debug.Log("Game_MessgeParser Loaded!");

        foreach (var user in globalData.gameUserList)
        {
            if (!user.Equals(globalData.userName)&&!user.Equals(""))
            {
                npcs.Add(user, Instantiate(npcPrefab));
                npcs[user].transform.position = position;
                npcs[user].GetComponent<npc_online>().setName(user);
            }
        }
        listenForServer();
    }

    //on message recieved from server, process it and executes command
    async private void listenForServer()
    {
        while (true)
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
                    //Other player's position update
                    case 20:
                        string[] names = commands[i].Replace("20/", "").Split('/');
                        npcs[names[0]].GetComponent<npc_online>().updateCharacter(float.Parse(names[1]),
                            float.Parse(names[2]),
                            int.Parse(names[3]),
                            float.Parse(names[4]));
                        break;
                   
                    case 21:
                      
                        break;
                    case 22:
                        break;
                    case 23:
                        break;
                       
                    case 24:
                       
                        break;
                }
                i++;
            }
        }

    }
    //executed when application is quited
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        serverConnection.SendMessage("0/");
    }

    //executed when scene is destroyed
    void OnDestroy()
    {
        Debug.Log("Destroyed...");
    }
}
