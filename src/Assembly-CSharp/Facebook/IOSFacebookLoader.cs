using System;

namespace Facebook
{
	// Token: 0x02000B1F RID: 2847
	public class IOSFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x00219786 File Offset: 0x00217986
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<IOSFacebook>(0);
			}
		}
	}
}
