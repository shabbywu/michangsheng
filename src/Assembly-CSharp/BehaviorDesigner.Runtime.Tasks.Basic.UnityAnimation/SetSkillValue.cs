using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("设置技能最优值")]
public class SetSkillValue : Action
{
	[Tooltip("The name of the animation")]
	public AI.skillWeight skillweith;

	private Avatar avatar;

	private Behavior selfBehavior;

	private SharedInt tempWeith;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
		selfBehavior = ((Task)this).gameObject.GetComponent<Behavior>();
	}

	public override TaskStatus OnUpdate()
	{
		SharedInt obj = selfBehavior.GetVariable("optimalSkillID") as SharedInt;
		SharedInt obj2 = selfBehavior.GetVariable("optimalSkillWeight") as SharedInt;
		SharedInt sharedInt = selfBehavior.GetVariable("NowSkill") as SharedInt;
		int skillWeight = avatar.ai.getSkillWeight(avatar.skill[((SharedVariable<int>)sharedInt).Value].skill_ID);
		((SharedVariable<int>)obj2).Value = skillWeight;
		((SharedVariable<int>)obj).Value = avatar.skill[((SharedVariable<int>)sharedInt).Value].skill_ID;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
