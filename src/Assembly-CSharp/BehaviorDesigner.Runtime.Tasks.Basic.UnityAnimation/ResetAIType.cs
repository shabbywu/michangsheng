namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("重置NPC AI类型")]
public class ResetAIType : Action
{
	private SeaAvatarObjBase avatar;

	public SharedInt AIType;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = ((Task)this).gameObject.GetComponent<SeaAvatarObjBase>();
	}

	public override TaskStatus OnUpdate()
	{
		avatar.ResetBehavirTree(((SharedVariable<int>)AIType).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
