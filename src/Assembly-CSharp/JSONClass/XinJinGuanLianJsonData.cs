using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D0F RID: 3343
	public class XinJinGuanLianJsonData : IJSONClass
	{
		// Token: 0x06004FA6 RID: 20390 RVA: 0x002164A0 File Offset: 0x002146A0
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

		// Token: 0x06004FA7 RID: 20391 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050EB RID: 20715
		public static Dictionary<int, XinJinGuanLianJsonData> DataDict = new Dictionary<int, XinJinGuanLianJsonData>();

		// Token: 0x040050EC RID: 20716
		public static List<XinJinGuanLianJsonData> DataList = new List<XinJinGuanLianJsonData>();

		// Token: 0x040050ED RID: 20717
		public static Action OnInitFinishAction = new Action(XinJinGuanLianJsonData.OnInitFinish);

		// Token: 0x040050EE RID: 20718
		public int id;

		// Token: 0x040050EF RID: 20719
		public int speed;
	}
}
