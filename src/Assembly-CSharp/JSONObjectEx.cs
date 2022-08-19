using System;

// Token: 0x020001C3 RID: 451
public static class JSONObjectEx
{
	// Token: 0x060012BB RID: 4795 RVA: 0x00075165 File Offset: 0x00073365
	public static int GetInt(this JSONObject json, string fieldName, int fallback = 0)
	{
		json.GetField(ref fallback, fieldName, null);
		return fallback;
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x00075173 File Offset: 0x00073373
	public static float GetFloat(this JSONObject json, string fieldName, float fallback = 0f)
	{
		json.GetField(ref fallback, fieldName, null);
		return fallback;
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x00075181 File Offset: 0x00073381
	public static string GetString(this JSONObject json, string fieldName, string fallback = "")
	{
		json.GetField(ref fallback, fieldName, null);
		return fallback;
	}
}
