using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200032B RID: 811
public class MainUISetColor : MonoBehaviour
{
	// Token: 0x06001BF5 RID: 7157 RVA: 0x000C82E8 File Offset: 0x000C64E8
	public virtual void HasColorSelect(string color_s, faceInfoDataBaseList face)
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

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void HideColorSelect()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x000C8492 File Offset: 0x000C6692
	public void ShowSelectColorList()
	{
		this.colorListPanel.SetActive(true);
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x000C84A0 File Offset: 0x000C66A0
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

	// Token: 0x04001694 RID: 5780
	public Image curSelectColor;

	// Token: 0x04001695 RID: 5781
	public GameObject colorListPanel;

	// Token: 0x04001696 RID: 5782
	public Transform colorList;

	// Token: 0x04001697 RID: 5783
	public GameObject colorImage;
}
