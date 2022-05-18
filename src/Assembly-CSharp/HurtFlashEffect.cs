using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class HurtFlashEffect : MonoBehaviour
{
	// Token: 0x0600111D RID: 4381 RVA: 0x000AB2CC File Offset: 0x000A94CC
	public void Flash()
	{
		if (this.mpb == null)
		{
			this.mpb = new MaterialPropertyBlock();
		}
		if (this.meshRenderer == null)
		{
			this.meshRenderer = base.GetComponent<MeshRenderer>();
		}
		this.meshRenderer.GetPropertyBlock(this.mpb);
		base.StartCoroutine(this.FlashRoutine());
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00010B29 File Offset: 0x0000ED29
	private IEnumerator FlashRoutine()
	{
		if (this.flashCount < 0)
		{
			this.flashCount = 3;
		}
		int fillPhase = Shader.PropertyToID(this.fillPhaseProperty);
		int fillColor = Shader.PropertyToID(this.fillColorProperty);
		WaitForSeconds wait = new WaitForSeconds(this.interval);
		int num;
		for (int i = 0; i < this.flashCount; i = num + 1)
		{
			this.mpb.SetColor(fillColor, this.flashColor);
			this.mpb.SetFloat(fillPhase, 1f);
			this.meshRenderer.SetPropertyBlock(this.mpb);
			yield return wait;
			this.mpb.SetFloat(fillPhase, 0f);
			this.meshRenderer.SetPropertyBlock(this.mpb);
			yield return wait;
			num = i;
		}
		yield return null;
		yield break;
	}

	// Token: 0x04000DD0 RID: 3536
	private const int DefaultFlashCount = 3;

	// Token: 0x04000DD1 RID: 3537
	public int flashCount = 3;

	// Token: 0x04000DD2 RID: 3538
	public Color flashColor = Color.white;

	// Token: 0x04000DD3 RID: 3539
	[Range(0.008333334f, 0.06666667f)]
	public float interval = 0.016666668f;

	// Token: 0x04000DD4 RID: 3540
	public string fillPhaseProperty = "_FillPhase";

	// Token: 0x04000DD5 RID: 3541
	public string fillColorProperty = "_FillColor";

	// Token: 0x04000DD6 RID: 3542
	private MaterialPropertyBlock mpb;

	// Token: 0x04000DD7 RID: 3543
	private MeshRenderer meshRenderer;
}
