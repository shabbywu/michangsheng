using System;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class AutoSetHeightByOther : MonoBehaviour
{
	// Token: 0x06001CC4 RID: 7364 RVA: 0x000CDC84 File Offset: 0x000CBE84
	private void Start()
	{
		this.self = base.GetComponent<RectTransform>();
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x000CDC92 File Offset: 0x000CBE92
	private void Update()
	{
		this.self.sizeDelta = new Vector2(this.Width, this.Other.sizeDelta.y + this.ExHeight);
	}

	// Token: 0x0400174E RID: 5966
	[Header("高度参考")]
	public RectTransform Other;

	// Token: 0x0400174F RID: 5967
	[Header("宽度")]
	public float Width;

	// Token: 0x04001750 RID: 5968
	[Header("额外高度")]
	public float ExHeight;

	// Token: 0x04001751 RID: 5969
	private RectTransform self;
}
