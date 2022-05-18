using System;
using UnityEngine;

// Token: 0x0200068A RID: 1674
public class EntityProperties : MonoBehaviour
{
	// Token: 0x060029D5 RID: 10709 RVA: 0x000207FC File Offset: 0x0001E9FC
	private void Awake()
	{
		this.originalPosition = base.transform.localPosition;
		this.originalScale = base.transform.localScale;
	}

	// Token: 0x04002372 RID: 9074
	public int Type;

	// Token: 0x04002373 RID: 9075
	[HideInInspector]
	public Vector3 originalPosition;

	// Token: 0x04002374 RID: 9076
	public int minimumLevel = 1;

	// Token: 0x04002375 RID: 9077
	public int Verovatnoca = 100;

	// Token: 0x04002376 RID: 9078
	[HideInInspector]
	public bool currentlyUsable;

	// Token: 0x04002377 RID: 9079
	public bool DozvoljenoSkaliranje;

	// Token: 0x04002378 RID: 9080
	[HideInInspector]
	public Vector3 originalScale;

	// Token: 0x04002379 RID: 9081
	public int maxBrojPojavljivanja;

	// Token: 0x0400237A RID: 9082
	[HideInInspector]
	public int brojPojavljivanja;

	// Token: 0x0400237B RID: 9083
	public bool slobodanEntitet = true;

	// Token: 0x0400237C RID: 9084
	public bool trenutnoJeAktivan;

	// Token: 0x0400237D RID: 9085
	[HideInInspector]
	public bool smanjivanjeVerovatnoce;

	// Token: 0x0400237E RID: 9086
	[HideInInspector]
	public bool instanciran;
}
