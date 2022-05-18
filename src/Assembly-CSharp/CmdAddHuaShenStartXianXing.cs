using System;
using Fungus;
using UnityEngine;

// Token: 0x020002EF RID: 751
[CommandInfo("YSPlayer", "增加化神时初始仙性", "增加化神时初始仙性", 0)]
[AddComponentMenu("")]
public class CmdAddHuaShenStartXianXing : Command
{
	// Token: 0x060016D0 RID: 5840 RVA: 0x0001437D File Offset: 0x0001257D
	public override void OnEnter()
	{
		PlayerEx.AddHuaShenStartXianXing(this.xianXing);
		this.Continue();
	}

	// Token: 0x04001236 RID: 4662
	[SerializeField]
	[Tooltip("仙性")]
	protected int xianXing;
}
