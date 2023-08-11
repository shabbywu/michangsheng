using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D;

[TaskCategory("Basic/Physics2D")]
[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
public class Linecast : Action
{
	[Tooltip("The starting position of the linecast.")]
	private SharedVector2 startPosition;

	[Tooltip("The ending position of the linecast.")]
	private SharedVector2 endPosition;

	[Tooltip("Selectively ignore colliders.")]
	public LayerMask layerMask = LayerMask.op_Implicit(-1);

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (RaycastHit2D.op_Implicit(Physics2D.Linecast(((SharedVariable<Vector2>)startPosition).Value, ((SharedVariable<Vector2>)endPosition).Value, LayerMask.op_Implicit(layerMask))))
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
		startPosition = Vector2.zero;
		endPosition = Vector2.zero;
		layerMask = LayerMask.op_Implicit(-1);
	}
}
