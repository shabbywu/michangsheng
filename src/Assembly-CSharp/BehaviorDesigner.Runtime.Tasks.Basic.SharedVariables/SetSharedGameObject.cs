using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables;

[TaskCategory("Basic/SharedVariable")]
[TaskDescription("Sets the SharedGameObject variable to the specified object. Returns Success.")]
public class SetSharedGameObject : Action
{
	[Tooltip("The value to set the SharedGameObject to. If null the variable will be set to the current GameObject")]
	public SharedGameObject targetValue;

	[RequiredField]
	[Tooltip("The SharedGameObject to set")]
	public SharedGameObject targetVariable;

	[Tooltip("Can the target value be null?")]
	public SharedBool valueCanBeNull;

	public override TaskStatus OnUpdate()
	{
		((SharedVariable<GameObject>)targetVariable).Value = (((Object)(object)((SharedVariable<GameObject>)targetValue).Value != (Object)null || ((SharedVariable<bool>)valueCanBeNull).Value) ? ((SharedVariable<GameObject>)targetValue).Value : ((Task)this).gameObject);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		valueCanBeNull = false;
		targetValue = null;
		targetVariable = null;
	}
}
