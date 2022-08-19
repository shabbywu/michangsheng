using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003DD RID: 989
public class goToSpace : MonoBehaviour
{
	// Token: 0x0600200A RID: 8202 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x000E1CF2 File Offset: 0x000DFEF2
	public void rejoinSpace()
	{
		((Account)KBEngineApp.app.player()).rejoinSpace();
		SceneManager.UnloadScene("goToSpace");
		Event.fireOut("ShowGameUI", Array.Empty<object>());
	}
}
