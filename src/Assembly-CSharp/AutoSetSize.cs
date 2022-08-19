using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
[ExecuteInEditMode]
public class AutoSetSize : MonoBehaviour
{
	// Token: 0x0600125C RID: 4700 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x0006F420 File Offset: 0x0006D620
	private void Update()
	{
		float num = 2.1666667f;
		float num2 = (float)Screen.width / (float)Screen.height / num;
		base.transform.localScale = Vector3.one * num2;
		base.transform.localPosition = new Vector3((float)this.Startwidth, (float)this.StartHigh - (float)this.StartHigh * (1f - num2) * 2f, 0f);
	}

	// Token: 0x04000CFB RID: 3323
	public int width = 2340;

	// Token: 0x04000CFC RID: 3324
	public int higt = 1080;

	// Token: 0x04000CFD RID: 3325
	public int StartHigh;

	// Token: 0x04000CFE RID: 3326
	public int Startwidth;
}
