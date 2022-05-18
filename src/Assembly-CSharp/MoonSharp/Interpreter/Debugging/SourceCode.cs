using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x0200117E RID: 4478
	public class SourceCode : IScriptPrivateResource
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06006CF2 RID: 27890 RVA: 0x0004A493 File Offset: 0x00048693
		// (set) Token: 0x06006CF3 RID: 27891 RVA: 0x0004A49B File Offset: 0x0004869B
		public string Name { get; private set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x0004A4A4 File Offset: 0x000486A4
		// (set) Token: 0x06006CF5 RID: 27893 RVA: 0x0004A4AC File Offset: 0x000486AC
		public string Code { get; private set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06006CF6 RID: 27894 RVA: 0x0004A4B5 File Offset: 0x000486B5
		// (set) Token: 0x06006CF7 RID: 27895 RVA: 0x0004A4BD File Offset: 0x000486BD
		public string[] Lines { get; private set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06006CF8 RID: 27896 RVA: 0x0004A4C6 File Offset: 0x000486C6
		// (set) Token: 0x06006CF9 RID: 27897 RVA: 0x0004A4CE File Offset: 0x000486CE
		public Script OwnerScript { get; private set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06006CFA RID: 27898 RVA: 0x0004A4D7 File Offset: 0x000486D7
		// (set) Token: 0x06006CFB RID: 27899 RVA: 0x0004A4DF File Offset: 0x000486DF
		public int SourceID { get; private set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06006CFC RID: 27900 RVA: 0x0004A4E8 File Offset: 0x000486E8
		// (set) Token: 0x06006CFD RID: 27901 RVA: 0x0004A4F0 File Offset: 0x000486F0
		internal List<SourceRef> Refs { get; private set; }

		// Token: 0x06006CFE RID: 27902 RVA: 0x002997C4 File Offset: 0x002979C4
		internal SourceCode(string name, string code, int sourceID, Script ownerScript)
		{
			this.Refs = new List<SourceRef>();
			List<string> list = new List<string>();
			this.Name = name;
			this.Code = code;
			list.Add(string.Format("-- Begin of chunk : {0} ", name));
			list.AddRange(this.Code.Split(new char[]
			{
				'\n'
			}));
			this.Lines = list.ToArray();
			this.OwnerScript = ownerScript;
			this.SourceID = sourceID;
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x00299840 File Offset: 0x00297A40
		public string GetCodeSnippet(SourceRef sourceCodeRef)
		{
			if (sourceCodeRef.FromLine == sourceCodeRef.ToLine)
			{
				int num = this.AdjustStrIndex(this.Lines[sourceCodeRef.FromLine], sourceCodeRef.FromChar);
				int num2 = this.AdjustStrIndex(this.Lines[sourceCodeRef.FromLine], sourceCodeRef.ToChar);
				return this.Lines[sourceCodeRef.FromLine].Substring(num, num2 - num);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = sourceCodeRef.FromLine; i <= sourceCodeRef.ToLine; i++)
			{
				if (i == sourceCodeRef.FromLine)
				{
					int startIndex = this.AdjustStrIndex(this.Lines[i], sourceCodeRef.FromChar);
					stringBuilder.Append(this.Lines[i].Substring(startIndex));
				}
				else if (i == sourceCodeRef.ToLine)
				{
					int num3 = this.AdjustStrIndex(this.Lines[i], sourceCodeRef.ToChar);
					stringBuilder.Append(this.Lines[i].Substring(0, num3 + 1));
				}
				else
				{
					stringBuilder.Append(this.Lines[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x0004A4F9 File Offset: 0x000486F9
		private int AdjustStrIndex(string str, int loc)
		{
			return Math.Max(Math.Min(str.Length, loc), 0);
		}
	}
}
