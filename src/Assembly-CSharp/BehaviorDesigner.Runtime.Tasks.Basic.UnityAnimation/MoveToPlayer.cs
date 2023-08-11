namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("移动到玩家坐标点")]
public class MoveToPlayer : Action
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
		avatar.MonstarMoveToPlayer();
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
