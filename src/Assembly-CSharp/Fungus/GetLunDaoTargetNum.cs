using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Get", "GetLunDaoTargetNum", "获取完成论道数目", 0)]
[AddComponentMenu("")]
public class GetLunDaoTargetNum : Command
{
	[Tooltip("完成论题数目")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable num;

	public override void OnEnter()
	{
		num.Value = Tools.instance.TargetLunTiNum;
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
