using System;
using UnityEngine;

// Token: 0x0200030C RID: 780
[Obsolete]
public class MoveController : MonoBehaviour
{
	// Token: 0x040012A2 RID: 4770
	private Animator animator;

	// Token: 0x040012A3 RID: 4771
	private SmoothFollow sf;

	// Token: 0x040012A4 RID: 4772
	private Transform moveDes;

	// Token: 0x040012A5 RID: 4773
	private bool hasDes;

	// Token: 0x040012A6 RID: 4774
	private float minLen;

	// Token: 0x040012A7 RID: 4775
	private int skillId = 1;

	// Token: 0x040012A8 RID: 4776
	private GameObject UIGame;

	// Token: 0x040012A9 RID: 4777
	private GameEntity gameEntity;
}
