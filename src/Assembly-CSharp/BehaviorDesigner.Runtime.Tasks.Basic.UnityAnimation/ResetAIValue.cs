using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("设置技能最优值")]
public class ResetAIValue : Action
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
		((SharedVariable<int>)NowSkill).Value = -1;
		((SharedVariable<int>)skillID).Value = 0;
		((SharedVariable<int>)skillWeight).Value = 999;
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
