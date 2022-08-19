using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C76 RID: 3190
	public class Dialog
	{
		// Token: 0x0600580C RID: 22540 RVA: 0x00248CC7 File Offset: 0x00246EC7
		public Dialog(Entity avater)
		{
			this.entity = avater;
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x00248CD8 File Offset: 0x00246ED8
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

		// Token: 0x0600580E RID: 22542 RVA: 0x00248EAA File Offset: 0x002470AA
		public void dialog(int targetID, uint dialogID)
		{
			this.talkByID((int)dialogID);
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x00248EB3 File Offset: 0x002470B3
		public void messagelog(int targetID, uint dialogID)
		{
			this.messageByID((int)dialogID);
		}

		// Token: 0x06005810 RID: 22544 RVA: 0x00248EBC File Offset: 0x002470BC
		public void messageByID(int id)
		{
			Avatar avatar = (Avatar)this.entity;
			JSONObject jsonobject = jsonData.instance.MessageJsonData[string.Concat(id)];
			avatar.messagelog_setContent(id, Tools.instance.Code64ToString(jsonobject["title"].str), Tools.instance.Code64ToString(jsonobject["body"].str), Tools.instance.Code64ToString(jsonobject["messageInfo"].str));
		}

		// Token: 0x040051FB RID: 20987
		public Entity entity;
	}
}
