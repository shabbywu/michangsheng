using System.Collections.Generic;

namespace KBEngine;

public class Dialog
{
	public Entity entity;

	public Dialog(Entity avater)
	{
		entity = avater;
	}

	public void talkByID(int id)
	{
		Avatar avatar = (Avatar)entity;
		List<string> list = new List<string>();
		List<uint> list2 = new List<uint>();
		JSONObject jSONObject = jsonData.instance.TalkingJsonData[string.Concat(id)];
		for (int i = 1; i <= 5; i++)
		{
			string str = jSONObject["func" + i].str;
			if (str != "")
			{
				switch (str)
				{
				case "fight":
					avatar.startFight((int)jSONObject["funcargs" + i][0].n);
					break;
				case "message":
					avatar.messagelog(10, (uint)jSONObject["funcargs" + i][0].n);
					break;
				}
			}
		}
		for (int j = 1; j <= 5; j++)
		{
			int num = (int)jSONObject["menu" + j].n;
			if (num != 0)
			{
				list2.Add((uint)num);
				list.Add(Tools.instance.Code64ToString(jsonData.instance.TalkingJsonData[string.Concat(num)]["title"].str));
			}
		}
		avatar.dialog_setContent(id, list2, list, Tools.instance.Code64ToString(jSONObject["title"].str), Tools.instance.Code64ToString(jSONObject["body"].str), Tools.instance.Code64ToString(jSONObject["sayname"].str));
	}

	public void dialog(int targetID, uint dialogID)
	{
		talkByID((int)dialogID);
	}

	public void messagelog(int targetID, uint dialogID)
	{
		messageByID((int)dialogID);
	}

	public void messageByID(int id)
	{
		Avatar obj = (Avatar)entity;
		JSONObject jSONObject = jsonData.instance.MessageJsonData[string.Concat(id)];
		obj.messagelog_setContent(id, Tools.instance.Code64ToString(jSONObject["title"].str), Tools.instance.Code64ToString(jSONObject["body"].str), Tools.instance.Code64ToString(jSONObject["messageInfo"].str));
	}
}
