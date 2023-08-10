using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics;

[TaskCategory("Basic/Physics")]
[TaskDescription("Casts a sphere against all colliders in the scene. Returns success if a collider was hit.")]
[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
public class SphereCast : Action
{
	[Tooltip("Starts the spherecast at the GameObject's position. If null the originPosition will be used")]
	public SharedGameObject originGameObject;

	[Tooltip("Starts the sherecast at the position. Only used if originGameObject is null")]
	public SharedVector3 originPosition;

	[Tooltip("The radius of the spherecast")]
	public SharedFloat radius;

	[Tooltip("The direction of the spherecast")]
	public SharedVector3 direction;

	[Tooltip("The length of the spherecast. Set to -1 for infinity")]
	public SharedFloat distance = -1f;

	[Tooltip("Selectively ignore colliders")]
	public LayerMask layerMask = LayerMask.op_Implicit(-1);

	[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified")]
	public Space space = (Space)1;

	[SharedRequired]
	[Tooltip("Stores the hit object of the spherecast")]
	public SharedGameObject storeHitObject;

	[SharedRequired]
	[Tooltip("Stores the hit point of the spherecast")]
	public SharedVector3 storeHitPoint;

	[SharedRequired]
	[Tooltip("Stores the hit normal of the spherecast")]
	public SharedVector3 storeHitNormal;

	[SharedRequired]
	[Tooltip("Stores the hit distance of the spherecast")]
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
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
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
		if (Physics.SphereCast(val2, ((SharedVariable<float>)radius).Value, val, ref val3, (((SharedVariable<float>)distance).Value == -1f) ? float.PositiveInfinity : ((SharedVariable<float>)distance).Value, LayerMask.op_Implicit(layerMask)))
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		originGameObject = null;
		originPosition = Vector3.zero;
		radius = 0f;
		direction = Vector3.zero;
		distance = -1f;
		layerMask = LayerMask.op_Implicit(-1);
		space = (Space)1;
	}
}
