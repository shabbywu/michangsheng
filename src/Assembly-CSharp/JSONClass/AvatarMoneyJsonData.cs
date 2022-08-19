using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000751 RID: 1873
	public class AvatarMoneyJsonData : IJSONClass
	{
		// Token: 0x06003B58 RID: 15192 RVA: 0x0019892C File Offset: 0x00196B2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.AvatarMoneyJsonData.list)
			{
				try
				{
					AvatarMoneyJsonData avatarMoneyJsonData = new AvatarMoneyJsonData();
					avatarMoneyJsonData.id = jsonobject["id"].I;
					avatarMoneyJsonData.level = jsonobject["level"].I;
					avatarMoneyJsonData.Min = jsonobject["Min"].I;
					avatarMoneyJsonData.Max = jsonobject["Max"].I;
					if (AvatarMoneyJsonData.DataDict.ContainsKey(avatarMoneyJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典AvatarMoneyJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", avatarMoneyJsonData.id));
					}
					else
					{
						AvatarMoneyJsonData.DataDict.Add(avatarMoneyJsonData.id, avatarMoneyJsonData);
						AvatarMoneyJsonData.DataList.Add(avatarMoneyJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典AvatarMoneyJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (AvatarMoneyJsonData.OnInitFinishAction != null)
			{
				AvatarMoneyJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003431 RID: 13361
		public static Dictionary<int, AvatarMoneyJsonData> DataDict = new Dictionary<int, AvatarMoneyJsonData>();

		// Token: 0x04003432 RID: 13362
		public static List<AvatarMoneyJsonData> DataList = new List<AvatarMoneyJsonData>();

		// Token: 0x04003433 RID: 13363
		public static Action OnInitFinishAction = new Action(AvatarMoneyJsonData.OnInitFinish);

		// Token: 0x04003434 RID: 13364
		public int id;

		// Token: 0x04003435 RID: 13365
		public int level;

		// Token: 0x04003436 RID: 13366
		public int Min;

		// Token: 0x04003437 RID: 13367
		public int Max;
	}
}
