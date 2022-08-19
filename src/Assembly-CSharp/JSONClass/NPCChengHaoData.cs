using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200089A RID: 2202
	public class NPCChengHaoData : IJSONClass
	{
		// Token: 0x0600407B RID: 16507 RVA: 0x001B84D4 File Offset: 0x001B66D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCChengHaoData.list)
			{
				try
				{
					NPCChengHaoData npcchengHaoData = new NPCChengHaoData();
					npcchengHaoData.id = jsonobject["id"].I;
					npcchengHaoData.NPCType = jsonobject["NPCType"].I;
					npcchengHaoData.GongXian = jsonobject["GongXian"].I;
					npcchengHaoData.IsOnly = jsonobject["IsOnly"].I;
					npcchengHaoData.ChengHaoLv = jsonobject["ChengHaoLv"].I;
					npcchengHaoData.MaxLevel = jsonobject["MaxLevel"].I;
					npcchengHaoData.ChengHaoType = jsonobject["ChengHaoType"].I;
					npcchengHaoData.ChengHao = jsonobject["ChengHao"].Str;
					npcchengHaoData.Level = jsonobject["Level"].ToList();
					npcchengHaoData.Change = jsonobject["Change"].ToList();
					npcchengHaoData.ChangeTo = jsonobject["ChangeTo"].ToList();
					if (NPCChengHaoData.DataDict.ContainsKey(npcchengHaoData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NPCChengHaoData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcchengHaoData.id));
					}
					else
					{
						NPCChengHaoData.DataDict.Add(npcchengHaoData.id, npcchengHaoData);
						NPCChengHaoData.DataList.Add(npcchengHaoData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NPCChengHaoData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NPCChengHaoData.OnInitFinishAction != null)
			{
				NPCChengHaoData.OnInitFinishAction();
			}
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DFF RID: 15871
		public static Dictionary<int, NPCChengHaoData> DataDict = new Dictionary<int, NPCChengHaoData>();

		// Token: 0x04003E00 RID: 15872
		public static List<NPCChengHaoData> DataList = new List<NPCChengHaoData>();

		// Token: 0x04003E01 RID: 15873
		public static Action OnInitFinishAction = new Action(NPCChengHaoData.OnInitFinish);

		// Token: 0x04003E02 RID: 15874
		public int id;

		// Token: 0x04003E03 RID: 15875
		public int NPCType;

		// Token: 0x04003E04 RID: 15876
		public int GongXian;

		// Token: 0x04003E05 RID: 15877
		public int IsOnly;

		// Token: 0x04003E06 RID: 15878
		public int ChengHaoLv;

		// Token: 0x04003E07 RID: 15879
		public int MaxLevel;

		// Token: 0x04003E08 RID: 15880
		public int ChengHaoType;

		// Token: 0x04003E09 RID: 15881
		public string ChengHao;

		// Token: 0x04003E0A RID: 15882
		public List<int> Level = new List<int>();

		// Token: 0x04003E0B RID: 15883
		public List<int> Change = new List<int>();

		// Token: 0x04003E0C RID: 15884
		public List<int> ChangeTo = new List<int>();
	}
}
