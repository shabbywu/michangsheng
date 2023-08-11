using System.Collections.Generic;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSkill : TooltipBase
{
	public int SkillID;

	public UILabel lengque;

	public UILabel Lname;

	public UILabel LLV;

	public UILabel desc;

	public GameObject slots;

	public GameObject grid;

	public GameObject toolstiiptemp;

	public GameObject cizhui;

	public GameObject itemobj;

	public List<Sprite> lingQi;

	public GameObject fengexianImage;

	public GameObject lingqiimage;

	public UITexture uITexture;

	public GameObject Daride;

	private bool canShowTooltip = true;

	private bool firstCall = true;

	protected override void Start()
	{
		base.Start();
		canShowTooltip = true;
	}

	public void setItemText(int skillID, ITEM_INFO _item)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		if (skillID == SkillID)
		{
			return;
		}
		SkillID = skillID;
		JSONObject jSONObject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		string baseName = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[_item.itemId.ToString()]["name"].str);
		Lname.text = Inventory2.GetItemName(_item.Seid, baseName);
		int oldCD = 0;
		foreach (Transform item in grid.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		if (jsonData.instance.SkillSeidJsonData[29].HasField(string.Concat(skillID)))
		{
			oldCD = (int)jsonData.instance.SkillSeidJsonData[29][skillID.ToString()]["value1"].n;
		}
		lengque.text = "[50a6ff]冷却：[-]" + Inventory2.GetItemCD(_item.Seid, oldCD) + "回合";
		string text = "";
		int num = 0;
		foreach (JSONObject item2 in Inventory2.GetItemAttackType(_item.Seid, jSONObject["AttackType"]).list)
		{
			if (num > 0)
			{
				text += "-";
			}
			text += Tools.getStr("xibieFight" + (int)item2.n);
			num++;
		}
		LLV.text = text;
		if (_item.Seid != null && _item.Seid.HasField("SeidDesc"))
		{
			string desstr = "主动：" + Tools.Code64(_item.Seid["SeidDesc"].str);
			desc.text = Tools.getDesc(desstr, 1).Replace("主动：", "[CDA14C]主动：[-]");
		}
		else
		{
			string desstr2 = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[_item.itemId.ToString()]["desc"].str).Replace("[FF0000]", "[CDA14C]").Replace("[FF00FF]", "[CDA14C]");
			desc.text = Tools.getDesc(desstr2, 1).Replace("主动：", "[CDA14C]主动：[-]");
		}
		clearCiZhui();
		Show();
	}

	public int GetUITextureHigh()
	{
		return 153 + desc.height;
	}

	public void ClickChenge()
	{
		bool value = UIToggle.current.value;
		if (firstCall)
		{
			firstCall = false;
		}
		else
		{
			canShowTooltip = value;
		}
		if (value)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	public void ClickXiangXi()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		if (!jsonData.instance.skillJsonData.ContainsKey(SkillID.ToString()))
		{
			return;
		}
		_ = UIToggle.current.value;
		int num = 0;
		foreach (Transform item in cizhui.transform)
		{
			if (((Component)item).gameObject.activeSelf)
			{
				num++;
			}
		}
		if (num == 0)
		{
			JSONObject jSONObject = jsonData.instance.skillJsonData[string.Concat(SkillID)];
			clearCiZhui();
			float num2 = 0f;
			{
				foreach (JSONObject item2 in jSONObject["Affix"].list)
				{
					num2 += addAffix((int)item2.n, num2);
				}
				return;
			}
		}
		clearCiZhui();
	}

	private void clearCiZhui()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		foreach (Transform item in cizhui.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public void Show()
	{
		if (canShowTooltip)
		{
			uITexture.height = GetUITextureHigh();
			Daride.SetActive(true);
		}
	}

	public void Hide()
	{
		uITexture.height = 40;
		Daride.SetActive(false);
	}

	public void setText(int skillID, GUIPackage.Skill keyCell)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (skillID == SkillID)
		{
			return;
		}
		foreach (Transform item in grid.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		SkillID = skillID;
		JSONObject jSONObject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		Lname.text = Tools.instance.getSkillName(skillID);
		string text = "";
		int num = 0;
		foreach (JSONObject item2 in jSONObject["AttackType"].list)
		{
			if (num > 0)
			{
				text += "-";
			}
			text += Tools.getStr("xibieFight" + (int)item2.n);
			num++;
		}
		lengque.text = "";
		LLV.text = text;
		string text2 = Tools.instance.getSkillText(skillID).Replace("[FF0000]", "[CDA14C]").Replace("[FF00FF]", "[CDA14C]");
		foreach (Match item3 in Regex.Matches(text2, "【\\w*】"))
		{
			text2 = text2.Replace(item3.Value, "[42E395]" + item3.Value + "[-]");
		}
		desc.text = text2;
		int num2 = 0;
		foreach (KeyValuePair<int, int> item4 in keyCell.getSkillCast(Tools.instance.getPlayer()))
		{
			for (int i = 0; i < item4.Value; i++)
			{
				CreatGameObjectToParent(grid, lingqiimage).GetComponent<Image>().sprite = lingQi[item4.Key];
			}
			num2++;
		}
		int num3 = 0;
		foreach (JSONObject item5 in jSONObject["skill_SameCastNum"].list)
		{
			if (num2 > 0 || num3 > 0)
			{
				CreatGameObjectToParent(grid, fengexianImage);
			}
			for (int j = 0; j < (int)item5.n; j++)
			{
				CreatGameObjectToParent(grid, lingqiimage).GetComponent<Image>().sprite = lingQi[lingQi.Count - 1];
			}
			num3++;
		}
		clearCiZhui();
		Show();
	}

	public float addAffix(int id, float start = -1f)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = Object.Instantiate<GameObject>(toolstiiptemp);
		val.gameObject.SetActive(true);
		val.transform.SetParent(cizhui.transform);
		val.transform.localPosition = Vector3.zero;
		val.transform.localScale = Vector3.one;
		val.GetComponent<TooltipScale>().shoudSetPos = false;
		string text = Tools.instance.Code64ToString(jsonData.instance.SkillTextInfoJsonData[id.ToString()]["name"].str);
		string desstr = Tools.instance.Code64ToString(jsonData.instance.SkillTextInfoJsonData[id.ToString()]["descr"].str).Replace("[FF0000]", "[ff8300]").Replace("[FF00FF]", "[ff8300]");
		val.GetComponentInChildren<UILabel>().text = "[42E395]" + text + ":[-] [E8E4BA]" + Tools.getDesc(desstr, 1) + "[-]";
		val.GetComponent<TooltipScale>().setBGwight();
		float result = val.GetComponent<TooltipScale>().childTexture.height + 18;
		if (start != -1f)
		{
			val.transform.localPosition = new Vector3(0f, start, 0f);
		}
		val.GetComponent<Animation>().Play("ShowBuff");
		return result;
	}

	public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		GameObject gameObject = ((Component)Object.Instantiate<Transform>(Temp.transform)).gameObject;
		gameObject.transform.SetParent(parent.transform);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}

	public void clearItem()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		foreach (Transform item in itemobj.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public void addItemAffix(int id)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(toolstiiptemp);
		obj.transform.SetParent(itemobj.transform);
		obj.transform.localPosition = new Vector3(0f, (float)(childTexture.height / 2), 0f);
		obj.transform.localScale = Vector3.one;
		obj.GetComponent<TooltipScale>().shoudSetPos = false;
		string text = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[id.ToString()]["name"].str);
		string desstr = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[id.ToString()]["desc"].str).Replace("[FF0000]", "[ff8300]").Replace("[FF00FF]", "[ff8300]");
		obj.GetComponentInChildren<UILabel>().text = "[ff8300]" + text + ":[-] " + Tools.getDesc(desstr, 1);
		obj.GetComponent<TooltipScale>().setBGwight();
		obj.GetComponent<Animation>().Play("ShowBuff");
	}

	public void newSlot(int type, int Num)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(slots);
		obj.SetActive(true);
		obj.transform.SetParent(grid.transform);
		obj.transform.localScale = Vector3.one;
		((Component)obj.transform.Find("Num")).GetComponent<UILabel>().text = Num.ToString();
		UITexture component = obj.GetComponent<UITexture>();
		Object obj2 = Resources.Load("Ui Icon/Fight/chast_" + type);
		component.mainTexture = (Texture)(object)((obj2 is Texture) ? obj2 : null);
	}

	protected override void Update()
	{
		if (shoudSetPos)
		{
			base.Update();
			updateCizhui();
		}
	}

	public void updateCizhui()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = default(Vector3);
		((Vector3)(ref val))._002Ector(0f, 0f, 0f);
		if (Input.mousePosition.x > (float)(Screen.width / 2))
		{
			val.x -= childTexture.width;
		}
		else
		{
			val.x += childTexture.width;
		}
		bool flag = true;
		if (Input.mousePosition.y < (float)(Screen.height / 2))
		{
			val.y -= childTexture.height / 2;
		}
		else
		{
			val.y += childTexture.height / 2;
			flag = false;
		}
		int num = 0;
		if (cizhui.transform.childCount > 0)
		{
			num = ((!flag) ? (num - ((Component)cizhui.transform.GetChild(0).GetChild(0)).GetComponent<UITexture>().height / 2) : (num + ((Component)cizhui.transform.GetChild(0).GetChild(0)).GetComponent<UITexture>().height / 2));
		}
		foreach (Transform item in cizhui.transform)
		{
			Transform val2 = item;
			((Component)val2).transform.localPosition = Vector3.zero + new Vector3(0f, (float)num, 0f);
			num = ((!flag) ? (num - ((Component)((Component)val2).transform.GetChild(0)).GetComponent<UITexture>().height) : (num + ((Component)((Component)val2).transform.GetChild(0)).GetComponent<UITexture>().height));
		}
	}
}
