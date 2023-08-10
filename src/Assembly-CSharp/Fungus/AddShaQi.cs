using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddShaQi", "增加煞气", 0)]
[AddComponentMenu("")]
public class AddShaQi : Command
{
	[Tooltip("增加煞气的数量")]
	[SerializeField]
	protected int AddShaQiNum;

	public override void OnEnter()
	{
		Tools.instance.getPlayer().addShaQi(AddShaQiNum);
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
