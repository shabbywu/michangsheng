public static class JSONObjectEx
{
	public static int GetInt(this JSONObject json, string fieldName, int fallback = 0)
	{
		json.GetField(ref fallback, fieldName);
		return fallback;
	}

	public static float GetFloat(this JSONObject json, string fieldName, float fallback = 0f)
	{
		json.GetField(ref fallback, fieldName);
		return fallback;
	}

	public static string GetString(this JSONObject json, string fieldName, string fallback = "")
	{
		json.GetField(ref fallback, fieldName);
		return fallback;
	}
}
