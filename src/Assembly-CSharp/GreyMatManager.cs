using UnityEngine;

public static class GreyMatManager
{
	private static Material grey1;

	private static Material grey2;

	private static Material black;

	public static Material Grey1
	{
		get
		{
			if ((Object)(object)grey1 == (Object)null)
			{
				grey1 = Resources.Load<Material>("NewUI/Misc/ImageGreyMat");
			}
			return grey1;
		}
	}

	public static Material Grey2
	{
		get
		{
			if ((Object)(object)grey2 == (Object)null)
			{
				grey2 = Resources.Load<Material>("NewUI/Misc/ImageGreyShader2");
			}
			return grey2;
		}
	}

	public static Material Black
	{
		get
		{
			if ((Object)(object)black == (Object)null)
			{
				black = Resources.Load<Material>("NewUI/Misc/ImageBlackMat");
			}
			return black;
		}
	}
}
