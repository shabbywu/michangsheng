using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "获取当前移除的BuffId", "获取当前移除的Buff", 0)]
[AddComponentMenu("")]
public class GetRemoveBuffId : Command
{
	[Tooltip("buffId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable id;

	public override void OnEnter()
	{
		id.Value = RoundManager.instance.curRemoveBuffId;
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
