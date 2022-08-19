using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000755 RID: 1877
	public class BiguanJsonData : IJSONClass
	{
		// Token: 0x06003B68 RID: 15208 RVA: 0x001992A0 File Offset: 0x001974A0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BiguanJsonData.list)
			{
				try
				{
					BiguanJsonData biguanJsonData = new BiguanJsonData();
					biguanJsonData.id = jsonobject["id"].I;
					biguanJsonData.speed = jsonobject["speed"].I;
					biguanJsonData.Text = jsonobject["Text"].Str;
					if (BiguanJsonData.DataDict.ContainsKey(biguanJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BiguanJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", biguanJsonData.id));
					}
					else
					{
						BiguanJsonData.DataDict.Add(biguanJsonData.id, biguanJsonData);
						BiguanJsonData.DataList.Add(biguanJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BiguanJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BiguanJsonData.OnInitFinishAction != null)
			{
				BiguanJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003473 RID: 13427
		public static Dictionary<int, BiguanJsonData> DataDict = new Dictionary<int, BiguanJsonData>();

		// Token: 0x04003474 RID: 13428
		public static List<BiguanJsonData> DataList = new List<BiguanJsonData>();

		// Token: 0x04003475 RID: 13429
		public static Action OnInitFinishAction = new Action(BiguanJsonData.OnInitFinish);

		// Token: 0x04003476 RID: 13430
		public int id;

		// Token: 0x04003477 RID: 13431
		public int speed;

		// Token: 0x04003478 RID: 13432
		public string Text;
	}
}
