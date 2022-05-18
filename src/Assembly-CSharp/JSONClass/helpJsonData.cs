using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BD6 RID: 3030
	public class helpJsonData : IJSONClass
	{
		// Token: 0x06004AC0 RID: 19136 RVA: 0x001F9EBC File Offset: 0x001F80BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.helpJsonData.list)
			{
				try
				{
					helpJsonData helpJsonData = new helpJsonData();
					helpJsonData.id = jsonobject["id"].I;
					helpJsonData.Image = jsonobject["Image"].I;
					helpJsonData.Titile = jsonobject["Titile"].Str;
					if (helpJsonData.DataDict.ContainsKey(helpJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典helpJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", helpJsonData.id));
					}
					else
					{
						helpJsonData.DataDict.Add(helpJsonData.id, helpJsonData);
						helpJsonData.DataList.Add(helpJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典helpJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (helpJsonData.OnInitFinishAction != null)
			{
				helpJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004697 RID: 18071
		public static Dictionary<int, helpJsonData> DataDict = new Dictionary<int, helpJsonData>();

		// Token: 0x04004698 RID: 18072
		public static List<helpJsonData> DataList = new List<helpJsonData>();

		// Token: 0x04004699 RID: 18073
		public static Action OnInitFinishAction = new Action(helpJsonData.OnInitFinish);

		// Token: 0x0400469A RID: 18074
		public int id;

		// Token: 0x0400469B RID: 18075
		public int Image;

		// Token: 0x0400469C RID: 18076
		public string Titile;
	}
}
