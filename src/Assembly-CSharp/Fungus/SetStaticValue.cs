using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetStaticValue", "设置全局变量", 0)]
[AddComponentMenu("")]
public class SetStaticValue : Command
{
	[Tooltip("全局变量的ID")]
	[SerializeField]
	public int StaticValueID;

	[Tooltip("全局变量的值")]
	[SerializeField]
	public int value;

	public override void OnEnter()
	{
		GlobalValue.Set(StaticValueID, value, GetCommandSourceDesc());
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
