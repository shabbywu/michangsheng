using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetJinDanLv", "获取金丹等级保存到Value中", 0)]
[AddComponentMenu("")]
public class GetJinDanLv : Command
{
	[Tooltip("保存到Value")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable JinDanValue;

	public override void OnEnter()
	{
		Avatar player = Tools.instance.getPlayer();
		int value = 1;
		try
		{
			value = jsonData.instance.JieDanBiao[player.hasJieDanSkillList[0].itemId.ToString()]["JinDanQuality"].I;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		JinDanValue.Value = value;
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
