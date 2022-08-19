using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000899 RID: 2201
	public class NpcBigMapBingDate : IJSONClass
	{
		// Token: 0x06004077 RID: 16503 RVA: 0x001B835C File Offset: 0x001B655C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcBigMapBingDate.list)
			{
				try
				{
					NpcBigMapBingDate npcBigMapBingDate = new NpcBigMapBingDate();
					npcBigMapBingDate.id = jsonobject["id"].I;
					npcBigMapBingDate.MapType = jsonobject["MapType"].I;
					npcBigMapBingDate.NPCType = jsonobject["NPCType"].I;
					npcBigMapBingDate.MapD = jsonobject["MapD"].I;
					if (NpcBigMapBingDate.DataDict.ContainsKey(npcBigMapBingDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcBigMapBingDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcBigMapBingDate.id));
					}
					else
					{
						NpcBigMapBingDate.DataDict.Add(npcBigMapBingDate.id, npcBigMapBingDate);
						NpcBigMapBingDate.DataList.Add(npcBigMapBingDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcBigMapBingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcBigMapBingDate.OnInitFinishAction != null)
			{
				NpcBigMapBingDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DF8 RID: 15864
		public static Dictionary<int, NpcBigMapBingDate> DataDict = new Dictionary<int, NpcBigMapBingDate>();

		// Token: 0x04003DF9 RID: 15865
		public static List<NpcBigMapBingDate> DataList = new List<NpcBigMapBingDate>();

		// Token: 0x04003DFA RID: 15866
		public static Action OnInitFinishAction = new Action(NpcBigMapBingDate.OnInitFinish);

		// Token: 0x04003DFB RID: 15867
		public int id;

		// Token: 0x04003DFC RID: 15868
		public int MapType;

		// Token: 0x04003DFD RID: 15869
		public int NPCType;

		// Token: 0x04003DFE RID: 15870
		public int MapD;
	}
}
