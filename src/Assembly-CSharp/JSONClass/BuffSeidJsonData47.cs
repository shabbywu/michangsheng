using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B68 RID: 2920
	public class BuffSeidJsonData47 : IJSONClass
	{
		// Token: 0x06004908 RID: 18696 RVA: 0x001F0B44 File Offset: 0x001EED44
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[47].list)
			{
				try
				{
					BuffSeidJsonData47 buffSeidJsonData = new BuffSeidJsonData47();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData47.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData47.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData47.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData47.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData47.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData47.OnInitFinishAction != null)
			{
				BuffSeidJsonData47.OnInitFinishAction();
			}
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400435A RID: 17242
		public static int SEIDID = 47;

		// Token: 0x0400435B RID: 17243
		public static Dictionary<int, BuffSeidJsonData47> DataDict = new Dictionary<int, BuffSeidJsonData47>();

		// Token: 0x0400435C RID: 17244
		public static List<BuffSeidJsonData47> DataList = new List<BuffSeidJsonData47>();

		// Token: 0x0400435D RID: 17245
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData47.OnInitFinish);

		// Token: 0x0400435E RID: 17246
		public int id;

		// Token: 0x0400435F RID: 17247
		public int value1;
	}
}
