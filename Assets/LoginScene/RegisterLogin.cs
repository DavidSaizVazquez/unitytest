using System;
using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;


namespace LoginUI
{
    public class RegisterLogin : MonoBehaviour
    {
        public Canvas registerWindow;
        public Text username;
        public InputField password;
        public Text email;
        public Text age;
        
        
        public Text error;
        public Canvas errorCanvas;
        public Animator errorAnimator;

        public Toggle spam;
        public Button registerButton;
        public InputField ageInputField;


        public bool fillError = false;
        public bool userError = false;

        private ServerConnection serverConnection;

        // Start is called before the first frame update
        void Start()
        {
            errorAnimator = errorCanvas.GetComponent<Animator>();
            GameObject loginWindow = GameObject.Find("LoginWindow");

            serverConnection = ServerConnection.getInstance();
            registerWindow.enabled = false;
            registerButton.onClick.AddListener(RegisterButton_Click);

        }

       

        //Handles a registration and sends information to server
        //Displays error if fields not full
        public void RegisterButton_Click()
        {
            Debug.Log("Register!!");


            if (String.IsNullOrEmpty(username.text) || String.IsNullOrEmpty(password.text) || String.IsNullOrEmpty(email.text))
            {

                errorAnimator.SetBool("open", true);
                error.text = "Enter all fields!";
                Debug.Log("Enter all fields!");
                fillError = true;
            }
            else
            {
                if (fillError) errorAnimator.SetBool("open", false);

                int spamint = spam.isOn ? 1 : 0;
                //send register petition
                serverConnection.SendMessage("2/" + username.text + "," + password.text + "," + age.text + "," + email.text + "," + spamint);
                
            }
        }
        }


    }  

