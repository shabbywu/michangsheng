using UnityEngine;

namespace Spine.Unity.Examples;

public class DraggableTransform : MonoBehaviour
{
	private Vector2 mousePreviousWorld;

	private Vector2 mouseDeltaWorld;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void Update()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Vector2.op_Implicit(Input.mousePosition);
		Vector2 val2 = Vector2.op_Implicit(mainCamera.ScreenToWorldPoint(new Vector3(val.x, val.y, 0f - ((Component)mainCamera).transform.position.z)));
		mouseDeltaWorld = val2 - mousePreviousWorld;
		mousePreviousWorld = val2;
	}

	private void OnMouseDrag()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.Translate(Vector2.op_Implicit(mouseDeltaWorld));
	}
}
