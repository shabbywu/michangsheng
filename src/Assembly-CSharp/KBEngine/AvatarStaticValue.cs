namespace KBEngine;

public class AvatarStaticValue
{
	public enum StaticValue
	{
		MaxNum = 2500
	}

	public int[] Value = new int[2500];

	public int[] talk = new int[2];

	public AvatarStaticValue()
	{
		for (int i = 0; i < 2500; i++)
		{
			Value[i] = 0;
		}
		talk[0] = 501;
		talk[1] = 1;
	}
}
