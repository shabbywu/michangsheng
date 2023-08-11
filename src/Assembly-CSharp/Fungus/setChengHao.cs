using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "setChengHao", "设置称号id", 0)]
[AddComponentMenu("")]
public class setChengHao : Command
{
	[Tooltip("设置称号的ID")]
	[SerializeField]
	protected int chengHaoID;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().SetChengHaoId(chengHaoID);
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
