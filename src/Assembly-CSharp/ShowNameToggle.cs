using System;
using UnityEngine;

// Token: 0x0200046E RID: 1134
public class ShowNameToggle : MonoBehaviour
{
	// Token: 0x06002381 RID: 9089 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x000F31BE File Offset: 0x000F13BE
	private void Start()
	{
		Tools.instance.getPlayer();
		Tools.instance.getPlayer().showSkillName = 0;
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x000F31DB File Offset: 0x000F13DB
	public void chenge()
	{
		if (this.toggle.value)
		{
			Tools.instance.getPlayer().showSkillName = 0;
			return;
		}
		Tools.instance.getPlayer().showSkillName = 1;
	}

	// Token: 0x04001C78 RID: 7288
	public UIToggle toggle;
}
