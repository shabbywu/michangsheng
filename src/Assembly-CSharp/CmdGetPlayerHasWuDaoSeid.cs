using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x0200035B RID: 859
[CommandInfo("YSPlayer", "检查玩家是否拥有某悟道特性", "检查玩家是否拥有某悟道特性", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerHasWuDaoSeid : Command
{
	// Token: 0x060018DE RID: 6366 RVA: 0x000DE4E4 File Offset: 0x000DC6E4
	public override void OnEnter()
	{
		List<SkillItem> allWuDaoSkills = PlayerEx.Player.wuDaoMag.GetAllWuDaoSkills();
		this.Result.Value = false;
		using (List<SkillItem>.Enumerator enumerator = allWuDaoSkills.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.itemId == this.SeidID)
				{
					this.Result.Value = true;
					break;
				}
			}
		}
		this.Continue();
	}

	// Token: 0x040013CE RID: 5070
	[Tooltip("悟道特性ID(如2006洗髓)")]
	[SerializeField]
	protected int SeidID;

	// Token: 0x040013CF RID: 5071
	[Tooltip("是否拥有")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable)
	})]
	protected BooleanVariable Result;
}
