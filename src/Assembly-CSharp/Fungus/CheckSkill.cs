using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YS", "CheckSkill", "检测是否拥有该技能", 0)]
[AddComponentMenu("")]
public class CheckSkill : Command
{
	[Tooltip("需要进行检测的技能ID")]
	[SerializeField]
	protected int SkillID;

	[Tooltip("获取到的值存放位置")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempValue;

	public override void OnEnter()
	{
		TempValue.Value = PlayerEx.HasSkill(SkillID);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
