using System;
using Fungus;
using UnityEngine;

// Token: 0x02000636 RID: 1590
[CommandInfo("YSSea", "移除SeaAI", "移除SeaAI", 0)]
[AddComponentMenu("")]
public class CmdRemoveSeaAI : Command
{
	// Token: 0x0600277C RID: 10108 RVA: 0x001349B8 File Offset: 0x00132BB8
	public override void OnEnter()
	{
		string text = "";
		if (this.SeaRemoveNPCUUID != null)
		{
			text = this.SeaRemoveNPCUUID.Value;
		}
		Tools.instance.StartRemoveSeaMonstarFight(text);
		Tools.instance.AutoSetSeaMonstartDie();
		EndlessSeaMag.Inst.flagMonstarTarget = true;
		foreach (SeaAvatarObjBase seaAvatarObjBase in Object.FindObjectsOfType<SeaAvatarObjBase>())
		{
			if (seaAvatarObjBase.UUID == text)
			{
				seaAvatarObjBase.gameObject.SetActive(false);
			}
		}
		this.Continue();
	}

	// Token: 0x04002170 RID: 8560
	[Tooltip("海域移除SeaAI的编号UUID")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	[SerializeField]
	protected StringVariable SeaRemoveNPCUUID;
}
