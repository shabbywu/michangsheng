using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetRandomTalkNotClick", "设置指定随机事件不再触发", 0)]
[AddComponentMenu("")]
public class SetRandomTalkClick : Command
{
	[Tooltip("事件ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable TaskID;

	public override void OnEnter()
	{
		AllMapManage.instance.RandomFlag[TaskID.Value] = 1;
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
