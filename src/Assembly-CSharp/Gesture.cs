using UnityEngine;

public class Gesture
{
	public int fingerIndex;

	public int touchCount;

	public Vector2 startPosition;

	public Vector2 position;

	public Vector2 deltaPosition;

	public float actionTime;

	public float deltaTime;

	public EasyTouch.SwipeType swipe;

	public float swipeLength;

	public Vector2 swipeVector;

	public float deltaPinch;

	public float twistAngle;

	public float twoFingerDistance;

	public GameObject pickObject;

	public GameObject otherReceiver;

	public bool isHoverReservedArea;

	public Vector3 GetTouchToWordlPoint(float z, bool worldZ = false)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (!worldZ)
		{
			return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(position.x, position.y, z));
		}
		return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(position.x, position.y, z - ((Component)EasyTouch.GetCamera()).transform.position.z));
	}

	public float GetSwipeOrDragAngle()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		return Mathf.Atan2(((Vector2)(ref swipeVector)).normalized.y, ((Vector2)(ref swipeVector)).normalized.x) * 57.29578f;
	}

	public bool IsInRect(Rect rect, bool guiRect = false)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (guiRect)
		{
			((Rect)(ref rect))._002Ector(((Rect)(ref rect)).x, (float)Screen.height - ((Rect)(ref rect)).y - ((Rect)(ref rect)).height, ((Rect)(ref rect)).width, ((Rect)(ref rect)).height);
		}
		return ((Rect)(ref rect)).Contains(position);
	}

	public Vector2 NormalizedPosition()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(100f / (float)Screen.width * position.x / 100f, 100f / (float)Screen.height * position.y / 100f);
	}
}
