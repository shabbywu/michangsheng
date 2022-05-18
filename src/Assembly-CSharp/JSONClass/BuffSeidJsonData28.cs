using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B52 RID: 2898
	public class BuffSeidJsonData28 : IJSONClass
	{
		// Token: 0x060048B0 RID: 18608 RVA: 0x001EED58 File Offset: 0x001ECF58
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[28].list)
			{
				try
				{
					BuffSeidJsonData28 buffSeidJsonData = new BuffSeidJsonData28();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData28.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData28.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData28.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData28.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData28.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData28.OnInitFinishAction != null)
			{
				BuffSeidJsonData28.OnInitFinishAction();
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042AA RID: 17066
		public static int SEIDID = 28;

		// Token: 0x040042AB RID: 17067
		public static Dictionary<int, BuffSeidJsonData28> DataDict = new Dictionary<int, BuffSeidJsonData28>();

		// Token: 0x040042AC RID: 17068
		public static List<BuffSeidJsonData28> DataList = new List<BuffSeidJsonData28>();

		// Token: 0x040042AD RID: 17069
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData28.OnInitFinish);

		// Token: 0x040042AE RID: 17070
		public int id;

		// Token: 0x040042AF RID: 17071
		public int value1;

		// Token: 0x040042B0 RID: 17072
		public int value2;
	}
}
