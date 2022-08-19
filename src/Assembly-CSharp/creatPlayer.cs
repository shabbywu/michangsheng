using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020003D8 RID: 984
public class creatPlayer : MonoBehaviour
{
	// Token: 0x06001FF1 RID: 8177 RVA: 0x000E159F File Offset: 0x000DF79F
	private void Start()
	{
		Event.registerOut("goToHome", this, "goToHome");
		Event.registerOut("HomeErrorMessage", this, "HomeErrorMessage");
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x000E15C3 File Offset: 0x000DF7C3
	public void HomeErrorMessage(string msg)
	{
		this.text_status.text = msg;
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x000E15D1 File Offset: 0x000DF7D1
	public void goToHome()
	{
		SceneManager.UnloadScene("creatPlayer");
		SceneManager.LoadSceneAsync("homeScene", 1);
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x000E15EC File Offset: 0x000DF7EC
	public void onHellohaha()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.reqCreatePlayer(this._userName.text);
		}
	}

	// Token: 0x040019F7 RID: 6647
	public InputField _userName;

	// Token: 0x040019F8 RID: 6648
	public Text text_status;
}
