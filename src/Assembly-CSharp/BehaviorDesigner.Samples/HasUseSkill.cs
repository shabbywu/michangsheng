using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples;

[TaskCategory("YS")]
[TaskDescription("检测是否拥有最优技能")]
public class HasUseSkill : Conditional
{
	[Tooltip("当前技能id")]
	public SharedInt currentSkillID;

	private Avatar avatar;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
	}

	public override TaskStatus OnUpdate()
	{
		if (((SharedVariable<int>)(((Task)this).Owner.GetVariable("optimalSkillWeight") as SharedInt)).Value == 20)
		{
			int listSum = RoundManager.instance.getListSum(avatar.crystal);
			if (avatar.NowCard >= listSum)
			{
				return (TaskStatus)1;
			}
			return (TaskStatus)2;
		}
		if (((SharedVariable<int>)currentSkillID).Value <= 0)
		{
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}
}
