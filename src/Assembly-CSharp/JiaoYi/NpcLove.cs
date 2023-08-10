using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JiaoYi;

public class NpcLove : MonoBehaviour
{
	public GameObject Temp;

	public Transform Parent;

	public GameObject Panel;

	private bool _isInit;

	public void InitNpcLove(int npcId)
	{
		if (!jsonData.instance.AvatarBackpackJsonData[string.Concat(npcId)].HasField("XinQuType"))
		{
			jsonData.instance.MonstarCreatInterstingType(npcId);
		}
		List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(npcId)]["XinQuType"].list;
		string text = "";
		string text2 = "";
		if (list.Count > 0)
		{
			foreach (JSONObject item in list)
			{
				text = (string)jsonData.instance.AllItemLeiXin[string.Concat(item["type"].I)][(object)"name"];
				text2 = item["percent"].I.ToString();
				Text component = Temp.Inst(Parent).GetComponent<Text>();
				component.SetText(text + "(+" + text2 + "%)");
				((Component)component).gameObject.SetActive(true);
			}
			return;
		}
		Temp.Inst(Parent).SetActive(true);
	}

	public void ShowLove()
	{
		if (!_isInit)
		{
			InitNpcLove(JiaoYiUIMag.Inst.NpcId);
			_isInit = true;
		}
		Panel.SetActive(true);
	}

	public void HideLove()
	{
		Panel.SetActive(false);
	}
}
