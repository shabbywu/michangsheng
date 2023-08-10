using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Add", "AddHaoGanDu", "增加好感度", 0)]
[AddComponentMenu("")]
public class AddHaoGanDu : Command
{
	[Tooltip("增加好感度")]
	[SerializeField]
	protected int AddHaoGanduNum;

	[Tooltip("增加好感度的武将")]
	[SerializeField]
	protected int AvatarID = 1;

	public override void OnEnter()
	{
		NPCEx.AddFavor(AvatarID, AddHaoGanduNum);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
