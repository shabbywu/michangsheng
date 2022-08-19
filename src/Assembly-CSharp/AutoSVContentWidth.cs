using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034F RID: 847
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class AutoSVContentWidth : MonoBehaviour
{
	// Token: 0x06001CCD RID: 7373 RVA: 0x000CDE6F File Offset: 0x000CC06F
	private void Start()
	{
		this.rt = (base.transform as RectTransform);
		this.hor = base.GetComponent<HorizontalLayoutGroup>();
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x000CDE90 File Offset: 0x000CC090
	private void Update()
	{
		int childCount = this.rt.childCount;
		if (this.lastChildCount != childCount)
		{
			float num = 0f;
			for (int i = 0; i < childCount; i++)
			{
				num += (this.rt.GetChild(i) as RectTransform).sizeDelta.x;
				if (i < childCount - 1)
				{
					num += this.hor.spacing;
				}
			}
			this.lastChildCount = childCount;
			this.rt.sizeDelta = new Vector2(num + (float)this.ExWidth, this.rt.sizeDelta.y);
			this.rt.anchoredPosition = new Vector2(0f, this.rt.anchoredPosition.y);
			if (this.IsLimitSliding)
			{
				if (num < (this.SR.transform as RectTransform).sizeDelta.x)
				{
					this.SR.horizontal = false;
					return;
				}
				this.SR.horizontal = true;
			}
		}
	}

	// Token: 0x0400175D RID: 5981
	private HorizontalLayoutGroup hor;

	// Token: 0x0400175E RID: 5982
	private RectTransform rt;

	// Token: 0x0400175F RID: 5983
	private int lastChildCount;

	// Token: 0x04001760 RID: 5984
	[Header("额外宽度")]
	public int ExWidth;

	// Token: 0x04001761 RID: 5985
	[Header("是否现在滑动")]
	[Tooltip("当子物体宽度小于SV宽度时，禁止滑动")]
	public bool IsLimitSliding;

	// Token: 0x04001762 RID: 5986
	public ScrollRect SR;
}
