using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200125D RID: 4701
	[CommandInfo("Audio", "Play Usfxr Sound", "Plays a usfxr synth sound. Use the usfxr editor [Tools > Fungus > Utilities > Generate usfxr Sound Effects] to create the SettingsString. Set a ParentTransform if using positional sound. See https://github.com/zeh/usfxr for more information about usfxr.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PlayUsfxrSound : Command
	{
		// Token: 0x06007229 RID: 29225 RVA: 0x0004DAE0 File Offset: 0x0004BCE0
		protected virtual void UpdateCache()
		{
			if (this._SettingsString.Value != null)
			{
				this._synth.parameters.SetSettingsString(this._SettingsString.Value);
				this._synth.CacheSound(null, false);
			}
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x0004DB18 File Offset: 0x0004BD18
		protected virtual void Awake()
		{
			this.UpdateCache();
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x00011424 File Offset: 0x0000F624
		protected void DoWait()
		{
			this.Continue();
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x002A79E8 File Offset: 0x002A5BE8
		public override void OnEnter()
		{
			this._synth.SetParentTransform(this.ParentTransform);
			this._synth.Play();
			if (Mathf.Approximately(this.waitDuration, 0f))
			{
				this.Continue();
				return;
			}
			base.Invoke("DoWait", this.waitDuration);
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x002A7A3C File Offset: 0x002A5C3C
		public override string GetSummary()
		{
			if (string.IsNullOrEmpty(this._SettingsString.Value))
			{
				return "Settings String hasn't been set!";
			}
			if (this.ParentTransform != null)
			{
				return this.ParentTransform.name + ": " + this._SettingsString.Value;
			}
			return "Camera.main: " + this._SettingsString.Value;
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x0004DB20 File Offset: 0x0004BD20
		public override Color GetButtonColor()
		{
			return new Color32(128, 200, 200, byte.MaxValue);
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x0004DB40 File Offset: 0x0004BD40
		public override bool HasReference(Variable variable)
		{
			return variable == this._SettingsString.stringRef;
		}

		// Token: 0x06007230 RID: 29232 RVA: 0x0004DB53 File Offset: 0x0004BD53
		protected virtual void OnEnable()
		{
			if (this.SettingsStringOLD != "")
			{
				this._SettingsString.Value = this.SettingsStringOLD;
				this.SettingsStringOLD = "";
			}
		}

		// Token: 0x04006477 RID: 25719
		[Tooltip("Transform to use for positional audio")]
		[SerializeField]
		protected Transform ParentTransform;

		// Token: 0x04006478 RID: 25720
		[Tooltip("Settings string which describes the audio")]
		[SerializeField]
		protected StringDataMulti _SettingsString = new StringDataMulti("");

		// Token: 0x04006479 RID: 25721
		[Tooltip("Time to wait before executing the next command")]
		[SerializeField]
		protected float waitDuration;

		// Token: 0x0400647A RID: 25722
		protected SfxrSynth _synth = new SfxrSynth();

		// Token: 0x0400647B RID: 25723
		[HideInInspector]
		[FormerlySerializedAs("SettingsString")]
		public string SettingsStringOLD = "";
	}
}
