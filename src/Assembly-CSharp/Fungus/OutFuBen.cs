using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000F9E RID: 3998
	[CommandInfo("YSTools", "OutFuBen", "�뿪����", 0)]
	[AddComponentMenu("")]
	public class OutFuBen : Command
	{
		// Token: 0x06006FA2 RID: 28578 RVA: 0x002A72E2 File Offset: 0x002A54E2
		public override void OnEnter()
		{
			Tools.instance.loadMapScenes(this._sceneName.Value, true);
			Tools.instance.getPlayer().fubenContorl.outFuBen(false);
			this.Continue();
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x002A7315 File Offset: 0x002A5515
		public override string GetSummary()
		{
			if (this._sceneName.Value.Length == 0)
			{
				return "Error: No scene name selected";
			}
			return this._sceneName.Value;
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x002A733A File Offset: 0x002A553A
		public override bool HasReference(Variable variable)
		{
			return this._sceneName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x002A7358 File Offset: 0x002A5558
		protected virtual void OnEnable()
		{
			if (this.sceneNameOLD != "")
			{
				this._sceneName.Value = this.sceneNameOLD;
				this.sceneNameOLD = "";
			}
		}

		// Token: 0x04005C1E RID: 23582
		[Tooltip("�����ĳ�������")]
		[SerializeField]
		protected StringData _sceneName = new StringData("");

		// Token: 0x04005C1F RID: 23583
		[HideInInspector]
		[FormerlySerializedAs("sceneName")]
		public string sceneNameOLD = "";
	}
}
