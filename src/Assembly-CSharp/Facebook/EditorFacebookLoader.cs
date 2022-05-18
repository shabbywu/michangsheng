using System;

namespace Facebook
{
	// Token: 0x02000E8D RID: 3725
	public class EditorFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600596D RID: 22893 RVA: 0x0003F817 File Offset: 0x0003DA17
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<EditorFacebook>(0);
			}
		}
	}
}
