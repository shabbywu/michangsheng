using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace script.Steam.Utils;

public class DownUtils : MonoBehaviour
{
	public void DownSpriteByUrl(string url, UnityAction<Sprite> action)
	{
		((MonoBehaviour)this).StartCoroutine(DownSprite(url, action));
	}

	private IEnumerator DownSprite(string url, UnityAction<Sprite> action)
	{
		UnityWebRequest request = new UnityWebRequest(url);
		try
		{
			DownloadHandlerTexture texDl = (DownloadHandlerTexture)(object)(request.downloadHandler = (DownloadHandler)new DownloadHandlerTexture(true));
			yield return request.SendWebRequest();
			if (request.isNetworkError)
			{
				Debug.LogError((object)request.error);
				yield break;
			}
			Rect val2 = default(Rect);
			((Rect)(ref val2))._002Ector(0f, 0f, (float)((Texture)texDl.texture).width, (float)((Texture)texDl.texture).height);
			Vector2 val3 = Vector2.one * 0.5f;
			Sprite val4 = Sprite.Create(texDl.texture, val2, val3);
			action?.Invoke(val4);
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}
}
