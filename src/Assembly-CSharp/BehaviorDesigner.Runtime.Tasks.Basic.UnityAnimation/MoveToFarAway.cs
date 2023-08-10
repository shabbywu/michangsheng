namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("逃离玩家")]
public class MoveToFarAway : Action
{
	private SeaAvatarObjBase avatar;

	private SharedInt tempWeith;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = ((Task)this).gameObject.GetComponent<SeaAvatarObjBase>();
	}

	public override TaskStatus OnUpdate()
	{
		avatar.moveAwayFromPositon();
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
