using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020005FF RID: 1535
public class FightScreenShake : MonoBehaviour
{
	// Token: 0x06002668 RID: 9832 RVA: 0x0001E9BC File Offset: 0x0001CBBC
	private void Start()
	{
		this.normalPosition = base.transform.position;
		this.normalRotation = base.transform.rotation;
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x0012E544 File Offset: 0x0012C744
	public void Shake()
	{
		ShortcutExtensions.DOShakePosition(base.transform, 0.3f, this.ShakePosition, 10, 90f, false, true).onComplete = delegate()
		{
			base.transform.position = this.normalPosition;
		};
		ShortcutExtensions.DOShakeRotation(base.transform, 0.3f, this.ShakeRotation, 10, 90f, true).onComplete = delegate()
		{
			base.transform.rotation = this.normalRotation;
		};
	}

	// Token: 0x040020C7 RID: 8391
	public Vector3 ShakePosition = new Vector3(0.6f, 0.3f, 0f);

	// Token: 0x040020C8 RID: 8392
	public Vector3 ShakeRotation = new Vector3(1f, 0.7f, 0f);

	// Token: 0x040020C9 RID: 8393
	private Vector3 normalPosition;

	// Token: 0x040020CA RID: 8394
	private Quaternion normalRotation;
}
