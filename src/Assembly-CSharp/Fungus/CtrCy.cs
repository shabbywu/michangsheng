using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "开关传音符功能", "开关传音符功能", 0)]
[AddComponentMenu("")]
public class CtrCy : Command
{
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable IsStop;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().emailDateMag.IsStopAll = IsStop.Value;
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
