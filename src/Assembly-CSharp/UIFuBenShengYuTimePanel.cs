using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000401 RID: 1025
public class UIFuBenShengYuTimePanel : MonoBehaviour
{
	// Token: 0x06001BB1 RID: 7089 RVA: 0x00017387 File Offset: 0x00015587
	private void Awake()
	{
		UIFuBenShengYuTimePanel.Inst = this;
	}

	// Token: 0x0400177B RID: 6011
	public static UIFuBenShengYuTimePanel Inst;

	// Token: 0x0400177C RID: 6012
	public GameObject ScaleObj;

	// Token: 0x0400177D RID: 6013
	public Text TimeText;
}
