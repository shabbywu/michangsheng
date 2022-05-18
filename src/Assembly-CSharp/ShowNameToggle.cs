using System;
using UnityEngine;

// Token: 0x0200062B RID: 1579
public class ShowNameToggle : MonoBehaviour
{
	// Token: 0x0600273A RID: 10042 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x0001F217 File Offset: 0x0001D417
	private void Start()
	{
		Tools.instance.getPlayer();
		Tools.instance.getPlayer().showSkillName = 0;
	}

	// Token: 0x0600273C RID: 10044 RVA: 0x0001F234 File Offset: 0x0001D434
	public void chenge()
	{
		if (this.toggle.value)
		{
			Tools.instance.getPlayer().showSkillName = 0;
			return;
		}
		Tools.instance.getPlayer().showSkillName = 1;
	}

	// Token: 0x04002150 RID: 8528
	public UIToggle toggle;
}
