using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x0200101C RID: 4124
	public class Dialog
	{
		// Token: 0x0600628C RID: 25228 RVA: 0x00044374 File Offset: 0x00042574
		public Dialog(Entity avater)
		{
			this.entity = avater;
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x00274BEC File Offset: 0x00272DEC
		public void talkByID(int id)
		{
			Avatar avatar = (Avatar)this.entity;
			List<string> list = new List<string>();
			List<uint> list2 = new List<uint>();
			JSONObject jsonobject = jsonData.instance.TalkingJsonData[string.Concat(id)];
			for (int i = 1; i <= 5; i++)
			{
				string str = jsonobject["func" + i].str;
				if (str != "")
				{
					if (str == "close")
					{
						return;
					}
					if (str == "fight")
					{
						avatar.startFight((int)jsonobject["funcargs" + i][0].n);
					}
					else if (str == "message")
					{
						avatar.messagelog(10, (uint)jsonobject["funcargs" + i][0].n);
					}
				}
			}
			for (int j = 1; j <= 5; j++)
			{
				int num = (int)jsonobject["menu" + j].n;
				if (num != 0)
				{
					list2.Add((uint)num);
					list.Add(Tools.instance.Code64ToString(jsonData.instance.TalkingJsonData[string.Concat(num)]["title"].str));
				}
			}
			avatar.dialog_setContent(id, list2, list, Tools.instance.Code64ToString(jsonobject["title"].str), Tools.instance.Code64ToString(jsonobject["body"].str), Tools.instance.Code64ToString(jsonobject["sayname"].str));
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x00044383 File Offset: 0x00042583
		public void dialog(int targetID, uint dialogID)
		{
			this.talkByID((int)dialogID);
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x0004438C File Offset: 0x0004258C
		public void messagelog(int targetID, uint dialogID)
		{
			this.messageByID((int)dialogID);
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x00274DC0 File Offset: 0x00272FC0
		public void messageByID(int id)
		{
			Avatar avatar = (Avatar)this.entity;
			JSONObject jsonobject = jsonData.instance.MessageJsonData[string.Concat(id)];
			avatar.messagelog_setContent(id, Tools.instance.Code64ToString(jsonobject["title"].str), Tools.instance.Code64ToString(jsonobject["body"].str), Tools.instance.Code64ToString(jsonobject["messageInfo"].str));
		}

		// Token: 0x04005CEE RID: 23790
		public Entity entity;
	}
}
