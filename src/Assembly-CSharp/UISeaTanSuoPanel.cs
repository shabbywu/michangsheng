using DG.Tweening;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISeaTanSuoPanel : MonoBehaviour
{
	public static UISeaTanSuoPanel Inst;

	public GameObject ScaleObj;

	public GameObject ProcessObj;

	public Image FillImage;

	public Text NameText;

	public Text JinDuText;

	public FpBtn TanSuoBtn;

	private int nowSeaTargetID;

	private void Awake()
	{
		Inst = this;
	}

	public void RefreshUI()
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
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
					nowSeaTargetID = seaHaiYuTanSuo.Value;
				}
			}
		}
		if (flag)
		{
			SeaHaiYuTanSuo tansuo = SeaHaiYuTanSuo.DataDict[num];
			NameText.text = SceneNameJsonData.DataDict[nowSceneName].EventName;
			int num2 = 0;
			if (PlayerEx.Player.SeaTanSuoDu.HasField(num.ToString()))
			{
				num2 = PlayerEx.Player.SeaTanSuoDu[num.ToString()].I;
			}
			if (num2 >= 100)
			{
				ProcessObj.SetActive(false);
				((Component)TanSuoBtn).gameObject.SetActive(true);
				((UnityEventBase)TanSuoBtn.mouseUpEvent).RemoveAllListeners();
				TanSuoBtn.mouseUpEvent.AddListener((UnityAction)delegate
				{
					EndlessSeaMag.AddSeeIsland(tansuo.ZuoBiao);
					MessageMag.Instance.Send(MessageName.MSG_Sea_TanSuoDu_Refresh, new MessageData(value: true));
				});
			}
			else
			{
				ProcessObj.SetActive(true);
				((Component)TanSuoBtn).gameObject.SetActive(false);
				JinDuText.text = $"{Mathf.Clamp(num2, 0, 100)}%";
				DOTweenModuleUI.DOFillAmount(FillImage, Mathf.Clamp((float)num2 / 100f, 0f, 1f), 1f);
			}
			ScaleObj.SetActive(true);
		}
		else
		{
			ScaleObj.SetActive(false);
		}
	}
}
