using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000743 RID: 1859
	public class _firstNameJsonData : IJSONClass
	{
		// Token: 0x06003B20 RID: 15136 RVA: 0x00196A04 File Offset: 0x00194C04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._firstNameJsonData.list)
			{
				try
				{
					_firstNameJsonData firstNameJsonData = new _firstNameJsonData();
					firstNameJsonData.id = jsonobject["id"].I;
					firstNameJsonData.Name = jsonobject["Name"].Str;
					if (_firstNameJsonData.DataDict.ContainsKey(firstNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_firstNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", firstNameJsonData.id));
					}
					else
					{
						_firstNameJsonData.DataDict.Add(firstNameJsonData.id, firstNameJsonData);
						_firstNameJsonData.DataList.Add(firstNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_firstNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_firstNameJsonData.OnInitFinishAction != null)
			{
				_firstNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400336A RID: 13162
		public static Dictionary<int, _firstNameJsonData> DataDict = new Dictionary<int, _firstNameJsonData>();

		// Token: 0x0400336B RID: 13163
		public static List<_firstNameJsonData> DataList = new List<_firstNameJsonData>();

		// Token: 0x0400336C RID: 13164
		public static Action OnInitFinishAction = new Action(_firstNameJsonData.OnInitFinish);

		// Token: 0x0400336D RID: 13165
		public int id;

		// Token: 0x0400336E RID: 13166
		public string Name;
	}
}
