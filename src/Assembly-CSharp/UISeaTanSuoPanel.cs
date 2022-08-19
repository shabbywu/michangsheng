using System;
using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200037C RID: 892
public class UISeaTanSuoPanel : MonoBehaviour
{
	// Token: 0x06001DA7 RID: 7591 RVA: 0x000D15F5 File Offset: 0x000CF7F5
	private void Awake()
	{
		UISeaTanSuoPanel.Inst = this;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x000D1600 File Offset: 0x000CF800
	public void RefreshUI()
	{
		bool flag = false;
		int num = 0;
		string nowSceneName = SceneEx.NowSceneName;
		if (nowSceneName.StartsWith("Sea"))
		{
			num = int.Parse(nowSceneName.Replace("Sea", ""));
			if (SeaHaiYuTanSuo.DataDict.ContainsKey(num))
			{
				SeaHaiYuTanSuo seaHaiYuTanSuo = SeaHaiYuTanSuo.DataDict[num];
				if (seaHaiYuTanSuo.IsTanSuo != 0 && !PlayerEx.Player.HideHaiYuTanSuo.HasItem(num))
				{
					flag = true;
					this.nowSeaTargetID = seaHaiYuTanSuo.Value;
				}
			}
		}
		if (flag)
		{
			SeaHaiYuTanSuo tansuo = SeaHaiYuTanSuo.DataDict[num];
			this.NameText.text = SceneNameJsonData.DataDict[nowSceneName].EventName;
			int num2 = 0;
			if (PlayerEx.Player.SeaTanSuoDu.HasField(num.ToString()))
			{
				num2 = PlayerEx.Player.SeaTanSuoDu[num.ToString()].I;
			}
			if (num2 >= 100)
			{
				this.ProcessObj.SetActive(false);
				this.TanSuoBtn.gameObject.SetActive(true);
				this.TanSuoBtn.mouseUpEvent.RemoveAllListeners();
				this.TanSuoBtn.mouseUpEvent.AddListener(delegate()
				{
					EndlessSeaMag.AddSeeIsland(tansuo.ZuoBiao);
					MessageMag.Instance.Send(MessageName.MSG_Sea_TanSuoDu_Refresh, new MessageData(true));
				});
			}
			else
			{
				this.ProcessObj.SetActive(true);
				this.TanSuoBtn.gameObject.SetActive(false);
				this.JinDuText.text = string.Format("{0}%", Mathf.Clamp(num2, 0, 100));
				DOTweenModuleUI.DOFillAmount(this.FillImage, Mathf.Clamp((float)num2 / 100f, 0f, 1f), 1f);
			}
			this.ScaleObj.SetActive(true);
			return;
		}
		this.ScaleObj.SetActive(false);
	}

	// Token: 0x04001832 RID: 6194
	public static UISeaTanSuoPanel Inst;

	// Token: 0x04001833 RID: 6195
	public GameObject ScaleObj;

	// Token: 0x04001834 RID: 6196
	public GameObject ProcessObj;

	// Token: 0x04001835 RID: 6197
	public Image FillImage;

	// Token: 0x04001836 RID: 6198
	public Text NameText;

	// Token: 0x04001837 RID: 6199
	public Text JinDuText;

	// Token: 0x04001838 RID: 6200
	public FpBtn TanSuoBtn;

	// Token: 0x04001839 RID: 6201
	private int nowSeaTargetID;
}
