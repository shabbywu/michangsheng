using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000E2B RID: 3627
	[CommandInfo("Scene", "Reload", "Reload the current scene", 0)]
	[AddComponentMenu("")]
	public class ReloadScene : Command
	{
		// Token: 0x06006639 RID: 26169 RVA: 0x00285C38 File Offset: 0x00283E38
		public override void OnEnter()
		{
			SceneLoader.LoadScene(SceneManager.GetActiveScene().name, this.loadingImage);
			this.Continue();
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x001D84A0 File Offset: 0x001D66A0
		public override string GetSummary()
		{
			return "";
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040057AA RID: 22442
		[Tooltip("Image to display while loading the scene")]
		[SerializeField]
		protected Texture2D loadingImage;
	}
}
