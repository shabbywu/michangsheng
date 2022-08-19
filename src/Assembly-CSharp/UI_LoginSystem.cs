using System;
using System.Text;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200040E RID: 1038
public class UI_LoginSystem : MonoBehaviour
{
	// Token: 0x06002179 RID: 8569 RVA: 0x000E8DF0 File Offset: 0x000E6FF0
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

	// Token: 0x0600217A RID: 8570 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x000E8EA8 File Offset: 0x000E70A8
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

	// Token: 0x0600217C RID: 8572 RVA: 0x000E8F21 File Offset: 0x000E7121
	public void goToHome()
	{
		SceneManager.UnloadScene("login");
		SceneManager.LoadSceneAsync("homeScene", 1);
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x000E8F3A File Offset: 0x000E713A
	public void goToCreatePlayer()
	{
		SceneManager.UnloadScene("login");
		SceneManager.LoadSceneAsync("creatPlayer", 1);
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x000E8F53 File Offset: 0x000E7153
	public void goToSpace()
	{
		World.instance.init();
		SceneManager.LoadScene("world");
		SceneManager.LoadSceneAsync("goToSpace", 1);
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x000E8F75 File Offset: 0x000E7175
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

	// Token: 0x06002180 RID: 8576 RVA: 0x000E8FA5 File Offset: 0x000E71A5
	public void onLoginFailed(ushort errorCode)
	{
		this.text_status.text = "登陆失败" + KBEngineApp.app.serverErr(errorCode);
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x000E8FC8 File Offset: 0x000E71C8
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

	// Token: 0x06002182 RID: 8578 RVA: 0x000E9024 File Offset: 0x000E7224
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

	// Token: 0x06002183 RID: 8579 RVA: 0x000E90A5 File Offset: 0x000E72A5
	public void saveAccountInfo(string name, string password)
	{
		PlayerPrefs.SetString("name", name);
		PlayerPrefs.SetString("password", password);
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x000E90C0 File Offset: 0x000E72C0
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

	// Token: 0x06002185 RID: 8581 RVA: 0x000E91B9 File Offset: 0x000E73B9
	public void onHellohaha()
	{
		Account account = (Account)KBEngineApp.app.player();
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x000E91CC File Offset: 0x000E73CC
	public void onHelloTest()
	{
		testEntity testEntity = (testEntity)KBEngineApp.app.player();
		if (testEntity != null)
		{
			testEntity.hello("你好");
		}
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x000E91F7 File Offset: 0x000E73F7
	public void helloClient(string msg)
	{
		this.text_status.text = msg;
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x000E91F7 File Offset: 0x000E73F7
	public void helloClient2(string msg)
	{
		this.text_status.text = msg;
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x000E9208 File Offset: 0x000E7408
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

	// Token: 0x0600218A RID: 8586 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x00049258 File Offset: 0x00047458
	public void onClose()
	{
		Application.Quit();
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x000E9295 File Offset: 0x000E7495
	public void closeRegisterUI()
	{
		this.registerUI.SetActive(false);
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x000E92A3 File Offset: 0x000E74A3
	public void opengisterUI()
	{
		this.registerUI.SetActive(true);
	}

	// Token: 0x04001B00 RID: 6912
	public InputField if_userName;

	// Token: 0x04001B01 RID: 6913
	public InputField if_passWord;

	// Token: 0x04001B02 RID: 6914
	public Text text_status;

	// Token: 0x04001B03 RID: 6915
	public InputField registerName;

	// Token: 0x04001B04 RID: 6916
	public InputField registerPassWord;

	// Token: 0x04001B05 RID: 6917
	public InputField reregisterPassWord;

	// Token: 0x04001B06 RID: 6918
	public GameObject registerUI;
}
