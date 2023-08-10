using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "LearningPassivitySkill", "给主角学习功法技能", 0)]
[AddComponentMenu("")]
public class LearningStaticSkill : Command
{
	[Tooltip("给主角学习功法")]
	[SerializeField]
	protected int skillID;

	public override void OnEnter()
	{
		Study(skillID);
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public static void Study(int skillID)
	{
		Tools.instance.getPlayer().addHasStaticSkillList(skillID);
	}
}
