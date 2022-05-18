using System;

namespace Facebook
{
	// Token: 0x02000E91 RID: 3729
	public class IOSFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06005995 RID: 22933 RVA: 0x0003F918 File Offset: 0x0003DB18
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<IOSFacebook>(0);
			}
		}
	}
}
