using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005C8 RID: 1480
public class UI_statr : MonoBehaviour
{
	// Token: 0x06002566 RID: 9574 RVA: 0x0001E008 File Offset: 0x0001C208
	private void Start()
	{
		Event.registerOut("MatchSuccess", this, "MatchSuccess");
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x0001E01B File Offset: 0x0001C21B
	public void MatchSuccess()
	{
		SceneManager.LoadScene("world");
		SceneManager.LoadSceneAsync("selectAvatar", 1);
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x0012AEA8 File Offset: 0x001290A8
	public void cancelMatch()
	{
		Account account = (Account)KBEngineApp.app.player();
		if (account != null)
		{
			account.cancelMatch();
		}
	}

	// Token: 0x04001FE2 RID: 8162
	public static int startStatus;
}
