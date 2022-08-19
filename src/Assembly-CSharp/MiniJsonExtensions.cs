using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000516 RID: 1302
public static class MiniJsonExtensions
{
	// Token: 0x060029E1 RID: 10721 RVA: 0x0013FE04 File Offset: 0x0013E004
	public static string toJson(this Hashtable obj)
	{
		return MiniJSON.jsonEncode(obj);
	}

	// Token: 0x060029E2 RID: 10722 RVA: 0x0013FE04 File Offset: 0x0013E004
	public static string toJson(this Dictionary<string, string> obj)
	{
		return MiniJSON.jsonEncode(obj);
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x0013FE0C File Offset: 0x0013E00C
	public static ArrayList arrayListFromJson(this string json)
	{
		return MiniJSON.jsonDecode(json) as ArrayList;
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x0013FE19 File Offset: 0x0013E019
	public static Hashtable hashtableFromJson(this string json)
	{
		return MiniJSON.jsonDecode(json) as Hashtable;
	}
}
