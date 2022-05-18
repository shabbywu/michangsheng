using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000624 RID: 1572
public class shoudengjitoggle : MonoBehaviour
{
	// Token: 0x06002714 RID: 10004 RVA: 0x0001F107 File Offset: 0x0001D307
	private void Awake()
	{
		if (Tools.instance.getPlayer().showStaticSkillDengJi == 1)
		{
			this.toggle.value = true;
			return;
		}
		this.toggle.value = false;
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x0001F134 File Offset: 0x0001D334
	public void chenge()
	{
		if (this.toggle.value)
		{
			Tools.instance.getPlayer().showStaticSkillDengJi = 1;
			return;
		}
		Tools.instance.getPlayer().showStaticSkillDengJi = 0;
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x00132148 File Offset: 0x00130348
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

	// Token: 0x0400213D RID: 8509
	public UIToggle toggle;
}
