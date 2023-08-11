using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddHP", "增加生命值", 0)]
[AddComponentMenu("")]
public class AddHP : Command
{
	[Tooltip("增加生命的数量")]
	[SerializeField]
	public int AddHpNum;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().AllMapAddHP(AddHpNum);
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
