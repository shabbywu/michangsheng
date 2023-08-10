using KBEngine;
using YSGame.Fight;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("设置技能最优值")]
public class UseSkill : Action
{
	private Avatar avatar;

	private Behavior selfBehavior;

	private SharedInt tempWeith;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
		selfBehavior = ((Task)this).gameObject.GetComponent<Behavior>();
	}

	public override TaskStatus OnUpdate()
	{
		SharedInt sharedInt = ((Task)this).Owner.GetVariable("optimalSkillID") as SharedInt;
		selfBehavior.GetVariable("optimalSkillWeight");
		if (((SharedVariable<int>)sharedInt).Value == 18011 || (((SharedVariable<int>)sharedInt).Value >= 9001 && ((SharedVariable<int>)sharedInt).Value <= 9999) || (((SharedVariable<int>)sharedInt).Value >= 13001 && ((SharedVariable<int>)sharedInt).Value <= 13999))
		{
			MessageData data = new MessageData(((SharedVariable<int>)sharedInt).Value);
			MessageMag.Instance.Send(FightFaBaoShow.NPCUseWeaponMsgKey, data);
		}
		avatar.spell.spellSkill(((SharedVariable<int>)sharedInt).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
	}
}
