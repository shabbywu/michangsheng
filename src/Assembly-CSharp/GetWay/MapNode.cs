namespace GetWay;

public class MapNode
{
	public float X;

	public float Y;

	public float F;

	public float G;

	public float H;

	public int Index;

	public MapNode Parent;

	public MapNode(int index, float x, float y)
	{
		X = x;
		Y = y;
		Index = index;
	}
}
