using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using script.YarnEditor.Component.TriggerData;

namespace script.YarnEditor.Mod;

[Serializable]
public class Yarn
{
	public string Name;

	public string Path;

	public TriggerConfig TriggerConfig;

	public void SaveTrigger()
	{
		FileStream fileStream = new FileStream(Path.Replace(".yarn", ".trigger"), FileMode.Create);
		new BinaryFormatter().Serialize(fileStream, TriggerConfig);
		fileStream.Close();
	}
}
