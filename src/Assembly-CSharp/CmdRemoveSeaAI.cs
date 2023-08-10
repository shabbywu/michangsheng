using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSSea", "移除SeaAI", "移除SeaAI", 0)]
[AddComponentMenu("")]
public class CmdRemoveSeaAI : Command
{
	[Tooltip("海域移除SeaAI的编号UUID")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable SeaRemoveNPCUUID;

	public override void OnEnter()
	{
		string text = "";
		if ((Object)(object)SeaRemoveNPCUUID != (Object)null)
		{
			text = SeaRemoveNPCUUID.Value;
		}
		Tools.instance.StartRemoveSeaMonstarFight(text);
		Tools.instance.AutoSetSeaMonstartDie();
		EndlessSeaMag.Inst.flagMonstarTarget = true;
		SeaAvatarObjBase[] array = Object.FindObjectsOfType<SeaAvatarObjBase>();
		foreach (SeaAvatarObjBase seaAvatarObjBase in array)
		{
			if (seaAvatarObjBase.UUID == text)
			{
				((Component)seaAvatarObjBase).gameObject.SetActive(false);
			}
		}
		Continue();
	}
}
