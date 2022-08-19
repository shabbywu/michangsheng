using System;
using UnityEngine;

// Token: 0x020004FA RID: 1274
[RequireComponent(typeof(TextMesh))]
public class TextMeshPlugin : MonoBehaviour
{
	// Token: 0x06002947 RID: 10567 RVA: 0x0013B8AC File Offset: 0x00139AAC
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

	// Token: 0x04002571 RID: 9585
	public TextMeshPlugin.TextEffect textEffect = TextMeshPlugin.TextEffect.Outline;

	// Token: 0x04002572 RID: 9586
	public float outlineOffset = 0.05f;

	// Token: 0x04002573 RID: 9587
	public float singleOutlineOffset = 1.1f;

	// Token: 0x04002574 RID: 9588
	public Vector2 shadowPosition;

	// Token: 0x04002575 RID: 9589
	public Color effectColor = Color.black;

	// Token: 0x04002576 RID: 9590
	private TextMesh thisComponent;

	// Token: 0x02001477 RID: 5239
	public enum TextEffect
	{
		// Token: 0x04006C1E RID: 27678
		None,
		// Token: 0x04006C1F RID: 27679
		Shadow,
		// Token: 0x04006C20 RID: 27680
		Outline,
		// Token: 0x04006C21 RID: 27681
		SingleOutline
	}
}
