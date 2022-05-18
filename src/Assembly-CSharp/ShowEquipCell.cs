using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000456 RID: 1110
public class ShowEquipCell : MonoBehaviour
{
	// Token: 0x06001DB4 RID: 7604 RVA: 0x00018B70 File Offset: 0x00016D70
	public void init()
	{
		this.equipImage.gameObject.SetActive(false);
		this.equipPingJie.text = "";
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x00018B93 File Offset: 0x00016D93
	public void setEquipImage(Sprite sprite)
	{
		this.equipImage.gameObject.SetActive(true);
		this.equipImage.sprite = sprite;
	}

	// Token: 0x06001DB6 RID: 7606 RVA: 0x00018BB2 File Offset: 0x00016DB2
	public Sprite getEquipImage()
	{
		return this.equipImage.sprite;
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x001031C0 File Offset: 0x001013C0
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

	// Token: 0x06001DB8 RID: 7608 RVA: 0x00103248 File Offset: 0x00101448
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

	// Token: 0x06001DB9 RID: 7609 RVA: 0x00103300 File Offset: 0x00101500
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

	// Token: 0x04001963 RID: 6499
	[SerializeField]
	private Image equipImage;

	// Token: 0x04001964 RID: 6500
	[SerializeField]
	private Text equipPingJie;
}
