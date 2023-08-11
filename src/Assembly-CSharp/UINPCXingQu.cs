using UnityEngine;
using UnityEngine.UI;

public class UINPCXingQu : MonoBehaviour
{
	[HideInInspector]
	public UINPCData npc;

	public Text XingQuText;

	public Image BGImage;

	public RectTransform TitleRT;

	public void RefreshUI()
	{
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		if (npc == null)
		{
			npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		}
		if (!jsonData.instance.AvatarBackpackJsonData[npc.ID.ToString()].HasField("XinQuType"))
		{
			jsonData.instance.MonstarCreatInterstingType(npc.ID);
		}
		string text = "<color=#d3b068>" + npc.Name + "感兴趣的物品：</color>";
		int num = 1;
		foreach (JSONObject item in jsonData.instance.AvatarBackpackJsonData[npc.ID.ToString()]["XinQuType"].list)
		{
			string text2 = "";
			try
			{
				text2 = (string)jsonData.instance.AllItemLeiXin[item["type"].I.ToString()][(object)"name"];
				text = text + "\n" + text2 + "(<color=#fdcb60>+" + item["percent"].I + "</color>%)";
				num++;
			}
			catch
			{
			}
		}
		XingQuText.text = text;
		((Graphic)XingQuText).rectTransform.sizeDelta = new Vector2(((Graphic)XingQuText).rectTransform.sizeDelta.x, (float)(num * 30));
		((Graphic)BGImage).rectTransform.sizeDelta = new Vector2(((Graphic)BGImage).rectTransform.sizeDelta.x, ((Graphic)XingQuText).rectTransform.sizeDelta.y + 30f);
		TitleRT.anchoredPosition = new Vector2(TitleRT.anchoredPosition.x, ((Graphic)BGImage).rectTransform.sizeDelta.y - 4f);
	}
}
