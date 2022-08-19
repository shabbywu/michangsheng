using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E69 RID: 3689
	[AddComponentMenu("")]
	public class CommandCopyBuffer : Block
	{
		// Token: 0x060067E2 RID: 26594 RVA: 0x0028B4E2 File Offset: 0x002896E2
		protected virtual void Start()
		{
			if (Application.isPlaying)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x0028B4F8 File Offset: 0x002896F8
		public static CommandCopyBuffer GetInstance()
		{
			if (CommandCopyBuffer.instance == null)
			{
				GameObject gameObject = GameObject.Find("_CommandCopyBuffer");
				if (gameObject == null)
				{
					gameObject = new GameObject("_CommandCopyBuffer");
					gameObject.hideFlags = 61;
				}
				CommandCopyBuffer.instance = gameObject.GetComponent<CommandCopyBuffer>();
				if (CommandCopyBuffer.instance == null)
				{
					CommandCopyBuffer.instance = gameObject.AddComponent<CommandCopyBuffer>();
				}
			}
			return CommandCopyBuffer.instance;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x0028B561 File Offset: 0x00289761
		public virtual bool HasCommands()
		{
			return this.GetCommands().Length != 0;
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x0028B56D File Offset: 0x0028976D
		public virtual Command[] GetCommands()
		{
			return base.GetComponents<Command>();
		}

		// Token: 0x060067E6 RID: 26598 RVA: 0x0028B578 File Offset: 0x00289778
		public virtual void Clear()
		{
			Command[] commands = this.GetCommands();
			for (int i = 0; i < commands.Length; i++)
			{
				Object.DestroyImmediate(commands[i]);
			}
		}

		// Token: 0x040058A2 RID: 22690
		protected static CommandCopyBuffer instance;
	}
}
