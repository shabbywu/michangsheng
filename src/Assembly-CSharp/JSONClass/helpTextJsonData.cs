using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000847 RID: 2119
	public class helpTextJsonData : IJSONClass
	{
		// Token: 0x06003F2E RID: 16174 RVA: 0x001AFAF4 File Offset: 0x001ADCF4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.helpTextJsonData.list)
			{
				try
				{
					helpTextJsonData helpTextJsonData = new helpTextJsonData();
					helpTextJsonData.id = jsonobject["id"].I;
					helpTextJsonData.link = jsonobject["link"].I;
					helpTextJsonData.Titile = jsonobject["Titile"].Str;
					helpTextJsonData.desc = jsonobject["desc"].Str;
					if (helpTextJsonData.DataDict.ContainsKey(helpTextJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典helpTextJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", helpTextJsonData.id));
					}
					else
					{
						helpTextJsonData.DataDict.Add(helpTextJsonData.id, helpTextJsonData);
						helpTextJsonData.DataList.Add(helpTextJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典helpTextJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (helpTextJsonData.OnInitFinishAction != null)
			{
				helpTextJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B3A RID: 15162
		public static Dictionary<int, helpTextJsonData> DataDict = new Dictionary<int, helpTextJsonData>();

		// Token: 0x04003B3B RID: 15163
		public static List<helpTextJsonData> DataList = new List<helpTextJsonData>();

		// Token: 0x04003B3C RID: 15164
		public static Action OnInitFinishAction = new Action(helpTextJsonData.OnInitFinish);

		// Token: 0x04003B3D RID: 15165
		public int id;

		// Token: 0x04003B3E RID: 15166
		public int link;

		// Token: 0x04003B3F RID: 15167
		public string Titile;

		// Token: 0x04003B40 RID: 15168
		public string desc;
	}
}
