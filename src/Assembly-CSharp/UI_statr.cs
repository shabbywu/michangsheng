using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000415 RID: 1045
public class UI_statr : MonoBehaviour
{
	// Token: 0x060021AC RID: 8620 RVA: 0x000E97D3 File Offset: 0x000E79D3
	private void Start()
	{
		Event.registerOut("MatchSuccess", this, "MatchSuccess");
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x000E97E6 File Offset: 0x000E79E6
	public void MatchSuccess()
	{
		SceneManager.LoadScene("world");
		SceneManager.LoadSceneAsync("selectAvatar", 1);
	}

	// Token: 0x060021B0 RID: 8624 RVA: 0x000E9800 File Offset: 0x000E7A00
	public void cancelMatch()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.cancelMatch();
		}
	}

	// Token: 0x04001B21 RID: 6945
	public static int startStatus;
}
