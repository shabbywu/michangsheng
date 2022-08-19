using System;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class Gesture
{
	// Token: 0x06000D69 RID: 3433 RVA: 0x00050940 File Offset: 0x0004EB40
	public Vector3 GetTouchToWordlPoint(float z, bool worldZ = false)
	{
		if (!worldZ)
		{
			return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z));
		}
		return EasyTouch.GetCamera().ScreenToWorldPoint(new Vector3(this.position.x, this.position.y, z - EasyTouch.GetCamera().transform.position.z));
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x000509B2 File Offset: 0x0004EBB2
	public float GetSwipeOrDragAngle()
	{
		return Mathf.Atan2(this.swipeVector.normalized.y, this.swipeVector.normalized.x) * 57.29578f;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x000509E0 File Offset: 0x0004EBE0
	public bool IsInRect(Rect rect, bool guiRect = false)
	{
		if (guiRect)
		{
			rect..ctor(rect.x, (float)Screen.height - rect.y - rect.height, rect.width, rect.height);
		}
		return rect.Contains(this.position);
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00050A30 File Offset: 0x0004EC30
	public Vector2 NormalizedPosition()
	{
		return new Vector2(100f / (float)Screen.width * this.position.x / 100f, 100f / (float)Screen.height * this.position.y / 100f);
	}

	// Token: 0x04000965 RID: 2405
	public int fingerIndex;

	// Token: 0x04000966 RID: 2406
	public int touchCount;

	// Token: 0x04000967 RID: 2407
	public Vector2 startPosition;

	// Token: 0x04000968 RID: 2408
	public Vector2 position;

	// Token: 0x04000969 RID: 2409
	public Vector2 deltaPosition;

	// Token: 0x0400096A RID: 2410
	public float actionTime;

	// Token: 0x0400096B RID: 2411
	public float deltaTime;

	// Token: 0x0400096C RID: 2412
	public EasyTouch.SwipeType swipe;

	// Token: 0x0400096D RID: 2413
	public float swipeLength;

	// Token: 0x0400096E RID: 2414
	public Vector2 swipeVector;

	// Token: 0x0400096F RID: 2415
	public float deltaPinch;

	// Token: 0x04000970 RID: 2416
	public float twistAngle;

	// Token: 0x04000971 RID: 2417
	public float twoFingerDistance;

	// Token: 0x04000972 RID: 2418
	public GameObject pickObject;

	// Token: 0x04000973 RID: 2419
	public GameObject otherReceiver;

	// Token: 0x04000974 RID: 2420
	public bool isHoverReservedArea;
}
