using System;
using Fungus;
using UnityEngine;

// Token: 0x02000174 RID: 372
[CommandInfo("YSTools", "OpenLianQi", "打开炼器界面", 0)]
[AddComponentMenu("")]
public class OpenLianQi : Command
{
	// Token: 0x06000FAC RID: 4012 RVA: 0x0005E382 File Offset: 0x0005C582
	public override void OnEnter()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼器, 0);
		this.Continue();
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0005E228 File Offset: 0x0005C428
	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, byte.MaxValue);
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00004095 File Offset: 0x00002295
	public override void OnReset()
	{
	}
}
