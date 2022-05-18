using System;

namespace MoonSharp.Interpreter.Serialization.Json
{
	// Token: 0x020010CB RID: 4299
	public sealed class JsonNull
	{
		// Token: 0x060067B9 RID: 26553 RVA: 0x0000A093 File Offset: 0x00008293
		public static bool isNull()
		{
			return true;
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00047488 File Offset: 0x00045688
		[MoonSharpHidden]
		public static bool IsJsonNull(DynValue v)
		{
			return v.Type == DataType.UserData && v.UserData.Descriptor != null && v.UserData.Descriptor.Type == typeof(JsonNull);
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x000474C1 File Offset: 0x000456C1
		[MoonSharpHidden]
		public static DynValue Create()
		{
			return UserData.CreateStatic<JsonNull>();
		}
	}
}
