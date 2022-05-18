using System;
using Fungus;
using UnityEngine;

// Token: 0x02000251 RID: 593
[CommandInfo("YSTools", "OpenLianQi", "打开炼器界面", 0)]
[AddComponentMenu("")]
public class OpenLianQi : Command
{
	// Token: 0x0600120C RID: 4620 RVA: 0x000113F7 File Offset: 0x0000F5F7
	public override void OnEnter()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼器, 0);
		this.Continue();
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x000113CF File Offset: 0x0000F5CF
	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, byte.MaxValue);
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x000042DD File Offset: 0x000024DD
	public override void OnReset()
	{
	}
}
