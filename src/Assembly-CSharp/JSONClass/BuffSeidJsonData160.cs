using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData160 : IJSONClass
{
	public static int SEIDID = 160;

	public static Dictionary<int, BuffSeidJsonData160> DataDict = new Dictionary<int, BuffSeidJsonData160>();

	public static List<BuffSeidJsonData160> DataList = new List<BuffSeidJsonData160>();

	public int id;

	public int value1;

	public int value2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[160].list)
		{
			BuffSeidJsonData160 buffSeidJsonData = new BuffSeidJsonData160();
			buffSeidJsonData.id = item["id"].I;
			buffSeidJsonData.value1 = item["value1"].I;
			buffSeidJsonData.value2 = item["value2"].I;
			DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
			DataList.Add(buffSeidJsonData);
		}
	}
}
