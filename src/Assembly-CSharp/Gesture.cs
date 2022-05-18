using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class Gesture
{
	// Token: 0x06000F3E RID: 3902 RVA: 0x000A16F4 File Offset: 0x0009F8F4
	public Vector3 GetTouchToWordlPoint(float z, bool worldZ = false)
	{
		if (!worldZ)
		{
			return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z));
		}
		return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z - EasyTouch.GetCamera().transform.position.z));
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x0000F897 File Offset: 0x0000DA97
	public float GetSwipeOrDragAngle()
	{
		return Mathf.Atan2(this.swipeVector.normalized.y, this.swipeVector.normalized.x) * 57.29578f;
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x000A1768 File Offset: 0x0009F968
	public bool IsInRect(Rect rect, bool guiRect = false)
	{
		if (guiRect)
		{
			rect..ctor(rect.x, (float)Screen.height - rect.y - rect.height, rect.width, rect.height);
		}
		return rect.Contains(this.position);
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x000A17B8 File Offset: 0x0009F9B8
	public Vector2 NormalizedPosition()
	{
		return new Vector2(100f / (float)Screen.width * this.position.x / 100f, 100f / (float)Screen.height * this.position.y / 100f);
	}

	// Token: 0x04000BE2 RID: 3042
	public int fingerIndex;

	// Token: 0x04000BE3 RID: 3043
	public int touchCount;

	// Token: 0x04000BE4 RID: 3044
	public Vector2 startPosition;

	// Token: 0x04000BE5 RID: 3045
	public Vector2 position;

	// Token: 0x04000BE6 RID: 3046
	public Vector2 deltaPosition;

	// Token: 0x04000BE7 RID: 3047
	public float actionTime;

	// Token: 0x04000BE8 RID: 3048
	public float deltaTime;

	// Token: 0x04000BE9 RID: 3049
	public EasyTouch.SwipeType swipe;

	// Token: 0x04000BEA RID: 3050
	public float swipeLength;

	// Token: 0x04000BEB RID: 3051
	public Vector2 swipeVector;

	// Token: 0x04000BEC RID: 3052
	public float deltaPinch;

	// Token: 0x04000BED RID: 3053
	public float twistAngle;

	// Token: 0x04000BEE RID: 3054
	public float twoFingerDistance;

	// Token: 0x04000BEF RID: 3055
	public GameObject pickObject;

	// Token: 0x04000BF0 RID: 3056
	public GameObject otherReceiver;

	// Token: 0x04000BF1 RID: 3057
	public bool isHoverReservedArea;
}
