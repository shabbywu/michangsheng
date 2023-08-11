public class Line
{
	private int _startVertexIndex;

	private int _endVertexIndex;

	private int _vertexCount;

	public int StartVertexIndex => _startVertexIndex;

	public int EndVertexIndex => _endVertexIndex;

	public int VertexCount => _vertexCount;

	public Line(int startVertexIndex, int length)
	{
		_startVertexIndex = startVertexIndex;
		_endVertexIndex = length * 6 - 1 + startVertexIndex;
		_vertexCount = length * 6;
	}
}
