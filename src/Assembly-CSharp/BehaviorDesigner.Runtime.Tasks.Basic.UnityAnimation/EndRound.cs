using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using YSGame;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

[TaskCategory("YS")]
[TaskDescription("结束回合")]
public class EndRound : Action
{
	private Avatar avatar;

	private SharedInt tempWeith;

	public override void OnAwake()
	{
	}

	public override void OnStart()
	{
		avatar = (Avatar)((Task)this).gameObject.GetComponent<AvaterAddScript>().entity;
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		if (!avatar.isPlayer())
		{
			if (!CanEndRound())
			{
				return (TaskStatus)3;
			}
			avatar.state = 2;
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction item = (UnityAction)delegate
			{
				avatar.AvatarEndRound();
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(item);
			YSFuncList.Ints.AddFunc(queue);
		}
		return (TaskStatus)2;
	}

	private bool CanEndRound()
	{
		string name = ((Object)((Task)this).transform.GetChild(((Task)this).transform.childCount - 1)).name;
		if ((Object)(object)RoundManager.instance != (Object)null)
		{
			name = name.Replace("(Clone)", "");
			if (RoundManager.instance.SkillList.Contains(name))
			{
				return false;
			}
		}
		return true;
	}

	public override void OnReset()
	{
	}
}
