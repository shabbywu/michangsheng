using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B1D RID: 2845
	public class BuffSeidJsonData161 : IJSONClass
	{
		// Token: 0x060047DC RID: 18396 RVA: 0x001EAC9C File Offset: 0x001E8E9C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[161].list)
			{
				try
				{
					BuffSeidJsonData161 buffSeidJsonData = new BuffSeidJsonData161();
					buffSeidJsonData.id = jsonobject["id"].I;
					buffSeidJsonData.panduan = jsonobject["panduan"].Str;
					if (BuffSeidJsonData161.DataDict.ContainsKey(buffSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BuffSeidJsonData161.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", buffSeidJsonData.id));
					}
					else
					{
						BuffSeidJsonData161.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
						BuffSeidJsonData161.DataList.Add(buffSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BuffSeidJsonData161.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BuffSeidJsonData161.OnInitFinishAction != null)
			{
				BuffSeidJsonData161.OnInitFinishAction();
			}
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400414D RID: 16717
		public static int SEIDID = 161;

		// Token: 0x0400414E RID: 16718
		public static Dictionary<int, BuffSeidJsonData161> DataDict = new Dictionary<int, BuffSeidJsonData161>();

		// Token: 0x0400414F RID: 16719
		public static List<BuffSeidJsonData161> DataList = new List<BuffSeidJsonData161>();

		// Token: 0x04004150 RID: 16720
		public static Action OnInitFinishAction = new Action(BuffSeidJsonData161.OnInitFinish);

		// Token: 0x04004151 RID: 16721
		public int id;

		// Token: 0x04004152 RID: 16722
		public string panduan;
	}
}
