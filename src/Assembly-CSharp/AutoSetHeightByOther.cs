using System;
using UnityEngine;

// Token: 0x020004C8 RID: 1224
public class AutoSetHeightByOther : MonoBehaviour
{
	// Token: 0x0600202B RID: 8235 RVA: 0x0001A60A File Offset: 0x0001880A
	private void Start()
	{
		this.self = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x0001A618 File Offset: 0x00018818
	private void Update()
	{
		this.self.sizeDelta = new Vector2(this.Width, this.Other.sizeDelta.y + this.ExHeight);
	}

	// Token: 0x04001BA4 RID: 7076
	[Header("高度参考")]
	public RectTransform Other;

	// Token: 0x04001BA5 RID: 7077
	[Header("宽度")]
	public float Width;

	// Token: 0x04001BA6 RID: 7078
	[Header("额外高度")]
	public float ExHeight;

	// Token: 0x04001BA7 RID: 7079
	private RectTransform self;
}
