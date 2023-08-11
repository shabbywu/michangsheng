using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Samples;

[TaskCategory("YSSea")]
[TaskDescription("检测NPC是否能进行移动")]
public class WhetherCanMove : Conditional
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
		if (!avatar.WhetherCanMove())
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}
}
