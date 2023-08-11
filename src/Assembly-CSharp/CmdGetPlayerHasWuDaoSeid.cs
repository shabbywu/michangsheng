using System;
using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

[CommandInfo("YSPlayer", "检查玩家是否拥有某悟道特性", "检查玩家是否拥有某悟道特性", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerHasWuDaoSeid : Command
{
	[Tooltip("悟道特性ID(如2006洗髓)")]
	[SerializeField]
	protected int SeidID;

	[Tooltip("是否拥有")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	protected BooleanVariable Result;

	public override void OnEnter()
	{
		List<SkillItem> allWuDaoSkills = PlayerEx.Player.wuDaoMag.GetAllWuDaoSkills();
		Result.Value = false;
		foreach (SkillItem item in allWuDaoSkills)
		{
			if (item.itemId == SeidID)
			{
				Result.Value = true;
				break;
			}
		}
		Continue();
	}
}
