using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x020003E9 RID: 1001
public class createAvatarChoice : MonoBehaviour
{
	// Token: 0x06002047 RID: 8263 RVA: 0x000E349B File Offset: 0x000E169B
	private void Start()
	{
		this.tianfuUI = base.transform.parent.parent.parent.GetComponent<createTianfu>();
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x000E34C0 File Offset: 0x000E16C0
	private void Update()
	{
		this.CastTianFu.text = string.Concat((int)jsonData.instance.CreateAvatarJsonData[this.id.ToString()]["feiYong"].n);
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x000E350C File Offset: 0x000E170C
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

	// Token: 0x0600204A RID: 8266 RVA: 0x000E35C7 File Offset: 0x000E17C7
	private void OnPress()
	{
		this.Tooltips.showTooltip = false;
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x000E35D5 File Offset: 0x000E17D5
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

	// Token: 0x0600204C RID: 8268 RVA: 0x000E35FF File Offset: 0x000E17FF
	public int getValue(int _seid)
	{
		if (this.seid.Contains(_seid))
		{
			return (int)jsonData.instance.CrateAvatarSeidJsonData[_seid][this.id.ToString()]["value1"].n;
		}
		return 0;
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x000E363D File Offset: 0x000E183D
	public int getSeidValue1()
	{
		return this.getValue(1);
	}

	// Token: 0x0600204E RID: 8270 RVA: 0x000E3646 File Offset: 0x000E1846
	public int getSeidValue2()
	{
		return this.getValue(2);
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x000E364F File Offset: 0x000E184F
	public int getSeidValue3()
	{
		return this.getValue(3);
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x000E3658 File Offset: 0x000E1858
	public int getSeidValue4()
	{
		return this.getValue(4);
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x000E3661 File Offset: 0x000E1861
	public List<JSONObject> getSeidValue9()
	{
		if (this.seid.Contains(9))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[9][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x000E36A0 File Offset: 0x000E18A0
	public List<JSONObject> getSeidValue10()
	{
		if (this.seid.Contains(10))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[10][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x000E36E0 File Offset: 0x000E18E0
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

	// Token: 0x06002054 RID: 8276 RVA: 0x000E3770 File Offset: 0x000E1970
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

	// Token: 0x06002055 RID: 8277 RVA: 0x000E37F3 File Offset: 0x000E19F3
	public void realizedSeid12()
	{
		this.PlayerSetSeid(12);
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x000E37FD File Offset: 0x000E19FD
	public void realizedSeid13()
	{
		this.PlayerSetSeid(13);
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x000E3808 File Offset: 0x000E1A08
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

	// Token: 0x06002058 RID: 8280 RVA: 0x000E3994 File Offset: 0x000E1B94
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

	// Token: 0x06002059 RID: 8281 RVA: 0x000C96AE File Offset: 0x000C78AE
	public void realizedSeid17()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(17), 0);
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x000C96D1 File Offset: 0x000C78D1
	public void realizedSeid18()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(18), 0);
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x000E3A26 File Offset: 0x000E1C26
	public void realizedSeid22()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(22), this.id.ToString());
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x000E3A54 File Offset: 0x000E1C54
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

	// Token: 0x04001A3B RID: 6715
	public int id;

	// Token: 0x04001A3C RID: 6716
	public UILabel Title;

	// Token: 0x04001A3D RID: 6717
	public UILabel descUI;

	// Token: 0x04001A3E RID: 6718
	public string desc;

	// Token: 0x04001A3F RID: 6719
	public string descInfo;

	// Token: 0x04001A40 RID: 6720
	public string LockMessager;

	// Token: 0x04001A41 RID: 6721
	public List<int> seid = new List<int>();

	// Token: 0x04001A42 RID: 6722
	public int cast;

	// Token: 0x04001A43 RID: 6723
	public UILabel CastTianFu;

	// Token: 0x04001A44 RID: 6724
	public TooltipScale Tooltips;

	// Token: 0x04001A45 RID: 6725
	private createTianfu tianfuUI;

	// Token: 0x04001A46 RID: 6726
	public UI2DSprite Background;

	// Token: 0x04001A47 RID: 6727
	public bool isLock;

	// Token: 0x02001380 RID: 4992
	public enum ChoiceID
	{
		// Token: 0x040068A7 RID: 26791
		Seid12 = 12,
		// Token: 0x040068A8 RID: 26792
		Seid13,
		// Token: 0x040068A9 RID: 26793
		Seid14,
		// Token: 0x040068AA RID: 26794
		Seid15,
		// Token: 0x040068AB RID: 26795
		Seid16,
		// Token: 0x040068AC RID: 26796
		Seid17,
		// Token: 0x040068AD RID: 26797
		Seid18,
		// Token: 0x040068AE RID: 26798
		Seid19,
		// Token: 0x040068AF RID: 26799
		Seid20,
		// Token: 0x040068B0 RID: 26800
		Seid21,
		// Token: 0x040068B1 RID: 26801
		Seid22,
		// Token: 0x040068B2 RID: 26802
		Seid23
	}
}
