using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000387 RID: 903
public class UINPCEventSVItem : MonoBehaviour
{
	// Token: 0x06001948 RID: 6472 RVA: 0x00015A97 File Offset: 0x00013C97
	private void Awake()
	{
		this.RT = (base.transform as RectTransform);
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x000E210C File Offset: 0x000E030C
	private void Update()
	{
		if (this.RT.sizeDelta.y != this.DescText.preferredHeight)
		{
			this.RT.sizeDelta = new Vector2(this.RT.sizeDelta.x, this.DescText.preferredHeight);
		}
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x00015AAA File Offset: 0x00013CAA
	public void SetEvent(string time, string desc)
	{
		this.TimeText.text = time;
		this.DescText.text = desc;
	}

	// Token: 0x0400145F RID: 5215
	public Text TimeText;

	// Token: 0x04001460 RID: 5216
	public Text DescText;

	// Token: 0x04001461 RID: 5217
	private RectTransform RT;
}
