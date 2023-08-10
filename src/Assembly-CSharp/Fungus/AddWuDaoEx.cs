using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddWuDaoEx", "增加悟道经验值", 0)]
[AddComponentMenu("")]
public class AddWuDaoEx : Command
{
	[Tooltip("增加悟道经验值的属性")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable Type;

	[Tooltip("增加悟道经验值数量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	[SerializeField]
	protected IntegerVariable AddNum;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().wuDaoMag.addWuDaoEx(Type.Value, AddNum.Value);
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
