using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Start a new behavior tree and return success after it has been started.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=20")]
[TaskIcon("{SkinColor}StartBehaviorTreeIcon.png")]
public class StartBehaviorTree : Action
{
	[Tooltip("The GameObject of the behavior tree that should be started. If null use the current behavior")]
	public SharedGameObject behaviorGameObject;

	[Tooltip("The group of the behavior tree that should be started")]
	public SharedInt group;

	[Tooltip("Should this task wait for the behavior tree to complete?")]
	public SharedBool waitForCompletion = false;

	[Tooltip("Should the variables be synchronized?")]
	public SharedBool synchronizeVariables;

	private bool behaviorComplete;

	private Behavior behavior;

	public override void OnStart()
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		Behavior[] components = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)behaviorGameObject).Value).GetComponents<Behavior>();
		if (components.Length == 1)
		{
			behavior = components[0];
		}
		else if (components.Length > 1)
		{
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
		if (!((Object)(object)behavior != (Object)null))
		{
			return;
		}
		List<SharedVariable> allVariables = ((Task)this).Owner.GetAllVariables();
		if (allVariables != null && ((SharedVariable<bool>)synchronizeVariables).Value)
		{
			for (int j = 0; j < allVariables.Count; j++)
			{
				behavior.SetVariableValue(allVariables[j].Name, (object)allVariables[j]);
			}
		}
		behavior.EnableBehavior();
		if (((SharedVariable<bool>)waitForCompletion).Value)
		{
			behaviorComplete = false;
			behavior.OnBehaviorEnd += new BehaviorHandler(BehaviorEnded);
		}
	}

	public override TaskStatus OnUpdate()
	{
		if (!((Object)(object)behavior == (Object)null))
		{
			if (((SharedVariable<bool>)waitForCompletion).Value && !behaviorComplete)
			{
				return (TaskStatus)3;
			}
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	private void BehaviorEnded(Behavior behavior)
	{
		behaviorComplete = true;
	}

	public override void OnEnd()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		if ((Object)(object)behavior != (Object)null && ((SharedVariable<bool>)waitForCompletion).Value)
		{
			behavior.OnBehaviorEnd -= new BehaviorHandler(BehaviorEnded);
		}
	}

	public override void OnReset()
	{
		behaviorGameObject = null;
		group = 0;
		waitForCompletion = false;
		synchronizeVariables = false;
	}
}
