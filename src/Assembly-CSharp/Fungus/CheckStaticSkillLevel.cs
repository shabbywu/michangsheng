using System;
using KBEngine;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "检测该功法等级是否达到目标等级", "检测该功法等级是否达到目标等级", 0)]
[AddComponentMenu("")]
public class CheckStaticSkillLevel : Command
{
	[Tooltip("需要进行检测的技能ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable SkillID;

	[Tooltip("目标等级")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable Level;

	[Tooltip("获取到的值存放位置")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempValue;

	public override void OnEnter()
	{
		TempValue.Value = false;
		foreach (SkillItem hasStaticSkill in Tools.instance.getPlayer().hasStaticSkillList)
		{
			if (hasStaticSkill.itemId == SkillID.Value && hasStaticSkill.level == Level.Value)
			{
				TempValue.Value = true;
				break;
			}
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
