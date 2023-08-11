namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("检测神识")]
public class CheckShenShi : Conditional
{
	private SeaAvatarObjBase avatar;

	[Tooltip("神识的值大于等于该值返回true")]
	public SharedInt Value;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = ((Task)this).gameObject.GetComponent<SeaAvatarObjBase>();
	}

	public override TaskStatus OnUpdate()
	{
		if (Tools.instance.getPlayer().shengShi < avatar.ShenShi)
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
