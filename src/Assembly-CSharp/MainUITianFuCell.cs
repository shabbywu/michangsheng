using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x0200049D RID: 1181
public class MainUITianFuCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001F62 RID: 8034 RVA: 0x0010E5AC File Offset: 0x0010C7AC
	public void Init(JSONObject json)
	{
		this.id = json["id"].I;
		this.costNum = json["feiYong"].I;
		this.cost.text = this.costNum.ToString();
		this.toggle.group = json["fenZu"].I;
		this.seidList = json["seid"].ToList();
		this.Name.text = json["Title"].Str;
		this.desc = "<color=#24a5d6>效果：</color>" + json["Desc"].Str;
		this.desc = this.desc + "\n\n<color=#db9a53>说明：</color>" + json["Info"].Str;
		this.page = json["fenLeiGuanLian"].I;
		if (json["jiesuo"].I > MainUIMag.inst.maxLevel)
		{
			this.toggle.SetDisable();
			this.desc = this.desc + "\n\n<color=#db9a53>解锁：</color>剧情模式境界达到" + jsonData.instance.LevelUpDataJsonData[json["jiesuo"].I.ToString()]["Name"].Str + "解锁";
		}
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x0010E71C File Offset: 0x0010C91C
	public void OnPointerEnter(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Show(this.desc, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z));
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x00019C25 File Offset: 0x00017E25
	public void OnPointerExit(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Hide();
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x00019E82 File Offset: 0x00018082
	public int getValue(int _seid)
	{
		if (this.seidList.Contains(_seid))
		{
			return (int)jsonData.instance.CrateAvatarSeidJsonData[_seid][this.id.ToString()]["value1"].n;
		}
		return 0;
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x00019EC0 File Offset: 0x000180C0
	public int getSeidValue1()
	{
		return this.getValue(1);
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x00019EC9 File Offset: 0x000180C9
	public int getSeidValue2()
	{
		return this.getValue(2);
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x00019ED2 File Offset: 0x000180D2
	public int getSeidValue3()
	{
		return this.getValue(3);
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x00019EDB File Offset: 0x000180DB
	public int getSeidValue4()
	{
		return this.getValue(4);
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x00019EE4 File Offset: 0x000180E4
	public List<JSONObject> getSeidValue9()
	{
		if (this.seidList.Contains(9))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[9][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x00019F23 File Offset: 0x00018123
	public List<JSONObject> getSeidValue10()
	{
		if (this.seidList.Contains(10))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[10][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x0010E774 File Offset: 0x0010C974
	public List<int> getSeidValue11()
	{
		if (this.seidList.Contains(11))
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

	// Token: 0x06001F6D RID: 8045 RVA: 0x0010E804 File Offset: 0x0010CA04
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

	// Token: 0x06001F6E RID: 8046 RVA: 0x00019F62 File Offset: 0x00018162
	public void realizedSeid12()
	{
		this.PlayerSetSeid(12);
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x00019F6C File Offset: 0x0001816C
	public void realizedSeid13()
	{
		this.PlayerSetSeid(13);
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x0010E888 File Offset: 0x0010CA88
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

	// Token: 0x06001F71 RID: 8049 RVA: 0x0010EA14 File Offset: 0x0010CC14
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

	// Token: 0x06001F72 RID: 8050 RVA: 0x00019F76 File Offset: 0x00018176
	public void realizedSeid17()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(17), 0);
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x00019F99 File Offset: 0x00018199
	public void realizedSeid18()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(18), 0);
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x00019FBC File Offset: 0x000181BC
	public void realizedSeid22()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(22), this.id.ToString());
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x0010EAA8 File Offset: 0x0010CCA8
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

	// Token: 0x04001ADE RID: 6878
	public MainUIToggle toggle;

	// Token: 0x04001ADF RID: 6879
	public List<int> seidList;

	// Token: 0x04001AE0 RID: 6880
	public string desc;

	// Token: 0x04001AE1 RID: 6881
	public int id;

	// Token: 0x04001AE2 RID: 6882
	public int costNum;

	// Token: 0x04001AE3 RID: 6883
	[SerializeField]
	private Text Name;

	// Token: 0x04001AE4 RID: 6884
	[SerializeField]
	private Text cost;

	// Token: 0x04001AE5 RID: 6885
	public int page;
}
