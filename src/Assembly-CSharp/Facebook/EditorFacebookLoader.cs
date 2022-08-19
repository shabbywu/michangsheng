using System;

namespace Facebook
{
	// Token: 0x02000B1D RID: 2845
	public class EditorFacebookLoader : FB.CompiledFacebookLoader
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x0021920C File Offset: 0x0021740C
		protected override IFacebook fb
		{
			get
			{
				return FBComponentFactory.GetComponent<EditorFacebook>(0);
			}
		}
	}
}
