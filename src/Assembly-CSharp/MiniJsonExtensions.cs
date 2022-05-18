using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020007AA RID: 1962
public static class MiniJsonExtensions
{
	// Token: 0x060031F4 RID: 12788 RVA: 0x0002478C File Offset: 0x0002298C
	public static string toJson(this Hashtable obj)
	{
		return MiniJSON.jsonEncode(obj);
	}

	// Token: 0x060031F5 RID: 12789 RVA: 0x0002478C File Offset: 0x0002298C
	public static string toJson(this Dictionary<string, string> obj)
	{
		return MiniJSON.jsonEncode(obj);
	}

	// Token: 0x060031F6 RID: 12790 RVA: 0x00024794 File Offset: 0x00022994
	public static ArrayList arrayListFromJson(this string json)
	{
		return MiniJSON.jsonDecode(json) as ArrayList;
	}

	// Token: 0x060031F7 RID: 12791 RVA: 0x000247A1 File Offset: 0x000229A1
	public static Hashtable hashtableFromJson(this string json)
	{
		return MiniJSON.jsonDecode(json) as Hashtable;
	}
}
