using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000ADD RID: 2781
	public class _LastNameJsonData : IJSONClass
	{
		// Token: 0x060046DE RID: 18142 RVA: 0x001E5268 File Offset: 0x001E3468
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance._LastNameJsonData.list)
			{
				try
				{
					_LastNameJsonData lastNameJsonData = new _LastNameJsonData();
					lastNameJsonData.id = jsonobject["id"].I;
					lastNameJsonData.Name = jsonobject["Name"].Str;
					if (_LastNameJsonData.DataDict.ContainsKey(lastNameJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典_LastNameJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", lastNameJsonData.id));
					}
					else
					{
						_LastNameJsonData.DataDict.Add(lastNameJsonData.id, lastNameJsonData);
						_LastNameJsonData.DataList.Add(lastNameJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典_LastNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (_LastNameJsonData.OnInitFinishAction != null)
			{
				_LastNameJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F28 RID: 16168
		public static Dictionary<int, _LastNameJsonData> DataDict = new Dictionary<int, _LastNameJsonData>();

		// Token: 0x04003F29 RID: 16169
		public static List<_LastNameJsonData> DataList = new List<_LastNameJsonData>();

		// Token: 0x04003F2A RID: 16170
		public static Action OnInitFinishAction = new Action(_LastNameJsonData.OnInitFinish);

		// Token: 0x04003F2B RID: 16171
		public int id;

		// Token: 0x04003F2C RID: 16172
		public string Name;
	}
}
