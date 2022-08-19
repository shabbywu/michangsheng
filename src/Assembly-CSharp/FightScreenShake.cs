using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class FightScreenShake : MonoBehaviour
{
	// Token: 0x060022AB RID: 8875 RVA: 0x000ED853 File Offset: 0x000EBA53
	private void Start()
	{
		this.normalPosition = base.transform.position;
		this.normalRotation = base.transform.rotation;
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x000ED878 File Offset: 0x000EBA78
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

	// Token: 0x04001BFB RID: 7163
	public Vector3 ShakePosition = new Vector3(0.6f, 0.3f, 0f);

	// Token: 0x04001BFC RID: 7164
	public Vector3 ShakeRotation = new Vector3(1f, 0.7f, 0f);

	// Token: 0x04001BFD RID: 7165
	private Vector3 normalPosition;

	// Token: 0x04001BFE RID: 7166
	private Quaternion normalRotation;
}
