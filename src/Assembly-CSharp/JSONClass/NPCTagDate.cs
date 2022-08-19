using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008AD RID: 2221
	public class NPCTagDate : IJSONClass
	{
		// Token: 0x060040C7 RID: 16583 RVA: 0x001BAD80 File Offset: 0x001B8F80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCTagDate.list)
			{
				try
				{
					NPCTagDate npctagDate = new NPCTagDate();
					npctagDate.id = jsonobject["id"].I;
					npctagDate.zhengxie = jsonobject["zhengxie"].I;
					npctagDate.GuanLianTalk = jsonobject["GuanLianTalk"].Str;
					npctagDate.WuDao = jsonobject["WuDao"].ToList();
					npctagDate.Change = jsonobject["Change"].ToList();
					npctagDate.ChangeTo = jsonobject["ChangeTo"].ToList();
					npctagDate.BeiBaoType = jsonobject["BeiBaoType"].ToList();
					if (NPCTagDate.DataDict.ContainsKey(npctagDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCTagDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npctagDate.id));
					}
					else
					{
						NPCTagDate.DataDict.Add(npctagDate.id, npctagDate);
						NPCTagDate.DataList.Add(npctagDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCTagDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCTagDate.OnInitFinishAction != null)
			{
				NPCTagDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EEB RID: 16107
		public static Dictionary<int, NPCTagDate> DataDict = new Dictionary<int, NPCTagDate>();

		// Token: 0x04003EEC RID: 16108
		public static List<NPCTagDate> DataList = new List<NPCTagDate>();

		// Token: 0x04003EED RID: 16109
		public static Action OnInitFinishAction = new Action(NPCTagDate.OnInitFinish);

		// Token: 0x04003EEE RID: 16110
		public int id;

		// Token: 0x04003EEF RID: 16111
		public int zhengxie;

		// Token: 0x04003EF0 RID: 16112
		public string GuanLianTalk;

		// Token: 0x04003EF1 RID: 16113
		public List<int> WuDao = new List<int>();

		// Token: 0x04003EF2 RID: 16114
		public List<int> Change = new List<int>();

		// Token: 0x04003EF3 RID: 16115
		public List<int> ChangeTo = new List<int>();

		// Token: 0x04003EF4 RID: 16116
		public List<int> BeiBaoType = new List<int>();
	}
}
