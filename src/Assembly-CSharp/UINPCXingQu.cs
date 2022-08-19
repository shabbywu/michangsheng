using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027F RID: 639
public class UINPCXingQu : MonoBehaviour
{
	// Token: 0x06001733 RID: 5939 RVA: 0x0009E604 File Offset: 0x0009C804
	public void RefreshUI()
	{
		if (this.npc == null)
		{
			this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		}
		if (!jsonData.instance.AvatarBackpackJsonData[this.npc.ID.ToString()].HasField("XinQuType"))
		{
			jsonData.instance.MonstarCreatInterstingType(this.npc.ID);
		}
		string text = "<color=#d3b068>" + this.npc.Name + "感兴趣的物品：</color>";
		int num = 1;
		foreach (JSONObject jsonobject in jsonData.instance.AvatarBackpackJsonData[this.npc.ID.ToString()]["XinQuType"].list)
		{
			try
			{
				string text2 = (string)jsonData.instance.AllItemLeiXin[jsonobject["type"].I.ToString()]["name"];
				text = string.Concat(new object[]
				{
					text,
					"\n",
					text2,
					"(<color=#fdcb60>+",
					jsonobject["percent"].I,
					"</color>%)"
				});
				num++;
			}
			catch
			{
			}
		}
		this.XingQuText.text = text;
		this.XingQuText.rectTransform.sizeDelta = new Vector2(this.XingQuText.rectTransform.sizeDelta.x, (float)(num * 30));
		this.BGImage.rectTransform.sizeDelta = new Vector2(this.BGImage.rectTransform.sizeDelta.x, this.XingQuText.rectTransform.sizeDelta.y + 30f);
		this.TitleRT.anchoredPosition = new Vector2(this.TitleRT.anchoredPosition.x, this.BGImage.rectTransform.sizeDelta.y - 4f);
	}

	// Token: 0x040011E4 RID: 4580
	[HideInInspector]
	public UINPCData npc;

	// Token: 0x040011E5 RID: 4581
	public Text XingQuText;

	// Token: 0x040011E6 RID: 4582
	public Image BGImage;

	// Token: 0x040011E7 RID: 4583
	public RectTransform TitleRT;
}
