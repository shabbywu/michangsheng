using System;

public class PinkNumber
{
	private int max_key;

	private int key;

	private uint[] white_values;

	private uint range;

	private Random randomGenerator;

	private float rangeBy5;

	private int last_key;

	private uint sum;

	private int diff;

	private int i;

	public PinkNumber()
	{
		max_key = 31;
		range = 128u;
		rangeBy5 = (float)range / 5f;
		key = 0;
		white_values = new uint[5];
		randomGenerator = new Random();
		for (i = 0; i < 5; i++)
		{
			white_values[i] = (uint)(randomGenerator.NextDouble() % 1.0 * (double)rangeBy5);
		}
	}

	public float getNextValue()
	{
		last_key = key;
		sum = 0u;
		key++;
		if (key > max_key)
		{
			key = 0;
		}
		diff = last_key ^ key;
		sum = 0u;
		for (i = 0; i < 5; i++)
		{
			if ((diff & (1 << i)) > 0)
			{
				white_values[i] = (uint)(randomGenerator.NextDouble() % 1.0 * (double)rangeBy5);
			}
			sum += white_values[i];
		}
		return (float)sum / 64f - 1f;
	}
}
