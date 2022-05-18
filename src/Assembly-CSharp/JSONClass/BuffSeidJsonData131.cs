using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B06 RID: 2822
	public class BuffSeidJsonData131 : IJSONClass
	{
		// Token: 0x06004782 RID: 18306 RVA: 0x001E9078 File Offset: 0x001E7278
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[131].list)
			{
				try
				{
					BuffSeidJsonData131 buffSeidJsonData = new BuffSeidJsonData131();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					buffSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (BuffSeidJsonData131.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData131.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData131.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData131.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData131.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData131.OnInitFinishAction != null)
			{
				BuffSeidJsonData131.OnInitFinishAction();
			}
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040040AC RID: 16556
		public static int SEIDID = 131;

		// Token: 0x040040AD RID: 16557
		public static Dictionary<int, BuffSeidJsonData131> DataDict = new Dictionary<int, BuffSeidJsonData131>();

		// Token: 0x040040AE RID: 16558
		public static List<BuffSeidJsonData131> DataList = new List<BuffSeidJsonData131>();

		// Token: 0x040040AF RID: 16559
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData131.OnInitFinish);

		// Token: 0x040040B0 RID: 16560
		public int id;

		// Token: 0x040040B1 RID: 16561
		public int value1;

		// Token: 0x040040B2 RID: 16562
		public List<int> value2 = new List<int>();

		// Token: 0x040040B3 RID: 16563
		public List<int> value3 = new List<int>();
	}
}
