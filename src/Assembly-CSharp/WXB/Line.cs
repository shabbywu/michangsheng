using UnityEngine;

namespace WXB;

public class Line
{
	private Vector2 size;

	public float x
	{
		get
		{
			return size.x;
		}
		set
		{
			size.x = value;
		}
	}

	public float y
	{
		get
		{
			return size.y;
		}
		set
		{
			size.y = value;
		}
	}

	public Vector2 s => size;

	public float minY { get; set; }

	public float maxY { get; set; }

	public float fontHeight => maxY - minY;

	public Line(Vector2 s)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		size = s;
	}

	public void Clear()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		size = Vector2.zero;
	}

	public override string ToString()
	{
		return $"w:{x} h:{y} minY:{minY} maxY:{maxY} fh:{fontHeight}";
	}
}
