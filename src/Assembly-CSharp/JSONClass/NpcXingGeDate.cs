using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C44 RID: 3140
	public class NpcXingGeDate : IJSONClass
	{
		// Token: 0x06004C79 RID: 19577 RVA: 0x00205300 File Offset: 0x00203500
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcXingGeDate.list)
			{
				try
				{
					NpcXingGeDate npcXingGeDate = new NpcXingGeDate();
					npcXingGeDate.id = jsonobject["id"].I;
					npcXingGeDate.zhengxie = jsonobject["zhengxie"].I;
					if (NpcXingGeDate.DataDict.ContainsKey(npcXingGeDate.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NpcXingGeDate.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", npcXingGeDate.id));
					}
					else
					{
						NpcXingGeDate.DataDict.Add(npcXingGeDate.id, npcXingGeDate);
						NpcXingGeDate.DataList.Add(npcXingGeDate);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NpcXingGeDate.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NpcXingGeDate.OnInitFinishAction != null)
			{
				NpcXingGeDate.OnInitFinishAction();
			}
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004AEC RID: 19180
		public static Dictionary<int, NpcXingGeDate> DataDict = new Dictionary<int, NpcXingGeDate>();

		// Token: 0x04004AED RID: 19181
		public static List<NpcXingGeDate> DataList = new List<NpcXingGeDate>();

		// Token: 0x04004AEE RID: 19182
		public static Action OnInitFinishAction = new Action(NpcXingGeDate.OnInitFinish);

		// Token: 0x04004AEF RID: 19183
		public int id;

		// Token: 0x04004AF0 RID: 19184
		public int zhengxie;
	}
}
