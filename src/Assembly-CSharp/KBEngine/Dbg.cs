using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EE4 RID: 3812
	public class Dbg
	{
		// Token: 0x06005BAC RID: 23468 RVA: 0x002512B8 File Offset: 0x0024F4B8
		public static string getHead()
		{
			return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "] ";
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x00040899 File Offset: 0x0003EA99
		public static void INFO_MSG(object s)
		{
			if (DEBUGLEVEL.INFO >= Dbg.debugLevel)
			{
				Debug.Log(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x000408B3 File Offset: 0x0003EAB3
		public static void DEBUG_MSG(object s)
		{
			if (DEBUGLEVEL.DEBUG >= Dbg.debugLevel)
			{
				Debug.Log(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x000408CD File Offset: 0x0003EACD
		public static void WARNING_MSG(object s)
		{
			if (DEBUGLEVEL.WARNING >= Dbg.debugLevel)
			{
				Debug.LogWarning(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x000408E7 File Offset: 0x0003EAE7
		public static void ERROR_MSG(object s)
		{
			if (DEBUGLEVEL.ERROR >= Dbg.debugLevel)
			{
				Debug.LogError(Dbg.getHead() + s);
			}
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x000042DD File Offset: 0x000024DD
		public static void profileStart(string name)
		{
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x000042DD File Offset: 0x000024DD
		public static void profileEnd(string name)
		{
		}

		// Token: 0x04005A15 RID: 23061
		public static DEBUGLEVEL debugLevel;
	}
}
