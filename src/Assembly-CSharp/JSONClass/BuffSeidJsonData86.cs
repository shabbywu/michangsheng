using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007F7 RID: 2039
	public class BuffSeidJsonData86 : IJSONClass
	{
		// Token: 0x06003DEE RID: 15854 RVA: 0x001A7C54 File Offset: 0x001A5E54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[86].list)
			{
				try
				{
					BuffSeidJsonData86 buffSeidJsonData = new BuffSeidJsonData86();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData86.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData86.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData86.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData86.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData86.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData86.OnInitFinishAction != null)
			{
				BuffSeidJsonData86.OnInitFinishAction();
			}
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040038C9 RID: 14537
		public static int SEIDID = 86;

		// Token: 0x040038CA RID: 14538
		public static Dictionary<int, BuffSeidJsonData86> DataDict = new Dictionary<int, BuffSeidJsonData86>();

		// Token: 0x040038CB RID: 14539
		public static List<BuffSeidJsonData86> DataList = new List<BuffSeidJsonData86>();

		// Token: 0x040038CC RID: 14540
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData86.OnInitFinish);

		// Token: 0x040038CD RID: 14541
		public int id;

		// Token: 0x040038CE RID: 14542
		public int value1;
	}
}
