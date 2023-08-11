using System.Collections.Generic;

namespace JSONClass;

public class SkillSeidJsonData153 : IJSONClass
{
	public static int SEIDID = 153;

	public static Dictionary<int, SkillSeidJsonData153> DataDict = new Dictionary<int, SkillSeidJsonData153>();

	public static List<SkillSeidJsonData153> DataList = new List<SkillSeidJsonData153>();

	public int id;

	public int target;

	public int value1;

	public int value2;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.SkillSeidJsonData[153].list)
		{
			SkillSeidJsonData153 skillSeidJsonData = new SkillSeidJsonData153();
			skillSeidJsonData.id = item["id"].I;
			skillSeidJsonData.target = item["target"].I;
			skillSeidJsonData.value1 = item["value1"].I;
			skillSeidJsonData.value2 = item["value2"].I;
			DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
			DataList.Add(skillSeidJsonData);
		}
	}
}
