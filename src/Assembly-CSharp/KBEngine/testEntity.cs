namespace KBEngine;

public class testEntity : GameObject
{
	public override void __init__()
	{
	}

	public void hello(string msg)
	{
		Dbg.DEBUG_MSG("Account::hello: dbid=" + msg);
		baseCall("hello", msg);
	}

	public void helloClient(string msg)
	{
		Dbg.DEBUG_MSG("Account::hello: dbid=" + msg);
		Event.fireOut("helloClient2", msg);
	}
}
