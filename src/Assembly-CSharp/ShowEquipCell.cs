using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F9 RID: 761
public class ShowEquipCell : MonoBehaviour
{
	// Token: 0x06001A8E RID: 6798 RVA: 0x000BD173 File Offset: 0x000BB373
	public void init()
	{
		this.equipImage.gameObject.SetActive(false);
		this.equipPingJie.text = "";
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x000BD196 File Offset: 0x000BB396
	public void setEquipImage(Sprite sprite)
	{
		this.equipImage.gameObject.SetActive(true);
		this.equipImage.sprite = sprite;
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x000BD1B5 File Offset: 0x000BB3B5
	public Sprite getEquipImage()
	{
		return this.equipImage.sprite;
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x000BD1C4 File Offset: 0x000BB3C4
	public void updateEquipPingJie()
	{
		JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
		if (jtoken != null)
		{
			string pinjie = "";
			switch ((int)jtoken["shangxia"])
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
			this.setEquipPingJie(pinjie, (int)jtoken["quality"]);
			return;
		}
		this.equipPingJie.text = "";
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x000BD24C File Offset: 0x000BB44C
	public void setEquipPingJie(string pinjie, int color = 1)
	{
		switch (color)
		{
		case 1:
			this.equipPingJie.text = "<color=#b3d951>" + pinjie + "符器</color>";
			return;
		case 2:
			this.equipPingJie.text = "<color=#71dbff>" + pinjie + "法器</color>";
			return;
		case 3:
			this.equipPingJie.text = "<color=#ef6fff>" + pinjie + "法宝</color>";
			return;
		case 4:
			this.equipPingJie.text = "<color=#ff9d43>" + pinjie + "纯阳法宝</color>";
			return;
		case 5:
			this.equipPingJie.text = "<color=#ff744d>" + pinjie + "通天灵宝</color>";
			return;
		default:
			return;
		}
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x000BD304 File Offset: 0x000BB504
	public string getEquipDesc()
	{
		string result = "";
		string str = "";
		JToken jtoken = LianQiTotalManager.inst.getcurEquipQualityDate();
		switch ((int)jtoken["shangxia"])
		{
		case 1:
			str = "下品";
			break;
		case 2:
			str = "中品";
			break;
		case 3:
			str = "上品";
			break;
		}
		switch ((int)jtoken["quality"])
		{
		case 1:
			result = str + "符器";
			break;
		case 2:
			result = str + "法器";
			break;
		case 3:
			result = str + "法宝";
			break;
		case 4:
			result = str + "纯阳法宝";
			break;
		case 5:
			result = str + "通天灵宝";
			break;
		}
		return result;
	}

	// Token: 0x04001556 RID: 5462
	[SerializeField]
	private Image equipImage;

	// Token: 0x04001557 RID: 5463
	[SerializeField]
	private Text equipPingJie;
}
