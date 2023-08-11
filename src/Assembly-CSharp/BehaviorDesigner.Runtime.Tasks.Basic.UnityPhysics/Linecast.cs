using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics;

[TaskCategory("Basic/Physics")]
[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
public class Linecast : Action
{
	[Tooltip("The starting position of the linecast")]
	private SharedVector3 startPosition;

	[Tooltip("The ending position of the linecast")]
	private SharedVector3 endPosition;

	[Tooltip("Selectively ignore colliders.")]
	public LayerMask layerMask = LayerMask.op_Implicit(-1);

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (Physics.Linecast(((SharedVariable<Vector3>)startPosition).Value, ((SharedVariable<Vector3>)endPosition).Value, LayerMask.op_Implicit(layerMask)))
		{
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		startPosition = Vector3.zero;
		endPosition = Vector3.zero;
		layerMask = LayerMask.op_Implicit(-1);
	}
}
