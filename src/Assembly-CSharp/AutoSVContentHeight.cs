using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004CA RID: 1226
public class AutoSVContentHeight : MonoBehaviour
{
	// Token: 0x06002031 RID: 8241 RVA: 0x0001A68B File Offset: 0x0001888B
	private void Start()
	{
		this.rt = (base.transform as RectTransform);
		this.ver = base.GetComponent<VerticalLayoutGroup>();
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x00112D30 File Offset: 0x00110F30
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

	// Token: 0x04001BAC RID: 7084
	private VerticalLayoutGroup ver;

	// Token: 0x04001BAD RID: 7085
	private RectTransform rt;

	// Token: 0x04001BAE RID: 7086
	private int lastChildCount;

	// Token: 0x04001BAF RID: 7087
	[Header("额外高度")]
	public int ExHeigt;

	// Token: 0x04001BB0 RID: 7088
	[Header("是否现在滑动")]
	[Tooltip("当子物体高度小于SV高度时，禁止滑动")]
	public bool IsLimitSliding;

	// Token: 0x04001BB1 RID: 7089
	public ScrollRect SR;

	// Token: 0x04001BB2 RID: 7090
	[Tooltip("根据物体数量的变动进行高度刷新。关闭的话会一直检测")]
	public bool ChildCountMode = true;
}
