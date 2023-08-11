using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetTianFu", "获取是否选择该天赋", 0)]
[AddComponentMenu("")]
public class GetTianFu : Command
{
	[Tooltip("天赋的ID")]
	[SerializeField]
	protected int TianFuID;

	[Tooltip("返回是否拥有的值")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable TempBool;

	public override void OnEnter()
	{
		TempBool.Value = PlayerEx.HasTianFu(TianFuID);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
