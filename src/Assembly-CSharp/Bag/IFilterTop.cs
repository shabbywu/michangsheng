using System;

namespace Bag
{
	// Token: 0x0200099B RID: 2459
	public interface IFilterTop
	{
		// Token: 0x0600448F RID: 17551
		void Init(object data, FilterType type, string title);

		// Token: 0x06004490 RID: 17552
		void ClickEvent();

		// Token: 0x06004491 RID: 17553
		void CreateChild();
	}
}
