using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "LearningSkill", "给主角学习技能", 0)]
[AddComponentMenu("")]
public class LearningSkill : Command
{
	[Tooltip("给主角学习技能")]
	[SerializeField]
	protected int skillID;

	public override void OnEnter()
	{
		Study(skillID);
		Continue();
	}

	public static void Study(int id)
	{
		Tools.instance.getPlayer().addHasSkillList(id);
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
