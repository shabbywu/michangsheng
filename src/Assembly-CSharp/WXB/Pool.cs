using System.Text;

namespace WXB;

internal static class Pool
{
	private static Factory<StringBuilder> sb_factory;

	public static PD<StringBuilder> GetSB()
	{
		if (sb_factory == null)
		{
			sb_factory = new Factory<StringBuilder>(delegate(StringBuilder sb)
			{
				sb.Length = 0;
			});
		}
		return (PD<StringBuilder>)sb_factory.create();
	}
}
