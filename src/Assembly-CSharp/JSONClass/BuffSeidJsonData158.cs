using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B1A RID: 2842
	public class BuffSeidJsonData158 : IJSONClass
	{
		// Token: 0x060047D1 RID: 18385 RVA: 0x001EA92C File Offset: 0x001E8B2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[158].list)
			{
				try
				{
					BuffSeidJsonData158 buffSeidJsonData = new BuffSeidJsonData158();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.target = jsonobject["target"].I;
					if (BuffSeidJsonData158.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData158.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData158.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData158.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData158.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData158.OnInitFinishAction != null)
			{
				BuffSeidJsonData158.OnInitFinishAction();
			}
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004138 RID: 16696
		public static int SEIDID = 158;

		// Token: 0x04004139 RID: 16697
		public static Dictionary<int, BuffSeidJsonData158> DataDict = new Dictionary<int, BuffSeidJsonData158>();

		// Token: 0x0400413A RID: 16698
		public static List<BuffSeidJsonData158> DataList = new List<BuffSeidJsonData158>();

		// Token: 0x0400413B RID: 16699
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData158.OnInitFinish);

		// Token: 0x0400413C RID: 16700
		public int id;

		// Token: 0x0400413D RID: 16701
		public int target;
	}
}
