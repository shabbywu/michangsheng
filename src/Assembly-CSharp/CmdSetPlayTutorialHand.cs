using System;
using Fungus;
using UnityEngine;

// Token: 0x020001B4 RID: 436
[CommandInfo("YSTutorial", "设置焦点", "设置焦点", 0)]
[AddComponentMenu("")]
public class CmdSetPlayTutorialHand : Command
{
	// Token: 0x06001250 RID: 4688 RVA: 0x0006F379 File Offset: 0x0006D579
	public override void OnEnter()
	{
		if (PlayTutorialCircle.Inst != null)
		{
			PlayTutorialCircle.Inst.SetShow(this.Show);
		}
		this.Continue();
	}

	// Token: 0x04000CF7 RID: 3319
	[SerializeField]
	[Tooltip("是否显示")]
	protected bool Show;
}
