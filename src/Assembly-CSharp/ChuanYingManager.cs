using System;
using KBEngine;

// Token: 0x020003BD RID: 957
public class ChuanYingManager
{
	// Token: 0x06001A7A RID: 6778 RVA: 0x000168BF File Offset: 0x00014ABF
	public ChuanYingManager(Entity avater)
	{
		this.NewTipsSum = 0;
		this.avatar = (Avatar)avater;
		this.rd = new Random();
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x000E9EA4 File Offset: 0x000E80A4
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

	// Token: 0x06001A7C RID: 6780 RVA: 0x000E9F78 File Offset: 0x000E8178
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
			dateTime = DateTime.Parse(obj["StarTime"].str);
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

	// Token: 0x040015E9 RID: 5609
	private Avatar avatar;

	// Token: 0x040015EA RID: 5610
	private Random rd;

	// Token: 0x040015EB RID: 5611
	public int NewTipsSum;
}
