namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("自动检测玩家等级是否大于NPC等级")]
public class autoCheckLV : Conditional
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
		if (Tools.instance.getPlayer().level <= avatar.LV)
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
