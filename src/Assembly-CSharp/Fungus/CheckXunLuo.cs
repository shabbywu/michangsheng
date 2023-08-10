using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "CheckXunLuo", "检查是否巡逻到玩家", 0)]
[AddComponentMenu("")]
public class CheckXunLuo : Command
{
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable npcId;

	public override void OnEnter()
	{
		if (NpcJieSuanManager.inst.isCanJieSuan)
		{
			List<int> xunLuoNpcList = NpcJieSuanManager.inst.GetXunLuoNpcList(Tools.getScreenName(), Tools.instance.getPlayer().fubenContorl[Tools.getScreenName()].NowIndex);
			if (xunLuoNpcList.Count > 0)
			{
				npcId.Value = xunLuoNpcList[NpcJieSuanManager.inst.getRandomInt(0, xunLuoNpcList.Count - 1)];
			}
			else
			{
				npcId.Value = 0;
			}
		}
		else
		{
			npcId.Value = 0;
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
