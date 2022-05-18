using System;

// Token: 0x02000605 RID: 1541
internal class Utility
{
	// Token: 0x0600267F RID: 9855 RVA: 0x0001EB53 File Offset: 0x0001CD53
	public static int getPostInt(string name)
	{
		return int.Parse(name.Substring(name.IndexOf('_') + 1));
	}

	// Token: 0x06002680 RID: 9856 RVA: 0x0001EB6A File Offset: 0x0001CD6A
	public static string getPreString(string name)
	{
		return name.Substring(0, name.IndexOf('_'));
	}
}
