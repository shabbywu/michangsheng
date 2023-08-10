namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("检测NPC是否在岛上")]
public class CheckInIsland : Conditional
{
	private SeaAvatarObjBase avatar;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = ((Task)this).gameObject.GetComponent<SeaAvatarObjBase>();
	}

	public override TaskStatus OnUpdate()
	{
		if (!((MapSeaCompent)AllMapManage.instance.mapIndex[avatar.NowMapIndex]).NodeHasIsLand())
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
