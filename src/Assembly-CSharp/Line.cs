using System;

// Token: 0x02000163 RID: 355
public class Line
{
	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0005CAE1 File Offset: 0x0005ACE1
	public int StartVertexIndex
	{
		get
		{
			return this._startVertexIndex;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0005CAE9 File Offset: 0x0005ACE9
	public int EndVertexIndex
	{
		get
		{
			return this._endVertexIndex;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0005CAF1 File Offset: 0x0005ACF1
	public int VertexCount
	{
		get
		{
			return this._vertexCount;
		}
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x0005CAF9 File Offset: 0x0005ACF9
	public Line(int startVertexIndex, int length)
	{
		this._startVertexIndex = startVertexIndex;
		this._endVertexIndex = length * 6 - 1 + startVertexIndex;
		this._vertexCount = length * 6;
	}

	// Token: 0x04000B82 RID: 2946
	private int _startVertexIndex;

	// Token: 0x04000B83 RID: 2947
	private int _endVertexIndex;

	// Token: 0x04000B84 RID: 2948
	private int _vertexCount;
}
