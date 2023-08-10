using System.Text;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LoginSystem : MonoBehaviour
{
	public InputField if_userName;

	public InputField if_passWord;

	public Text text_status;

	public InputField registerName;

	public InputField registerPassWord;

	public InputField reregisterPassWord;

	public GameObject registerUI;

	private void Start()
	{
		Event.registerOut("onConnectStatus", this, "onConnectStatus");
		Event.registerOut("onLoginFailed", this, "onLoginFailed");
		Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
		Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
		Event.registerOut("helloClient", this, "helloClient");
		Event.registerOut("helloClient2", this, "helloClient2");
		Event.registerOut("goToCreatePlayer", this, "goToCreatePlayer");
		Event.registerOut("goToHome", this, "goToHome");
		Event.registerOut("goToSpace", this, "goToSpace");
		registerUI.SetActive(false);
		autoLogin();
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void autoLogin()
	{
		string @string = PlayerPrefs.GetString("name");
		string string2 = PlayerPrefs.GetString("password");
		if (@string != "" && string2 != "")
		{
			text_status.text = "连接到服务端...";
			string text = string2;
			Event.fireIn("login", @string, text, Encoding.UTF8.GetBytes("lxqLogin"));
		}
	}

	public void goToHome()
	{
		SceneManager.UnloadScene("login");
		SceneManager.LoadSceneAsync("homeScene", (LoadSceneMode)1);
	}

	public void goToCreatePlayer()
	{
		SceneManager.UnloadScene("login");
		SceneManager.LoadSceneAsync("creatPlayer", (LoadSceneMode)1);
	}

	public void goToSpace()
	{
		World.instance.init();
		SceneManager.LoadScene("world");
		SceneManager.LoadSceneAsync("goToSpace", (LoadSceneMode)1);
	}

	public void onConnectStatus(bool beSuccess)
	{
		if (beSuccess)
		{
			MonoBehaviour.print((object)"连接成功，正在登陆");
			text_status.text = "连接成功，正在登陆";
		}
		else
		{
			text_status.text = "连接错误";
		}
	}

	public void onLoginFailed(ushort errorCode)
	{
		text_status.text = "登陆失败" + KBEngineApp.app.serverErr(errorCode);
	}

	public void onLoginSuccessfully(ulong uuuid, int id, Account account)
	{
		if (account != null)
		{
			text_status.text = "登录成功";
			PLAYER_INFO playerInfo = account.playerInfo;
			ulong dbid = playerInfo.dbid;
			string name = playerInfo.name;
			TDGAAccount tDGAAccount = TDGAAccount.SetAccount(string.Concat(dbid));
			tDGAAccount.SetLevel(playerInfo.level);
			tDGAAccount.SetAccountName(name);
			tDGAAccount.SetAccountType(AccountType.REGISTERED);
		}
	}

	public void onLogin()
	{
		MonoBehaviour.print((object)"connect to server...(连接到服务端...)");
		text_status.text = "连接到服务端...";
		Event.fireIn("login", if_userName.text, if_passWord.text, Encoding.UTF8.GetBytes("lxqLogin"));
		saveAccountInfo(if_userName.text, if_passWord.text);
	}

	public void saveAccountInfo(string name, string password)
	{
		PlayerPrefs.SetString("name", name);
		PlayerPrefs.SetString("password", password);
	}

	public void onRegister()
	{
		text_status.text = "连接到服务端...";
		if (reregisterPassWord.text.Length < 6)
		{
			text_status.text = "密码过短";
		}
		else if (registerName.text.Length < 6)
		{
			text_status.text = "账号过短";
		}
		else if (Regex.IsMatch(registerName.text, "[\\u4e00-\\u9fa5]"))
		{
			text_status.text = "暂不支持中文账号";
		}
		else if (registerPassWord.text == reregisterPassWord.text)
		{
			Event.fireIn("createAccount", registerName.text, registerPassWord.text, Encoding.UTF8.GetBytes("lxqRegister"));
		}
		else
		{
			text_status.text = "两次输入的密码不同";
		}
	}

	public void onHellohaha()
	{
		_ = (Account)KBEngineApp.app.player();
	}

	public void onHelloTest()
	{
		((testEntity)KBEngineApp.app.player())?.hello("你好");
	}

	public void helloClient(string msg)
	{
		text_status.text = msg;
	}

	public void helloClient2(string msg)
	{
		text_status.text = msg;
	}

	public void onCreateAccountResult(ushort retcode, byte[] datas)
	{
		if (retcode != 0)
		{
			MonoBehaviour.print((object)("(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode)));
			text_status.text = "(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode);
			return;
		}
		if_userName.text = registerName.text;
		if_passWord.text = registerPassWord.text;
		closeRegisterUI();
		text_status.text = "注册账号成功! 请点击登录进入游戏";
	}

	private void Update()
	{
	}

	public void onClose()
	{
		Application.Quit();
	}

	public void closeRegisterUI()
	{
		registerUI.SetActive(false);
	}

	public void opengisterUI()
	{
		registerUI.SetActive(true);
	}
}
