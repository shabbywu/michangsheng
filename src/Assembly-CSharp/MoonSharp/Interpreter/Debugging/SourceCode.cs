using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D68 RID: 3432
	public class SourceCode : IScriptPrivateResource
	{
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x00272A34 File Offset: 0x00270C34
		// (set) Token: 0x060060FF RID: 24831 RVA: 0x00272A3C File Offset: 0x00270C3C
		public string Name { get; private set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06006100 RID: 24832 RVA: 0x00272A45 File Offset: 0x00270C45
		// (set) Token: 0x06006101 RID: 24833 RVA: 0x00272A4D File Offset: 0x00270C4D
		public string Code { get; private set; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06006102 RID: 24834 RVA: 0x00272A56 File Offset: 0x00270C56
		// (set) Token: 0x06006103 RID: 24835 RVA: 0x00272A5E File Offset: 0x00270C5E
		public string[] Lines { get; private set; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06006104 RID: 24836 RVA: 0x00272A67 File Offset: 0x00270C67
		// (set) Token: 0x06006105 RID: 24837 RVA: 0x00272A6F File Offset: 0x00270C6F
		public Script OwnerScript { get; private set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x00272A78 File Offset: 0x00270C78
		// (set) Token: 0x06006107 RID: 24839 RVA: 0x00272A80 File Offset: 0x00270C80
		public int SourceID { get; private set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06006108 RID: 24840 RVA: 0x00272A89 File Offset: 0x00270C89
		// (set) Token: 0x06006109 RID: 24841 RVA: 0x00272A91 File Offset: 0x00270C91
		internal List<SourceRef> Refs { get; private set; }

		// Token: 0x0600610A RID: 24842 RVA: 0x00272A9C File Offset: 0x00270C9C
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

		// Token: 0x0600610B RID: 24843 RVA: 0x00272B18 File Offset: 0x00270D18
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

		// Token: 0x0600610C RID: 24844 RVA: 0x00272C26 File Offset: 0x00270E26
		private int AdjustStrIndex(string str, int loc)
		{
			return Math.Max(Math.Min(str.Length, loc), 0);
		}
	}
}
