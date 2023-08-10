using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSFuBen", "LightFubenNode", "角色传送", 0)]
[AddComponentMenu("")]
public class LightFubenNode : Command
{
	[Tooltip("传送到的地点的ID")]
	[SerializeField]
	protected string ScenceName;

	[Tooltip("点亮的地点的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable MapID;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().fubenContorl[ScenceName].addExploredNode(MapID.Value);
		WASDMove.Inst.IsMoved = true;
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
