using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;

[TaskCategory("Basic/Transform")]
[TaskDescription("Gets the Angle between a GameObject's forward direction and a target. Returns Success.")]
public class GetAngleToTarget : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The target object to measure the angle to. If null the targetPosition will be used.")]
	public SharedGameObject targetObject;

	[Tooltip("The world position to measure an angle to. If the targetObject is also not null, this value is used as an offset from that object's position.")]
	public SharedVector3 targetPosition;

	[Tooltip("Ignore height differences when calculating the angle?")]
	public SharedBool ignoreHeight = true;

	[Tooltip("The angle to the target")]
	[RequiredField]
	public SharedFloat storeValue;

	private Transform targetTransform;

	private GameObject prevGameObject;

	public override void OnStart()
	{
		GameObject defaultGameObject = ((Task)this).GetDefaultGameObject(((SharedVariable<GameObject>)targetGameObject).Value);
		if ((Object)(object)defaultGameObject != (Object)(object)prevGameObject)
		{
			targetTransform = defaultGameObject.GetComponent<Transform>();
			prevGameObject = defaultGameObject;
		}
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)targetTransform == (Object)null)
		{
			Debug.LogWarning((object)"Transform is null");
			return (TaskStatus)1;
		}
		Vector3 val = ((!((Object)(object)((SharedVariable<GameObject>)targetObject).Value != (Object)null)) ? ((SharedVariable<Vector3>)targetPosition).Value : ((SharedVariable<GameObject>)targetObject).Value.transform.InverseTransformPoint(((SharedVariable<Vector3>)targetPosition).Value));
		if (((SharedVariable<bool>)ignoreHeight).Value)
		{
			val.y = targetTransform.position.y;
		}
		Vector3 val2 = val - targetTransform.position;
		((SharedVariable<float>)storeValue).Value = Vector3.Angle(val2, targetTransform.forward);
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		targetObject = null;
		targetPosition = Vector3.zero;
		ignoreHeight = true;
		storeValue = 0f;
	}
}
