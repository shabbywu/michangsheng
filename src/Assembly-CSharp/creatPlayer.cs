using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000573 RID: 1395
public class creatPlayer : MonoBehaviour
{
	// Token: 0x06002371 RID: 9073 RVA: 0x0001CA87 File Offset: 0x0001AC87
	private void Start()
	{
		Event.registerOut("goToHome", this, "goToHome");
		Event.registerOut("HomeErrorMessage", this, "HomeErrorMessage");
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x0001CAAB File Offset: 0x0001ACAB
	public void HomeErrorMessage(string msg)
	{
		this.text_status.text = msg;
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x0001CAB9 File Offset: 0x0001ACB9
	public void goToHome()
	{
		SceneManager.UnloadScene("creatPlayer");
		SceneManager.LoadSceneAsync("homeScene", 1);
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x00124060 File Offset: 0x00122260
	public void onHellohaha()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.reqCreatePlayer(this._userName.text);
		}
	}

	// Token: 0x04001E89 RID: 7817
	public InputField _userName;

	// Token: 0x04001E8A RID: 7818
	public Text text_status;
}
