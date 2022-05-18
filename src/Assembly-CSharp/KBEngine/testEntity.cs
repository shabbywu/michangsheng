using System;

namespace KBEngine
{
	// Token: 0x0200104E RID: 4174
	public class testEntity : GameObject
	{
		// Token: 0x0600643F RID: 25663 RVA: 0x000042DD File Offset: 0x000024DD
		public override void __init__()
		{
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x00044F15 File Offset: 0x00043115
		public void hello(string msg)
		{
			Dbg.DEBUG_MSG("Account::hello: dbid=" + msg);
			base.baseCall("hello", new object[]
			{
				msg
			});
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x00044F3C File Offset: 0x0004313C
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
