using JSONClass;
using UnityEngine;
using UnityEngine.UI;

public class UIMapSeaName : MonoBehaviour
{
	public int SeaID;

	public int BindHighlightID;

	public Text SeaNameText;

	public Text TanSuoDuText;

	public bool HasTanSuoDu;

	public int TanSuoDu;

	public void RefreshUI()
	{
		if (SeaHaiYuTanSuo.DataDict.ContainsKey(SeaID))
		{
			SeaHaiYuTanSuo seaHaiYuTanSuo = SeaHaiYuTanSuo.DataDict[SeaID];
			HasTanSuoDu = seaHaiYuTanSuo.IsTanSuo == 1;
			if (HasTanSuoDu)
			{
				int seaTanSuoDu = PlayerEx.GetSeaTanSuoDu(SeaID);
				TanSuoDu = Mathf.Clamp(seaTanSuoDu, 0, 100);
			}
			SetTanSuoDuShow(show: false);
		}
		else
		{
			Debug.LogError((object)$"UIMap海域名字出错，海域探索度表中没有ID为{SeaID}的海域");
		}
	}

	public void SetTanSuoDuShow(bool show)
	{
		if (show)
		{
			if (HasTanSuoDu)
			{
				TanSuoDuText.text = $"探索度 {TanSuoDu}%";
				((Component)TanSuoDuText).gameObject.SetActive(true);
			}
		}
		else
		{
			((Component)TanSuoDuText).gameObject.SetActive(false);
		}
	}
}
