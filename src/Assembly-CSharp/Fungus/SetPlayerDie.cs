using UnityEngine;

namespace Fungus;

[CommandInfo("YSNew/Set", "SetPlayerDie", "让玩家直接死亡", 0)]
[AddComponentMenu("")]
public class SetPlayerDie : Command
{
	[Tooltip("描述")]
	[SerializeField]
	protected string Desc = "让玩家直接死亡";

	public override void OnEnter()
	{
		UIDeath.Inst.Show(DeathType.身死道消);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
