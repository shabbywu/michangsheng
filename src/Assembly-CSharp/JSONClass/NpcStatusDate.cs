using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008AC RID: 2220
	public class NpcStatusDate : IJSONClass
	{
		// Token: 0x060040C3 RID: 16579 RVA: 0x001BAC08 File Offset: 0x001B8E08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcStatusDate.list)
			{
				try
				{
					NpcStatusDate npcStatusDate = new NpcStatusDate();
					npcStatusDate.id = jsonobject["id"].I;
					npcStatusDate.Time = jsonobject["Time"].I;
					npcStatusDate.LunDao = jsonobject["LunDao"].I;
					npcStatusDate.ZhuangTaiInfo = jsonobject["ZhuangTaiInfo"].Str;
					if (NpcStatusDate.DataDict.ContainsKey(npcStatusDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcStatusDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcStatusDate.id));
					}
					else
					{
						NpcStatusDate.DataDict.Add(npcStatusDate.id, npcStatusDate);
						NpcStatusDate.DataList.Add(npcStatusDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcStatusDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcStatusDate.OnInitFinishAction != null)
			{
				NpcStatusDate.OnInitFinishAction();
			}
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003EE4 RID: 16100
		public static Dictionary<int, NpcStatusDate> DataDict = new Dictionary<int, NpcStatusDate>();

		// Token: 0x04003EE5 RID: 16101
		public static List<NpcStatusDate> DataList = new List<NpcStatusDate>();

		// Token: 0x04003EE6 RID: 16102
		public static Action OnInitFinishAction = new Action(NpcStatusDate.OnInitFinish);

		// Token: 0x04003EE7 RID: 16103
		public int id;

		// Token: 0x04003EE8 RID: 16104
		public int Time;

		// Token: 0x04003EE9 RID: 16105
		public int LunDao;

		// Token: 0x04003EEA RID: 16106
		public string ZhuangTaiInfo;
	}
}
