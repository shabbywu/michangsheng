using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x0200117F RID: 4479
	public class SourceRef
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06006D01 RID: 27905 RVA: 0x0004A50D File Offset: 0x0004870D
		// (set) Token: 0x06006D02 RID: 27906 RVA: 0x0004A515 File Offset: 0x00048715
		public bool IsClrLocation { get; private set; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06006D03 RID: 27907 RVA: 0x0004A51E File Offset: 0x0004871E
		// (set) Token: 0x06006D04 RID: 27908 RVA: 0x0004A526 File Offset: 0x00048726
		public int SourceIdx { get; private set; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06006D05 RID: 27909 RVA: 0x0004A52F File Offset: 0x0004872F
		// (set) Token: 0x06006D06 RID: 27910 RVA: 0x0004A537 File Offset: 0x00048737
		public int FromChar { get; private set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06006D07 RID: 27911 RVA: 0x0004A540 File Offset: 0x00048740
		// (set) Token: 0x06006D08 RID: 27912 RVA: 0x0004A548 File Offset: 0x00048748
		public int ToChar { get; private set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06006D09 RID: 27913 RVA: 0x0004A551 File Offset: 0x00048751
		// (set) Token: 0x06006D0A RID: 27914 RVA: 0x0004A559 File Offset: 0x00048759
		public int FromLine { get; private set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06006D0B RID: 27915 RVA: 0x0004A562 File Offset: 0x00048762
		// (set) Token: 0x06006D0C RID: 27916 RVA: 0x0004A56A File Offset: 0x0004876A
		public int ToLine { get; private set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06006D0D RID: 27917 RVA: 0x0004A573 File Offset: 0x00048773
		// (set) Token: 0x06006D0E RID: 27918 RVA: 0x0004A57B File Offset: 0x0004877B
		public bool IsStepStop { get; private set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06006D0F RID: 27919 RVA: 0x0004A584 File Offset: 0x00048784
		// (set) Token: 0x06006D10 RID: 27920 RVA: 0x0004A58C File Offset: 0x0004878C
		public bool CannotBreakpoint { get; private set; }

		// Token: 0x06006D11 RID: 27921 RVA: 0x0004A595 File Offset: 0x00048795
		internal static SourceRef GetClrLocation()
		{
			return new SourceRef(0, 0, 0, 0, 0, false)
			{
				IsClrLocation = true
			};
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x00299950 File Offset: 0x00297B50
		public SourceRef(SourceRef src, bool isStepStop)
		{
			this.SourceIdx = src.SourceIdx;
			this.FromChar = src.FromChar;
			this.ToChar = src.ToChar;
			this.FromLine = src.FromLine;
			this.ToLine = src.ToLine;
			this.IsStepStop = isStepStop;
		}

		// Token: 0x06006D13 RID: 27923 RVA: 0x0004A5A9 File Offset: 0x000487A9
		public SourceRef(int sourceIdx, int from, int to, int fromline, int toline, bool isStepStop)
		{
			this.SourceIdx = sourceIdx;
			this.FromChar = from;
			this.ToChar = to;
			this.FromLine = fromline;
			this.ToLine = toline;
			this.IsStepStop = isStepStop;
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x002999A8 File Offset: 0x00297BA8
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

		// Token: 0x06006D15 RID: 27925 RVA: 0x00299A24 File Offset: 0x00297C24
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

		// Token: 0x06006D16 RID: 27926 RVA: 0x00299B10 File Offset: 0x00297D10
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

		// Token: 0x06006D17 RID: 27927 RVA: 0x0004A5DE File Offset: 0x000487DE
		public SourceRef SetNoBreakPoint()
		{
			this.CannotBreakpoint = true;
			return this;
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x00299B90 File Offset: 0x00297D90
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

		// Token: 0x0400620C RID: 25100
		public bool Breakpoint;
	}
}
