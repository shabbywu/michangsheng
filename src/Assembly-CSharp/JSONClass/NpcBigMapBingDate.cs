using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C27 RID: 3111
	public class NpcBigMapBingDate : IJSONClass
	{
		// Token: 0x06004C05 RID: 19461 RVA: 0x00201658 File Offset: 0x001FF858
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

		// Token: 0x06004C06 RID: 19462 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004951 RID: 18769
		public static Dictionary<int, NpcBigMapBingDate> DataDict = new Dictionary<int, NpcBigMapBingDate>();

		// Token: 0x04004952 RID: 18770
		public static List<NpcBigMapBingDate> DataList = new List<NpcBigMapBingDate>();

		// Token: 0x04004953 RID: 18771
		public static Action OnInitFinishAction = new Action(NpcBigMapBingDate.OnInitFinish);

		// Token: 0x04004954 RID: 18772
		public int id;

		// Token: 0x04004955 RID: 18773
		public int MapType;

		// Token: 0x04004956 RID: 18774
		public int NPCType;

		// Token: 0x04004957 RID: 18775
		public int MapD;
	}
}
