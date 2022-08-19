using System;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class UIDataTip : MonoBehaviour
{
	// Token: 0x060018D5 RID: 6357 RVA: 0x000B26DC File Offset: 0x000B08DC
	private void Awake()
	{
		UIDataTip.Inst = this;
		this.mainLeftPos = base.transform.Find("MainPanelLeftPos");
		this.exLeftPos = base.transform.Find("ExPanelLeftPos");
		this.mainRightPos = base.transform.Find("MainPanelRightPos");
		this.exRightPos = base.transform.Find("ExPanelRightPos");
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x040013F1 RID: 5105
	public static UIDataTip Inst;

	// Token: 0x040013F2 RID: 5106
	private Transform mainLeftPos;

	// Token: 0x040013F3 RID: 5107
	private Transform exLeftPos;

	// Token: 0x040013F4 RID: 5108
	private Transform mainRightPos;

	// Token: 0x040013F5 RID: 5109
	private Transform exRightPos;
}
