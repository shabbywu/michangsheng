using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000ADB RID: 2779
	public class _firstNameJsonData : IJSONClass
	{
		// Token: 0x060046D6 RID: 18134 RVA: 0x001E4DCC File Offset: 0x001E2FCC
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

		// Token: 0x060046D7 RID: 18135 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003F04 RID: 16132
		public static Dictionary<int, _firstNameJsonData> DataDict = new Dictionary<int, _firstNameJsonData>();

		// Token: 0x04003F05 RID: 16133
		public static List<_firstNameJsonData> DataList = new List<_firstNameJsonData>();

		// Token: 0x04003F06 RID: 16134
		public static Action OnInitFinishAction = new Action(_firstNameJsonData.OnInitFinish);

		// Token: 0x04003F07 RID: 16135
		public int id;

		// Token: 0x04003F08 RID: 16136
		public string Name;
	}
}
