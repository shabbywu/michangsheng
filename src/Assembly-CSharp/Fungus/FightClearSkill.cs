using UnityEngine;
using YSGame.Fight;

namespace Fungus;

[CommandInfo("YSFight", "FightClearSkill", "清空所有当前技能", 0)]
[AddComponentMenu("")]
public class FightClearSkill : Command
{
	[Tooltip("描述")]
	[SerializeField]
	protected string desc = "清空所有当前技能";

	public override void OnEnter()
	{
		Tools.instance.getPlayer().skill.Clear();
		foreach (UIFightSkillItem fightSkill in UIFightPanel.Inst.FightSkills)
		{
			fightSkill.Clear();
		}
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
