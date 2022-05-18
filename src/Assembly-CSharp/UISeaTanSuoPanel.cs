using System;
using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004FD RID: 1277
public class UISeaTanSuoPanel : MonoBehaviour
{
	// Token: 0x0600211C RID: 8476 RVA: 0x0001B43F File Offset: 0x0001963F
	private void Awake()
	{
		UISeaTanSuoPanel.Inst = this;
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x001155FC File Offset: 0x001137FC
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

	// Token: 0x04001C8A RID: 7306
	public static UISeaTanSuoPanel Inst;

	// Token: 0x04001C8B RID: 7307
	public GameObject ScaleObj;

	// Token: 0x04001C8C RID: 7308
	public GameObject ProcessObj;

	// Token: 0x04001C8D RID: 7309
	public Image FillImage;

	// Token: 0x04001C8E RID: 7310
	public Text NameText;

	// Token: 0x04001C8F RID: 7311
	public Text JinDuText;

	// Token: 0x04001C90 RID: 7312
	public FpBtn TanSuoBtn;

	// Token: 0x04001C91 RID: 7313
	private int nowSeaTargetID;
}
