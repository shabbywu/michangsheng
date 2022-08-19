using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000990 RID: 2448
	public class XinJinJsonData : IJSONClass
	{
		// Token: 0x06004454 RID: 17492 RVA: 0x001D1784 File Offset: 0x001CF984
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
			{
				try
				{
					XinJinJsonData xinJinJsonData = new XinJinJsonData();
					xinJinJsonData.id = jsonobject["id"].I;
					xinJinJsonData.Max = jsonobject["Max"].I;
					xinJinJsonData.Text = jsonobject["Text"].Str;
					if (XinJinJsonData.DataDict.ContainsKey(xinJinJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典XinJinJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", xinJinJsonData.id));
					}
					else
					{
						XinJinJsonData.DataDict.Add(xinJinJsonData.id, xinJinJsonData);
						XinJinJsonData.DataList.Add(xinJinJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典XinJinJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (XinJinJsonData.OnInitFinishAction != null)
			{
				XinJinJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040045FC RID: 17916
		public static Dictionary<int, XinJinJsonData> DataDict = new Dictionary<int, XinJinJsonData>();

		// Token: 0x040045FD RID: 17917
		public static List<XinJinJsonData> DataList = new List<XinJinJsonData>();

		// Token: 0x040045FE RID: 17918
		public static Action OnInitFinishAction = new Action(XinJinJsonData.OnInitFinish);

		// Token: 0x040045FF RID: 17919
		public int id;

		// Token: 0x04004600 RID: 17920
		public int Max;

		// Token: 0x04004601 RID: 17921
		public string Text;
	}
}
