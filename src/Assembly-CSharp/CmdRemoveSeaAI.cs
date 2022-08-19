using System;
using Fungus;
using UnityEngine;

// Token: 0x02000478 RID: 1144
[CommandInfo("YSSea", "移除SeaAI", "移除SeaAI", 0)]
[AddComponentMenu("")]
public class CmdRemoveSeaAI : Command
{
	// Token: 0x060023BF RID: 9151 RVA: 0x000F4A9C File Offset: 0x000F2C9C
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

	// Token: 0x04001C95 RID: 7317
	[Tooltip("海域移除SeaAI的编号UUID")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	[SerializeField]
	protected StringVariable SeaRemoveNPCUUID;
}
