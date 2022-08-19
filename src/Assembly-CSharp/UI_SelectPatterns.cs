using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class UI_SelectPatterns : MonoBehaviour
{
	// Token: 0x0600219C RID: 8604 RVA: 0x000E95DE File Offset: 0x000E77DE
	private void Start()
	{
		World.instance.init();
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x000E95EA File Offset: 0x000E77EA
	public void onZombieEnterGame()
	{
		if ((Account)KBEngineApp.app.player() != null)
		{
			Application.LoadLevel("world");
		}
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x000E95EA File Offset: 0x000E77EA
	public void onHeroEnterGame()
	{
		if ((Account)KBEngineApp.app.player() != null)
		{
			Application.LoadLevel("world");
		}
	}
}
