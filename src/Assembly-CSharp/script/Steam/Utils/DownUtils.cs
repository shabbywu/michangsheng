using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace script.Steam.Utils
{
	// Token: 0x020009D9 RID: 2521
	public class DownUtils : MonoBehaviour
	{
		// Token: 0x060045F9 RID: 17913 RVA: 0x001D9FA7 File Offset: 0x001D81A7
		public void DownSpriteByUrl(string url, UnityAction<Sprite> action)
		{
			base.StartCoroutine(this.DownSprite(url, action));
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x001D9FB8 File Offset: 0x001D81B8
		private IEnumerator DownSprite(string url, UnityAction<Sprite> action)
		{
			using (UnityWebRequest request = new UnityWebRequest(url))
			{
				DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
				request.downloadHandler = texDl;
				yield return request.SendWebRequest();
				if (request.isNetworkError)
				{
					Debug.LogError(request.error);
				}
				else
				{
					Rect rect;
					rect..ctor(0f, 0f, (float)texDl.texture.width, (float)texDl.texture.height);
					Vector2 vector = Vector2.one * 0.5f;
					Sprite sprite = Sprite.Create(texDl.texture, rect, vector);
					if (action != null)
					{
						action.Invoke(sprite);
					}
				}
				texDl = null;
			}
			UnityWebRequest request = null;
			yield break;
			yield break;
		}
	}
}
