using UnityEngine;
using UnityEngine.UI;
using NoGameFoundClient;


namespace LoginUI
{

public class LoginLogic : MonoBehaviour
{


	public Canvas loginCanvas;
	public Text username;
	public InputField password;
	
	public Text error;
	public Canvas errorCanvas;
	public Animator errorAnimator;


	public Button loginButton;
	public Button registerButton;
	private ServerConnection serverConnection;	


	public bool credentialsError = false;


	private RegisterLogin registerLogin;

	// Start is called before the first frame update
	void Start()
	{

		serverConnection = ServerConnection.getInstance();


		errorAnimator = errorCanvas.GetComponent<Animator>();
	
		loginButton.enabled = false;
		registerButton.enabled = false;
		errorCanvas.enabled = false;

		GameObject registerWindow = GameObject.Find("RegisterWindow");
		registerLogin = registerWindow.GetComponent<RegisterLogin>();

		loginButton.onClick.AddListener(LoginButtonClick);
		registerButton.onClick.AddListener(RegisterButtonClick);

		}

	public void LoginButtonClick() {
		//send login petition
		serverConnection.SendMessage("1/" + username.text + "," + password.text);

	}

	public void RegisterButtonClick()
	{
		registerLogin.registerWindow.enabled = true;
		loginCanvas.enabled = false;
	}
}

}
