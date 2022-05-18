using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000506 RID: 1286
public class UISWShiLiToggle : MonoBehaviour
{
	// Token: 0x06002136 RID: 8502 RVA: 0x00115D64 File Offset: 0x00113F64
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

	// Token: 0x06002137 RID: 8503 RVA: 0x0001B5C0 File Offset: 0x000197C0
	public void OnValueChanged(bool isOn)
	{
		if (isOn)
		{
			UIShengWangManager.Inst.NowShowShiLiID = this.ShiLiID;
			UIShengWangManager.Inst.NowShowShiLiName = this.ShiLiName;
			UIShengWangManager.Inst.SetShiLiInfo(this.ShiLiID, this.ShiLiName);
		}
	}

	// Token: 0x04001CB5 RID: 7349
	public int ShiLiID;

	// Token: 0x04001CB6 RID: 7350
	public string ShiLiName;

	// Token: 0x04001CB7 RID: 7351
	public bool MoRenOn;

	// Token: 0x04001CB8 RID: 7352
	private Toggle ShiLiToggle;
}
