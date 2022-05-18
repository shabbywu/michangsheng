using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B49 RID: 2889
	public class BuffSeidJsonData213 : IJSONClass
	{
		// Token: 0x0600488C RID: 18572 RVA: 0x001EE224 File Offset: 0x001EC424
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[213].list)
			{
				try
				{
					BuffSeidJsonData213 buffSeidJsonData = new BuffSeidJsonData213();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData213.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData213.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData213.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData213.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData213.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData213.OnInitFinishAction != null)
			{
				BuffSeidJsonData213.OnInitFinishAction();
			}
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400426C RID: 17004
		public static int SEIDID = 213;

		// Token: 0x0400426D RID: 17005
		public static Dictionary<int, BuffSeidJsonData213> DataDict = new Dictionary<int, BuffSeidJsonData213>();

		// Token: 0x0400426E RID: 17006
		public static List<BuffSeidJsonData213> DataList = new List<BuffSeidJsonData213>();

		// Token: 0x0400426F RID: 17007
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData213.OnInitFinish);

		// Token: 0x04004270 RID: 17008
		public int id;

		// Token: 0x04004271 RID: 17009
		public int value1;

		// Token: 0x04004272 RID: 17010
		public string panduan;
	}
}
