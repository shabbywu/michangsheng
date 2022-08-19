using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class HurtFlashEffect : MonoBehaviour
{
	// Token: 0x06000EF7 RID: 3831 RVA: 0x0005B260 File Offset: 0x00059460
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

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0005B2B8 File Offset: 0x000594B8
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

	// Token: 0x04000B32 RID: 2866
	private const int DefaultFlashCount = 3;

	// Token: 0x04000B33 RID: 2867
	public int flashCount = 3;

	// Token: 0x04000B34 RID: 2868
	public Color flashColor = Color.white;

	// Token: 0x04000B35 RID: 2869
	[Range(0.008333334f, 0.06666667f)]
	public float interval = 0.016666668f;

	// Token: 0x04000B36 RID: 2870
	public string fillPhaseProperty = "_FillPhase";

	// Token: 0x04000B37 RID: 2871
	public string fillColorProperty = "_FillColor";

	// Token: 0x04000B38 RID: 2872
	private MaterialPropertyBlock mpb;

	// Token: 0x04000B39 RID: 2873
	private MeshRenderer meshRenderer;
}
