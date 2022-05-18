using System;
using Fungus;
using UnityEngine;

// Token: 0x02000252 RID: 594
[CommandInfo("YSTools", "OpenPanel", "打开UI界面", 0)]
[AddComponentMenu("")]
public class OpenPanel : Command
{
	// Token: 0x06001210 RID: 4624 RVA: 0x0001140B File Offset: 0x0000F60B
	public override void OnEnter()
	{
		PanelMamager.inst.OpenPanel(this.PanelName, 0);
		this.Continue();
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x000113CF File Offset: 0x0000F5CF
	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, byte.MaxValue);
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x000042DD File Offset: 0x000024DD
	public override void OnReset()
	{
	}

	// Token: 0x04000E92 RID: 3730
	[Tooltip("打开界面名称")]
	[SerializeField]
	protected PanelMamager.PanelType PanelName;
}
