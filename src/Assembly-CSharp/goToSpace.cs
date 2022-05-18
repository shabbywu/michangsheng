using System;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000578 RID: 1400
public class goToSpace : MonoBehaviour
{
	// Token: 0x0600238A RID: 9098 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x0001CB8C File Offset: 0x0001AD8C
	public void rejoinSpace()
	{
		((Account)KBEngineApp.app.player()).rejoinSpace();
		SceneManager.UnloadScene("goToSpace");
		Event.fireOut("ShowGameUI", Array.Empty<object>());
	}
}
