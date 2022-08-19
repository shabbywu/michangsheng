using System;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F14 RID: 3860
	[CommandInfo("YSFuBen", "RemoveSeaMonstart", "移除无尽之海NPC", 0)]
	[AddComponentMenu("")]
	public class RemoveSeaMonstart : Command
	{
		// Token: 0x06006D83 RID: 28035 RVA: 0x002A3608 File Offset: 0x002A1808
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

		// Token: 0x06006D84 RID: 28036 RVA: 0x002A36B8 File Offset: 0x002A18B8
		public void removeWait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = false;
			}
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x002A36E8 File Offset: 0x002A18E8
		public void wait()
		{
			CamaraFollow component = GameObject.Find("Main Camera").GetComponent<CamaraFollow>();
			if (component != null)
			{
				component.follwPlayer = true;
			}
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B31 RID: 23345
		[Tooltip("说明")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable UUID;
	}
}
