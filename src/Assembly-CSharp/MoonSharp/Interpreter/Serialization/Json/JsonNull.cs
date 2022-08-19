using System;

namespace MoonSharp.Interpreter.Serialization.Json
{
	// Token: 0x02000CEE RID: 3310
	public sealed class JsonNull
	{
		// Token: 0x06005CA3 RID: 23715 RVA: 0x00024C5F File Offset: 0x00022E5F
		public static bool isNull()
		{
			return true;
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x00261277 File Offset: 0x0025F477
		[MoonSharpHidden]
		public static bool IsJsonNull(DynValue v)
		{
			return v.Type == DataType.UserData && v.UserData.Descriptor != null && v.UserData.Descriptor.Type == typeof(JsonNull);
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x002612B0 File Offset: 0x0025F4B0
		[MoonSharpHidden]
		public static DynValue Create()
		{
			return UserData.CreateStatic<JsonNull>();
		}
	}
}
