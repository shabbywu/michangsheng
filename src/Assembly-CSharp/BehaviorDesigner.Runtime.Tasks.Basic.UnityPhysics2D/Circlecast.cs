using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D;

[TaskCategory("Basic/Physics2D")]
[TaskDescription("Casts a circle against all colliders in the scene. Returns success if a collider was hit.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
public class Circlecast : Action
{
	[Tooltip("Starts the circlecast at the GameObject's position. If null the originPosition will be used.")]
	public SharedGameObject originGameObject;

	[Tooltip("Starts the circlecast at the position. Only used if originGameObject is null.")]
	public SharedVector2 originPosition;

	[Tooltip("The radius of the circlecast")]
	public SharedFloat radius;

	[Tooltip("The direction of the circlecast")]
	public SharedVector2 direction;

	[Tooltip("The length of the ray. Set to -1 for infinity.")]
	public SharedFloat distance = -1f;

	[Tooltip("Selectively ignore colliders.")]
	public LayerMask layerMask = LayerMask.op_Implicit(-1);

	[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified.")]
	public Space space = (Space)1;

	[SharedRequired]
	[Tooltip("Stores the hit object of the circlecast.")]
	public SharedGameObject storeHitObject;

	[SharedRequired]
	[Tooltip("Stores the hit point of the circlecast.")]
	public SharedVector2 storeHitPoint;

	[SharedRequired]
	[Tooltip("Stores the hit normal of the circlecast.")]
	public SharedVector2 storeHitNormal;

	[SharedRequired]
	[Tooltip("Stores the hit distance of the circlecast.")]
	public SharedFloat storeHitDistance;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = ((SharedVariable<Vector2>)direction).Value;
		Vector2 val2;
		if ((Object)(object)((SharedVariable<GameObject>)originGameObject).Value != (Object)null)
		{
			val2 = Vector2.op_Implicit(((SharedVariable<GameObject>)originGameObject).Value.transform.position);
			if ((int)space == 1)
			{
				val = Vector2.op_Implicit(((SharedVariable<GameObject>)originGameObject).Value.transform.TransformDirection(Vector2.op_Implicit(((SharedVariable<Vector2>)direction).Value)));
			}
		}
		else
		{
			val2 = ((SharedVariable<Vector2>)originPosition).Value;
		}
		RaycastHit2D val3 = Physics2D.CircleCast(val2, ((SharedVariable<float>)radius).Value, val, (((SharedVariable<float>)distance).Value == -1f) ? float.PositiveInfinity : ((SharedVariable<float>)distance).Value, LayerMask.op_Implicit(layerMask));
		if ((Object)(object)((RaycastHit2D)(ref val3)).collider != (Object)null)
		{
			((SharedVariable<GameObject>)storeHitObject).Value = ((Component)((RaycastHit2D)(ref val3)).collider).gameObject;
			((SharedVariable<Vector2>)storeHitPoint).Value = ((RaycastHit2D)(ref val3)).point;
			((SharedVariable<Vector2>)storeHitNormal).Value = ((RaycastHit2D)(ref val3)).normal;
			((SharedVariable<float>)storeHitDistance).Value = ((RaycastHit2D)(ref val3)).distance;
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		originGameObject = null;
		originPosition = Vector2.zero;
		direction = Vector2.zero;
		radius = 0f;
		distance = -1f;
		layerMask = LayerMask.op_Implicit(-1);
		space = (Space)1;
	}
}
