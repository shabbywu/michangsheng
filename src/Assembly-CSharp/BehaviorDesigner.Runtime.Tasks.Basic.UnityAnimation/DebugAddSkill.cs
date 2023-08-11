using GUIPackage;
using KBEngine;
using YSGame.Fight;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("测试自动加载技能")]
public class DebugAddSkill : Action
{
	public SharedInt NowSkill;

	public SharedInt skillID;

	public SharedInt skillWeight;

	private Avatar avatar;

	private Behavior selfBehavior;

	private SharedInt tempWeith;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
	}

	public override TaskStatus OnUpdate()
	{
		SharedInt sharedInt = skillID;
		int value = ((SharedVariable<int>)sharedInt).Value;
		((SharedVariable<int>)sharedInt).Value = value + 1;
		if (jsonData.instance.skillJsonData.HasField(((SharedVariable<int>)skillID).Value.ToString()))
		{
			Avatar avatar = this.avatar;
			GUIPackage.Skill skill = new GUIPackage.Skill(((SharedVariable<int>)skillID).Value, 0, 10);
			avatar.skill.Add(skill);
			if (avatar.isPlayer())
			{
				foreach (UIFightSkillItem fightSkill in UIFightPanel.Inst.FightSkills)
				{
					if (!fightSkill.HasSkill)
					{
						fightSkill.SetSkill(skill);
						break;
					}
				}
			}
			if (avatar.cardMag.getCardNum() < 30)
			{
				int num = 0;
				foreach (JSONObject item in jsonData.instance.skillJsonData[((SharedVariable<int>)skillID).Value.ToString()]["skill_CastType"].list)
				{
					avatar.cardMag.addCard((int)item.n, (int)jsonData.instance.skillJsonData[((SharedVariable<int>)skillID).Value.ToString()]["skill_Cast"][num].n);
					num++;
				}
				int num2 = 0;
				foreach (JSONObject item2 in jsonData.instance.skillJsonData[((SharedVariable<int>)skillID).Value.ToString()]["skill_SameCastNum"].list)
				{
					avatar.cardMag.addCard(num2, (int)item2.n);
					num2++;
				}
			}
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
	}
}
