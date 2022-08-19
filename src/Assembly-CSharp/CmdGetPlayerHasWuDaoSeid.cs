using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

// Token: 0x0200023F RID: 575
[CommandInfo("YSPlayer", "检查玩家是否拥有某悟道特性", "检查玩家是否拥有某悟道特性", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerHasWuDaoSeid : Command
{
	// Token: 0x06001626 RID: 5670 RVA: 0x00095F40 File Offset: 0x00094140
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

	// Token: 0x04001076 RID: 4214
	[Tooltip("悟道特性ID(如2006洗髓)")]
	[SerializeField]
	protected int SeidID;

	// Token: 0x04001077 RID: 4215
	[Tooltip("是否拥有")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable)
	})]
	protected BooleanVariable Result;
}
