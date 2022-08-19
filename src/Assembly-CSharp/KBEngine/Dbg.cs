using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B67 RID: 2919
	public class Dbg
	{
		// Token: 0x06005170 RID: 20848 RVA: 0x00222004 File Offset: 0x00220204
		public static string getHead()
		{
			return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "] ";
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00222032 File Offset: 0x00220232
		public static void INFO_MSG(object s)
		{
			if (DEBUGLEVEL.INFO >= Dbg.debugLevel)
			{
				Debug.Log(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0022204C File Offset: 0x0022024C
		public static void DEBUG_MSG(object s)
		{
			if (DEBUGLEVEL.DEBUG >= Dbg.debugLevel)
			{
				Debug.Log(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x00222066 File Offset: 0x00220266
		public static void WARNING_MSG(object s)
		{
			if (DEBUGLEVEL.WARNING >= Dbg.debugLevel)
			{
				Debug.LogWarning(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x00222080 File Offset: 0x00220280
		public static void ERROR_MSG(object s)
		{
			if (DEBUGLEVEL.ERROR >= Dbg.debugLevel)
			{
				Debug.LogError(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x00004095 File Offset: 0x00002295
		public static void profileStart(string name)
		{
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x00004095 File Offset: 0x00002295
		public static void profileEnd(string name)
		{
		}

		// Token: 0x04004F8A RID: 20362
		public static DEBUGLEVEL debugLevel;
	}
}
