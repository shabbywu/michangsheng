using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B76 RID: 2934
	public class BuffSeidJsonData60 : IJSONClass
	{
		// Token: 0x06004940 RID: 18752 RVA: 0x001F1CF8 File Offset: 0x001EFEF8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[60].list)
			{
				try
				{
					BuffSeidJsonData60 buffSeidJsonData = new BuffSeidJsonData60();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData60.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData60.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData60.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData60.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData60.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData60.OnInitFinishAction != null)
			{
				BuffSeidJsonData60.OnInitFinishAction();
			}
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043BE RID: 17342
		public static int SEIDID = 60;

		// Token: 0x040043BF RID: 17343
		public static Dictionary<int, BuffSeidJsonData60> DataDict = new Dictionary<int, BuffSeidJsonData60>();

		// Token: 0x040043C0 RID: 17344
		public static List<BuffSeidJsonData60> DataList = new List<BuffSeidJsonData60>();

		// Token: 0x040043C1 RID: 17345
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData60.OnInitFinish);

		// Token: 0x040043C2 RID: 17346
		public int id;

		// Token: 0x040043C3 RID: 17347
		public int value1;
	}
}
