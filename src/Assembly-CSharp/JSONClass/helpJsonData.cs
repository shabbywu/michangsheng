using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000846 RID: 2118
	public class helpJsonData : IJSONClass
	{
		// Token: 0x06003F2A RID: 16170 RVA: 0x001AF990 File Offset: 0x001ADB90
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.helpJsonData.list)
			{
				try
				{
					helpJsonData helpJsonData = new helpJsonData();
					helpJsonData.id = jsonobject["id"].I;
					helpJsonData.Image = jsonobject["Image"].I;
					helpJsonData.Titile = jsonobject["Titile"].Str;
					if (helpJsonData.DataDict.ContainsKey(helpJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典helpJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", helpJsonData.id));
					}
					else
					{
						helpJsonData.DataDict.Add(helpJsonData.id, helpJsonData);
						helpJsonData.DataList.Add(helpJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典helpJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (helpJsonData.OnInitFinishAction != null)
			{
				helpJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B34 RID: 15156
		public static Dictionary<int, helpJsonData> DataDict = new Dictionary<int, helpJsonData>();

		// Token: 0x04003B35 RID: 15157
		public static List<helpJsonData> DataList = new List<helpJsonData>();

		// Token: 0x04003B36 RID: 15158
		public static Action OnInitFinishAction = new Action(helpJsonData.OnInitFinish);

		// Token: 0x04003B37 RID: 15159
		public int id;

		// Token: 0x04003B38 RID: 15160
		public int Image;

		// Token: 0x04003B39 RID: 15161
		public string Titile;
	}
}
