using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("设置技能最优值")]
public class SetNowSkill : Action
{
	[Tooltip("The name of the animation")]
	public SharedInt NowSkill;

	private Avatar avatar;

	private Behavior selfBehavior;

	private SharedInt tempWeith;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		AvaterAddScript component = ((Task)this).gameObject.GetComponent<AvaterAddScript>();
		avatar = (Avatar)component.entity;
		selfBehavior = ((Task)this).gameObject.GetComponent<Behavior>();
	}

	public override TaskStatus OnUpdate()
	{
		SharedInt nowSkill = NowSkill;
		int value = ((SharedVariable<int>)nowSkill).Value;
		((SharedVariable<int>)nowSkill).Value = value + 1;
		if (avatar.skill.Count > ((SharedVariable<int>)NowSkill).Value)
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		((SharedVariable<int>)NowSkill).Value = 0;
	}
}
