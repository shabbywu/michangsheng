using System;
using Fungus;
using UnityEngine;

// Token: 0x020001DC RID: 476
[CommandInfo("YSPlayer", "增加化神时初始仙性", "增加化神时初始仙性", 0)]
[AddComponentMenu("")]
public class CmdAddHuaShenStartXianXing : Command
{
	// Token: 0x0600142B RID: 5163 RVA: 0x00082813 File Offset: 0x00080A13
	public override void OnEnter()
	{
		PlayerEx.AddHuaShenStartXianXing(this.xianXing);
		this.Continue();
	}

	// Token: 0x04000EF8 RID: 3832
	[SerializeField]
	[Tooltip("仙性")]
	protected int xianXing;
}
