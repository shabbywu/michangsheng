using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

public class SceneLoader : MonoBehaviour
{
	protected Texture2D loadingTexture;

	protected string sceneToLoad;

	protected bool displayedImage;

	protected virtual void Start()
	{
		((MonoBehaviour)this).StartCoroutine(DoLoadBlock());
	}

	protected virtual IEnumerator DoLoadBlock()
	{
		while ((Object)(object)loadingTexture != (Object)null && !displayedImage)
		{
			yield return (object)new WaitForEndOfFrame();
		}
		yield return (object)new WaitForEndOfFrame();
		yield return Resources.UnloadUnusedAssets();
		while (!Application.CanStreamedLevelBeLoaded(sceneToLoad))
		{
			yield return (object)new WaitForEndOfFrame();
		}
		SceneManager.LoadScene(sceneToLoad);
		yield return (object)new WaitForEndOfFrame();
		Resources.UnloadUnusedAssets();
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	protected virtual void OnGUI()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Invalid comparison between Unknown and I4
		if (!((Object)(object)loadingTexture == (Object)null))
		{
			GUI.depth = -2000;
			float num = Screen.height;
			float num2 = (float)((Texture)loadingTexture).width * (num / (float)((Texture)loadingTexture).height);
			float num3 = (float)(Screen.width / 2) - num2 / 2f;
			float num4 = 0f;
			GUI.DrawTexture(new Rect(num3, num4, num2, num), (Texture)(object)loadingTexture);
			if ((int)Event.current.type == 7)
			{
				displayedImage = true;
			}
		}
	}

	public static void LoadScene(string _sceneToLoad, Texture2D _loadingTexture)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		GameObject val = new GameObject("SceneLoader");
		Object.DontDestroyOnLoad((Object)val);
		SceneLoader sceneLoader = val.AddComponent<SceneLoader>();
		sceneLoader.sceneToLoad = _sceneToLoad;
		sceneLoader.loadingTexture = _loadingTexture;
	}
}
