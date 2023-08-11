using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples;

[TaskCategory("YS")]
[TaskDescription("检测当前技能是否可用")]
public class SkillCanUse : Conditional
{
	[Tooltip("当前技能id")]
	public SharedInt currentSkillIndex;

	private Avatar avatar;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
	}

	public override TaskStatus OnUpdate()
	{
		if (avatar.skill[(int)((SharedVariable)currentSkillIndex).GetValue()].skill_ID > 0)
		{
			if (avatar.skill[(int)((SharedVariable)currentSkillIndex).GetValue()].CanUse(avatar, avatar) != SkillCanUseType.可以使用)
			{
				return (TaskStatus)1;
			}
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}
}
