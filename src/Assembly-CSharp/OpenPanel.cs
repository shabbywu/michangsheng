using System;
using Fungus;
using UnityEngine;

// Token: 0x02000175 RID: 373
[CommandInfo("YSTools", "OpenPanel", "打开UI界面", 0)]
[AddComponentMenu("")]
public class OpenPanel : Command
{
	// Token: 0x06000FB0 RID: 4016 RVA: 0x0005E396 File Offset: 0x0005C596
	public override void OnEnter()
	{
		PanelMamager.inst.OpenPanel(this.PanelName, 0);
		this.Continue();
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x0005E228 File Offset: 0x0005C428
	public override Color GetButtonColor()
	{
		return new Color32(184, 210, 235, byte.MaxValue);
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00004095 File Offset: 0x00002295
	public override void OnReset()
	{
	}

	// Token: 0x04000BC1 RID: 3009
	[Tooltip("打开界面名称")]
	[SerializeField]
	protected PanelMamager.PanelType PanelName;
}
