using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
[Obsolete]
public class MoveController : MonoBehaviour
{
	// Token: 0x04000F5C RID: 3932
	private Animator animator;

	// Token: 0x04000F5D RID: 3933
	private SmoothFollow sf;

	// Token: 0x04000F5E RID: 3934
	private Transform moveDes;

	// Token: 0x04000F5F RID: 3935
	private bool hasDes;

	// Token: 0x04000F60 RID: 3936
	private float minLen;

	// Token: 0x04000F61 RID: 3937
	private int skillId = 1;

	// Token: 0x04000F62 RID: 3938
	private GameObject UIGame;

	// Token: 0x04000F63 RID: 3939
	private GameEntity gameEntity;
}
