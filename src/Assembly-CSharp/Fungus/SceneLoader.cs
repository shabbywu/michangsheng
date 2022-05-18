using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x020012F4 RID: 4852
	public class SceneLoader : MonoBehaviour
	{
		// Token: 0x06007655 RID: 30293 RVA: 0x000508CE File Offset: 0x0004EACE
		protected virtual void Start()
		{
			base.StartCoroutine(this.DoLoadBlock());
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x000508DD File Offset: 0x0004EADD
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

		// Token: 0x06007657 RID: 30295 RVA: 0x002B31D0 File Offset: 0x002B13D0
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

		// Token: 0x06007658 RID: 30296 RVA: 0x000508EC File Offset: 0x0004EAEC
		public static void LoadScene(string _sceneToLoad, Texture2D _loadingTexture)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			Object.DontDestroyOnLoad(gameObject);
			SceneLoader sceneLoader = gameObject.AddComponent<SceneLoader>();
			sceneLoader.sceneToLoad = _sceneToLoad;
			sceneLoader.loadingTexture = _loadingTexture;
		}

		// Token: 0x04006732 RID: 26418
		protected Texture2D loadingTexture;

		// Token: 0x04006733 RID: 26419
		protected string sceneToLoad;

		// Token: 0x04006734 RID: 26420
		protected bool displayedImage;
	}
}
