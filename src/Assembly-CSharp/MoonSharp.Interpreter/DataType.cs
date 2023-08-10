namespace MoonSharp.Interpreter;

public enum DataType
{
	Nil,
	Void,
	Boolean,
	Number,
	String,
	Function,
	Table,
	Tuple,
	UserData,
	Thread,
	ClrFunction,
	TailCallRequest,
	YieldRequest
}
