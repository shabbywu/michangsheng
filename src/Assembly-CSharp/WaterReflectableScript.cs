using System;
using UnityEngine;

// Token: 0x0200024A RID: 586
[RequireComponent(typeof(SpriteRenderer))]
public class WaterReflectableScript : MonoBehaviour
{
	// Token: 0x060011EF RID: 4591 RVA: 0x000ACFA4 File Offset: 0x000AB1A4
	private void Awake()
	{
		GameObject gameObject = new GameObject("Water Reflect");
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = this.localPosition;
		gameObject.transform.localRotation = Quaternion.Euler(this.localRotation);
		gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		this.spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		this.spriteRenderer.sortingLayerName = this.spriteLayer;
		this.spriteRenderer.sortingOrder = this.spriteLayerOrder;
		this.spriteSource = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x00011353 File Offset: 0x0000F553
	private void OnDestroy()
	{
		if (this.spriteRenderer != null)
		{
			Object.Destroy(this.spriteRenderer.gameObject);
		}
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x000AD070 File Offset: 0x000AB270
	private void LateUpdate()
	{
		if (this.spriteSource != null)
		{
			if (this.sprite == null)
			{
				this.spriteRenderer.sprite = this.spriteSource.sprite;
			}
			else
			{
				this.spriteRenderer.sprite = this.sprite;
			}
			this.spriteRenderer.flipX = this.spriteSource.flipX;
			this.spriteRenderer.flipY = this.spriteSource.flipY;
			this.spriteRenderer.color = this.spriteSource.color;
		}
	}

	// Token: 0x04000E79 RID: 3705
	[Header("Reflect properties")]
	public Vector3 localPosition = new Vector3(0f, -0.25f, 0f);

	// Token: 0x04000E7A RID: 3706
	public Vector3 localRotation = new Vector3(0f, 0f, -180f);

	// Token: 0x04000E7B RID: 3707
	[Tooltip("Optionnal: force the reflected sprite. If null it will be a copy of the source.")]
	public Sprite sprite;

	// Token: 0x04000E7C RID: 3708
	public string spriteLayer = "Default";

	// Token: 0x04000E7D RID: 3709
	public int spriteLayerOrder = -5;

	// Token: 0x04000E7E RID: 3710
	private SpriteRenderer spriteSource;

	// Token: 0x04000E7F RID: 3711
	private SpriteRenderer spriteRenderer;
}
