using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000383 RID: 899
public class UISWShiLiToggle : MonoBehaviour
{
	// Token: 0x06001DBD RID: 7613 RVA: 0x000D1E88 File Offset: 0x000D0088
	private void Start()
	{
		this.ShiLiToggle = base.GetComponent<Toggle>();
		if (this.MoRenOn)
		{
			this.ShiLiToggle.isOn = true;
			UIShengWangManager.Inst.NowShowShiLiID = this.ShiLiID;
			UIShengWangManager.Inst.NowShowShiLiName = this.ShiLiName;
		}
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x000D1ED5 File Offset: 0x000D00D5
	public void OnValueChanged(bool isOn)
	{
		if (isOn)
		{
			UIShengWangManager.Inst.NowShowShiLiID = this.ShiLiID;
			UIShengWangManager.Inst.NowShowShiLiName = this.ShiLiName;
			UIShengWangManager.Inst.SetShiLiInfo(this.ShiLiID, this.ShiLiName);
		}
	}

	// Token: 0x0400185A RID: 6234
	public int ShiLiID;

	// Token: 0x0400185B RID: 6235
	public string ShiLiName;

	// Token: 0x0400185C RID: 6236
	public bool MoRenOn;

	// Token: 0x0400185D RID: 6237
	private Toggle ShiLiToggle;
}
