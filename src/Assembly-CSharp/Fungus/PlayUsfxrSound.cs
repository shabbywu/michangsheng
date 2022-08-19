using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E10 RID: 3600
	[CommandInfo("Audio", "Play Usfxr Sound", "Plays a usfxr synth sound. Use the usfxr editor [Tools > Fungus > Utilities > Generate usfxr Sound Effects] to create the SettingsString. Set a ParentTransform if using positional sound. See https://github.com/zeh/usfxr for more information about usfxr.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class PlayUsfxrSound : Command
	{
		// Token: 0x0600659B RID: 26011 RVA: 0x002839AD File Offset: 0x00281BAD
		protected virtual void UpdateCache()
		{
			if (this._SettingsString.Value != null)
			{
				this._synth.parameters.SetSettingsString(this._SettingsString.Value);
				this._synth.CacheSound(null, false);
			}
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x002839E5 File Offset: 0x00281BE5
		protected virtual void Awake()
		{
			this.UpdateCache();
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		protected void DoWait()
		{
			this.Continue();
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x002839F0 File Offset: 0x00281BF0
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

		// Token: 0x0600659F RID: 26015 RVA: 0x00283A44 File Offset: 0x00281C44
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

		// Token: 0x060065A0 RID: 26016 RVA: 0x00283AAD File Offset: 0x00281CAD
		public override Color GetButtonColor()
		{
			return new Color32(128, 200, 200, byte.MaxValue);
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x00283ACD File Offset: 0x00281CCD
		public override bool HasReference(Variable variable)
		{
			return variable == this._SettingsString.stringRef;
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x00283AE0 File Offset: 0x00281CE0
		protected virtual void OnEnable()
		{
			if (this.SettingsStringOLD != "")
			{
				this._SettingsString.Value = this.SettingsStringOLD;
				this.SettingsStringOLD = "";
			}
		}

		// Token: 0x04005742 RID: 22338
		[Tooltip("Transform to use for positional audio")]
		[SerializeField]
		protected Transform ParentTransform;

		// Token: 0x04005743 RID: 22339
		[Tooltip("Settings string which describes the audio")]
		[SerializeField]
		protected StringDataMulti _SettingsString = new StringDataMulti("");

		// Token: 0x04005744 RID: 22340
		[Tooltip("Time to wait before executing the next command")]
		[SerializeField]
		protected float waitDuration;

		// Token: 0x04005745 RID: 22341
		protected SfxrSynth _synth = new SfxrSynth();

		// Token: 0x04005746 RID: 22342
		[HideInInspector]
		[FormerlySerializedAs("SettingsString")]
		public string SettingsStringOLD = "";
	}
}
