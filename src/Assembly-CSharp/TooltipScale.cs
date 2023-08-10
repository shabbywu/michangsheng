using UnityEngine;

public class TooltipScale : TooltipBase
{
	public UILabel uILabel;

	public bool IsSprite;

	public UISprite uISprite;

	public int BaseHeight = 28;

	protected override void Update()
	{
		base.Update();
		setBGwight();
	}

	public void setBGwight()
	{
		if (IsSprite)
		{
			uISprite.height = uILabel.height + BaseHeight;
		}
		if ((Object)(object)childTexture != (Object)null)
		{
			childTexture.height = uILabel.height + BaseHeight;
		}
	}
}
