using System;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class Finger
{
	// Token: 0x04000BD6 RID: 3030
	public int fingerIndex;

	// Token: 0x04000BD7 RID: 3031
	public int touchCount;

	// Token: 0x04000BD8 RID: 3032
	public Vector2 startPosition;

	// Token: 0x04000BD9 RID: 3033
	public Vector2 complexStartPosition;

	// Token: 0x04000BDA RID: 3034
	public Vector2 position;

	// Token: 0x04000BDB RID: 3035
	public Vector2 deltaPosition;

	// Token: 0x04000BDC RID: 3036
	public Vector2 oldPosition;

	// Token: 0x04000BDD RID: 3037
	public int tapCount;

	// Token: 0x04000BDE RID: 3038
	public float deltaTime;

	// Token: 0x04000BDF RID: 3039
	public TouchPhase phase;

	// Token: 0x04000BE0 RID: 3040
	public EasyTouch.GestureType gesture;

	// Token: 0x04000BE1 RID: 3041
	public GameObject pickedObject;
}
