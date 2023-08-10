using UnityEngine;
using UnityEngine.UI;

public class UISiXuItem : MonoBehaviour
{
	public Image SiXuImage;

	public Text PinJieText;

	public Text TypeText;

	public Text TimeText;

	private SiXuData data;

	public void SetData(SiXuData sixudata)
	{
		data = sixudata;
		PinJieText.text = data.PinJieStr;
		TypeText.text = data.WuDaoTypeStr;
		TimeText.text = data.ShengYuTime;
		SiXuImage.sprite = UIBiGuanGanWuPanel.Inst.WuDaoTypeSpriteList[data.wuDaoFilter - 1];
	}

	public void OnToggleChanged(bool value)
	{
		if (value)
		{
			UIBiGuanGanWuPanel.Inst.NowSiXu = data;
		}
		else
		{
			UIBiGuanGanWuPanel.Inst.NowSiXu = null;
		}
		UIBiGuanGanWuPanel.Inst.SetGanWu(UIBiGuanGanWuPanel.Inst.NowSiXu);
	}
}
