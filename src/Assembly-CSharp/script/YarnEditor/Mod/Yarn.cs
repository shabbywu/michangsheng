using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using script.YarnEditor.Component.TriggerData;

namespace script.YarnEditor.Mod
{
	// Token: 0x020009C6 RID: 2502
	[Serializable]
	public class Yarn
	{
		// Token: 0x06004592 RID: 17810 RVA: 0x001D7B50 File Offset: 0x001D5D50
		public void SaveTrigger()
		{
			FileStream fileStream = new FileStream(this.Path.Replace(".yarn", ".trigger"), FileMode.Create);
			new BinaryFormatter().Serialize(fileStream, this.TriggerConfig);
			fileStream.Close();
		}

		// Token: 0x04004712 RID: 18194
		public string Name;

		// Token: 0x04004713 RID: 18195
		public string Path;

		// Token: 0x04004714 RID: 18196
		public TriggerConfig TriggerConfig;
	}
}
