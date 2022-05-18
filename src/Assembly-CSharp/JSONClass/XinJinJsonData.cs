using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000D10 RID: 3344
	public class XinJinJsonData : IJSONClass
	{
		// Token: 0x06004FAA RID: 20394 RVA: 0x002165C4 File Offset: 0x002147C4
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

		// Token: 0x06004FAB RID: 20395 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040050F0 RID: 20720
		public static Dictionary<int, XinJinJsonData> DataDict = new Dictionary<int, XinJinJsonData>();

		// Token: 0x040050F1 RID: 20721
		public static List<XinJinJsonData> DataList = new List<XinJinJsonData>();

		// Token: 0x040050F2 RID: 20722
		public static Action OnInitFinishAction = new Action(XinJinJsonData.OnInitFinish);

		// Token: 0x040050F3 RID: 20723
		public int id;

		// Token: 0x040050F4 RID: 20724
		public int Max;

		// Token: 0x040050F5 RID: 20725
		public string Text;
	}
}
