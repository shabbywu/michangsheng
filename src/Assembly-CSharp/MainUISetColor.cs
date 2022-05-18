using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000493 RID: 1171
public class MainUISetColor : MonoBehaviour
{
	// Token: 0x06001F3D RID: 7997 RVA: 0x0010D884 File Offset: 0x0010BA84
	public void HasColorSelect(string color_s, faceInfoDataBaseList face)
	{
		JSONObject colorJson = this.GetColorJson(color_s);
		List<int> suijiList = jsonData.instance.getSuijiList(color_s, "Sex" + MainUIPlayerInfo.inst.sex);
		Tools.ClearObj(this.colorImage.transform);
		int num = 0;
		int i = jsonData.instance.AvatarRandomJsonData["1"][color_s].I;
		for (int j = 0; j < suijiList.Count; j++)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.colorImage, this.colorList);
			createImageCell colorCell = gameObject.GetComponent<createImageCell>();
			colorCell.SkinIndex = num;
			colorCell.SkinType = color_s;
			JSONObject jsonobject = colorJson[suijiList[num]];
			colorCell.SetColor(new Color(jsonobject["R"].n / 255f, jsonobject["G"].n / 255f, jsonobject["B"].n / 255f));
			if (i == num)
			{
				colorCell.toggle.isOn = true;
				this.curSelectColor.color = colorCell.GetColor();
			}
			colorCell.toggle.onValueChanged.AddListener(delegate(bool b)
			{
				if (b)
				{
					MainUIMag.inst.createAvatarPanel.setFacePanel.setFace(colorCell.SkinType, colorCell.SkinIndex);
					this.curSelectColor.color = colorCell.GetColor();
					this.colorListPanel.SetActive(false);
				}
			});
			gameObject.SetActive(true);
			num++;
		}
		base.gameObject.SetActive(true);
		this.colorListPanel.SetActive(false);
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void HideColorSelect()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x00019CEC File Offset: 0x00017EEC
	public void ShowSelectColorList()
	{
		this.colorListPanel.SetActive(true);
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x0010DA30 File Offset: 0x0010BC30
	public JSONObject GetColorJson(string color_s)
	{
		JSONObject result = null;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(color_s);
		if (num <= 1128880211U)
		{
			if (num <= 527219179U)
			{
				if (num != 153553283U)
				{
					if (num == 527219179U)
					{
						if (color_s == "yanzhuColor")
						{
							result = jsonData.instance.YanZhuYanSeRandomColorJsonData;
						}
					}
				}
				else if (color_s == "tezhengColor")
				{
					result = jsonData.instance.MianWenYanSeRandomColorJsonData;
				}
			}
			else if (num != 596278282U)
			{
				if (num == 1128880211U)
				{
					if (color_s == "tattooColor")
					{
						result = jsonData.instance.WenShenRandomColorJsonData;
					}
				}
			}
			else if (color_s == "hairColorR")
			{
				result = jsonData.instance.HairRandomColorJsonData;
			}
		}
		else if (num <= 2189298307U)
		{
			if (num != 1180500499U)
			{
				if (num == 2189298307U)
				{
					if (color_s == "mouthColor")
					{
						result = jsonData.instance.MouthRandomColorJsonData;
					}
				}
			}
			else if (color_s == "eyebrowColor")
			{
				result = jsonData.instance.MeiMaoYanSeRandomColorJsonData;
			}
		}
		else if (num != 2546894512U)
		{
			if (num == 4294355740U)
			{
				if (color_s == "blushColor")
				{
					result = jsonData.instance.SaiHonRandomColorJsonData;
				}
			}
		}
		else if (color_s == "featureColor")
		{
			result = jsonData.instance.MeiMaoYanSeRandomColorJsonData;
		}
		return result;
	}

	// Token: 0x04001ABE RID: 6846
	public Image curSelectColor;

	// Token: 0x04001ABF RID: 6847
	public GameObject colorListPanel;

	// Token: 0x04001AC0 RID: 6848
	public Transform colorList;

	// Token: 0x04001AC1 RID: 6849
	public GameObject colorImage;
}
