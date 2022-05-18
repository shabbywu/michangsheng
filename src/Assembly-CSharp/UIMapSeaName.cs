using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004B9 RID: 1209
public class UIMapSeaName : MonoBehaviour
{
	// Token: 0x06001FF7 RID: 8183 RVA: 0x00111BD8 File Offset: 0x0010FDD8
	public void RefreshUI()
	{
		if (SeaHaiYuTanSuo.DataDict.ContainsKey(this.SeaID))
		{
			SeaHaiYuTanSuo seaHaiYuTanSuo = SeaHaiYuTanSuo.DataDict[this.SeaID];
			this.HasTanSuoDu = (seaHaiYuTanSuo.IsTanSuo == 1);
			if (this.HasTanSuoDu)
			{
				int seaTanSuoDu = PlayerEx.GetSeaTanSuoDu(this.SeaID);
				this.TanSuoDu = Mathf.Clamp(seaTanSuoDu, 0, 100);
			}
			this.SetTanSuoDuShow(false);
			return;
		}
		Debug.LogError(string.Format("UIMap海域名字出错，海域探索度表中没有ID为{0}的海域", this.SeaID));
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x00111C5C File Offset: 0x0010FE5C
	public void SetTanSuoDuShow(bool show)
	{
		if (show)
		{
			if (this.HasTanSuoDu)
			{
				this.TanSuoDuText.text = string.Format("探索度 {0}%", this.TanSuoDu);
				this.TanSuoDuText.gameObject.SetActive(true);
				return;
			}
		}
		else
		{
			this.TanSuoDuText.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001B64 RID: 7012
	public int SeaID;

	// Token: 0x04001B65 RID: 7013
	public int BindHighlightID;

	// Token: 0x04001B66 RID: 7014
	public Text SeaNameText;

	// Token: 0x04001B67 RID: 7015
	public Text TanSuoDuText;

	// Token: 0x04001B68 RID: 7016
	public bool HasTanSuoDu;

	// Token: 0x04001B69 RID: 7017
	public int TanSuoDu;
}
