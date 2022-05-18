using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BFE RID: 3070
	public class KillAvatarLingGuangJson : IJSONClass
	{
		// Token: 0x06004B61 RID: 19297 RVA: 0x001FD414 File Offset: 0x001FB614
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

		// Token: 0x06004B62 RID: 19298 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040047C7 RID: 18375
		public static Dictionary<int, KillAvatarLingGuangJson> DataDict = new Dictionary<int, KillAvatarLingGuangJson>();

		// Token: 0x040047C8 RID: 18376
		public static List<KillAvatarLingGuangJson> DataList = new List<KillAvatarLingGuangJson>();

		// Token: 0x040047C9 RID: 18377
		public static Action OnInitFinishAction = new Action(KillAvatarLingGuangJson.OnInitFinish);

		// Token: 0x040047CA RID: 18378
		public int id;

		// Token: 0x040047CB RID: 18379
		public int lingguangid;

		// Token: 0x040047CC RID: 18380
		public List<int> avatar = new List<int>();
	}
}
