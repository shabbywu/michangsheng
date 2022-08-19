using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020007A2 RID: 1954
	public class BuffSeidJsonData199 : IJSONClass
	{
		// Token: 0x06003C9A RID: 15514 RVA: 0x001A006C File Offset: 0x0019E26C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[199].list)
			{
				try
				{
					BuffSeidJsonData199 buffSeidJsonData = new BuffSeidJsonData199();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					buffSeidJsonData.value2 = jsonobject["value2"].I;
					if (BuffSeidJsonData199.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData199.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData199.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData199.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData199.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData199.OnInitFinishAction != null)
			{
				BuffSeidJsonData199.OnInitFinishAction();
			}
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003675 RID: 13941
		public static int SEIDID = 199;

		// Token: 0x04003676 RID: 13942
		public static Dictionary<int, BuffSeidJsonData199> DataDict = new Dictionary<int, BuffSeidJsonData199>();

		// Token: 0x04003677 RID: 13943
		public static List<BuffSeidJsonData199> DataList = new List<BuffSeidJsonData199>();

		// Token: 0x04003678 RID: 13944
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData199.OnInitFinish);

		// Token: 0x04003679 RID: 13945
		public int id;

		// Token: 0x0400367A RID: 13946
		public int value1;

		// Token: 0x0400367B RID: 13947
		public int value2;
	}
}
