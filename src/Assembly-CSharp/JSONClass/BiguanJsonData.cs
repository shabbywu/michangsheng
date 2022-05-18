using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AED RID: 2797
	public class BiguanJsonData : IJSONClass
	{
		// Token: 0x0600471E RID: 18206 RVA: 0x001E72A4 File Offset: 0x001E54A4
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

		// Token: 0x0600471F RID: 18207 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400400C RID: 16396
		public static Dictionary<int, BiguanJsonData> DataDict = new Dictionary<int, BiguanJsonData>();

		// Token: 0x0400400D RID: 16397
		public static List<BiguanJsonData> DataList = new List<BiguanJsonData>();

		// Token: 0x0400400E RID: 16398
		public static Action OnInitFinishAction = new Action(BiguanJsonData.OnInitFinish);

		// Token: 0x0400400F RID: 16399
		public int id;

		// Token: 0x04004010 RID: 16400
		public int speed;

		// Token: 0x04004011 RID: 16401
		public string Text;
	}
}
