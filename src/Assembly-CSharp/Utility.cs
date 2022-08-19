using System;

// Token: 0x0200044B RID: 1099
internal class Utility
{
	// Token: 0x060022BF RID: 8895 RVA: 0x000EDD00 File Offset: 0x000EBF00
	public static int getPostInt(string name)
	{
		return int.Parse(name.Substring(name.IndexOf('_') + 1));
	}

	// Token: 0x060022C0 RID: 8896 RVA: 0x000EDD17 File Offset: 0x000EBF17
	public static string getPreString(string name)
	{
		return name.Substring(0, name.IndexOf('_'));
	}
}
