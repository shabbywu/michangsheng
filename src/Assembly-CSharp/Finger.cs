using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class Finger
{
	// Token: 0x04000959 RID: 2393
	public int fingerIndex;

	// Token: 0x0400095A RID: 2394
	public int touchCount;

	// Token: 0x0400095B RID: 2395
	public Vector2 startPosition;

	// Token: 0x0400095C RID: 2396
	public Vector2 complexStartPosition;

	// Token: 0x0400095D RID: 2397
	public Vector2 position;

	// Token: 0x0400095E RID: 2398
	public Vector2 deltaPosition;

	// Token: 0x0400095F RID: 2399
	public Vector2 oldPosition;

	// Token: 0x04000960 RID: 2400
	public int tapCount;

	// Token: 0x04000961 RID: 2401
	public float deltaTime;

	// Token: 0x04000962 RID: 2402
	public TouchPhase phase;

	// Token: 0x04000963 RID: 2403
	public EasyTouch.GestureType gesture;

	// Token: 0x04000964 RID: 2404
	public GameObject pickedObject;
}
