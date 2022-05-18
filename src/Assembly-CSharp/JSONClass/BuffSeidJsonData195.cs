using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B37 RID: 2871
	public class BuffSeidJsonData195 : IJSONClass
	{
		// Token: 0x06004844 RID: 18500 RVA: 0x001ECC44 File Offset: 0x001EAE44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[195].list)
			{
				try
				{
					BuffSeidJsonData195 buffSeidJsonData = new BuffSeidJsonData195();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					buffSeidJsonData.value3 = jsonobject["value3"].I;
					buffSeidJsonData.value4 = jsonobject["value4"].I;
					if (BuffSeidJsonData195.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData195.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData195.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData195.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData195.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData195.OnInitFinishAction != null)
			{
				BuffSeidJsonData195.OnInitFinishAction();
			}
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041F7 RID: 16887
		public static int SEIDID = 195;

		// Token: 0x040041F8 RID: 16888
		public static Dictionary<int, BuffSeidJsonData195> DataDict = new Dictionary<int, BuffSeidJsonData195>();

		// Token: 0x040041F9 RID: 16889
		public static List<BuffSeidJsonData195> DataList = new List<BuffSeidJsonData195>();

		// Token: 0x040041FA RID: 16890
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData195.OnInitFinish);

		// Token: 0x040041FB RID: 16891
		public int id;

		// Token: 0x040041FC RID: 16892
		public int value1;

		// Token: 0x040041FD RID: 16893
		public int value2;

		// Token: 0x040041FE RID: 16894
		public int value3;

		// Token: 0x040041FF RID: 16895
		public int value4;
	}
}
