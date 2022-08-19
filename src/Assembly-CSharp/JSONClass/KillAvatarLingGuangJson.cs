using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000870 RID: 2160
	public class KillAvatarLingGuangJson : IJSONClass
	{
		// Token: 0x06003FD3 RID: 16339 RVA: 0x001B39C0 File Offset: 0x001B1BC0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.KillAvatarLingGuangJson.list)
			{
				try
				{
					KillAvatarLingGuangJson killAvatarLingGuangJson = new KillAvatarLingGuangJson();
					killAvatarLingGuangJson.id = jsonobject["id"].I;
					killAvatarLingGuangJson.lingguangid = jsonobject["lingguangid"].I;
					killAvatarLingGuangJson.avatar = jsonobject["avatar"].ToList();
					if (KillAvatarLingGuangJson.DataDict.ContainsKey(killAvatarLingGuangJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典KillAvatarLingGuangJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", killAvatarLingGuangJson.id));
					}
					else
					{
						KillAvatarLingGuangJson.DataDict.Add(killAvatarLingGuangJson.id, killAvatarLingGuangJson);
						KillAvatarLingGuangJson.DataList.Add(killAvatarLingGuangJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典KillAvatarLingGuangJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (KillAvatarLingGuangJson.OnInitFinishAction != null)
			{
				KillAvatarLingGuangJson.OnInitFinishAction();
			}
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C6E RID: 15470
		public static Dictionary<int, KillAvatarLingGuangJson> DataDict = new Dictionary<int, KillAvatarLingGuangJson>();

		// Token: 0x04003C6F RID: 15471
		public static List<KillAvatarLingGuangJson> DataList = new List<KillAvatarLingGuangJson>();

		// Token: 0x04003C70 RID: 15472
		public static Action OnInitFinishAction = new Action(KillAvatarLingGuangJson.OnInitFinish);

		// Token: 0x04003C71 RID: 15473
		public int id;

		// Token: 0x04003C72 RID: 15474
		public int lingguangid;

		// Token: 0x04003C73 RID: 15475
		public List<int> avatar = new List<int>();
	}
}
