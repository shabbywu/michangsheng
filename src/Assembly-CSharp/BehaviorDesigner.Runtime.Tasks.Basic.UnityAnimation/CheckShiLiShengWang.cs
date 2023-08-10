using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YSSea")]
[TaskDescription("检测势力声望")]
public class CheckShiLiShengWang : Conditional
{
	private SeaAvatarObjBase avatar;

	[Tooltip("声望的值大于等于该值返回true")]
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
		Avatar player = Tools.instance.getPlayer();
		if ((player.MenPaiHaoGanDu.HasField(string.Concat(avatar.MenPai)) ? ((int)player.MenPaiHaoGanDu[string.Concat(avatar.MenPai)].n) : 0) < ((SharedVariable<int>)Value).Value)
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
