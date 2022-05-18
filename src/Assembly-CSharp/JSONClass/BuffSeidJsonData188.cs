using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B33 RID: 2867
	public class BuffSeidJsonData188 : IJSONClass
	{
		// Token: 0x06004834 RID: 18484 RVA: 0x001EC76C File Offset: 0x001EA96C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[188].list)
			{
				try
				{
					BuffSeidJsonData188 buffSeidJsonData = new BuffSeidJsonData188();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.value1 = jsonobject["value1"].I;
					if (BuffSeidJsonData188.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData188.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData188.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData188.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData188.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData188.OnInitFinishAction != null)
			{
				BuffSeidJsonData188.OnInitFinishAction();
			}
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041DD RID: 16861
		public static int SEIDID = 188;

		// Token: 0x040041DE RID: 16862
		public static Dictionary<int, BuffSeidJsonData188> DataDict = new Dictionary<int, BuffSeidJsonData188>();

		// Token: 0x040041DF RID: 16863
		public static List<BuffSeidJsonData188> DataList = new List<BuffSeidJsonData188>();

		// Token: 0x040041E0 RID: 16864
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData188.OnInitFinish);

		// Token: 0x040041E1 RID: 16865
		public int id;

		// Token: 0x040041E2 RID: 16866
		public int value1;
	}
}
