using System;
using KBEngine;
using UnityEngine;

public class ChuanYingManager
{
	private Avatar avatar;

	private Random rd;

	public int NewTipsSum;

	public ChuanYingManager(Entity avater)
	{
		NewTipsSum = 0;
		avatar = (Avatar)avater;
		rd = new Random();
	}

	public void addChuanYingFu(int id)
	{
		JSONObject jSONObject = jsonData.instance.ChuanYingFuBiao[id.ToString()];
		if (jSONObject["DelayTime"].Count > 0)
		{
			avatar.NoGetChuanYingList.SetField(id.ToString(), ReadData(jSONObject));
			return;
		}
		JSONObject jSONObject2 = ReadData(jSONObject);
		avatar.NewChuanYingList.SetField(id.ToString(), ReadData(jSONObject));
		avatar.emailDateMag.OldToPlayer(jSONObject2["AvatarID"].I, id, jSONObject2["sendTime"].str);
		if (jSONObject["Type"].I == 3)
		{
			avatar.chuanYingManager.NewTipsSum = 1;
		}
	}

	public void AddCy(int cyId, int itemID)
	{
		JSONObject jSONObject = jsonData.instance.ChuanYingFuBiao[cyId.ToString()];
		if (jSONObject["DelayTime"].Count > 0)
		{
			avatar.NoGetChuanYingList.SetField(cyId.ToString(), ReadData(jSONObject));
			return;
		}
		JSONObject jSONObject2 = ReadData(jSONObject);
		jSONObject2.SetField("ItemID", itemID);
		avatar.NewChuanYingList.SetField(cyId.ToString(), jSONObject2.Copy());
		avatar.emailDateMag.OldToPlayer(jSONObject2["AvatarID"].I, cyId, jSONObject2["sendTime"].str);
	}

	public void AddCyByExchange(int cyId, int itemId)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("AvatarID", 912);
		jSONObject.SetField("id", cyId + PlayerEx.Player.ExchangeMeetingID);
		jSONObject.SetField("ItemID", itemId);
		jSONObject.SetField("ItemHasGet", val: false);
		jSONObject.SetField("sendTime", PlayerEx.Player.worldTimeMag.nowTime);
		avatar.NewChuanYingList.SetField((cyId + PlayerEx.Player.ExchangeMeetingID).ToString(), jSONObject.Copy());
		avatar.emailDateMag.OldToPlayerByExchange(jSONObject["AvatarID"].I, cyId + PlayerEx.Player.ExchangeMeetingID, jSONObject["sendTime"].str);
		PlayerEx.Player.ExchangeMeetingID++;
	}

	private JSONObject ReadData(JSONObject obj)
	{
		JSONObject jSONObject = new JSONObject();
		int i = obj["AvatarID"].I;
		jSONObject.SetField("id", obj["id"].I);
		jSONObject.SetField("AvatarID", i);
		string str = obj["info"].str;
		str = str.Replace("{LastName}", avatar.lastName).Replace("{FirstName}", avatar.firstName).Replace("{gongzi}", (avatar.Sex == 1) ? "公子" : "姑娘")
			.Replace("{xiongdi}", (avatar.Sex == 1) ? "兄弟" : "姑娘")
			.Replace("{shidi}", (avatar.Sex == 1) ? "师弟" : "师妹")
			.Replace("{shixiong}", (avatar.Sex == 1) ? "师兄" : "师姐")
			.Replace("{xiaozi}", (avatar.Sex == 1) ? "小子" : "丫头")
			.Replace("{ta}", (avatar.Sex == 1) ? "他" : "她")
			.Replace("{menpai}", Tools.getStr("menpai" + avatar.menPai));
		jSONObject.SetField("info", Tools.Code64(str));
		JSONObject jSONObject2 = obj["DelayTime"];
		DateTime dateTime = avatar.worldTimeMag.getNowTime();
		if (obj["Type"].I == 1)
		{
			try
			{
				dateTime = DateTime.Parse(obj["StarTime"].str);
			}
			catch
			{
				Debug.Log((object)("错误" + obj["StarTime"].str));
			}
		}
		if (obj["DelayTime"].Count > 0)
		{
			int num = rd.Next(jSONObject2[0].I, jSONObject2[1].I);
			dateTime = dateTime.AddDays(num);
		}
		jSONObject.SetField("sendTime", dateTime.ToString());
		bool flag = false;
		if (obj["TaskID"].I > 0)
		{
			jSONObject.SetField("TaskID", obj["TaskID"].I);
			flag = true;
		}
		if (obj["TaskIndex"].Count > 0)
		{
			jSONObject.SetField("TaskIndex", obj["TaskIndex"]);
			flag = true;
		}
		if (obj["WeiTuo"].I > 0)
		{
			jSONObject.SetField("WeiTuo", obj["WeiTuo"].I);
			flag = true;
		}
		if (obj["ItemID"].I > 0)
		{
			jSONObject.SetField("ItemID", obj["ItemID"].I);
			jSONObject.SetField("ItemHasGet", val: false);
		}
		if (obj["valueID"].Count > 0)
		{
			jSONObject.SetField("valueID", obj["valueID"]);
			jSONObject.SetField("value", obj["value"]);
			flag = true;
		}
		if (obj["IsAdd"].I > 0)
		{
			jSONObject.SetField("IsAdd", obj["IsAdd"].I);
		}
		if (obj["IsDelete"].I > 0)
		{
			jSONObject.SetField("IsDelete", obj["IsDelete"].I);
		}
		if (flag)
		{
			jSONObject.SetField("CanCaoZuo", val: true);
		}
		else
		{
			jSONObject.SetField("CanCaoZuo", val: false);
		}
		jSONObject.SetField("AvatarName", jsonData.instance.AvatarJsonData[i.ToString()]["Name"].Str);
		return jSONObject;
	}
}
