using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000E84 RID: 3716
	public class SceneLoader : MonoBehaviour
	{
		// Token: 0x0600695C RID: 26972 RVA: 0x00290DC5 File Offset: 0x0028EFC5
		protected virtual void Start()
		{
			base.StartCoroutine(this.DoLoadBlock());
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x00290DD4 File Offset: 0x0028EFD4
		protected virtual IEnumerator DoLoadBlock()
		{
			while (this.loadingTexture != null && !this.displayedImage)
			{
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
			yield return Resources.UnloadUnusedAssets();
			while (!Application.CanStreamedLevelBeLoaded(this.sceneToLoad))
			{
				yield return new WaitForEndOfFrame();
			}
			SceneManager.LoadScene(this.sceneToLoad);
			yield return new WaitForEndOfFrame();
			Resources.UnloadUnusedAssets();
			Object.Destroy(base.gameObject);
			yield break;
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x00290DE4 File Offset: 0x0028EFE4
		protected virtual void OnGUI()
		{
			if (this.loadingTexture == null)
			{
				return;
			}
			GUI.depth = -2000;
			float num = (float)Screen.height;
			float num2 = (float)this.loadingTexture.width * (num / (float)this.loadingTexture.height);
			float num3 = (float)(Screen.width / 2) - num2 / 2f;
			float num4 = 0f;
			GUI.DrawTexture(new Rect(num3, num4, num2, num), this.loadingTexture);
			if (Event.current.type == 7)
			{
				this.displayedImage = true;
			}
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x00290E6C File Offset: 0x0028F06C
		public static void LoadScene(string _sceneToLoad, Texture2D _loadingTexture)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			Object.DontDestroyOnLoad(gameObject);
			SceneLoader sceneLoader = gameObject.AddComponent<SceneLoader>();
			sceneLoader.sceneToLoad = _sceneToLoad;
			sceneLoader.loadingTexture = _loadingTexture;
		}

		// Token: 0x04005954 RID: 22868
		protected Texture2D loadingTexture;

		// Token: 0x04005955 RID: 22869
		protected string sceneToLoad;

		// Token: 0x04005956 RID: 22870
		protected bool displayedImage;
	}
}
