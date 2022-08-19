using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
[RequireComponent(typeof(SpriteRenderer))]
public class WaterReflectableScript : MonoBehaviour
{
	// Token: 0x06000F91 RID: 3985 RVA: 0x0005D4AC File Offset: 0x0005B6AC
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

	// Token: 0x06000F92 RID: 3986 RVA: 0x0005D576 File Offset: 0x0005B776
	private void OnDestroy()
	{
		if (this.spriteRenderer != null)
		{
			Object.Destroy(this.spriteRenderer.gameObject);
		}
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x0005D598 File Offset: 0x0005B798
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

	// Token: 0x04000BA9 RID: 2985
	[Header("Reflect properties")]
	public Vector3 localPosition = new Vector3(0f, -0.25f, 0f);

	// Token: 0x04000BAA RID: 2986
	public Vector3 localRotation = new Vector3(0f, 0f, -180f);

	// Token: 0x04000BAB RID: 2987
	[Tooltip("Optionnal: force the reflected sprite. If null it will be a copy of the source.")]
	public Sprite sprite;

	// Token: 0x04000BAC RID: 2988
	public string spriteLayer = "Default";

	// Token: 0x04000BAD RID: 2989
	public int spriteLayerOrder = -5;

	// Token: 0x04000BAE RID: 2990
	private SpriteRenderer spriteSource;

	// Token: 0x04000BAF RID: 2991
	private SpriteRenderer spriteRenderer;
}
