using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000785 RID: 1925
	public class BuffSeidJsonData161 : IJSONClass
	{
		// Token: 0x06003C26 RID: 15398 RVA: 0x0019D69C File Offset: 0x0019B89C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[161].list)
			{
				try
				{
					BuffSeidJsonData161 buffSeidJsonData = new BuffSeidJsonData161();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData161.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData161.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData161.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData161.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData161.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData161.OnInitFinishAction != null)
			{
				BuffSeidJsonData161.OnInitFinishAction();
			}
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040035B4 RID: 13748
		public static int SEIDID = 161;

		// Token: 0x040035B5 RID: 13749
		public static Dictionary<int, BuffSeidJsonData161> DataDict = new Dictionary<int, BuffSeidJsonData161>();

		// Token: 0x040035B6 RID: 13750
		public static List<BuffSeidJsonData161> DataList = new List<BuffSeidJsonData161>();

		// Token: 0x040035B7 RID: 13751
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData161.OnInitFinish);

		// Token: 0x040035B8 RID: 13752
		public int id;

		// Token: 0x040035B9 RID: 13753
		public string panduan;
	}
}
