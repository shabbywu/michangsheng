using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;

[TaskCategory("Basic/Transform")]
[TaskDescription("Rotates the transform so the forward vector points at worldPosition. Returns Success.")]
public class LookAt : Action
{
	[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
	public SharedGameObject targetGameObject;

	[Tooltip("The GameObject to look at. If null the world position will be used.")]
	public SharedGameObject targetLookAt;

	[Tooltip("Point to look at")]
	public SharedVector3 worldPosition;

	[Tooltip("Vector specifying the upward direction")]
	public Vector3 worldUp;

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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)targetTransform == (Object)null)
		{
			Debug.LogWarning((object)"Transform is null");
			return (TaskStatus)1;
		}
		if ((Object)(object)((SharedVariable<GameObject>)targetLookAt).Value != (Object)null)
		{
			targetTransform.LookAt(((SharedVariable<Vector3>)worldPosition).Value, worldUp);
		}
		else
		{
			targetTransform.LookAt(((SharedVariable<GameObject>)targetLookAt).Value.transform);
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		targetGameObject = null;
		targetLookAt = null;
		worldPosition = Vector3.up;
		worldUp = Vector3.up;
	}
}
