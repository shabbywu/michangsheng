using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x0200032F RID: 815
public class MainUITianFuCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001C10 RID: 7184 RVA: 0x000C90C0 File Offset: 0x000C72C0
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

	// Token: 0x06001C11 RID: 7185 RVA: 0x000C9230 File Offset: 0x000C7430
	public void OnPointerEnter(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Show(this.desc, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z));
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x000C69EF File Offset: 0x000C4BEF
	public void OnPointerExit(PointerEventData eventData)
	{
		MainUIMag.inst.tooltip.Hide();
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x000C9287 File Offset: 0x000C7487
	public int getValue(int _seid)
	{
		if (this.seidList.Contains(_seid))
		{
			return (int)jsonData.instance.CrateAvatarSeidJsonData[_seid][this.id.ToString()]["value1"].n;
		}
		return 0;
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x000C92C5 File Offset: 0x000C74C5
	public int getSeidValue1()
	{
		return this.getValue(1);
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x000C92CE File Offset: 0x000C74CE
	public int getSeidValue2()
	{
		return this.getValue(2);
	}

	// Token: 0x06001C16 RID: 7190 RVA: 0x000C92D7 File Offset: 0x000C74D7
	public int getSeidValue3()
	{
		return this.getValue(3);
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000C92E0 File Offset: 0x000C74E0
	public int getSeidValue4()
	{
		return this.getValue(4);
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x000C92E9 File Offset: 0x000C74E9
	public List<JSONObject> getSeidValue9()
	{
		if (this.seidList.Contains(9))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[9][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x06001C19 RID: 7193 RVA: 0x000C9328 File Offset: 0x000C7528
	public List<JSONObject> getSeidValue10()
	{
		if (this.seidList.Contains(10))
		{
			return jsonData.instance.CrateAvatarSeidJsonData[10][this.id.ToString()]["value1"].list;
		}
		return null;
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x000C9368 File Offset: 0x000C7568
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

	// Token: 0x06001C1B RID: 7195 RVA: 0x000C93F8 File Offset: 0x000C75F8
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

	// Token: 0x06001C1C RID: 7196 RVA: 0x000C947B File Offset: 0x000C767B
	public void realizedSeid12()
	{
		this.PlayerSetSeid(12);
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x000C9485 File Offset: 0x000C7685
	public void realizedSeid13()
	{
		this.PlayerSetSeid(13);
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x000C9490 File Offset: 0x000C7690
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

	// Token: 0x06001C1F RID: 7199 RVA: 0x000C961C File Offset: 0x000C781C
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

	// Token: 0x06001C20 RID: 7200 RVA: 0x000C96AE File Offset: 0x000C78AE
	public void realizedSeid17()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(17), 0);
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x000C96D1 File Offset: 0x000C78D1
	public void realizedSeid18()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(18), 0);
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x000C96F4 File Offset: 0x000C78F4
	public void realizedSeid22()
	{
		Tools.instance.getPlayer().TianFuID.SetField(string.Concat(22), this.id.ToString());
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x000C9724 File Offset: 0x000C7924
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

	// Token: 0x040016A8 RID: 5800
	public MainUIToggle toggle;

	// Token: 0x040016A9 RID: 5801
	public List<int> seidList;

	// Token: 0x040016AA RID: 5802
	public string desc;

	// Token: 0x040016AB RID: 5803
	public int id;

	// Token: 0x040016AC RID: 5804
	public int costNum;

	// Token: 0x040016AD RID: 5805
	[SerializeField]
	private Text Name;

	// Token: 0x040016AE RID: 5806
	[SerializeField]
	private Text cost;

	// Token: 0x040016AF RID: 5807
	public int page;
}
