using System;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class EntityProperties : MonoBehaviour
{
	// Token: 0x06002599 RID: 9625 RVA: 0x001043D6 File Offset: 0x001025D6
	private void Awake()
	{
		this.originalPosition = base.transform.localPosition;
		this.originalScale = base.transform.localScale;
	}

	// Token: 0x04001E4C RID: 7756
	public int Type;

	// Token: 0x04001E4D RID: 7757
	[HideInInspector]
	public Vector3 originalPosition;

	// Token: 0x04001E4E RID: 7758
	public int minimumLevel = 1;

	// Token: 0x04001E4F RID: 7759
	public int Verovatnoca = 100;

	// Token: 0x04001E50 RID: 7760
	[HideInInspector]
	public bool currentlyUsable;

	// Token: 0x04001E51 RID: 7761
	public bool DozvoljenoSkaliranje;

	// Token: 0x04001E52 RID: 7762
	[HideInInspector]
	public Vector3 originalScale;

	// Token: 0x04001E53 RID: 7763
	public int maxBrojPojavljivanja;

	// Token: 0x04001E54 RID: 7764
	[HideInInspector]
	public int brojPojavljivanja;

	// Token: 0x04001E55 RID: 7765
	public bool slobodanEntitet = true;

	// Token: 0x04001E56 RID: 7766
	public bool trenutnoJeAktivan;

	// Token: 0x04001E57 RID: 7767
	[HideInInspector]
	public bool smanjivanjeVerovatnoce;

	// Token: 0x04001E58 RID: 7768
	[HideInInspector]
	public bool instanciran;
}
