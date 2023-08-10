namespace Fungus;

public interface IExecuteHandlerConfigurator
{
	int UpdateExecuteStartOnFrame { set; }

	int UpdateExecuteRepeatFrequency { set; }

	bool UpdateExecuteRepeat { set; }

	float TimeExecuteStartAfter { set; }

	float TimeExecuteRepeatFrequency { set; }

	bool TimeExecuteRepeat { set; }

	ExecuteHandler Component { get; }
}
