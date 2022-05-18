using System;

namespace Bag
{
	// Token: 0x02000D20 RID: 3360
	public interface IFilterTop
	{
		// Token: 0x06004FEF RID: 20463
		void Init(object data, FilterType type, string title);

		// Token: 0x06004FF0 RID: 20464
		void ClickEvent();

		// Token: 0x06004FF1 RID: 20465
		void CreateChild();
	}
}
