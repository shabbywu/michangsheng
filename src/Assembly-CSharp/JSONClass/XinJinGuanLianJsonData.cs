using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200098F RID: 2447
	public class XinJinGuanLianJsonData : IJSONClass
	{
		// Token: 0x06004450 RID: 17488 RVA: 0x001D1638 File Offset: 0x001CF838
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.XinJinGuanLianJsonData.list)
			{
				try
				{
					XinJinGuanLianJsonData xinJinGuanLianJsonData = new XinJinGuanLianJsonData();
					xinJinGuanLianJsonData.id = jsonobject["id"].I;
					xinJinGuanLianJsonData.speed = jsonobject["speed"].I;
					if (XinJinGuanLianJsonData.DataDict.ContainsKey(xinJinGuanLianJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典XinJinGuanLianJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", xinJinGuanLianJsonData.id));
					}
					else
					{
						XinJinGuanLianJsonData.DataDict.Add(xinJinGuanLianJsonData.id, xinJinGuanLianJsonData);
						XinJinGuanLianJsonData.DataList.Add(xinJinGuanLianJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典XinJinGuanLianJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (XinJinGuanLianJsonData.OnInitFinishAction != null)
			{
				XinJinGuanLianJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045F7 RID: 17911
		public static Dictionary<int, XinJinGuanLianJsonData> DataDict = new Dictionary<int, XinJinGuanLianJsonData>();

		// Token: 0x040045F8 RID: 17912
		public static List<XinJinGuanLianJsonData> DataList = new List<XinJinGuanLianJsonData>();

		// Token: 0x040045F9 RID: 17913
		public static Action OnInitFinishAction = new Action(XinJinGuanLianJsonData.OnInitFinish);

		// Token: 0x040045FA RID: 17914
		public int id;

		// Token: 0x040045FB RID: 17915
		public int speed;
	}
}
