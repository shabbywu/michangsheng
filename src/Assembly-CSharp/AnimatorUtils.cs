using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200031F RID: 799
public class AnimatorUtils : MonoBehaviour
{
	// Token: 0x06001BA4 RID: 7076 RVA: 0x000C5130 File Offset: 0x000C3330
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x000C514A File Offset: 0x000C334A
	private void Update()
	{
		if (this.iscomplete)
		{
			this.completeCallBack.Invoke();
			Object.Destroy(base.gameObject);
			return;
		}
		this.image.sprite = this.spriteRenderer.sprite;
	}

	// Token: 0x04001628 RID: 5672
	public bool iscomplete;

	// Token: 0x04001629 RID: 5673
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400162A RID: 5674
	private Image image;

	// Token: 0x0400162B RID: 5675
	public UnityAction completeCallBack;
}
