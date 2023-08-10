using UnityEngine;

namespace WXB;

public class HyperlinkNode : TextNode
{
	private bool isEnter;

	public Color hoveColor = Color.red;

	public string d_link;

	public override Color currentColor
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			if (!isEnter)
			{
				return d_color;
			}
			return hoveColor;
		}
	}

	public override void onMouseEnter()
	{
		isEnter = true;
		owner.SetRenderDirty();
	}

	public override void onMouseLeave()
	{
		isEnter = false;
		owner.SetRenderDirty();
	}

	public override bool IsHyText()
	{
		return true;
	}

	public override void Release()
	{
		base.Release();
		isEnter = false;
	}
}
