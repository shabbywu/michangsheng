using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200034E RID: 846
public class AutoSVContentHeight : MonoBehaviour
{
	// Token: 0x06001CCA RID: 7370 RVA: 0x000CDD05 File Offset: 0x000CBF05
	private void Start()
	{
		this.rt = (base.transform as RectTransform);
		this.ver = base.GetComponent<VerticalLayoutGroup>();
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x000CDD24 File Offset: 0x000CBF24
	private void Update()
	{
		int childCount = this.rt.childCount;
		if (this.ChildCountMode && this.lastChildCount == childCount)
		{
			return;
		}
		float num = 0f;
		for (int i = 0; i < childCount; i++)
		{
			num += (this.rt.GetChild(i) as RectTransform).sizeDelta.y;
			if (i < childCount - 1 && this.ver != null)
			{
				num += this.ver.spacing;
			}
		}
		this.lastChildCount = childCount;
		if (this.rt.sizeDelta.y != num + (float)this.ExHeigt)
		{
			this.rt.sizeDelta = new Vector2(this.rt.sizeDelta.x, num + (float)this.ExHeigt);
			this.rt.anchoredPosition = new Vector2(this.rt.anchoredPosition.x, 0f);
			if (this.ver != null && this.IsLimitSliding)
			{
				if (num < (this.SR.transform as RectTransform).sizeDelta.y)
				{
					this.SR.vertical = false;
					return;
				}
				this.SR.vertical = true;
			}
		}
	}

	// Token: 0x04001756 RID: 5974
	private VerticalLayoutGroup ver;

	// Token: 0x04001757 RID: 5975
	private RectTransform rt;

	// Token: 0x04001758 RID: 5976
	private int lastChildCount;

	// Token: 0x04001759 RID: 5977
	[Header("额外高度")]
	public int ExHeigt;

	// Token: 0x0400175A RID: 5978
	[Header("是否现在滑动")]
	[Tooltip("当子物体高度小于SV高度时，禁止滑动")]
	public bool IsLimitSliding;

	// Token: 0x0400175B RID: 5979
	public ScrollRect SR;

	// Token: 0x0400175C RID: 5980
	[Tooltip("根据物体数量的变动进行高度刷新。关闭的话会一直检测")]
	public bool ChildCountMode = true;
}
