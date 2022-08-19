using System;

namespace Facebook
{
	// Token: 0x02000B1A RID: 2842
	public class AndroidFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x00218E33 File Offset: 0x00217033
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<AndroidFacebook>(0);
			}
		}
	}
}
