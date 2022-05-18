using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C3B RID: 3131
	public class NPCTagDate : IJSONClass
	{
		// Token: 0x06004C55 RID: 19541 RVA: 0x00203C14 File Offset: 0x00201E14
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

		// Token: 0x06004C56 RID: 19542 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A43 RID: 19011
		public static Dictionary<int, NPCTagDate> DataDict = new Dictionary<int, NPCTagDate>();

		// Token: 0x04004A44 RID: 19012
		public static List<NPCTagDate> DataList = new List<NPCTagDate>();

		// Token: 0x04004A45 RID: 19013
		public static Action OnInitFinishAction = new Action(NPCTagDate.OnInitFinish);

		// Token: 0x04004A46 RID: 19014
		public int id;

		// Token: 0x04004A47 RID: 19015
		public int zhengxie;

		// Token: 0x04004A48 RID: 19016
		public string GuanLianTalk;

		// Token: 0x04004A49 RID: 19017
		public List<int> WuDao = new List<int>();

		// Token: 0x04004A4A RID: 19018
		public List<int> Change = new List<int>();

		// Token: 0x04004A4B RID: 19019
		public List<int> ChangeTo = new List<int>();

		// Token: 0x04004A4C RID: 19020
		public List<int> BeiBaoType = new List<int>();
	}
}
