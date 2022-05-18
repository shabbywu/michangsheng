using System;

// Token: 0x0200023E RID: 574
public class Line
{
	// Token: 0x17000242 RID: 578
	// (get) Token: 0x060011C0 RID: 4544 RVA: 0x000111F5 File Offset: 0x0000F3F5
	public int StartVertexIndex
	{
		get
		{
			return this._startVertexIndex;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x060011C1 RID: 4545 RVA: 0x000111FD File Offset: 0x0000F3FD
	public int EndVertexIndex
	{
		get
		{
			return this._endVertexIndex;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00011205 File Offset: 0x0000F405
	public int VertexCount
	{
		get
		{
			return this._vertexCount;
		}
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x0001120D File Offset: 0x0000F40D
	public Line(int startVertexIndex, int length)
	{
		this._startVertexIndex = startVertexIndex;
		this._endVertexIndex = length * 6 - 1 + startVertexIndex;
		this._vertexCount = length * 6;
	}

	// Token: 0x04000E4F RID: 3663
	private int _startVertexIndex;

	// Token: 0x04000E50 RID: 3664
	private int _endVertexIndex;

	// Token: 0x04000E51 RID: 3665
	private int _vertexCount;
}
