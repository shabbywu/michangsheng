using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C7 RID: 5063
	[CommandInfo("YSFuBen", "RemoveSeaMonstart", "移除无尽之海NPC", 0)]
	[AddComponentMenu("")]
	public class RemoveSeaMonstart : Command
	{
		// Token: 0x06007B6C RID: 31596 RVA: 0x002C3AB4 File Offset: 0x002C1CB4
		public override void OnEnter()
		{
			Avatar player = Tools.instance.getPlayer();
			foreach (SeaAvatarObjBase seaAvatarObjBase in EndlessSeaMag.Inst.MonstarList)
			{
				if (seaAvatarObjBase.UUID == this.UUID.Value)
				{
					Object.Destroy(seaAvatarObjBase.gameObject);
					EndlessSeaMag.Inst.MonstarList.Remove(seaAvatarObjBase);
					break;
				}
			}
			player.seaNodeMag.RemoveSeaMonstar(this.UUID.Value);
			this.Continue();
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x002C37E4 File Offset: 0x002C19E4
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x002C3814 File Offset: 0x002C1A14
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06007B6F RID: 31599 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B70 RID: 31600 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x040069F8 RID: 27128
		[Tooltip("说明")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable UUID;
	}
}
