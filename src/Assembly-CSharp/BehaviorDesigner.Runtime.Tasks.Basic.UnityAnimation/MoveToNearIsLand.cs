namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("移动到最近的岛屿")]
public class MoveToNearIsLand : Action
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
		avatar.moveToNearlIsland();
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
