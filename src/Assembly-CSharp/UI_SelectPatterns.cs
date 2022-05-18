using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005C4 RID: 1476
public class UI_SelectPatterns : MonoBehaviour
{
	// Token: 0x06002554 RID: 9556 RVA: 0x0001DF0F File Offset: 0x0001C10F
	private void Start()
	{
		World.instance.init();
	}

	// Token: 0x06002555 RID: 9557 RVA: 0x0001DF1B File Offset: 0x0001C11B
	public void onZombieEnterGame()
	{
		if ((Account)KBEngineApp.app.player() != null)
		{
			Application.LoadLevel("world");
		}
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x0001DF1B File Offset: 0x0001C11B
	public void onHeroEnterGame()
	{
		if ((Account)KBEngineApp.app.player() != null)
		{
			Application.LoadLevel("world");
		}
	}
}
