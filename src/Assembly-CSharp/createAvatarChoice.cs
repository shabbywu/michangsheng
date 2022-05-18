using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x02000585 RID: 1413
public class createAvatarChoice : MonoBehaviour
{
	// Token: 0x060023CC RID: 9164 RVA: 0x0001CE00 File Offset: 0x0001B000
	private void Start()
	{
		this.tianfuUI = base.transform.parent.parent.parent.GetComponent<createTianfu>();
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x0012584C File Offset: 0x00123A4C
	private void Update()
	{
		this.CastTianFu.text = string.Concat((int)jsonData.instance.CreateAvatarJsonData[this.id.ToString()]["feiYong"].n);
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x00125898 File Offset: 0x00123A98
	protected void OnHover(bool isOver)
	{
		if (isOver)
		{
			this.Tooltips.uILabel.text = "";
			UILabel uILabel = this.Tooltips.uILabel;
			uILabel.text = uILabel.text + "[24a5d6]效果：[-]" + this.desc;
			UILabel uILabel2 = this.Tooltips.uILabel;
			uILabel2.text = uILabel2.text + "\n\n[db9a53]说明：[-]" + this.descInfo;
			if (this.isLock)
			{
				UILabel uILabel3 = this.Tooltips.uILabel;
				uILabel3.text = uILabel3.text + "\n\n[db9a53]解锁：[-]" + this.LockMessager;
			}
			this.Tooltips.showTooltip = true;
			return;
		}
		this.Tooltips.showTooltip = false;
	}

	// Token: 0x060023CF RID: 9167 RVA: 0x0001CE22 File Offset: 0x0001B022
	private void OnPress()
	{
		this.Tooltips.showTooltip = false;
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x0001CE30 File Offset: 0x0001B030
	public void setValue()
	{
		UIToggle toggle = base.GetComponent<UIToggle>();
		delegate
		{
			if (this.cast > 0 && toggle.value && CreateAvatarMag.inst.tianfuUI.TianFuDian < 0 && !this.seid.Contains(19))
			{
				toggle.value = false;
				UIPopTip.Inst.Pop("天赋点不足", PopTipIconType.叹号);
			}
			if (toggle.value && this.seid.Contains(19))
			{
				CreateAvatarMag.inst.tianfuUI.TianFuDian = this.getValue(19);
			}
			foreach (object obj in CreateAvatarMag.inst.tianfuUI.grid.transform)
			{
				Transform transform = (Transform)obj;
				createAvatarChoice componentInChildren = transform.GetComponentInChildren<createAvatarChoice>();
				UIToggle componentInChildren2 = transform.GetComponentInChildren<UIToggle>();
				if (componentInChildren2.value && componentInChildren.seid.Contains(20) && Tools.JsonListToList(jsonData.instance.CrateAvatarSeidJsonData[20][componentInChildren.id.ToString()]["value1"]).Contains(toggle.group) && componentInChildren2.value)
				{
					componentInChildren2.value = false;
					transform.GetChild(0).Find("Background").gameObject.SetActive(true);
				}
			}
			if (toggle.value && this.seid.Contains(20))
			{
				List<int> list = Tools.JsonListToList(jsonData.instance.CrateAvatarSeidJsonData[20][this.id.ToString()]["value1"]);
				foreach (object obj2 in CreateAvatarMag.inst.tianfuUI.grid.transform)
				{
					Transform transform2 = (Transform)obj2;
					UIToggle componentInChildren3 = transform2.GetComponentInChildren<UIToggle>();
					if (list.Contains(componentInChildren3.group) && componentInChildren3.value)
					{
						componentInChildren3.value = false;
						transform2.GetChild(0).Find("Background").gameObject.SetActive(true);
					}
				}
			}
		}();
	}

	// Token: 0x060023D1 RID: 9169 RVA: 0x0001CE5A File Offset: 0x0001B05A
	public int getValue(int _seid)
	{
		if (this.seid.Contains(_seid))
		{
			return (int)jsonData.instance.CrateAvatarSeidJsonData[_seid][this.id.ToString()]["value1"].n;
		}
		return 0;
	}

	// Token: 0x060023D2 RID: 9170 RVA: 0x0001CE98 File Offset: 0x0001B098
	public int getSeidValue1()
	{
		return this.getValue(1);
	}

	// Token: 0x060023D3 RID: 9171 RVA: 0x0001CEA1 File Offset: 0x0001B0A1
	public int getSeidValue2()
	{
		return this.getValue(2);
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x0001CEAA File Offset: 0x0001B0AA
	public int getSeidValue3()
	{
		return this.getValue(3);
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x0001CEB3 File Offset: 0x0001B0B3
	public int getSeidValue4()
	{
		return this.getValue(4);
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x0001CEBC File Offset: 0x0001B0BC
	public List<JSONObject> getSeidValue9()
	{
		if (this.seid.Contains(9))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[9][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x0001CEFB File Offset: 0x0001B0FB
	public List<JSONObject> getSeidValue10()
	{
		if (this.seid.Contains(10))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[10][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x00125954 File Offset: 0x00123B54
	public List<int> getSeidValue11()
	{
		if (this.seid.Contains(11))
		{
			List<int> list = new List<int>();
			int item = (int)jsonData.instance.CrateAvatarSeidJsonData[11][this.id.ToString()]["value1"].n;
			int item2 = (int)jsonData.instance.CrateAvatarSeidJsonData[11][this.id.ToString()]["value2"].n;
			list.Add(item);
			list.Add(item2);
			return list;
		}
		return null;
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x001259E4 File Offset: 0x00123BE4
	public void PlayerSetSeid(int Seid)
	{
		Avatar player = Tools.instance.getPlayer();
		int value = this.getValue(Seid);
		if (!player.TianFuID.HasField(string.Concat(Seid)))
		{
			player.TianFuID.AddField(string.Concat(Seid), 0);
		}
		player.TianFuID.SetField(string.Concat(Seid), (int)player.TianFuID[string.Concat(Seid)].n + value);
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x0001CF3A File Offset: 0x0001B13A
	public void realizedSeid12()
	{
		this.PlayerSetSeid(12);
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x0001CF44 File Offset: 0x0001B144
	public void realizedSeid13()
	{
		this.PlayerSetSeid(13);
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x00125A68 File Offset: 0x00123C68
	public void realizedSeid15()
	{
		Avatar player = Tools.instance.getPlayer();
		int num = (int)jsonData.instance.CrateAvatarSeidJsonData[15][this.id.ToString()]["value1"].n;
		int num2 = (int)jsonData.instance.CrateAvatarSeidJsonData[15][this.id.ToString()]["value2"].n;
		if (!player.TianFuID.HasField(string.Concat(15)))
		{
			player.TianFuID.SetField(string.Concat(15), new JSONObject(JSONObject.Type.ARRAY));
		}
		for (int i = num; i <= num2; i++)
		{
			player.TianFuID[string.Concat(15)].Add(i);
			foreach (JSONObject jsonobject in jsonData.instance._ItemJsonData.list)
			{
				if (jsonobject["TuJianType"].I == 1 && jsonobject["quality"].I == i)
				{
					int i2 = jsonobject["id"].I;
					TuJianManager.Inst.UnlockItem(i2);
					TuJianManager.Inst.UnlockZhuYao(i2);
					TuJianManager.Inst.UnlockFuYao(i2);
					TuJianManager.Inst.UnlockYaoYin(i2);
				}
			}
		}
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x00125BF4 File Offset: 0x00123DF4
	public void realizedSeid16()
	{
		Avatar player = Tools.instance.getPlayer();
		int value = this.getValue(16);
		if (player.TianFuID.HasField(string.Concat(16)))
		{
			player.TianFuID[string.Concat(16)].Add(value);
			return;
		}
		player.TianFuID.SetField(string.Concat(16), new JSONObject(JSONObject.Type.ARRAY));
		player.TianFuID[string.Concat(16)].Add(value);
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x00019F76 File Offset: 0x00018176
	public void realizedSeid17()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(17), 0);
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x00019F99 File Offset: 0x00018199
	public void realizedSeid18()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(18), 0);
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x0001CF4E File Offset: 0x0001B14E
	public void realizedSeid22()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(22), this.id.ToString());
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x00125C88 File Offset: 0x00123E88
	public void realizedSeid23()
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jsonobject = jsonData.instance.CrateAvatarSeidJsonData[23][this.id.ToString()]["value1"];
		int count = jsonobject.Count;
		JSONObject jsonobject2 = jsonData.instance.CrateAvatarSeidJsonData[23][this.id.ToString()]["value2"];
		for (int i = 0; i < count; i++)
		{
			player.wuDaoMag.addWuDaoEx(jsonobject[i].I, jsonobject2[i].I);
		}
	}

	// Token: 0x04001ED0 RID: 7888
	public int id;

	// Token: 0x04001ED1 RID: 7889
	public UILabel Title;

	// Token: 0x04001ED2 RID: 7890
	public UILabel descUI;

	// Token: 0x04001ED3 RID: 7891
	public string desc;

	// Token: 0x04001ED4 RID: 7892
	public string descInfo;

	// Token: 0x04001ED5 RID: 7893
	public string LockMessager;

	// Token: 0x04001ED6 RID: 7894
	public List<int> seid = new List<int>();

	// Token: 0x04001ED7 RID: 7895
	public int cast;

	// Token: 0x04001ED8 RID: 7896
	public UILabel CastTianFu;

	// Token: 0x04001ED9 RID: 7897
	public TooltipScale Tooltips;

	// Token: 0x04001EDA RID: 7898
	private createTianfu tianfuUI;

	// Token: 0x04001EDB RID: 7899
	public UI2DSprite Background;

	// Token: 0x04001EDC RID: 7900
	public bool isLock;

	// Token: 0x02000586 RID: 1414
	public enum ChoiceID
	{
		// Token: 0x04001EDE RID: 7902
		Seid12 = 12,
		// Token: 0x04001EDF RID: 7903
		Seid13,
		// Token: 0x04001EE0 RID: 7904
		Seid14,
		// Token: 0x04001EE1 RID: 7905
		Seid15,
		// Token: 0x04001EE2 RID: 7906
		Seid16,
		// Token: 0x04001EE3 RID: 7907
		Seid17,
		// Token: 0x04001EE4 RID: 7908
		Seid18,
		// Token: 0x04001EE5 RID: 7909
		Seid19,
		// Token: 0x04001EE6 RID: 7910
		Seid20,
		// Token: 0x04001EE7 RID: 7911
		Seid21,
		// Token: 0x04001EE8 RID: 7912
		Seid22,
		// Token: 0x04001EE9 RID: 7913
		Seid23
	}
}
