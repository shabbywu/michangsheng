using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class shoudengjitoggle : MonoBehaviour
{
	// Token: 0x0600235E RID: 9054 RVA: 0x000F1F60 File Offset: 0x000F0160
	private void Awake()
	{
		if (Tools.instance.getPlayer().showStaticSkillDengJi == 1)
		{
			this.toggle.value = true;
			return;
		}
		this.toggle.value = false;
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x000F1F8D File Offset: 0x000F018D
	public void chenge()
	{
		if (this.toggle.value)
		{
			Tools.instance.getPlayer().showStaticSkillDengJi = 1;
			return;
		}
		Tools.instance.getPlayer().showStaticSkillDengJi = 0;
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x000F1FC0 File Offset: 0x000F01C0
	private void Update()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.showStaticSkillDengJi == 1 && !this.toggle.value)
		{
			this.toggle.value = true;
			return;
		}
		if (player.showStaticSkillDengJi != 1 && this.toggle.value)
		{
			this.toggle.value = false;
		}
	}

	// Token: 0x04001C68 RID: 7272
	public UIToggle toggle;
}
