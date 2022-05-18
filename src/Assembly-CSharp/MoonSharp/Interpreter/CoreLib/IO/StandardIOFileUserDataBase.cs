using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x020011AD RID: 4525
	internal class StandardIOFileUserDataBase : StreamFileUserDataBase
	{
		// Token: 0x06006EC3 RID: 28355 RVA: 0x0004B552 File Offset: 0x00049752
		protected override string Close()
		{
			return "cannot close standard file";
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x0004B559 File Offset: 0x00049759
		public static StandardIOFileUserDataBase CreateInputStream(Stream stream)
		{
			StandardIOFileUserDataBase standardIOFileUserDataBase = new StandardIOFileUserDataBase();
			standardIOFileUserDataBase.Initialize(stream, new StreamReader(stream), null);
			return standardIOFileUserDataBase;
		}

		// Token: 0x06006EC5 RID: 28357 RVA: 0x0004B56E File Offset: 0x0004976E
		public static StandardIOFileUserDataBase CreateOutputStream(Stream stream)
		{
			StandardIOFileUserDataBase standardIOFileUserDataBase = new StandardIOFileUserDataBase();
			standardIOFileUserDataBase.Initialize(stream, null, new StreamWriter(stream));
			return standardIOFileUserDataBase;
		}
	}
}
