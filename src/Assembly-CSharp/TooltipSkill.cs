using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200042C RID: 1068
public class TooltipSkill : TooltipBase
{
	// Token: 0x0600221B RID: 8731 RVA: 0x000EB011 File Offset: 0x000E9211
	protected override void Start()
	{
		base.Start();
		this.canShowTooltip = true;
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x000EB020 File Offset: 0x000E9220
	public void setItemText(int skillID, ITEM_INFO _item)
	{
		if (skillID == this.SkillID)
		{
			return;
		}
		this.SkillID = skillID;
		JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		string baseName = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[_item.itemId.ToString()]["name"].str);
		this.Lname.text = Inventory2.GetItemName(_item.Seid, baseName);
		int oldCD = 0;
		foreach (object obj in this.grid.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		if (jsonData.instance.SkillSeidJsonData[29].HasField(string.Concat(skillID)))
		{
			oldCD = (int)jsonData.instance.SkillSeidJsonData[29][skillID.ToString()]["value1"].n;
		}
		this.lengque.text = "[50a6ff]冷却：[-]" + Inventory2.GetItemCD(_item.Seid, oldCD) + "回合";
		string text = "";
		int num = 0;
		foreach (JSONObject jsonobject2 in Inventory2.GetItemAttackType(_item.Seid, jsonobject["AttackType"]).list)
		{
			if (num > 0)
			{
				text += "-";
			}
			text += Tools.getStr("xibieFight" + (int)jsonobject2.n);
			num++;
		}
		this.LLV.text = text;
		if (_item.Seid != null && _item.Seid.HasField("SeidDesc"))
		{
			string desstr = "主动：" + Tools.Code64(_item.Seid["SeidDesc"].str);
			this.desc.text = Tools.getDesc(desstr, 1).Replace("主动：", "[CDA14C]主动：[-]");
		}
		else
		{
			string desstr2 = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[_item.itemId.ToString()]["desc"].str).Replace("[FF0000]", "[CDA14C]").Replace("[FF00FF]", "[CDA14C]");
			this.desc.text = Tools.getDesc(desstr2, 1).Replace("主动：", "[CDA14C]主动：[-]");
		}
		this.clearCiZhui();
		this.Show();
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x000EB30C File Offset: 0x000E950C
	public int GetUITextureHigh()
	{
		return 153 + this.desc.height;
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x000EB320 File Offset: 0x000E9520
	public void ClickChenge()
	{
		bool value = UIToggle.current.value;
		if (this.firstCall)
		{
			this.firstCall = false;
		}
		else
		{
			this.canShowTooltip = value;
		}
		if (value)
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x000EB360 File Offset: 0x000E9560
	public void ClickXiangXi()
	{
		if (!jsonData.instance.skillJsonData.ContainsKey(this.SkillID.ToString()))
		{
			return;
		}
		bool value = UIToggle.current.value;
		int num = 0;
		using (IEnumerator enumerator = this.cizhui.transform.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((Transform)enumerator.Current).gameObject.activeSelf)
				{
					num++;
				}
			}
		}
		if (num == 0)
		{
			JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(this.SkillID)];
			this.clearCiZhui();
			float num2 = 0f;
			using (List<JSONObject>.Enumerator enumerator2 = jsonobject["Affix"].list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					JSONObject jsonobject2 = enumerator2.Current;
					num2 += this.addAffix((int)jsonobject2.n, num2);
				}
				return;
			}
		}
		this.clearCiZhui();
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x000EB47C File Offset: 0x000E967C
	private void clearCiZhui()
	{
		foreach (object obj in this.cizhui.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06002221 RID: 8737 RVA: 0x000EB4EC File Offset: 0x000E96EC
	public void Show()
	{
		if (this.canShowTooltip)
		{
			this.uITexture.height = this.GetUITextureHigh();
			this.Daride.SetActive(true);
		}
	}

	// Token: 0x06002222 RID: 8738 RVA: 0x000EB513 File Offset: 0x000E9713
	public void Hide()
	{
		this.uITexture.height = 40;
		this.Daride.SetActive(false);
	}

	// Token: 0x06002223 RID: 8739 RVA: 0x000EB530 File Offset: 0x000E9730
	public void setText(int skillID, GUIPackage.Skill keyCell)
	{
		if (skillID == this.SkillID)
		{
			return;
		}
		foreach (object obj in this.grid.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		this.SkillID = skillID;
		JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		this.Lname.text = Tools.instance.getSkillName(skillID, false);
		string text = "";
		int num = 0;
		foreach (JSONObject jsonobject2 in jsonobject["AttackType"].list)
		{
			if (num > 0)
			{
				text += "-";
			}
			text += Tools.getStr("xibieFight" + (int)jsonobject2.n);
			num++;
		}
		this.lengque.text = "";
		this.LLV.text = text;
		string text2 = Tools.instance.getSkillText(skillID).Replace("[FF0000]", "[CDA14C]").Replace("[FF00FF]", "[CDA14C]");
		foreach (object obj2 in Regex.Matches(text2, "【\\w*】"))
		{
			Match match = (Match)obj2;
			text2 = text2.Replace(match.Value, "[42E395]" + match.Value + "[-]");
		}
		this.desc.text = text2;
		int num2 = 0;
		foreach (KeyValuePair<int, int> keyValuePair in keyCell.getSkillCast(Tools.instance.getPlayer()))
		{
			for (int i = 0; i < keyValuePair.Value; i++)
			{
				this.CreatGameObjectToParent(this.grid, this.lingqiimage).GetComponent<Image>().sprite = this.lingQi[keyValuePair.Key];
			}
			num2++;
		}
		int num3 = 0;
		foreach (JSONObject jsonobject3 in jsonobject["skill_SameCastNum"].list)
		{
			if (num2 > 0 || num3 > 0)
			{
				this.CreatGameObjectToParent(this.grid, this.fengexianImage);
			}
			for (int j = 0; j < (int)jsonobject3.n; j++)
			{
				this.CreatGameObjectToParent(this.grid, this.lingqiimage).GetComponent<Image>().sprite = this.lingQi[this.lingQi.Count - 1];
			}
			num3++;
		}
		this.clearCiZhui();
		this.Show();
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x000EB890 File Offset: 0x000E9A90
	public float addAffix(int id, float start = -1f)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.toolstiiptemp);
		gameObject.gameObject.SetActive(true);
		gameObject.transform.SetParent(this.cizhui.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<TooltipScale>().shoudSetPos = false;
		string text = Tools.instance.Code64ToString(jsonData.instance.SkillTextInfoJsonData[id.ToString()]["name"].str);
		string desstr = Tools.instance.Code64ToString(jsonData.instance.SkillTextInfoJsonData[id.ToString()]["descr"].str).Replace("[FF0000]", "[ff8300]").Replace("[FF00FF]", "[ff8300]");
		gameObject.GetComponentInChildren<UILabel>().text = string.Concat(new string[]
		{
			"[42E395]",
			text,
			":[-] [E8E4BA]",
			Tools.getDesc(desstr, 1),
			"[-]"
		});
		gameObject.GetComponent<TooltipScale>().setBGwight();
		float result = (float)(gameObject.GetComponent<TooltipScale>().childTexture.height + 18);
		if (start != -1f)
		{
			gameObject.transform.localPosition = new Vector3(0f, start, 0f);
		}
		gameObject.GetComponent<Animation>().Play("ShowBuff");
		return result;
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x000EBA04 File Offset: 0x000E9C04
	public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
	{
		GameObject gameObject = Object.Instantiate<Transform>(Temp.transform).gameObject;
		gameObject.transform.SetParent(parent.transform);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x000EBA40 File Offset: 0x000E9C40
	public void clearItem()
	{
		foreach (object obj in this.itemobj.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x000EBAB0 File Offset: 0x000E9CB0
	public void addItemAffix(int id)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.toolstiiptemp);
		gameObject.transform.SetParent(this.itemobj.transform);
		gameObject.transform.localPosition = new Vector3(0f, (float)(this.childTexture.height / 2), 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.GetComponent<TooltipScale>().shoudSetPos = false;
		string str = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[id.ToString()]["name"].str);
		string desstr = Tools.instance.Code64ToString(jsonData.instance.ItemJsonData[id.ToString()]["desc"].str).Replace("[FF0000]", "[ff8300]").Replace("[FF00FF]", "[ff8300]");
		gameObject.GetComponentInChildren<UILabel>().text = "[ff8300]" + str + ":[-] " + Tools.getDesc(desstr, 1);
		gameObject.GetComponent<TooltipScale>().setBGwight();
		gameObject.GetComponent<Animation>().Play("ShowBuff");
	}

	// Token: 0x06002228 RID: 8744 RVA: 0x000EBBE0 File Offset: 0x000E9DE0
	public void newSlot(int type, int Num)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.slots);
		gameObject.SetActive(true);
		gameObject.transform.SetParent(this.grid.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.Find("Num").GetComponent<UILabel>().text = Num.ToString();
		gameObject.GetComponent<UITexture>().mainTexture = (Resources.Load("Ui Icon/Fight/chast_" + type) as Texture);
	}

	// Token: 0x06002229 RID: 8745 RVA: 0x000EBC6A File Offset: 0x000E9E6A
	protected override void Update()
	{
		if (this.shoudSetPos)
		{
			base.Update();
			this.updateCizhui();
		}
	}

	// Token: 0x0600222A RID: 8746 RVA: 0x000EBC80 File Offset: 0x000E9E80
	public void updateCizhui()
	{
		Vector3 vector;
		vector..ctor(0f, 0f, 0f);
		if (Input.mousePosition.x > (float)(Screen.width / 2))
		{
			vector.x -= (float)this.childTexture.width;
		}
		else
		{
			vector.x += (float)this.childTexture.width;
		}
		bool flag = true;
		if (Input.mousePosition.y < (float)(Screen.height / 2))
		{
			vector.y -= (float)(this.childTexture.height / 2);
		}
		else
		{
			vector.y += (float)(this.childTexture.height / 2);
			flag = false;
		}
		int num = 0;
		if (this.cizhui.transform.childCount > 0)
		{
			if (flag)
			{
				num += this.cizhui.transform.GetChild(0).GetChild(0).GetComponent<UITexture>().height / 2;
			}
			else
			{
				num -= this.cizhui.transform.GetChild(0).GetChild(0).GetComponent<UITexture>().height / 2;
			}
		}
		foreach (object obj in this.cizhui.transform)
		{
			Transform transform = (Transform)obj;
			transform.transform.localPosition = Vector3.zero + new Vector3(0f, (float)num, 0f);
			if (flag)
			{
				num += transform.transform.GetChild(0).GetComponent<UITexture>().height;
			}
			else
			{
				num -= transform.transform.GetChild(0).GetComponent<UITexture>().height;
			}
		}
	}

	// Token: 0x04001BA0 RID: 7072
	public int SkillID;

	// Token: 0x04001BA1 RID: 7073
	public UILabel lengque;

	// Token: 0x04001BA2 RID: 7074
	public UILabel Lname;

	// Token: 0x04001BA3 RID: 7075
	public UILabel LLV;

	// Token: 0x04001BA4 RID: 7076
	public UILabel desc;

	// Token: 0x04001BA5 RID: 7077
	public GameObject slots;

	// Token: 0x04001BA6 RID: 7078
	public GameObject grid;

	// Token: 0x04001BA7 RID: 7079
	public GameObject toolstiiptemp;

	// Token: 0x04001BA8 RID: 7080
	public GameObject cizhui;

	// Token: 0x04001BA9 RID: 7081
	public GameObject itemobj;

	// Token: 0x04001BAA RID: 7082
	public List<Sprite> lingQi;

	// Token: 0x04001BAB RID: 7083
	public GameObject fengexianImage;

	// Token: 0x04001BAC RID: 7084
	public GameObject lingqiimage;

	// Token: 0x04001BAD RID: 7085
	public UITexture uITexture;

	// Token: 0x04001BAE RID: 7086
	public GameObject Daride;

	// Token: 0x04001BAF RID: 7087
	private bool canShowTooltip = true;

	// Token: 0x04001BB0 RID: 7088
	private bool firstCall = true;
}
