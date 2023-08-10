using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.SetFace;

public class SetColor : MainUISetColor
{
	public override void HasColorSelect(string color_s, faceInfoDataBaseList face)
	{
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		JSONObject colorJson = GetColorJson(color_s);
		List<int> suijiList = jsonData.instance.getSuijiList(color_s, "Sex" + SetFaceUI.Inst.GetSex());
		Tools.ClearObj(colorImage.transform);
		int num = 0;
		int i = jsonData.instance.AvatarRandomJsonData["1"][color_s].I;
		for (int j = 0; j < suijiList.Count; j++)
		{
			GameObject val = Object.Instantiate<GameObject>(colorImage, colorList);
			createImageCell colorCell = val.GetComponent<createImageCell>();
			colorCell.SkinIndex = num;
			colorCell.SkinType = color_s;
			JSONObject jSONObject = colorJson[suijiList[num]];
			colorCell.SetColor(new Color(jSONObject["R"].n / 255f, jSONObject["G"].n / 255f, jSONObject["B"].n / 255f));
			if (i == num)
			{
				colorCell.toggle.isOn = true;
				((Graphic)curSelectColor).color = colorCell.GetColor();
			}
			((UnityEvent<bool>)(object)colorCell.toggle.onValueChanged).AddListener((UnityAction<bool>)delegate(bool b)
			{
				//IL_0039: Unknown result type (might be due to invalid IL or missing references)
				if (b)
				{
					SetFaceUI.Inst.SetFaceB.setFace(colorCell.SkinType, colorCell.SkinIndex);
					((Graphic)curSelectColor).color = colorCell.GetColor();
					colorListPanel.SetActive(false);
				}
			});
			val.SetActive(true);
			num++;
		}
		((Component)this).gameObject.SetActive(true);
		colorListPanel.SetActive(false);
	}
}
