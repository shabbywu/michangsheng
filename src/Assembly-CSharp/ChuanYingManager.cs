using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class ChuanYingManager
{
	// Token: 0x0600179D RID: 6045 RVA: 0x000A2AAB File Offset: 0x000A0CAB
	public ChuanYingManager(Entity avater)
	{
		this.NewTipsSum = 0;
		this.avatar = (Avatar)avater;
		this.rd = new Random();
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x000A2AD4 File Offset: 0x000A0CD4
	public void addChuanYingFu(int id)
	{
		JSONObject jsonobject = jsonData.instance.ChuanYingFuBiao[id.ToString()];
		if (jsonobject["DelayTime"].Count > 0)
		{
			this.avatar.NoGetChuanYingList.SetField(id.ToString(), this.ReadData(jsonobject));
			return;
		}
		JSONObject jsonobject2 = this.ReadData(jsonobject);
		this.avatar.NewChuanYingList.SetField(id.ToString(), this.ReadData(jsonobject));
		this.avatar.emailDateMag.OldToPlayer(jsonobject2["AvatarID"].I, id, jsonobject2["sendTime"].str);
		if (jsonobject["Type"].I == 3)
		{
			this.avatar.chuanYingManager.NewTipsSum = 1;
		}
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x000A2BA8 File Offset: 0x000A0DA8
	public void AddCy(int cyId, int itemID)
	{
		JSONObject jsonobject = jsonData.instance.ChuanYingFuBiao[cyId.ToString()];
		if (jsonobject["DelayTime"].Count > 0)
		{
			this.avatar.NoGetChuanYingList.SetField(cyId.ToString(), this.ReadData(jsonobject));
			return;
		}
		JSONObject jsonobject2 = this.ReadData(jsonobject);
		jsonobject2.SetField("ItemID", itemID);
		this.avatar.NewChuanYingList.SetField(cyId.ToString(), jsonobject2.Copy());
		this.avatar.emailDateMag.OldToPlayer(jsonobject2["AvatarID"].I, cyId, jsonobject2["sendTime"].str);
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x000A2C60 File Offset: 0x000A0E60
	public void AddCyByExchange(int cyId, int itemId)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("AvatarID", 912);
		jsonobject.SetField("id", cyId + PlayerEx.Player.ExchangeMeetingID);
		jsonobject.SetField("ItemID", itemId);
		jsonobject.SetField("ItemHasGet", false);
		jsonobject.SetField("sendTime", PlayerEx.Player.worldTimeMag.nowTime);
		this.avatar.NewChuanYingList.SetField((cyId + PlayerEx.Player.ExchangeMeetingID).ToString(), jsonobject.Copy());
		this.avatar.emailDateMag.OldToPlayerByExchange(jsonobject["AvatarID"].I, cyId + PlayerEx.Player.ExchangeMeetingID, jsonobject["sendTime"].str);
		PlayerEx.Player.ExchangeMeetingID++;
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x000A2D44 File Offset: 0x000A0F44
	private JSONObject ReadData(JSONObject obj)
	{
		JSONObject jsonobject = new JSONObject();
		int i = obj["AvatarID"].I;
		jsonobject.SetField("id", obj["id"].I);
		jsonobject.SetField("AvatarID", i);
		string text = obj["info"].str;
		text = text.Replace("{LastName}", this.avatar.lastName).Replace("{FirstName}", this.avatar.firstName).Replace("{gongzi}", (this.avatar.Sex == 1) ? "公子" : "姑娘").Replace("{xiongdi}", (this.avatar.Sex == 1) ? "兄弟" : "姑娘").Replace("{shidi}", (this.avatar.Sex == 1) ? "师弟" : "师妹").Replace("{shixiong}", (this.avatar.Sex == 1) ? "师兄" : "师姐").Replace("{xiaozi}", (this.avatar.Sex == 1) ? "小子" : "丫头").Replace("{ta}", (this.avatar.Sex == 1) ? "他" : "她").Replace("{menpai}", Tools.getStr("menpai" + this.avatar.menPai));
		jsonobject.SetField("info", Tools.Code64(text));
		JSONObject jsonobject2 = obj["DelayTime"];
		DateTime dateTime = this.avatar.worldTimeMag.getNowTime();
		if (obj["Type"].I == 1)
		{
			try
			{
				dateTime = DateTime.Parse(obj["StarTime"].str);
			}
			catch
			{
				Debug.Log("错误" + obj["StarTime"].str);
			}
		}
		if (obj["DelayTime"].Count > 0)
		{
			int num = this.rd.Next(jsonobject2[0].I, jsonobject2[1].I);
			dateTime = dateTime.AddDays((double)num);
		}
		jsonobject.SetField("sendTime", dateTime.ToString());
		bool flag = false;
		if (obj["TaskID"].I > 0)
		{
			jsonobject.SetField("TaskID", obj["TaskID"].I);
			flag = true;
		}
		if (obj["TaskIndex"].Count > 0)
		{
			jsonobject.SetField("TaskIndex", obj["TaskIndex"]);
			flag = true;
		}
		if (obj["WeiTuo"].I > 0)
		{
			jsonobject.SetField("WeiTuo", obj["WeiTuo"].I);
			flag = true;
		}
		if (obj["ItemID"].I > 0)
		{
			jsonobject.SetField("ItemID", obj["ItemID"].I);
			jsonobject.SetField("ItemHasGet", false);
		}
		if (obj["valueID"].Count > 0)
		{
			jsonobject.SetField("valueID", obj["valueID"]);
			jsonobject.SetField("value", obj["value"]);
			flag = true;
		}
		if (obj["IsAdd"].I > 0)
		{
			jsonobject.SetField("IsAdd", obj["IsAdd"].I);
		}
		if (obj["IsDelete"].I > 0)
		{
			jsonobject.SetField("IsDelete", obj["IsDelete"].I);
		}
		if (flag)
		{
			jsonobject.SetField("CanCaoZuo", true);
		}
		else
		{
			jsonobject.SetField("CanCaoZuo", false);
		}
		jsonobject.SetField("AvatarName", jsonData.instance.AvatarJsonData[i.ToString()]["Name"].Str);
		return jsonobject;
	}

	// Token: 0x04001266 RID: 4710
	private Avatar avatar;

	// Token: 0x04001267 RID: 4711
	private Random rd;

	// Token: 0x04001268 RID: 4712
	public int NewTipsSum;
}
