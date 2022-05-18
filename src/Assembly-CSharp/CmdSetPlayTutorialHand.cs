using System;
using Fungus;
using UnityEngine;

// Token: 0x020002B2 RID: 690
[CommandInfo("YSTutorial", "设置焦点", "设置焦点", 0)]
[AddComponentMenu("")]
public class CmdSetPlayTutorialHand : Command
{
	// Token: 0x060014FB RID: 5371 RVA: 0x000133BB File Offset: 0x000115BB
	public override void OnEnter()
	{
		if (PlayTutorialCircle.Inst != null)
		{
			PlayTutorialCircle.Inst.SetShow(this.Show);
		}
		this.Continue();
	}

	// Token: 0x0400101F RID: 4127
	[SerializeField]
	[Tooltip("是否显示")]
	protected bool Show;
}
