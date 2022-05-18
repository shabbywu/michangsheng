using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using script.YarnEditor.Component.TriggerData;

namespace script.YarnEditor.Mod
{
	// Token: 0x02000AAB RID: 2731
	[Serializable]
	public class Yarn
	{
		// Token: 0x060045E7 RID: 17895 RVA: 0x001DDB14 File Offset: 0x001DBD14
		public void SaveTrigger()
		{
			FileStream fileStream = new FileStream(this.Path.Replace(".yarn", ".trigger"), FileMode.Create);
			new BinaryFormatter().Serialize(fileStream, this.TriggerConfig);
			fileStream.Close();
		}

		// Token: 0x04003E1E RID: 15902
		public string Name;

		// Token: 0x04003E1F RID: 15903
		public string Path;

		// Token: 0x04003E20 RID: 15904
		public TriggerConfig TriggerConfig;
	}
}
