using System;

namespace Facebook
{
	// Token: 0x02000E89 RID: 3721
	public class AndroidFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x0003F703 File Offset: 0x0003D903
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<AndroidFacebook>(0);
			}
		}
	}
}
