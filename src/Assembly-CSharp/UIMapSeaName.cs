using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000345 RID: 837
public class UIMapSeaName : MonoBehaviour
{
	// Token: 0x06001CA5 RID: 7333 RVA: 0x000CD1C4 File Offset: 0x000CB3C4
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

	// Token: 0x06001CA6 RID: 7334 RVA: 0x000CD248 File Offset: 0x000CB448
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

	// Token: 0x04001723 RID: 5923
	public int SeaID;

	// Token: 0x04001724 RID: 5924
	public int BindHighlightID;

	// Token: 0x04001725 RID: 5925
	public Text SeaNameText;

	// Token: 0x04001726 RID: 5926
	public Text TanSuoDuText;

	// Token: 0x04001727 RID: 5927
	public bool HasTanSuoDu;

	// Token: 0x04001728 RID: 5928
	public int TanSuoDu;
}
