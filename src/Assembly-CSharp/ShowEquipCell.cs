using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowEquipCell : MonoBehaviour
{
	[SerializeField]
	private Image equipImage;

	[SerializeField]
	private Text equipPingJie;

	public void init()
	{
		((Component)equipImage).gameObject.SetActive(false);
		equipPingJie.text = "";
	}

	public void setEquipImage(Sprite sprite)
	{
		((Component)equipImage).gameObject.SetActive(true);
		equipImage.sprite = sprite;
	}

	public Sprite getEquipImage()
	{
		return equipImage.sprite;
	}

	public void updateEquipPingJie()
	{
		JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
		if (val != null)
		{
			string pinjie = "";
			switch ((int)val[(object)"shangxia"])
			{
			case 1:
				pinjie = "下品";
				break;
			case 2:
				pinjie = "中品";
				break;
			case 3:
				pinjie = "上品";
				break;
			}
			setEquipPingJie(pinjie, (int)val[(object)"quality"]);
		}
		else
		{
			equipPingJie.text = "";
		}
	}

	public void setEquipPingJie(string pinjie, int color = 1)
	{
		switch (color)
		{
		case 1:
			equipPingJie.text = "<color=#b3d951>" + pinjie + "符器</color>";
			break;
		case 2:
			equipPingJie.text = "<color=#71dbff>" + pinjie + "法器</color>";
			break;
		case 3:
			equipPingJie.text = "<color=#ef6fff>" + pinjie + "法宝</color>";
			break;
		case 4:
			equipPingJie.text = "<color=#ff9d43>" + pinjie + "纯阳法宝</color>";
			break;
		case 5:
			equipPingJie.text = "<color=#ff744d>" + pinjie + "通天灵宝</color>";
			break;
		}
	}

	public string getEquipDesc()
	{
		string result = "";
		string text = "";
		JToken val = LianQiTotalManager.inst.getcurEquipQualityDate();
		switch ((int)val[(object)"shangxia"])
		{
		case 1:
			text = "下品";
			break;
		case 2:
			text = "中品";
			break;
		case 3:
			text = "上品";
			break;
		}
		switch ((int)val[(object)"quality"])
		{
		case 1:
			result = text + "符器";
			break;
		case 2:
			result = text + "法器";
			break;
		case 3:
			result = text + "法宝";
			break;
		case 4:
			result = text + "纯阳法宝";
			break;
		case 5:
			result = text + "通天灵宝";
			break;
		}
		return result;
	}
}
