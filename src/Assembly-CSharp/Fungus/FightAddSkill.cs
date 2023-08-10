using UnityEngine;

namespace Fungus;

[CommandInfo("YSFight", "FightAddSkill", "增加技能", 0)]
[AddComponentMenu("")]
public class FightAddSkill : Command
{
	[Tooltip("技能ID")]
	[SerializeField]
	protected int skillID;

	public override void OnEnter()
	{
		PlayerEx.Player.FightAddSkill(skillID, 0, 12);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
