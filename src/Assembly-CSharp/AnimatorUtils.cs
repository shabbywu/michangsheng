using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000484 RID: 1156
public class AnimatorUtils : MonoBehaviour
{
	// Token: 0x06001EE4 RID: 7908 RVA: 0x00019999 File Offset: 0x00017B99
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x000199B3 File Offset: 0x00017BB3
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

	// Token: 0x04001A4D RID: 6733
	public bool iscomplete;

	// Token: 0x04001A4E RID: 6734
	private SpriteRenderer spriteRenderer;

	// Token: 0x04001A4F RID: 6735
	private Image image;

	// Token: 0x04001A50 RID: 6736
	public UnityAction completeCallBack;
}
