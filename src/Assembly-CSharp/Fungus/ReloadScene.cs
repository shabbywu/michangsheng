using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x0200127C RID: 4732
	[CommandInfo("Scene", "Reload", "Reload the current scene", 0)]
	[AddComponentMenu("")]
	public class ReloadScene : Command
	{
		// Token: 0x060072C7 RID: 29383 RVA: 0x002A94B4 File Offset: 0x002A76B4
		public override void OnEnter()
		{
			SceneLoader.LoadScene(SceneManager.GetActiveScene().name, this.loadingImage);
			this.Continue();
		}

		// Token: 0x060072C8 RID: 29384 RVA: 0x00032110 File Offset: 0x00030310
		public override string GetSummary()
		{
			return "";
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040064EE RID: 25838
		[Tooltip("Image to display while loading the scene")]
		[SerializeField]
		protected Texture2D loadingImage;
	}
}
