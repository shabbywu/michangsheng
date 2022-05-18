using System;
using UnityEngine;

// Token: 0x02000405 RID: 1029
public class UIDataTip : MonoBehaviour
{
	// Token: 0x06001BCE RID: 7118 RVA: 0x000F882C File Offset: 0x000F6A2C
	private void Awake()
	{
		UIDataTip.Inst = this;
		this.mainLeftPos = base.transform.Find("MainPanelLeftPos");
		this.exLeftPos = base.transform.Find("ExPanelLeftPos");
		this.mainRightPos = base.transform.Find("MainPanelRightPos");
		this.exRightPos = base.transform.Find("ExPanelRightPos");
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0400179F RID: 6047
	public static UIDataTip Inst;

	// Token: 0x040017A0 RID: 6048
	private Transform mainLeftPos;

	// Token: 0x040017A1 RID: 6049
	private Transform exLeftPos;

	// Token: 0x040017A2 RID: 6050
	private Transform mainRightPos;

	// Token: 0x040017A3 RID: 6051
	private Transform exRightPos;
}
