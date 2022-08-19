using System;

// Token: 0x02000440 RID: 1088
[AttributeUsage(AttributeTargets.Class)]
public class BindPrefab : Attribute
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06002293 RID: 8851 RVA: 0x000ED5C5 File Offset: 0x000EB7C5
	// (set) Token: 0x06002294 RID: 8852 RVA: 0x000ED5CD File Offset: 0x000EB7CD
	public string Path { get; private set; }

	// Token: 0x06002295 RID: 8853 RVA: 0x000ED5D6 File Offset: 0x000EB7D6
	public BindPrefab(string path)
	{
		this.Path = path;
	}
}
