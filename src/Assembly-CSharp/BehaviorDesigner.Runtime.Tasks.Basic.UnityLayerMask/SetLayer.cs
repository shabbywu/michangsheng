using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLayerMask;

[TaskCategory("Basic/LayerMask")]
[TaskDescription("Sets the layer of a GameObject.")]
public class SetLayer : Action
{
	[Tooltip("The GameObject to set the layer of")]
	public SharedGameObject targetGameObject;

	[Tooltip("The name of the layer to set")]
	public SharedString layerName = "Default";

	public override TaskStatus OnUpdate()
	{
		((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value).layer = LayerMask.NameToLayer(((SharedVariable<string>)layerName).Value);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		targetGameObject = null;
		layerName = "Default";
	}
}
