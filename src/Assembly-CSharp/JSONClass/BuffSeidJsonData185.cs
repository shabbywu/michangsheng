using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B30 RID: 2864
	public class BuffSeidJsonData185 : IJSONClass
	{
		// Token: 0x06004828 RID: 18472 RVA: 0x001EC3C0 File Offset: 0x001EA5C0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[185].list)
			{
				try
				{
					BuffSeidJsonData185 buffSeidJsonData = new BuffSeidJsonData185();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData185.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData185.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData185.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData185.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData185.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData185.OnInitFinishAction != null)
			{
				BuffSeidJsonData185.OnInitFinishAction();
			}
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041C9 RID: 16841
		public static int SEIDID = 185;

		// Token: 0x040041CA RID: 16842
		public static Dictionary<int, BuffSeidJsonData185> DataDict = new Dictionary<int, BuffSeidJsonData185>();

		// Token: 0x040041CB RID: 16843
		public static List<BuffSeidJsonData185> DataList = new List<BuffSeidJsonData185>();

		// Token: 0x040041CC RID: 16844
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData185.OnInitFinish);

		// Token: 0x040041CD RID: 16845
		public int id;

		// Token: 0x040041CE RID: 16846
		public List<int> value1 = new List<int>();

		// Token: 0x040041CF RID: 16847
		public List<int> value2 = new List<int>();
	}
}
