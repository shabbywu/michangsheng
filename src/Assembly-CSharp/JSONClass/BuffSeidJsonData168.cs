using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B23 RID: 2851
	public class BuffSeidJsonData168 : IJSONClass
	{
		// Token: 0x060047F4 RID: 18420 RVA: 0x001EB3B8 File Offset: 0x001E95B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[168].list)
			{
				try
				{
					BuffSeidJsonData168 buffSeidJsonData = new BuffSeidJsonData168();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData168.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData168.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData168.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData168.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData168.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData168.OnInitFinishAction != null)
			{
				BuffSeidJsonData168.OnInitFinishAction();
			}
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004172 RID: 16754
		public static int SEIDID = 168;

		// Token: 0x04004173 RID: 16755
		public static Dictionary<int, BuffSeidJsonData168> DataDict = new Dictionary<int, BuffSeidJsonData168>();

		// Token: 0x04004174 RID: 16756
		public static List<BuffSeidJsonData168> DataList = new List<BuffSeidJsonData168>();

		// Token: 0x04004175 RID: 16757
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData168.OnInitFinish);

		// Token: 0x04004176 RID: 16758
		public int id;

		// Token: 0x04004177 RID: 16759
		public int target;

		// Token: 0x04004178 RID: 16760
		public int value1;

		// Token: 0x04004179 RID: 16761
		public int value2;

		// Token: 0x0400417A RID: 16762
		public string panduan;
	}
}
