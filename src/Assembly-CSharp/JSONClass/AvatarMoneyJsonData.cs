using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AE9 RID: 2793
	public class AvatarMoneyJsonData : IJSONClass
	{
		// Token: 0x0600470E RID: 18190 RVA: 0x001E69F0 File Offset: 0x001E4BF0
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

		// Token: 0x0600470F RID: 18191 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FCA RID: 16330
		public static Dictionary<int, AvatarMoneyJsonData> DataDict = new Dictionary<int, AvatarMoneyJsonData>();

		// Token: 0x04003FCB RID: 16331
		public static List<AvatarMoneyJsonData> DataList = new List<AvatarMoneyJsonData>();

		// Token: 0x04003FCC RID: 16332
		public static Action OnInitFinishAction = new Action(AvatarMoneyJsonData.OnInitFinish);

		// Token: 0x04003FCD RID: 16333
		public int id;

		// Token: 0x04003FCE RID: 16334
		public int level;

		// Token: 0x04003FCF RID: 16335
		public int Min;

		// Token: 0x04003FD0 RID: 16336
		public int Max;
	}
}
