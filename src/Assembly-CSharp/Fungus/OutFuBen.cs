using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200144E RID: 5198
	[CommandInfo("YSTools", "OutFuBen", "�뿪����", 0)]
	[AddComponentMenu("")]
	public class OutFuBen : Command
	{
		// Token: 0x06007D82 RID: 32130 RVA: 0x00054DC0 File Offset: 0x00052FC0
		public override void OnEnter()
		{
			Tools.instance.loadMapScenes(this._sceneName.Value, true);
			Tools.instance.getPlayer().fubenContorl.outFuBen(false);
			this.Continue();
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x00054DF3 File Offset: 0x00052FF3
		public override string GetSummary()
		{
			if (this._sceneName.Value.Length == 0)
			{
				return "Error: No scene name selected";
			}
			return this._sceneName.Value;
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007D85 RID: 32133 RVA: 0x00054E18 File Offset: 0x00053018
		public override bool HasReference(Variable variable)
		{
			return this._sceneName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x00054E36 File Offset: 0x00053036
		protected virtual void OnEnable()
		{
			if (this.sceneNameOLD != "")
			{
				this._sceneName.Value = this.sceneNameOLD;
				this.sceneNameOLD = "";
			}
		}

		// Token: 0x04006AED RID: 27373
		[Tooltip("�����ĳ�������")]
		[SerializeField]
		protected StringData _sceneName = new StringData("");

		// Token: 0x04006AEE RID: 27374
		[HideInInspector]
		[FormerlySerializedAs("sceneName")]
		public string sceneNameOLD = "";
	}
}
