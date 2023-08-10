using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Restarts a behavior tree, returns success after it has been restarted.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=66")]
[TaskIcon("{SkinColor}RestartBehaviorTreeIcon.png")]
public class RestartBehaviorTree : Action
{
	[Tooltip("The GameObject of the behavior tree that should be restarted. If null use the current behavior")]
	public SharedGameObject behaviorGameObject;

	[Tooltip("The group of the behavior tree that should be restarted")]
	public SharedInt group;

	private Behavior behavior;

	public override void OnAwake()
	{
		Behavior[] components = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)behaviorGameObject).Value).GetComponents<Behavior>();
		if (components.Length == 1)
		{
			behavior = components[0];
		}
		else
		{
			if (components.Length <= 1)
			{
				return;
			}
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].Group == ((SharedVariable<int>)group).Value)
				{
					behavior = components[i];
					break;
				}
			}
			if ((Object)(object)behavior == (Object)null)
			{
				behavior = components[0];
			}
		}
	}

	public override TaskStatus OnUpdate()
	{
		if (!((Object)(object)behavior == (Object)null))
		{
			behavior.DisableBehavior();
			behavior.EnableBehavior();
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		behavior = null;
	}
}
