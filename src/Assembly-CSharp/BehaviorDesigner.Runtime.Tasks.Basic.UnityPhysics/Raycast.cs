using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics;

[TaskCategory("Basic/Physics")]
[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
public class Raycast : Action
{
	[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used")]
	public SharedGameObject originGameObject;

	[Tooltip("Starts the ray at the position. Only used if originGameObject is null")]
	public SharedVector3 originPosition;

	[Tooltip("The direction of the ray")]
	public SharedVector3 direction;

	[Tooltip("The length of the ray. Set to -1 for infinity")]
	public SharedFloat distance = -1f;

	[Tooltip("Selectively ignore colliders")]
	public LayerMask layerMask = LayerMask.op_Implicit(-1);

	[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified")]
	public Space space = (Space)1;

	[SharedRequired]
	[Tooltip("Stores the hit object of the raycast")]
	public SharedGameObject storeHitObject;

	[SharedRequired]
	[Tooltip("Stores the hit point of the raycast")]
	public SharedVector3 storeHitPoint;

	[SharedRequired]
	[Tooltip("Stores the hit normal of the raycast")]
	public SharedVector3 storeHitNormal;

	[SharedRequired]
	[Tooltip("Stores the hit distance of the raycast")]
	public SharedFloat storeHitDistance;

	public override TaskStatus OnUpdate()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Invalid comparison between Unknown and I4
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((SharedVariable<Vector3>)direction).Value;
		Vector3 val2;
		if ((Object)(object)((SharedVariable<GameObject>)originGameObject).Value != (Object)null)
		{
			val2 = ((SharedVariable<GameObject>)originGameObject).Value.transform.position;
			if ((int)space == 1)
			{
				val = ((SharedVariable<GameObject>)originGameObject).Value.transform.TransformDirection(((SharedVariable<Vector3>)direction).Value);
			}
		}
		else
		{
			val2 = ((SharedVariable<Vector3>)originPosition).Value;
		}
		RaycastHit val3 = default(RaycastHit);
		if (Physics.Raycast(val2, val, ref val3, (((SharedVariable<float>)distance).Value == -1f) ? float.PositiveInfinity : ((SharedVariable<float>)distance).Value, LayerMask.op_Implicit(layerMask)))
		{
			((SharedVariable<GameObject>)storeHitObject).Value = ((Component)((RaycastHit)(ref val3)).collider).gameObject;
			((SharedVariable<Vector3>)storeHitPoint).Value = ((RaycastHit)(ref val3)).point;
			((SharedVariable<Vector3>)storeHitNormal).Value = ((RaycastHit)(ref val3)).normal;
			((SharedVariable<float>)storeHitDistance).Value = ((RaycastHit)(ref val3)).distance;
			return (TaskStatus)2;
		}
		return (TaskStatus)1;
	}

	public override void OnReset()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		originGameObject = null;
		originPosition = Vector3.zero;
		direction = Vector3.zero;
		distance = -1f;
		layerMask = LayerMask.op_Implicit(-1);
		space = (Space)1;
	}
}
