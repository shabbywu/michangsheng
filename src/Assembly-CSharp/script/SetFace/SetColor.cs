using System;
using System.Collections.Generic;
using UnityEngine;

namespace script.SetFace
{
	// Token: 0x020009EF RID: 2543
	public class SetColor : MainUISetColor
	{
		// Token: 0x0600468B RID: 18059 RVA: 0x001DD60C File Offset: 0x001DB80C
		public override void HasColorSelect(string color_s, faceInfoDataBaseList face)
		{
			JSONObject colorJson = base.GetColorJson(color_s);
			List<int> suijiList = jsonData.instance.getSuijiList(color_s, "Sex" + SetFaceUI.Inst.GetSex());
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
						SetFaceUI.Inst.SetFaceB.setFace(colorCell.SkinType, colorCell.SkinIndex);
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
	}
}
