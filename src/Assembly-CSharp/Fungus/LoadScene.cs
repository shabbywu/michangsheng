using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x0200122D RID: 4653
	[CommandInfo("Flow", "Load Scene", "Loads a new Unity scene and displays an optional loading image. This is useful for splitting a large game across multiple scene files to reduce peak memory usage. Previously loaded assets will be released before loading the scene to free up memory.The scene to be loaded must be added to the scene list in Build Settings.", 0)]
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public class LoadScene : Command
	{
		// Token: 0x0600717A RID: 29050 RVA: 0x002A5BF0 File Offset: 0x002A3DF0
		public override void OnEnter()
		{
			Tools.instance.getPlayer().zulinContorl.kezhanLastScence = Tools.getScreenName();
			if (this._sceneName.Value == "LianDan")
			{
				PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼丹, 0);
			}
			else
			{
				Tools.instance.loadMapScenes(this._sceneName.Value, this.ResetLastScene);
			}
			this.Continue();
		}

		// Token: 0x0600717B RID: 29051 RVA: 0x0004D259 File Offset: 0x0004B459
		public override string GetSummary()
		{
			if (this._sceneName.Value.Length == 0)
			{
				return "Error: No scene name selected";
			}
			return this._sceneName.Value;
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x0004D27E File Offset: 0x0004B47E
		public override bool HasReference(Variable variable)
		{
			return this._sceneName.stringRef == variable || base.HasReference(variable);
		}

		// Token: 0x0600717E RID: 29054 RVA: 0x0004D29C File Offset: 0x0004B49C
		protected virtual void OnEnable()
		{
			if (this.sceneNameOLD != "")
			{
				this._sceneName.Value = this.sceneNameOLD;
				this.sceneNameOLD = "";
			}
		}

		// Token: 0x040063E6 RID: 25574
		[Tooltip("Name of the scene to load. The scene must also be added to the build settings.")]
		[SerializeField]
		protected StringData _sceneName = new StringData("");

		// Token: 0x040063E7 RID: 25575
		[Tooltip("Image to display while loading the scene")]
		[SerializeField]
		protected Texture2D loadingImage;

		// Token: 0x040063E8 RID: 25576
		[Tooltip("是否重新設置返回的上一個場景為當前設置的值")]
		[SerializeField]
		protected bool ResetLastScene = true;

		// Token: 0x040063E9 RID: 25577
		[HideInInspector]
		[FormerlySerializedAs("sceneName")]
		public string sceneNameOLD = "";
	}
}
