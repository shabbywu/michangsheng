using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples;

[TaskCategory("YS")]
[TaskDescription("检测当前技能是否可用")]
public class CheckSkillType : Conditional
{
	[Tooltip("当前技能id")]
	public AI.skillWeight weight = AI.skillWeight.Circle;

	protected Avatar avatar;

	private SharedInt currentSkillIndex;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
		currentSkillIndex = ((Task)this).Owner.GetVariable("NowSkill") as SharedInt;
	}

	public override TaskStatus OnUpdate()
	{
		int skill_ID = avatar.skill[(int)((SharedVariable)currentSkillIndex).GetValue()].skill_ID;
		SharedInt sharedInt = ((Task)this).Owner.GetVariable("optimalSkillWeight") as SharedInt;
		if (avatar.ai.getSkillWeight(skill_ID) >= (int)((SharedVariable)sharedInt).GetValue())
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}
}
