using System;
using System.Text;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020005C0 RID: 1472
public class UI_LoginSystem : MonoBehaviour
{
	// Token: 0x06002531 RID: 9521 RVA: 0x0012A780 File Offset: 0x00128980
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
		this.registerUI.SetActive(false);
		this.autoLogin();
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x0012A838 File Offset: 0x00128A38
	public void autoLogin()
	{
		string @string = PlayerPrefs.GetString("name");
		string string2 = PlayerPrefs.GetString("password");
		if (@string != "" && string2 != "")
		{
			this.text_status.text = "连接到服务端...";
			string text = string2;
			Event.fireIn("login", new object[]
			{
				@string,
				text,
				Encoding.UTF8.GetBytes("lxqLogin")
			});
		}
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x0001DD50 File Offset: 0x0001BF50
	public void goToHome()
	{
		SceneManager.UnloadScene("login");
		SceneManager.LoadSceneAsync("homeScene", 1);
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x0001DD69 File Offset: 0x0001BF69
	public void goToCreatePlayer()
	{
		SceneManager.UnloadScene("login");
		SceneManager.LoadSceneAsync("creatPlayer", 1);
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x0001DD82 File Offset: 0x0001BF82
	public void goToSpace()
	{
		World.instance.init();
		SceneManager.LoadScene("world");
		SceneManager.LoadSceneAsync("goToSpace", 1);
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x0001DDA4 File Offset: 0x0001BFA4
	public void onConnectStatus(bool beSuccess)
	{
		if (beSuccess)
		{
			MonoBehaviour.print("连接成功，正在登陆");
			this.text_status.text = "连接成功，正在登陆";
			return;
		}
		this.text_status.text = "连接错误";
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x0001DDD4 File Offset: 0x0001BFD4
	public void onLoginFailed(ushort errorCode)
	{
		this.text_status.text = "登陆失败" + KBEngineApp.app.serverErr(errorCode);
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x0012A8B4 File Offset: 0x00128AB4
	public void onLoginSuccessfully(ulong uuuid, int id, Account account)
	{
		if (account != null)
		{
			this.text_status.text = "登录成功";
			PLAYER_INFO playerInfo = account.playerInfo;
			ulong dbid = playerInfo.dbid;
			string name = playerInfo.name;
			TDGAAccount tdgaaccount = TDGAAccount.SetAccount(string.Concat(dbid));
			tdgaaccount.SetLevel((int)playerInfo.level);
			tdgaaccount.SetAccountName(name);
			tdgaaccount.SetAccountType(AccountType.REGISTERED);
		}
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x0012A910 File Offset: 0x00128B10
	public void onLogin()
	{
		MonoBehaviour.print("connect to server...(连接到服务端...)");
		this.text_status.text = "连接到服务端...";
		Event.fireIn("login", new object[]
		{
			this.if_userName.text,
			this.if_passWord.text,
			Encoding.UTF8.GetBytes("lxqLogin")
		});
		this.saveAccountInfo(this.if_userName.text, this.if_passWord.text);
	}

	// Token: 0x0600253B RID: 9531 RVA: 0x0001DDF6 File Offset: 0x0001BFF6
	public void saveAccountInfo(string name, string password)
	{
		PlayerPrefs.SetString("name", name);
		PlayerPrefs.SetString("password", password);
	}

	// Token: 0x0600253C RID: 9532 RVA: 0x0012A994 File Offset: 0x00128B94
	public void onRegister()
	{
		this.text_status.text = "连接到服务端...";
		if (this.reregisterPassWord.text.Length < 6)
		{
			this.text_status.text = "密码过短";
			return;
		}
		if (this.registerName.text.Length < 6)
		{
			this.text_status.text = "账号过短";
			return;
		}
		if (Regex.IsMatch(this.registerName.text, "[\\u4e00-\\u9fa5]"))
		{
			this.text_status.text = "暂不支持中文账号";
			return;
		}
		if (this.registerPassWord.text == this.reregisterPassWord.text)
		{
			Event.fireIn("createAccount", new object[]
			{
				this.registerName.text,
				this.registerPassWord.text,
				Encoding.UTF8.GetBytes("lxqRegister")
			});
			return;
		}
		this.text_status.text = "两次输入的密码不同";
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x0001DE0E File Offset: 0x0001C00E
	public void onHellohaha()
	{
		Account account = (Account)KBEngineApp.app.player();
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x0012AA90 File Offset: 0x00128C90
	public void onHelloTest()
	{
		testEntity testEntity = (testEntity)KBEngineApp.app.player();
		if (testEntity != null)
		{
			testEntity.hello("你好");
		}
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x0001DE20 File Offset: 0x0001C020
	public void helloClient(string msg)
	{
		this.text_status.text = msg;
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x0001DE20 File Offset: 0x0001C020
	public void helloClient2(string msg)
	{
		this.text_status.text = msg;
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x0012AABC File Offset: 0x00128CBC
	public void onCreateAccountResult(ushort retcode, byte[] datas)
	{
		if (retcode != 0)
		{
			MonoBehaviour.print("(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode));
			this.text_status.text = "(注册账号错误)! err=" + KBEngineApp.app.serverErr(retcode);
			return;
		}
		this.if_userName.text = this.registerName.text;
		this.if_passWord.text = this.registerPassWord.text;
		this.closeRegisterUI();
		this.text_status.text = "注册账号成功! 请点击登录进入游戏";
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x0000EF72 File Offset: 0x0000D172
	public void onClose()
	{
		Application.Quit();
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x0001DE2E File Offset: 0x0001C02E
	public void closeRegisterUI()
	{
		this.registerUI.SetActive(false);
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x0001DE3C File Offset: 0x0001C03C
	public void opengisterUI()
	{
		this.registerUI.SetActive(true);
	}

	// Token: 0x04001FBF RID: 8127
	public InputField if_userName;

	// Token: 0x04001FC0 RID: 8128
	public InputField if_passWord;

	// Token: 0x04001FC1 RID: 8129
	public Text text_status;

	// Token: 0x04001FC2 RID: 8130
	public InputField registerName;

	// Token: 0x04001FC3 RID: 8131
	public InputField registerPassWord;

	// Token: 0x04001FC4 RID: 8132
	public InputField reregisterPassWord;

	// Token: 0x04001FC5 RID: 8133
	public GameObject registerUI;
}
