using System;

namespace KBEngine
{
	// Token: 0x02000C8B RID: 3211
	public class testEntity : GameObject
	{
		// Token: 0x06005982 RID: 22914 RVA: 0x00004095 File Offset: 0x00002295
		public override void __init__()
		{
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x00255FF1 File Offset: 0x002541F1
		public void hello(string msg)
		{
			Dbg.DEBUG_MSG("Account::hello: dbid=" + msg);
			base.baseCall("hello", new object[]
			{
				msg
			});
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x00256018 File Offset: 0x00254218
		public void helloClient(string msg)
		{
			Dbg.DEBUG_MSG("Account::hello: dbid=" + msg);
			Event.fireOut("helloClient2", new object[]
			{
				msg
			});
		}
	}
}
