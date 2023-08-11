using System.Collections.Generic;

namespace JSONClass;

public class BuffSeidJsonData157 : IJSONClass
{
	public static int SEIDID = 157;

	public static Dictionary<int, BuffSeidJsonData157> DataDict = new Dictionary<int, BuffSeidJsonData157>();

	public static List<BuffSeidJsonData157> DataList = new List<BuffSeidJsonData157>();

	public int id;

	public int target;

	public int value1;

	public int value2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.BuffSeidJsonData[157].list)
		{
			BuffSeidJsonData157 buffSeidJsonData = new BuffSeidJsonData157();
			buffSeidJsonData.id = item["id"].I;
			buffSeidJsonData.target = item["target"].I;
			buffSeidJsonData.value1 = item["value1"].I;
			buffSeidJsonData.value2 = item["value2"].I;
			DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
			DataList.Add(buffSeidJsonData);
		}
	}
}
