using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026F RID: 623
public class UINPCEventSVItem : MonoBehaviour
{
	// Token: 0x06001696 RID: 5782 RVA: 0x0009A1F4 File Offset: 0x000983F4
	private void Awake()
	{
		this.RT = (base.transform as RectTransform);
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x0009A208 File Offset: 0x00098408
	private void Update()
	{
		if (this.RT.sizeDelta.y != this.DescText.preferredHeight)
		{
			this.RT.sizeDelta = new Vector2(this.RT.sizeDelta.x, this.DescText.preferredHeight);
		}
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x0009A25D File Offset: 0x0009845D
	public void SetEvent(string time, string desc)
	{
		this.TimeText.text = time;
		this.DescText.text = desc;
	}

	// Token: 0x0400110F RID: 4367
	public Text TimeText;

	// Token: 0x04001110 RID: 4368
	public Text DescText;

	// Token: 0x04001111 RID: 4369
	private RectTransform RT;
}
