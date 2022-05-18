using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B34 RID: 2868
	public class BuffSeidJsonData190 : IJSONClass
	{
		// Token: 0x06004838 RID: 18488 RVA: 0x001EC898 File Offset: 0x001EAA98
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[190].list)
			{
				try
				{
					BuffSeidJsonData190 buffSeidJsonData = new BuffSeidJsonData190();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].ToList();
					buffSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (BuffSeidJsonData190.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData190.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData190.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData190.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData190.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData190.OnInitFinishAction != null)
			{
				BuffSeidJsonData190.OnInitFinishAction();
			}
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041E3 RID: 16867
		public static int SEIDID = 190;

		// Token: 0x040041E4 RID: 16868
		public static Dictionary<int, BuffSeidJsonData190> DataDict = new Dictionary<int, BuffSeidJsonData190>();

		// Token: 0x040041E5 RID: 16869
		public static List<BuffSeidJsonData190> DataList = new List<BuffSeidJsonData190>();

		// Token: 0x040041E6 RID: 16870
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData190.OnInitFinish);

		// Token: 0x040041E7 RID: 16871
		public int id;

		// Token: 0x040041E8 RID: 16872
		public List<int> value1 = new List<int>();

		// Token: 0x040041E9 RID: 16873
		public List<int> value2 = new List<int>();
	}
}
