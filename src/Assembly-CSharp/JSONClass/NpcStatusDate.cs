using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C3A RID: 3130
	public class NpcStatusDate : IJSONClass
	{
		// Token: 0x06004C51 RID: 19537 RVA: 0x00203AC4 File Offset: 0x00201CC4
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

		// Token: 0x06004C52 RID: 19538 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004A3C RID: 19004
		public static Dictionary<int, NpcStatusDate> DataDict = new Dictionary<int, NpcStatusDate>();

		// Token: 0x04004A3D RID: 19005
		public static List<NpcStatusDate> DataList = new List<NpcStatusDate>();

		// Token: 0x04004A3E RID: 19006
		public static Action OnInitFinishAction = new Action(NpcStatusDate.OnInitFinish);

		// Token: 0x04004A3F RID: 19007
		public int id;

		// Token: 0x04004A40 RID: 19008
		public int Time;

		// Token: 0x04004A41 RID: 19009
		public int LunDao;

		// Token: 0x04004A42 RID: 19010
		public string ZhuangTaiInfo;
	}
}
