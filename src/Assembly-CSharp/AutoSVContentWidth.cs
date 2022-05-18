using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CB RID: 1227
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class AutoSVContentWidth : MonoBehaviour
{
	// Token: 0x06002034 RID: 8244 RVA: 0x0001A6B9 File Offset: 0x000188B9
	private void Start()
	{
		this.rt = (base.transform as RectTransform);
		this.hor = base.GetComponent<HorizontalLayoutGroup>();
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x00112E6C File Offset: 0x0011106C
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

	// Token: 0x04001BB3 RID: 7091
	private HorizontalLayoutGroup hor;

	// Token: 0x04001BB4 RID: 7092
	private RectTransform rt;

	// Token: 0x04001BB5 RID: 7093
	private int lastChildCount;

	// Token: 0x04001BB6 RID: 7094
	[Header("额外宽度")]
	public int ExWidth;

	// Token: 0x04001BB7 RID: 7095
	[Header("是否现在滑动")]
	[Tooltip("当子物体宽度小于SV宽度时，禁止滑动")]
	public bool IsLimitSliding;

	// Token: 0x04001BB8 RID: 7096
	public ScrollRect SR;
}
