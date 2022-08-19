using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D69 RID: 3433
	public class SourceRef
	{
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x00272C3A File Offset: 0x00270E3A
		// (set) Token: 0x0600610E RID: 24846 RVA: 0x00272C42 File Offset: 0x00270E42
		public bool IsClrLocation { get; private set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600610F RID: 24847 RVA: 0x00272C4B File Offset: 0x00270E4B
		// (set) Token: 0x06006110 RID: 24848 RVA: 0x00272C53 File Offset: 0x00270E53
		public int SourceIdx { get; private set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x00272C5C File Offset: 0x00270E5C
		// (set) Token: 0x06006112 RID: 24850 RVA: 0x00272C64 File Offset: 0x00270E64
		public int FromChar { get; private set; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x00272C6D File Offset: 0x00270E6D
		// (set) Token: 0x06006114 RID: 24852 RVA: 0x00272C75 File Offset: 0x00270E75
		public int ToChar { get; private set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x00272C7E File Offset: 0x00270E7E
		// (set) Token: 0x06006116 RID: 24854 RVA: 0x00272C86 File Offset: 0x00270E86
		public int FromLine { get; private set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06006117 RID: 24855 RVA: 0x00272C8F File Offset: 0x00270E8F
		// (set) Token: 0x06006118 RID: 24856 RVA: 0x00272C97 File Offset: 0x00270E97
		public int ToLine { get; private set; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06006119 RID: 24857 RVA: 0x00272CA0 File Offset: 0x00270EA0
		// (set) Token: 0x0600611A RID: 24858 RVA: 0x00272CA8 File Offset: 0x00270EA8
		public bool IsStepStop { get; private set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600611B RID: 24859 RVA: 0x00272CB1 File Offset: 0x00270EB1
		// (set) Token: 0x0600611C RID: 24860 RVA: 0x00272CB9 File Offset: 0x00270EB9
		public bool CannotBreakpoint { get; private set; }

		// Token: 0x0600611D RID: 24861 RVA: 0x00272CC2 File Offset: 0x00270EC2
		internal static SourceRef GetClrLocation()
		{
			return new SourceRef(0, 0, 0, 0, 0, false)
			{
				IsClrLocation = true
			};
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x00272CD8 File Offset: 0x00270ED8
		public SourceRef(SourceRef src, bool isStepStop)
		{
			this.SourceIdx = src.SourceIdx;
			this.FromChar = src.FromChar;
			this.ToChar = src.ToChar;
			this.FromLine = src.FromLine;
			this.ToLine = src.ToLine;
			this.IsStepStop = isStepStop;
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x00272D2E File Offset: 0x00270F2E
		public SourceRef(int sourceIdx, int from, int to, int fromline, int toline, bool isStepStop)
		{
			this.SourceIdx = sourceIdx;
			this.FromChar = from;
			this.ToChar = to;
			this.FromLine = fromline;
			this.ToLine = toline;
			this.IsStepStop = isStepStop;
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x00272D64 File Offset: 0x00270F64
		public override string ToString()
		{
			return string.Format("[{0}]{1} ({2}, {3}) -> ({4}, {5})", new object[]
			{
				this.SourceIdx,
				this.IsStepStop ? "*" : " ",
				this.FromLine,
				this.FromChar,
				this.ToLine,
				this.ToChar
			});
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x00272DE0 File Offset: 0x00270FE0
		internal int GetLocationDistance(int sourceIdx, int line, int col)
		{
			if (sourceIdx != this.SourceIdx)
			{
				return int.MaxValue;
			}
			if (this.FromLine == this.ToLine)
			{
				if (line != this.FromLine)
				{
					return Math.Abs(line - this.FromLine) * 1600;
				}
				if (col >= this.FromChar && col <= this.ToChar)
				{
					return 0;
				}
				if (col < this.FromChar)
				{
					return this.FromChar - col;
				}
				return col - this.ToChar;
			}
			else if (line == this.FromLine)
			{
				if (col < this.FromChar)
				{
					return this.FromChar - col;
				}
				return 0;
			}
			else if (line == this.ToLine)
			{
				if (col > this.ToChar)
				{
					return col - this.ToChar;
				}
				return 0;
			}
			else
			{
				if (line > this.FromLine && line < this.ToLine)
				{
					return 0;
				}
				if (line < this.FromLine)
				{
					return (this.FromLine - line) * 1600;
				}
				return (line - this.ToLine) * 1600;
			}
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x00272ECC File Offset: 0x002710CC
		public bool IncludesLocation(int sourceIdx, int line, int col)
		{
			if (sourceIdx != this.SourceIdx || line < this.FromLine || line > this.ToLine)
			{
				return false;
			}
			if (this.FromLine == this.ToLine)
			{
				return col >= this.FromChar && col <= this.ToChar;
			}
			if (line == this.FromLine)
			{
				return col >= this.FromChar;
			}
			return line != this.ToLine || col <= this.ToChar;
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x00272F49 File Offset: 0x00271149
		public SourceRef SetNoBreakPoint()
		{
			this.CannotBreakpoint = true;
			return this;
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x00272F54 File Offset: 0x00271154
		public string FormatLocation(Script script, bool forceClassicFormat = false)
		{
			SourceCode sourceCode = script.GetSourceCode(this.SourceIdx);
			if (this.IsClrLocation)
			{
				return "[clr]";
			}
			if (script.Options.UseLuaErrorLocations || forceClassicFormat)
			{
				return string.Format("{0}:{1}", sourceCode.Name, this.FromLine);
			}
			if (this.FromLine != this.ToLine)
			{
				return string.Format("{0}:({1},{2}-{3},{4})", new object[]
				{
					sourceCode.Name,
					this.FromLine,
					this.FromChar,
					this.ToLine,
					this.ToChar
				});
			}
			if (this.FromChar == this.ToChar)
			{
				return string.Format("{0}:({1},{2})", new object[]
				{
					sourceCode.Name,
					this.FromLine,
					this.FromChar,
					this.ToLine,
					this.ToChar
				});
			}
			return string.Format("{0}:({1},{2}-{4})", new object[]
			{
				sourceCode.Name,
				this.FromLine,
				this.FromChar,
				this.ToLine,
				this.ToChar
			});
		}

		// Token: 0x04005571 RID: 21873
		public bool Breakpoint;
	}
}
