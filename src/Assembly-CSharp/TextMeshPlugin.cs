using System;
using UnityEngine;

// Token: 0x02000787 RID: 1927
[RequireComponent(typeof(TextMesh))]
public class TextMeshPlugin : MonoBehaviour
{
	// Token: 0x0600313A RID: 12602 RVA: 0x00188BBC File Offset: 0x00186DBC
	private void Start()
	{
		this.thisComponent = base.GetComponent<TextMesh>();
		if (this.textEffect == TextMeshPlugin.TextEffect.Outline)
		{
			this.thisComponent.AddOutline(this.effectColor, this.outlineOffset);
			return;
		}
		if (this.textEffect == TextMeshPlugin.TextEffect.Shadow)
		{
			this.thisComponent.AddShadow(this.effectColor, this.shadowPosition);
			return;
		}
		if (this.textEffect == TextMeshPlugin.TextEffect.SingleOutline)
		{
			this.thisComponent.AddSingleOutline(this.effectColor, this.singleOutlineOffset);
		}
	}

	// Token: 0x04002D4C RID: 11596
	public TextMeshPlugin.TextEffect textEffect = TextMeshPlugin.TextEffect.Outline;

	// Token: 0x04002D4D RID: 11597
	public float outlineOffset = 0.05f;

	// Token: 0x04002D4E RID: 11598
	public float singleOutlineOffset = 1.1f;

	// Token: 0x04002D4F RID: 11599
	public Vector2 shadowPosition;

	// Token: 0x04002D50 RID: 11600
	public Color effectColor = Color.black;

	// Token: 0x04002D51 RID: 11601
	private TextMesh thisComponent;

	// Token: 0x02000788 RID: 1928
	public enum TextEffect
	{
		// Token: 0x04002D53 RID: 11603
		None,
		// Token: 0x04002D54 RID: 11604
		Shadow,
		// Token: 0x04002D55 RID: 11605
		Outline,
		// Token: 0x04002D56 RID: 11606
		SingleOutline
	}
}
