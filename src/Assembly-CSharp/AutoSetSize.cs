using System;
using UnityEngine;

// Token: 0x020002B5 RID: 693
[ExecuteInEditMode]
public class AutoSetSize : MonoBehaviour
{
	// Token: 0x06001507 RID: 5383 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000BCF38 File Offset: 0x000BB138
	private void Update()
	{
		float num = 2.1666667f;
		float num2 = (float)Screen.width / (float)Screen.height / num;
		base.transform.localScale = Vector3.one * num2;
		base.transform.localPosition = new Vector3((float)this.Startwidth, (float)this.StartHigh - (float)this.StartHigh * (1f - num2) * 2f, 0f);
	}

	// Token: 0x04001023 RID: 4131
	public int width = 2340;

	// Token: 0x04001024 RID: 4132
	public int higt = 1080;

	// Token: 0x04001025 RID: 4133
	public int StartHigh;

	// Token: 0x04001026 RID: 4134
	public int Startwidth;
}
