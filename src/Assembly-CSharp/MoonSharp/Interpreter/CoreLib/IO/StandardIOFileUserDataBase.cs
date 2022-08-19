using System;
using System.IO;

namespace MoonSharp.Interpreter.CoreLib.IO
{
	// Token: 0x02000D8A RID: 3466
	internal class StandardIOFileUserDataBase : StreamFileUserDataBase
	{
		// Token: 0x06006292 RID: 25234 RVA: 0x00279902 File Offset: 0x00277B02
		protected override string Close()
		{
			return "cannot close standard file";
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x00279909 File Offset: 0x00277B09
		public static StandardIOFileUserDataBase CreateInputStream(Stream stream)
		{
			StandardIOFileUserDataBase standardIOFileUserDataBase = new StandardIOFileUserDataBase();
			standardIOFileUserDataBase.Initialize(stream, new StreamReader(stream), null);
			return standardIOFileUserDataBase;
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x0027991E File Offset: 0x00277B1E
		public static StandardIOFileUserDataBase CreateOutputStream(Stream stream)
		{
			StandardIOFileUserDataBase standardIOFileUserDataBase = new StandardIOFileUserDataBase();
			standardIOFileUserDataBase.Initialize(stream, null, new StreamWriter(stream));
			return standardIOFileUserDataBase;
		}
	}
}
