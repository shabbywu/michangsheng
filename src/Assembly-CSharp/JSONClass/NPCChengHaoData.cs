using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C28 RID: 3112
	public class NPCChengHaoData : IJSONClass
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x002017A8 File Offset: 0x001FF9A8
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

		// Token: 0x06004C0A RID: 19466 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004958 RID: 18776
		public static Dictionary<int, NPCChengHaoData> DataDict = new Dictionary<int, NPCChengHaoData>();

		// Token: 0x04004959 RID: 18777
		public static List<NPCChengHaoData> DataList = new List<NPCChengHaoData>();

		// Token: 0x0400495A RID: 18778
		public static Action OnInitFinishAction = new Action(NPCChengHaoData.OnInitFinish);

		// Token: 0x0400495B RID: 18779
		public int id;

		// Token: 0x0400495C RID: 18780
		public int NPCType;

		// Token: 0x0400495D RID: 18781
		public int GongXian;

		// Token: 0x0400495E RID: 18782
		public int IsOnly;

		// Token: 0x0400495F RID: 18783
		public int ChengHaoLv;

		// Token: 0x04004960 RID: 18784
		public int MaxLevel;

		// Token: 0x04004961 RID: 18785
		public int ChengHaoType;

		// Token: 0x04004962 RID: 18786
		public string ChengHao;

		// Token: 0x04004963 RID: 18787
		public List<int> Level = new List<int>();

		// Token: 0x04004964 RID: 18788
		public List<int> Change = new List<int>();

		// Token: 0x04004965 RID: 18789
		public List<int> ChangeTo = new List<int>();
	}
}
