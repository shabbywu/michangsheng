using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012C6 RID: 4806
	[AddComponentMenu("")]
	public class CommandCopyBuffer : Block
	{
		// Token: 0x06007494 RID: 29844 RVA: 0x0004F8F3 File Offset: 0x0004DAF3
		protected virtual void Start()
		{
			if (Application.isPlaying)
			{
				Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06007495 RID: 29845 RVA: 0x002AE100 File Offset: 0x002AC300
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

		// Token: 0x06007496 RID: 29846 RVA: 0x0004F907 File Offset: 0x0004DB07
		public virtual bool HasCommands()
		{
			return this.GetCommands().Length != 0;
		}

		// Token: 0x06007497 RID: 29847 RVA: 0x0004F913 File Offset: 0x0004DB13
		public virtual Command[] GetCommands()
		{
			return base.GetComponents<Command>();
		}

		// Token: 0x06007498 RID: 29848 RVA: 0x002AE16C File Offset: 0x002AC36C
		public virtual void Clear()
		{
			Command[] commands = this.GetCommands();
			for (int i = 0; i < commands.Length; i++)
			{
				Object.DestroyImmediate(commands[i]);
			}
		}

		// Token: 0x0400663A RID: 26170
		protected static CommandCopyBuffer instance;
	}
}
