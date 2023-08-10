using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckStaticSkill", "检测是否拥有该功法", 0)]
[AddComponentMenu("")]
public class CheckStaticSkill : Command
{
	[Tooltip("需要进行检测的功法ID")]
	[SerializeField]
	protected int SkillID;

	[Tooltip("获取到的值存放位置")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempValue;

	public override void OnEnter()
	{
		if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => (aa.itemId == SkillID) ? true : false) == null)
		{
			TempValue.Value = false;
		}
		else
		{
			TempValue.Value = true;
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
