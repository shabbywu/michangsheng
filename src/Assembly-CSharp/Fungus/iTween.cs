using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013B3 RID: 5043
	[AddComponentMenu("")]
	public class iTween : MonoBehaviour
	{
		// Token: 0x06007A25 RID: 31269 RVA: 0x0005350E File Offset: 0x0005170E
		public static void Init(GameObject target)
		{
			iTween.MoveBy(target, Vector3.zero, 0f);
			iTween.cameraFade = null;
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x002B9A48 File Offset: 0x002B7C48
		public static void CameraFadeFrom(float amount, float time)
		{
			if (iTween.cameraFade)
			{
				iTween.CameraFadeFrom(iTween.Hash(new object[]
				{
					"amount",
					amount,
					"time",
					time
				}));
				return;
			}
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x00053526 File Offset: 0x00051726
		public static void CameraFadeFrom(Hashtable args)
		{
			if (iTween.cameraFade)
			{
				iTween.ColorFrom(iTween.cameraFade, args);
				return;
			}
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x002B9AA0 File Offset: 0x002B7CA0
		public static void CameraFadeTo(float amount, float time)
		{
			if (iTween.cameraFade)
			{
				iTween.CameraFadeTo(iTween.Hash(new object[]
				{
					"amount",
					amount,
					"time",
					time
				}));
				return;
			}
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x0005354A File Offset: 0x0005174A
		public static void CameraFadeTo(Hashtable args)
		{
			if (iTween.cameraFade)
			{
				iTween.ColorTo(iTween.cameraFade, args);
				return;
			}
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x002B9AF8 File Offset: 0x002B7CF8
		public static void ValueTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (!args.Contains("onupdate") || !args.Contains("from") || !args.Contains("to"))
			{
				Debug.LogError("iTween Error: ValueTo() requires an 'onupdate' callback function and a 'from' and 'to' property.  The supplied 'onupdate' callback must accept a single argument that is the same type as the supplied 'from' and 'to' properties!");
				return;
			}
			args["type"] = "value";
			if (args["from"].GetType() == typeof(Vector2))
			{
				args["method"] = "vector2";
			}
			else if (args["from"].GetType() == typeof(Vector3))
			{
				args["method"] = "vector3";
			}
			else if (args["from"].GetType() == typeof(Rect))
			{
				args["method"] = "rect";
			}
			else if (args["from"].GetType() == typeof(float))
			{
				args["method"] = "float";
			}
			else
			{
				if (!(args["from"].GetType() == typeof(Color)))
				{
					Debug.LogError("iTween Error: ValueTo() only works with interpolating Vector3s, Vector2s, floats, ints, Rects and Colors!");
					return;
				}
				args["method"] = "color";
			}
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", iTween.EaseType.linear);
			}
			iTween.Launch(target, args);
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x0005356E File Offset: 0x0005176E
		public static void FadeFrom(GameObject target, float alpha, float time)
		{
			iTween.FadeFrom(target, iTween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x000535A3 File Offset: 0x000517A3
		public static void FadeFrom(GameObject target, Hashtable args)
		{
			iTween.ColorFrom(target, args);
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x000535AC File Offset: 0x000517AC
		public static void FadeTo(GameObject target, float alpha, float time)
		{
			iTween.FadeTo(target, iTween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x000535E1 File Offset: 0x000517E1
		public static void FadeTo(GameObject target, Hashtable args)
		{
			iTween.ColorTo(target, args);
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x000535EA File Offset: 0x000517EA
		public static void ColorFrom(GameObject target, Color color, float time)
		{
			iTween.ColorFrom(target, iTween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x002B9C88 File Offset: 0x002B7E88
		public static void ColorFrom(GameObject target, Hashtable args)
		{
			Color color = default(Color);
			Color color2 = default(Color);
			args = iTween.CleanArgs(args);
			if (!args.Contains("includechildren") || (bool)args["includechildren"])
			{
				foreach (object obj in target.transform)
				{
					Component component = (Transform)obj;
					Hashtable hashtable = (Hashtable)args.Clone();
					hashtable["ischild"] = true;
					iTween.ColorFrom(component.gameObject, hashtable);
				}
			}
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", iTween.EaseType.linear);
			}
			if (target.GetComponent<Renderer>())
			{
				color = (color2 = target.GetComponent<Renderer>().material.color);
			}
			else if (target.GetComponent<Light>())
			{
				color = (color2 = target.GetComponent<Light>().color);
			}
			if (args.Contains("color"))
			{
				color = (Color)args["color"];
			}
			else
			{
				if (args.Contains("r"))
				{
					color.r = (float)args["r"];
				}
				if (args.Contains("g"))
				{
					color.g = (float)args["g"];
				}
				if (args.Contains("b"))
				{
					color.b = (float)args["b"];
				}
				if (args.Contains("a"))
				{
					color.a = (float)args["a"];
				}
			}
			if (args.Contains("amount"))
			{
				color.a = (float)args["amount"];
				args.Remove("amount");
			}
			else if (args.Contains("alpha"))
			{
				color.a = (float)args["alpha"];
				args.Remove("alpha");
			}
			if (target.GetComponent<Renderer>())
			{
				target.GetComponent<Renderer>().material.color = color;
			}
			else if (target.GetComponent<Light>())
			{
				target.GetComponent<Light>().color = color;
			}
			args["color"] = color2;
			args["type"] = "color";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x0005361F File Offset: 0x0005181F
		public static void ColorTo(GameObject target, Color color, float time)
		{
			iTween.ColorTo(target, iTween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x002B9F1C File Offset: 0x002B811C
		public static void ColorTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (!args.Contains("includechildren") || (bool)args["includechildren"])
			{
				foreach (object obj in target.transform)
				{
					Component component = (Transform)obj;
					Hashtable hashtable = (Hashtable)args.Clone();
					hashtable["ischild"] = true;
					iTween.ColorTo(component.gameObject, hashtable);
				}
			}
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", iTween.EaseType.linear);
			}
			args["type"] = "color";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x002BA004 File Offset: 0x002B8204
		public static void AudioFrom(GameObject target, float volume, float pitch, float time)
		{
			iTween.AudioFrom(target, iTween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x002BA058 File Offset: 0x002B8258
		public static void AudioFrom(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			AudioSource audioSource;
			if (args.Contains("audiosource"))
			{
				audioSource = (AudioSource)args["audiosource"];
			}
			else
			{
				if (!target.GetComponent<AudioSource>())
				{
					Debug.LogError("iTween Error: AudioFrom requires an AudioSource.");
					return;
				}
				audioSource = target.GetComponent<AudioSource>();
			}
			Vector2 vector;
			Vector2 vector2;
			vector.x = (vector2.x = audioSource.volume);
			vector.y = (vector2.y = audioSource.pitch);
			if (args.Contains("volume"))
			{
				vector2.x = (float)args["volume"];
			}
			if (args.Contains("pitch"))
			{
				vector2.y = (float)args["pitch"];
			}
			audioSource.volume = vector2.x;
			audioSource.pitch = vector2.y;
			args["volume"] = vector.x;
			args["pitch"] = vector.y;
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", iTween.EaseType.linear);
			}
			args["type"] = "audio";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x002BA1B0 File Offset: 0x002B83B0
		public static void AudioTo(GameObject target, float volume, float pitch, float time)
		{
			iTween.AudioTo(target, iTween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x002BA204 File Offset: 0x002B8404
		public static void AudioTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (!args.Contains("easetype"))
			{
				args.Add("easetype", iTween.EaseType.linear);
			}
			args["type"] = "audio";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x00053654 File Offset: 0x00051854
		public static void Stab(GameObject target, AudioClip audioclip, float delay)
		{
			iTween.Stab(target, iTween.Hash(new object[]
			{
				"audioclip",
				audioclip,
				"delay",
				delay
			}));
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x00053684 File Offset: 0x00051884
		public static void Stab(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "stab";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x000536A5 File Offset: 0x000518A5
		public static void LookFrom(GameObject target, Vector3 looktarget, float time)
		{
			iTween.LookFrom(target, iTween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x002BA260 File Offset: 0x002B8460
		public static void LookFrom(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			Vector3 eulerAngles = target.transform.eulerAngles;
			if (args["looktarget"].GetType() == typeof(Transform))
			{
				target.transform.LookAt((Transform)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
			}
			else if (args["looktarget"].GetType() == typeof(Vector3))
			{
				target.transform.LookAt((Vector3)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
			}
			if (args.Contains("axis"))
			{
				Vector3 eulerAngles2 = target.transform.eulerAngles;
				string a = (string)args["axis"];
				if (!(a == "x"))
				{
					if (!(a == "y"))
					{
						if (a == "z")
						{
							eulerAngles2.x = eulerAngles.x;
							eulerAngles2.y = eulerAngles.y;
						}
					}
					else
					{
						eulerAngles2.x = eulerAngles.x;
						eulerAngles2.z = eulerAngles.z;
					}
				}
				else
				{
					eulerAngles2.y = eulerAngles.y;
					eulerAngles2.z = eulerAngles.z;
				}
				target.transform.eulerAngles = eulerAngles2;
			}
			args["rotation"] = eulerAngles;
			args["type"] = "rotate";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x000536DA File Offset: 0x000518DA
		public static void LookTo(GameObject target, Vector3 looktarget, float time)
		{
			iTween.LookTo(target, iTween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x002BA438 File Offset: 0x002B8638
		public static void LookTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["looktarget"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			}
			args["type"] = "look";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x0005370F File Offset: 0x0005190F
		public static void MoveTo(GameObject target, Vector3 position, float time)
		{
			iTween.MoveTo(target, iTween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x002BA524 File Offset: 0x002B8724
		public static void MoveTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["position"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "move";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x00053744 File Offset: 0x00051944
		public static void MoveFrom(GameObject target, Vector3 position, float time)
		{
			iTween.MoveFrom(target, iTween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x002BA648 File Offset: 0x002B8848
		public static void MoveFrom(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = iTween.Defaults.isLocal;
			}
			if (args.Contains("path"))
			{
				Vector3[] array2;
				if (args["path"].GetType() == typeof(Vector3[]))
				{
					Vector3[] array = (Vector3[])args["path"];
					array2 = new Vector3[array.Length];
					Array.Copy(array, array2, array.Length);
				}
				else
				{
					Transform[] array3 = (Transform[])args["path"];
					array2 = new Vector3[array3.Length];
					for (int i = 0; i < array3.Length; i++)
					{
						array2[i] = array3[i].position;
					}
				}
				if (array2[array2.Length - 1] != target.transform.position)
				{
					Vector3[] array4 = new Vector3[array2.Length + 1];
					Array.Copy(array2, array4, array2.Length);
					if (flag)
					{
						array4[array4.Length - 1] = target.transform.localPosition;
						target.transform.localPosition = array4[0];
					}
					else
					{
						array4[array4.Length - 1] = target.transform.position;
						target.transform.position = array4[0];
					}
					args["path"] = array4;
				}
				else
				{
					if (flag)
					{
						target.transform.localPosition = array2[0];
					}
					else
					{
						target.transform.position = array2[0];
					}
					args["path"] = array2;
				}
			}
			else
			{
				Vector3 vector2;
				Vector3 vector;
				if (flag)
				{
					vector = (vector2 = target.transform.localPosition);
				}
				else
				{
					vector = (vector2 = target.transform.position);
				}
				if (args.Contains("position"))
				{
					if (args["position"].GetType() == typeof(Transform))
					{
						vector = ((Transform)args["position"]).position;
					}
					else if (args["position"].GetType() == typeof(Vector3))
					{
						vector = (Vector3)args["position"];
					}
				}
				else
				{
					if (args.Contains("x"))
					{
						vector.x = (float)args["x"];
					}
					if (args.Contains("y"))
					{
						vector.y = (float)args["y"];
					}
					if (args.Contains("z"))
					{
						vector.z = (float)args["z"];
					}
				}
				if (flag)
				{
					target.transform.localPosition = vector;
				}
				else
				{
					target.transform.position = vector;
				}
				args["position"] = vector2;
			}
			args["type"] = "move";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A41 RID: 31297 RVA: 0x00053779 File Offset: 0x00051979
		public static void MoveAdd(GameObject target, Vector3 amount, float time)
		{
			iTween.MoveAdd(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x000537AE File Offset: 0x000519AE
		public static void MoveAdd(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "add";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x000537DF File Offset: 0x000519DF
		public static void MoveBy(GameObject target, Vector3 amount, float time)
		{
			iTween.MoveBy(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x00053814 File Offset: 0x00051A14
		public static void MoveBy(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "by";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x00053845 File Offset: 0x00051A45
		public static void ScaleTo(GameObject target, Vector3 scale, float time)
		{
			iTween.ScaleTo(target, iTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x002BA958 File Offset: 0x002B8B58
		public static void ScaleTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["scale"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "scale";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x0005387A File Offset: 0x00051A7A
		public static void ScaleFrom(GameObject target, Vector3 scale, float time)
		{
			iTween.ScaleFrom(target, iTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x002BAA7C File Offset: 0x002B8C7C
		public static void ScaleFrom(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			Vector3 localScale2;
			Vector3 localScale = localScale2 = target.transform.localScale;
			if (args.Contains("scale"))
			{
				if (args["scale"].GetType() == typeof(Transform))
				{
					localScale = ((Transform)args["scale"]).localScale;
				}
				else if (args["scale"].GetType() == typeof(Vector3))
				{
					localScale = (Vector3)args["scale"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					localScale.x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					localScale.y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					localScale.z = (float)args["z"];
				}
			}
			target.transform.localScale = localScale;
			args["scale"] = localScale2;
			args["type"] = "scale";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x000538AF File Offset: 0x00051AAF
		public static void ScaleAdd(GameObject target, Vector3 amount, float time)
		{
			iTween.ScaleAdd(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x000538E4 File Offset: 0x00051AE4
		public static void ScaleAdd(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "add";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A4B RID: 31307 RVA: 0x00053915 File Offset: 0x00051B15
		public static void ScaleBy(GameObject target, Vector3 amount, float time)
		{
			iTween.ScaleBy(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x0005394A File Offset: 0x00051B4A
		public static void ScaleBy(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "by";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x0005397B File Offset: 0x00051B7B
		public static void RotateTo(GameObject target, Vector3 rotation, float time)
		{
			iTween.RotateTo(target, iTween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x002BABCC File Offset: 0x002B8DCC
		public static void RotateTo(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["rotation"];
				args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
				args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
			args["type"] = "rotate";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x000539B0 File Offset: 0x00051BB0
		public static void RotateFrom(GameObject target, Vector3 rotation, float time)
		{
			iTween.RotateFrom(target, iTween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x002BACF0 File Offset: 0x002B8EF0
		public static void RotateFrom(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = iTween.Defaults.isLocal;
			}
			Vector3 vector2;
			Vector3 vector;
			if (flag)
			{
				vector = (vector2 = target.transform.localEulerAngles);
			}
			else
			{
				vector = (vector2 = target.transform.eulerAngles);
			}
			if (args.Contains("rotation"))
			{
				if (args["rotation"].GetType() == typeof(Transform))
				{
					vector = ((Transform)args["rotation"]).eulerAngles;
				}
				else if (args["rotation"].GetType() == typeof(Vector3))
				{
					vector = (Vector3)args["rotation"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					vector.x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					vector.y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					vector.z = (float)args["z"];
				}
			}
			if (flag)
			{
				target.transform.localEulerAngles = vector;
			}
			else
			{
				target.transform.eulerAngles = vector;
			}
			args["rotation"] = vector2;
			args["type"] = "rotate";
			args["method"] = "to";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x000539E5 File Offset: 0x00051BE5
		public static void RotateAdd(GameObject target, Vector3 amount, float time)
		{
			iTween.RotateAdd(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x00053A1A File Offset: 0x00051C1A
		public static void RotateAdd(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "add";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x00053A4B File Offset: 0x00051C4B
		public static void RotateBy(GameObject target, Vector3 amount, float time)
		{
			iTween.RotateBy(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x00053A80 File Offset: 0x00051C80
		public static void RotateBy(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "by";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x00053AB1 File Offset: 0x00051CB1
		public static void ShakePosition(GameObject target, Vector3 amount, float time)
		{
			iTween.ShakePosition(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x00053AE6 File Offset: 0x00051CE6
		public static void ShakePosition(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "position";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x00053B17 File Offset: 0x00051D17
		public static void ShakeScale(GameObject target, Vector3 amount, float time)
		{
			iTween.ShakeScale(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x00053B4C File Offset: 0x00051D4C
		public static void ShakeScale(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "scale";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x00053B7D File Offset: 0x00051D7D
		public static void ShakeRotation(GameObject target, Vector3 amount, float time)
		{
			iTween.ShakeRotation(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x00053BB2 File Offset: 0x00051DB2
		public static void ShakeRotation(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "rotation";
			iTween.Launch(target, args);
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x00053BE3 File Offset: 0x00051DE3
		public static void PunchPosition(GameObject target, Vector3 amount, float time)
		{
			iTween.PunchPosition(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x002BAE8C File Offset: 0x002B908C
		public static void PunchPosition(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "punch";
			args["method"] = "position";
			args["easetype"] = iTween.EaseType.punch;
			iTween.Launch(target, args);
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x00053C18 File Offset: 0x00051E18
		public static void PunchRotation(GameObject target, Vector3 amount, float time)
		{
			iTween.PunchRotation(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x002BAEDC File Offset: 0x002B90DC
		public static void PunchRotation(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "punch";
			args["method"] = "rotation";
			args["easetype"] = iTween.EaseType.punch;
			iTween.Launch(target, args);
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x00053C4D File Offset: 0x00051E4D
		public static void PunchScale(GameObject target, Vector3 amount, float time)
		{
			iTween.PunchScale(target, iTween.Hash(new object[]
			{
				"amount",
				amount,
				"time",
				time
			}));
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x002BAF2C File Offset: 0x002B912C
		public static void PunchScale(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "punch";
			args["method"] = "scale";
			args["easetype"] = iTween.EaseType.punch;
			iTween.Launch(target, args);
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x002BAF7C File Offset: 0x002B917C
		private void GenerateTargets()
		{
			string text = this.type;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2361356451U)
			{
				if (num <= 1031692888U)
				{
					if (num != 407568404U)
					{
						if (num != 1031692888U)
						{
							return;
						}
						if (!(text == "color"))
						{
							return;
						}
						text = this.method;
						if (text == "to")
						{
							this.GenerateColorToTargets();
							this.apply = new iTween.ApplyTween(this.ApplyColorToTargets);
							return;
						}
					}
					else
					{
						if (!(text == "move"))
						{
							return;
						}
						text = this.method;
						if (!(text == "to"))
						{
							if (!(text == "by") && !(text == "add"))
							{
								return;
							}
							this.GenerateMoveByTargets();
							this.apply = new iTween.ApplyTween(this.ApplyMoveByTargets);
							return;
						}
						else
						{
							if (this.tweenArguments.Contains("path"))
							{
								this.GenerateMoveToPathTargets();
								this.apply = new iTween.ApplyTween(this.ApplyMoveToPathTargets);
								return;
							}
							this.GenerateMoveToTargets();
							this.apply = new iTween.ApplyTween(this.ApplyMoveToTargets);
							return;
						}
					}
				}
				else if (num != 1113510858U)
				{
					if (num != 2190941297U)
					{
						if (num != 2361356451U)
						{
							return;
						}
						if (!(text == "punch"))
						{
							return;
						}
						text = this.method;
						if (text == "position")
						{
							this.GeneratePunchPositionTargets();
							this.apply = new iTween.ApplyTween(this.ApplyPunchPositionTargets);
							return;
						}
						if (text == "rotation")
						{
							this.GeneratePunchRotationTargets();
							this.apply = new iTween.ApplyTween(this.ApplyPunchRotationTargets);
							return;
						}
						if (!(text == "scale"))
						{
							return;
						}
						this.GeneratePunchScaleTargets();
						this.apply = new iTween.ApplyTween(this.ApplyPunchScaleTargets);
						return;
					}
					else
					{
						if (!(text == "scale"))
						{
							return;
						}
						text = this.method;
						if (text == "to")
						{
							this.GenerateScaleToTargets();
							this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
							return;
						}
						if (text == "by")
						{
							this.GenerateScaleByTargets();
							this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
							return;
						}
						if (!(text == "add"))
						{
							return;
						}
						this.GenerateScaleAddTargets();
						this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
						return;
					}
				}
				else
				{
					if (!(text == "value"))
					{
						return;
					}
					text = this.method;
					if (text == "float")
					{
						this.GenerateFloatTargets();
						this.apply = new iTween.ApplyTween(this.ApplyFloatTargets);
						return;
					}
					if (text == "vector2")
					{
						this.GenerateVector2Targets();
						this.apply = new iTween.ApplyTween(this.ApplyVector2Targets);
						return;
					}
					if (text == "vector3")
					{
						this.GenerateVector3Targets();
						this.apply = new iTween.ApplyTween(this.ApplyVector3Targets);
						return;
					}
					if (text == "color")
					{
						this.GenerateColorTargets();
						this.apply = new iTween.ApplyTween(this.ApplyColorTargets);
						return;
					}
					if (!(text == "rect"))
					{
						return;
					}
					this.GenerateRectTargets();
					this.apply = new iTween.ApplyTween(this.ApplyRectTargets);
					return;
				}
			}
			else if (num <= 3180049141U)
			{
				if (num != 2784296202U)
				{
					if (num != 3180049141U)
					{
						return;
					}
					if (!(text == "shake"))
					{
						return;
					}
					text = this.method;
					if (text == "position")
					{
						this.GenerateShakePositionTargets();
						this.apply = new iTween.ApplyTween(this.ApplyShakePositionTargets);
						return;
					}
					if (text == "scale")
					{
						this.GenerateShakeScaleTargets();
						this.apply = new iTween.ApplyTween(this.ApplyShakeScaleTargets);
						return;
					}
					if (!(text == "rotation"))
					{
						return;
					}
					this.GenerateShakeRotationTargets();
					this.apply = new iTween.ApplyTween(this.ApplyShakeRotationTargets);
					return;
				}
				else
				{
					if (!(text == "rotate"))
					{
						return;
					}
					text = this.method;
					if (text == "to")
					{
						this.GenerateRotateToTargets();
						this.apply = new iTween.ApplyTween(this.ApplyRotateToTargets);
						return;
					}
					if (text == "add")
					{
						this.GenerateRotateAddTargets();
						this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
						return;
					}
					if (!(text == "by"))
					{
						return;
					}
					this.GenerateRotateByTargets();
					this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
					return;
				}
			}
			else if (num != 3764468121U)
			{
				if (num != 3778758817U)
				{
					if (num != 3874444950U)
					{
						return;
					}
					if (!(text == "look"))
					{
						return;
					}
					text = this.method;
					if (text == "to")
					{
						this.GenerateLookToTargets();
						this.apply = new iTween.ApplyTween(this.ApplyLookToTargets);
						return;
					}
				}
				else
				{
					if (!(text == "stab"))
					{
						return;
					}
					this.GenerateStabTargets();
					this.apply = new iTween.ApplyTween(this.ApplyStabTargets);
				}
			}
			else
			{
				if (!(text == "audio"))
				{
					return;
				}
				text = this.method;
				if (text == "to")
				{
					this.GenerateAudioToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyAudioToTargets);
					return;
				}
			}
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x002BB4A4 File Offset: 0x002B96A4
		private void GenerateRectTargets()
		{
			this.rects = new Rect[3];
			this.rects[0] = (Rect)this.tweenArguments["from"];
			this.rects[1] = (Rect)this.tweenArguments["to"];
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x002BB500 File Offset: 0x002B9700
		private void GenerateColorTargets()
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (Color)this.tweenArguments["from"];
			this.colors[0, 1] = (Color)this.tweenArguments["to"];
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x002BB560 File Offset: 0x002B9760
		private void GenerateVector3Targets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (Vector3)this.tweenArguments["from"];
			this.vector3s[1] = (Vector3)this.tweenArguments["to"];
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x002BB610 File Offset: 0x002B9810
		private void GenerateVector2Targets()
		{
			this.vector2s = new Vector2[3];
			this.vector2s[0] = (Vector2)this.tweenArguments["from"];
			this.vector2s[1] = (Vector2)this.tweenArguments["to"];
			if (this.tweenArguments.Contains("speed"))
			{
				Vector3 vector = new Vector3(this.vector2s[0].x, this.vector2s[0].y, 0f);
				Vector3 vector2;
				vector2..ctor(this.vector2s[1].x, this.vector2s[1].y, 0f);
				float num = Math.Abs(Vector3.Distance(vector, vector2));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x002BB704 File Offset: 0x002B9904
		private void GenerateFloatTargets()
		{
			this.floats = new float[3];
			this.floats[0] = (float)this.tweenArguments["from"];
			this.floats[1] = (float)this.tweenArguments["to"];
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(this.floats[0] - this.floats[1]);
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x002BB7A0 File Offset: 0x002B99A0
		private void GenerateColorToTargets()
		{
			if (base.GetComponent<Renderer>())
			{
				this.colors = new Color[base.GetComponent<Renderer>().materials.Length, 3];
				for (int i = 0; i < base.GetComponent<Renderer>().materials.Length; i++)
				{
					this.colors[i, 0] = base.GetComponent<Renderer>().materials[i].GetColor(this.namedcolorvalue.ToString());
					this.colors[i, 1] = base.GetComponent<Renderer>().materials[i].GetColor(this.namedcolorvalue.ToString());
				}
			}
			else if (base.GetComponent<Light>())
			{
				this.colors = new Color[1, 3];
				this.colors[0, 0] = (this.colors[0, 1] = base.GetComponent<Light>().color);
			}
			else
			{
				this.colors = new Color[1, 3];
			}
			if (this.tweenArguments.Contains("color"))
			{
				for (int j = 0; j < this.colors.GetLength(0); j++)
				{
					this.colors[j, 1] = (Color)this.tweenArguments["color"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("r"))
				{
					for (int k = 0; k < this.colors.GetLength(0); k++)
					{
						this.colors[k, 1].r = (float)this.tweenArguments["r"];
					}
				}
				if (this.tweenArguments.Contains("g"))
				{
					for (int l = 0; l < this.colors.GetLength(0); l++)
					{
						this.colors[l, 1].g = (float)this.tweenArguments["g"];
					}
				}
				if (this.tweenArguments.Contains("b"))
				{
					for (int m = 0; m < this.colors.GetLength(0); m++)
					{
						this.colors[m, 1].b = (float)this.tweenArguments["b"];
					}
				}
				if (this.tweenArguments.Contains("a"))
				{
					for (int n = 0; n < this.colors.GetLength(0); n++)
					{
						this.colors[n, 1].a = (float)this.tweenArguments["a"];
					}
				}
			}
			if (this.tweenArguments.Contains("amount"))
			{
				for (int num = 0; num < this.colors.GetLength(0); num++)
				{
					this.colors[num, 1].a = (float)this.tweenArguments["amount"];
				}
				return;
			}
			if (this.tweenArguments.Contains("alpha"))
			{
				for (int num2 = 0; num2 < this.colors.GetLength(0); num2++)
				{
					this.colors[num2, 1].a = (float)this.tweenArguments["alpha"];
				}
			}
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x002BBAEC File Offset: 0x002B9CEC
		private void GenerateAudioToTargets()
		{
			this.vector2s = new Vector2[3];
			if (this.tweenArguments.Contains("audiosource"))
			{
				this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
			}
			else if (base.GetComponent<AudioSource>())
			{
				this.audioSource = base.GetComponent<AudioSource>();
			}
			else
			{
				Debug.LogError("iTween Error: AudioTo requires an AudioSource.");
				this.Dispose();
			}
			this.vector2s[0] = (this.vector2s[1] = new Vector2(this.audioSource.volume, this.audioSource.pitch));
			if (this.tweenArguments.Contains("volume"))
			{
				this.vector2s[1].x = (float)this.tweenArguments["volume"];
			}
			if (this.tweenArguments.Contains("pitch"))
			{
				this.vector2s[1].y = (float)this.tweenArguments["pitch"];
			}
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x002BBC08 File Offset: 0x002B9E08
		private void GenerateStabTargets()
		{
			if (this.tweenArguments.Contains("audiosource"))
			{
				this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
			}
			else if (base.GetComponent<AudioSource>())
			{
				this.audioSource = base.GetComponent<AudioSource>();
			}
			else
			{
				base.gameObject.AddComponent<AudioSource>();
				this.audioSource = base.GetComponent<AudioSource>();
				this.audioSource.playOnAwake = false;
			}
			this.audioSource.clip = (AudioClip)this.tweenArguments["audioclip"];
			if (this.tweenArguments.Contains("pitch"))
			{
				this.audioSource.pitch = (float)this.tweenArguments["pitch"];
			}
			if (this.tweenArguments.Contains("volume"))
			{
				this.audioSource.volume = (float)this.tweenArguments["volume"];
			}
			this.time = this.audioSource.clip.length / this.audioSource.pitch;
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x002BBD2C File Offset: 0x002B9F2C
		private void GenerateLookToTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = this.thisTransform.eulerAngles;
			if (this.tweenArguments.Contains("looktarget"))
			{
				if (this.tweenArguments["looktarget"].GetType() == typeof(Transform))
				{
					this.thisTransform.LookAt((Transform)this.tweenArguments["looktarget"], ((Vector3?)this.tweenArguments["up"]) ?? iTween.Defaults.up);
				}
				else if (this.tweenArguments["looktarget"].GetType() == typeof(Vector3))
				{
					this.thisTransform.LookAt((Vector3)this.tweenArguments["looktarget"], ((Vector3?)this.tweenArguments["up"]) ?? iTween.Defaults.up);
				}
			}
			else
			{
				Debug.LogError("iTween Error: LookTo needs a 'looktarget' property!");
				this.Dispose();
			}
			this.vector3s[1] = this.thisTransform.eulerAngles;
			this.thisTransform.eulerAngles = this.vector3s[0];
			if (this.tweenArguments.Contains("axis"))
			{
				string a = (string)this.tweenArguments["axis"];
				if (!(a == "x"))
				{
					if (!(a == "y"))
					{
						if (a == "z")
						{
							this.vector3s[1].x = this.vector3s[0].x;
							this.vector3s[1].y = this.vector3s[0].y;
						}
					}
					else
					{
						this.vector3s[1].x = this.vector3s[0].x;
						this.vector3s[1].z = this.vector3s[0].z;
					}
				}
				else
				{
					this.vector3s[1].y = this.vector3s[0].y;
					this.vector3s[1].z = this.vector3s[0].z;
				}
			}
			this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x002BC0B4 File Offset: 0x002BA2B4
		private void GenerateMoveToPathTargets()
		{
			Vector3[] array2;
			if (this.tweenArguments["path"].GetType() == typeof(Vector3[]))
			{
				Vector3[] array = (Vector3[])this.tweenArguments["path"];
				if (array.Length == 1)
				{
					Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
					this.Dispose();
				}
				array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
			}
			else
			{
				Transform[] array3 = (Transform[])this.tweenArguments["path"];
				if (array3.Length == 1)
				{
					Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
					this.Dispose();
				}
				array2 = new Vector3[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array2[i] = array3[i].position;
				}
			}
			bool flag;
			int num;
			if (this.thisTransform.position != array2[0])
			{
				if (!this.tweenArguments.Contains("movetopath") || (bool)this.tweenArguments["movetopath"])
				{
					flag = true;
					num = 3;
				}
				else
				{
					flag = false;
					num = 2;
				}
			}
			else
			{
				flag = false;
				num = 2;
			}
			this.vector3s = new Vector3[array2.Length + num];
			if (flag)
			{
				this.vector3s[1] = this.thisTransform.position;
				num = 2;
			}
			else
			{
				num = 1;
			}
			Array.Copy(array2, 0, this.vector3s, num, array2.Length);
			this.vector3s[0] = this.vector3s[1] + (this.vector3s[1] - this.vector3s[2]);
			this.vector3s[this.vector3s.Length - 1] = this.vector3s[this.vector3s.Length - 2] + (this.vector3s[this.vector3s.Length - 2] - this.vector3s[this.vector3s.Length - 3]);
			if (this.vector3s[1] == this.vector3s[this.vector3s.Length - 2])
			{
				Vector3[] array4 = new Vector3[this.vector3s.Length];
				Array.Copy(this.vector3s, array4, this.vector3s.Length);
				array4[0] = array4[array4.Length - 3];
				array4[array4.Length - 1] = array4[2];
				this.vector3s = new Vector3[array4.Length];
				Array.Copy(array4, this.vector3s, array4.Length);
			}
			this.path = new iTween.CRSpline(this.vector3s);
			if (this.tweenArguments.Contains("speed"))
			{
				float num2 = iTween.PathLength(this.vector3s);
				this.time = num2 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x002BC394 File Offset: 0x002BA594
		private void GenerateMoveToTargets()
		{
			this.vector3s = new Vector3[3];
			if (this.isLocal)
			{
				this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localPosition);
			}
			else
			{
				this.vector3s[0] = (this.vector3s[1] = this.thisTransform.position);
			}
			if (this.tweenArguments.Contains("position"))
			{
				if (this.tweenArguments["position"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)this.tweenArguments["position"];
					this.vector3s[1] = transform.position;
				}
				else if (this.tweenArguments["position"].GetType() == typeof(Vector3))
				{
					this.vector3s[1] = (Vector3)this.tweenArguments["position"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
			{
				this.tweenArguments["looktarget"] = this.vector3s[1];
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x002BC600 File Offset: 0x002BA800
		private void GenerateMoveByTargets()
		{
			this.vector3s = new Vector3[6];
			this.vector3s[4] = this.thisTransform.eulerAngles;
			this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.position));
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = this.vector3s[0] + (Vector3)this.tweenArguments["amount"];
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = this.vector3s[0].x + (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = this.vector3s[0].y + (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = this.vector3s[0].z + (float)this.tweenArguments["z"];
				}
			}
			this.thisTransform.Translate(this.vector3s[1], this.space);
			this.vector3s[5] = this.thisTransform.position;
			this.thisTransform.position = this.vector3s[0];
			if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
			{
				this.tweenArguments["looktarget"] = this.vector3s[1];
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x002BC874 File Offset: 0x002BAA74
		private void GenerateScaleToTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localScale);
			if (this.tweenArguments.Contains("scale"))
			{
				if (this.tweenArguments["scale"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)this.tweenArguments["scale"];
					this.vector3s[1] = transform.localScale;
				}
				else if (this.tweenArguments["scale"].GetType() == typeof(Vector3))
				{
					this.vector3s[1] = (Vector3)this.tweenArguments["scale"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A6F RID: 31343 RVA: 0x002BCA64 File Offset: 0x002BAC64
		private void GenerateScaleByTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localScale);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = Vector3.Scale(this.vector3s[1], (Vector3)this.tweenArguments["amount"]);
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x * (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y * (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z * (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x002BCBF4 File Offset: 0x002BADF4
		private void GenerateScaleAddTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localScale);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x + (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x002BCD84 File Offset: 0x002BAF84
		private void GenerateRotateToTargets()
		{
			this.vector3s = new Vector3[3];
			if (this.isLocal)
			{
				this.vector3s[0] = (this.vector3s[1] = this.thisTransform.localEulerAngles);
			}
			else
			{
				this.vector3s[0] = (this.vector3s[1] = this.thisTransform.eulerAngles);
			}
			if (this.tweenArguments.Contains("rotation"))
			{
				if (this.tweenArguments["rotation"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)this.tweenArguments["rotation"];
					this.vector3s[1] = transform.eulerAngles;
				}
				else if (this.tweenArguments["rotation"].GetType() == typeof(Vector3))
				{
					this.vector3s[1] = (Vector3)this.tweenArguments["rotation"];
				}
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					this.vector3s[1].x = (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					this.vector3s[1].y = (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					this.vector3s[1].z = (float)this.tweenArguments["z"];
				}
			}
			this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
			if (this.tweenArguments.Contains("speed"))
			{
				float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x002BD03C File Offset: 0x002BB23C
		private void GenerateRotateAddTargets()
		{
			this.vector3s = new Vector3[5];
			this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.eulerAngles));
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x + (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x002BD1D8 File Offset: 0x002BB3D8
		private void GenerateRotateByTargets()
		{
			this.vector3s = new Vector3[4];
			this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.thisTransform.eulerAngles));
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] += Vector3.Scale((Vector3)this.tweenArguments["amount"], new Vector3(360f, 360f, 360f));
			}
			else
			{
				if (this.tweenArguments.Contains("x"))
				{
					Vector3[] array = this.vector3s;
					int num = 1;
					array[num].x = array[num].x + 360f * (float)this.tweenArguments["x"];
				}
				if (this.tweenArguments.Contains("y"))
				{
					Vector3[] array2 = this.vector3s;
					int num2 = 1;
					array2[num2].y = array2[num2].y + 360f * (float)this.tweenArguments["y"];
				}
				if (this.tweenArguments.Contains("z"))
				{
					Vector3[] array3 = this.vector3s;
					int num3 = 1;
					array3[num3].z = array3[num3].z + 360f * (float)this.tweenArguments["z"];
				}
			}
			if (this.tweenArguments.Contains("speed"))
			{
				float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
				this.time = num4 / (float)this.tweenArguments["speed"];
			}
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x002BD3A0 File Offset: 0x002BB5A0
		private void GenerateShakePositionTargets()
		{
			this.vector3s = new Vector3[4];
			this.vector3s[3] = this.thisTransform.eulerAngles;
			this.vector3s[0] = this.thisTransform.position;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
				return;
			}
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x002BD4C4 File Offset: 0x002BB6C4
		private void GenerateShakeScaleTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = this.thisTransform.localScale;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
				return;
			}
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x002BD5D0 File Offset: 0x002BB7D0
		private void GenerateShakeRotationTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = this.thisTransform.eulerAngles;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
				return;
			}
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x002BD6DC File Offset: 0x002BB8DC
		private void GeneratePunchPositionTargets()
		{
			this.vector3s = new Vector3[5];
			this.vector3s[4] = this.thisTransform.eulerAngles;
			this.vector3s[0] = this.thisTransform.position;
			this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
				return;
			}
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x002BD820 File Offset: 0x002BBA20
		private void GeneratePunchRotationTargets()
		{
			this.vector3s = new Vector3[4];
			this.vector3s[0] = this.thisTransform.eulerAngles;
			this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
				return;
			}
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x002BD94C File Offset: 0x002BBB4C
		private void GeneratePunchScaleTargets()
		{
			this.vector3s = new Vector3[3];
			this.vector3s[0] = this.thisTransform.localScale;
			this.vector3s[1] = Vector3.zero;
			if (this.tweenArguments.Contains("amount"))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
				return;
			}
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x002BDA6C File Offset: 0x002BBC6C
		private void ApplyRectTargets()
		{
			this.rects[2].x = this.ease(this.rects[0].x, this.rects[1].x, this.percentage);
			this.rects[2].y = this.ease(this.rects[0].y, this.rects[1].y, this.percentage);
			this.rects[2].width = this.ease(this.rects[0].width, this.rects[1].width, this.percentage);
			this.rects[2].height = this.ease(this.rects[0].height, this.rects[1].height, this.percentage);
			this.tweenArguments["onupdateparams"] = this.rects[2];
			if (this.percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.rects[1];
			}
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x002BDBD8 File Offset: 0x002BBDD8
		private void ApplyColorTargets()
		{
			this.colors[0, 2].r = this.ease(this.colors[0, 0].r, this.colors[0, 1].r, this.percentage);
			this.colors[0, 2].g = this.ease(this.colors[0, 0].g, this.colors[0, 1].g, this.percentage);
			this.colors[0, 2].b = this.ease(this.colors[0, 0].b, this.colors[0, 1].b, this.percentage);
			this.colors[0, 2].a = this.ease(this.colors[0, 0].a, this.colors[0, 1].a, this.percentage);
			this.tweenArguments["onupdateparams"] = this.colors[0, 2];
			if (this.percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.colors[0, 1];
			}
		}

		// Token: 0x06007A7C RID: 31356 RVA: 0x002BDD54 File Offset: 0x002BBF54
		private void ApplyVector3Targets()
		{
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			this.tweenArguments["onupdateparams"] = this.vector3s[2];
			if (this.percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.vector3s[1];
			}
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x002BDE7C File Offset: 0x002BC07C
		private void ApplyVector2Targets()
		{
			this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
			this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
			this.tweenArguments["onupdateparams"] = this.vector2s[2];
			if (this.percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.vector2s[1];
			}
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x002BDF60 File Offset: 0x002BC160
		private void ApplyFloatTargets()
		{
			this.floats[2] = this.ease(this.floats[0], this.floats[1], this.percentage);
			this.tweenArguments["onupdateparams"] = this.floats[2];
			if (this.percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.floats[1];
			}
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x002BDFE0 File Offset: 0x002BC1E0
		private void ApplyColorToTargets()
		{
			for (int i = 0; i < this.colors.GetLength(0); i++)
			{
				this.colors[i, 2].r = this.ease(this.colors[i, 0].r, this.colors[i, 1].r, this.percentage);
				this.colors[i, 2].g = this.ease(this.colors[i, 0].g, this.colors[i, 1].g, this.percentage);
				this.colors[i, 2].b = this.ease(this.colors[i, 0].b, this.colors[i, 1].b, this.percentage);
				this.colors[i, 2].a = this.ease(this.colors[i, 0].a, this.colors[i, 1].a, this.percentage);
			}
			if (base.GetComponent<Renderer>())
			{
				for (int j = 0; j < this.colors.GetLength(0); j++)
				{
					base.GetComponent<Renderer>().materials[j].SetColor(this.namedcolorvalue.ToString(), this.colors[j, 2]);
				}
			}
			else if (base.GetComponent<Light>())
			{
				base.GetComponent<Light>().color = this.colors[0, 2];
			}
			if (this.percentage == 1f)
			{
				if (base.GetComponent<Renderer>())
				{
					for (int k = 0; k < this.colors.GetLength(0); k++)
					{
						base.GetComponent<Renderer>().materials[k].SetColor(this.namedcolorvalue.ToString(), this.colors[k, 1]);
					}
					return;
				}
				if (base.GetComponent<Light>())
				{
					base.GetComponent<Light>().color = this.colors[0, 1];
				}
			}
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x002BE228 File Offset: 0x002BC428
		private void ApplyAudioToTargets()
		{
			this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
			this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
			this.audioSource.volume = this.vector2s[2].x;
			this.audioSource.pitch = this.vector2s[2].y;
			if (this.percentage == 1f)
			{
				this.audioSource.volume = this.vector2s[1].x;
				this.audioSource.pitch = this.vector2s[1].y;
			}
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x000042DD File Offset: 0x000024DD
		private void ApplyStabTargets()
		{
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x002BE33C File Offset: 0x002BC53C
		private void ApplyMoveToPathTargets()
		{
			this.preUpdate = this.thisTransform.position;
			float num = this.ease(0f, 1f, this.percentage);
			if (this.isLocal)
			{
				this.thisTransform.localPosition = this.path.Interp(Mathf.Clamp(num, 0f, 1f));
			}
			else
			{
				this.thisTransform.position = this.path.Interp(Mathf.Clamp(num, 0f, 1f));
			}
			if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
			{
				float num2;
				if (this.tweenArguments.Contains("lookahead"))
				{
					num2 = (float)this.tweenArguments["lookahead"];
				}
				else
				{
					num2 = iTween.Defaults.lookAhead;
				}
				float num3 = this.ease(0f, 1f, Mathf.Min(1f, this.percentage + num2));
				this.tweenArguments["looktarget"] = this.path.Interp(Mathf.Clamp(num3, 0f, 1f));
			}
			this.postUpdate = this.thisTransform.position;
			if (this.physics)
			{
				this.thisTransform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x002BE4C0 File Offset: 0x002BC6C0
		private void ApplyMoveToTargets()
		{
			this.preUpdate = this.thisTransform.position;
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			if (this.isLocal)
			{
				this.thisTransform.localPosition = this.vector3s[2];
			}
			else
			{
				this.thisTransform.position = this.vector3s[2];
			}
			if (this.percentage == 1f)
			{
				if (this.isLocal)
				{
					this.thisTransform.localPosition = this.vector3s[1];
				}
				else
				{
					this.thisTransform.position = this.vector3s[1];
				}
			}
			this.postUpdate = this.thisTransform.position;
			if (this.physics)
			{
				this.thisTransform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06007A84 RID: 31364 RVA: 0x002BE664 File Offset: 0x002BC864
		private void ApplyMoveByTargets()
		{
			this.preUpdate = this.thisTransform.position;
			Vector3 eulerAngles = default(Vector3);
			if (this.tweenArguments.Contains("looktarget"))
			{
				eulerAngles = this.thisTransform.eulerAngles;
				this.thisTransform.eulerAngles = this.vector3s[4];
			}
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			this.thisTransform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			if (this.tweenArguments.Contains("looktarget"))
			{
				this.thisTransform.eulerAngles = eulerAngles;
			}
			this.postUpdate = this.thisTransform.position;
			if (this.physics)
			{
				this.thisTransform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06007A85 RID: 31365 RVA: 0x002BE82C File Offset: 0x002BCA2C
		private void ApplyScaleToTargets()
		{
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			this.thisTransform.localScale = this.vector3s[2];
			if (this.percentage == 1f)
			{
				this.thisTransform.localScale = this.vector3s[1];
			}
		}

		// Token: 0x06007A86 RID: 31366 RVA: 0x002BE940 File Offset: 0x002BCB40
		private void ApplyLookToTargets()
		{
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			if (this.isLocal)
			{
				this.thisTransform.localRotation = Quaternion.Euler(this.vector3s[2]);
				return;
			}
			this.thisTransform.rotation = Quaternion.Euler(this.vector3s[2]);
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x002BEA5C File Offset: 0x002BCC5C
		private void ApplyRotateToTargets()
		{
			this.preUpdate = this.thisTransform.eulerAngles;
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			if (this.isLocal)
			{
				this.thisTransform.localRotation = Quaternion.Euler(this.vector3s[2]);
			}
			else
			{
				this.thisTransform.rotation = Quaternion.Euler(this.vector3s[2]);
			}
			if (this.percentage == 1f)
			{
				if (this.isLocal)
				{
					this.thisTransform.localRotation = Quaternion.Euler(this.vector3s[1]);
				}
				else
				{
					this.thisTransform.rotation = Quaternion.Euler(this.vector3s[1]);
				}
			}
			this.postUpdate = this.thisTransform.eulerAngles;
			if (this.physics)
			{
				this.thisTransform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x002BEC18 File Offset: 0x002BCE18
		private void ApplyRotateAddTargets()
		{
			this.preUpdate = this.thisTransform.eulerAngles;
			this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
			this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
			this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
			this.thisTransform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			this.postUpdate = this.thisTransform.eulerAngles;
			if (this.physics)
			{
				this.thisTransform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06007A89 RID: 31369 RVA: 0x002BED88 File Offset: 0x002BCF88
		private void ApplyShakePositionTargets()
		{
			if (this.isLocal)
			{
				this.preUpdate = this.thisTransform.localPosition;
			}
			else
			{
				this.preUpdate = this.thisTransform.position;
			}
			Vector3 eulerAngles = default(Vector3);
			if (this.tweenArguments.Contains("looktarget"))
			{
				eulerAngles = this.thisTransform.eulerAngles;
				this.thisTransform.eulerAngles = this.vector3s[3];
			}
			if (this.percentage == 0f)
			{
				this.thisTransform.Translate(this.vector3s[1], this.space);
			}
			if (this.isLocal)
			{
				this.thisTransform.localPosition = this.vector3s[0];
			}
			else
			{
				this.thisTransform.position = this.vector3s[0];
			}
			float num = 1f - this.percentage;
			this.vector3s[2].x = Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
			this.vector3s[2].y = Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
			this.vector3s[2].z = Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
			if (this.isLocal)
			{
				this.thisTransform.localPosition += this.vector3s[2];
			}
			else
			{
				this.thisTransform.position += this.vector3s[2];
			}
			if (this.tweenArguments.Contains("looktarget"))
			{
				this.thisTransform.eulerAngles = eulerAngles;
			}
			this.postUpdate = this.thisTransform.position;
			if (this.physics)
			{
				this.thisTransform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06007A8A RID: 31370 RVA: 0x002BEFCC File Offset: 0x002BD1CC
		private void ApplyShakeScaleTargets()
		{
			if (this.percentage == 0f)
			{
				this.thisTransform.localScale = this.vector3s[1];
			}
			this.thisTransform.localScale = this.vector3s[0];
			float num = 1f - this.percentage;
			this.vector3s[2].x = Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
			this.vector3s[2].y = Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
			this.vector3s[2].z = Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
			this.thisTransform.localScale += this.vector3s[2];
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x002BF0FC File Offset: 0x002BD2FC
		private void ApplyShakeRotationTargets()
		{
			this.preUpdate = this.thisTransform.eulerAngles;
			if (this.percentage == 0f)
			{
				this.thisTransform.Rotate(this.vector3s[1], this.space);
			}
			this.thisTransform.eulerAngles = this.vector3s[0];
			float num = 1f - this.percentage;
			this.vector3s[2].x = Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
			this.vector3s[2].y = Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
			this.vector3s[2].z = Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
			this.thisTransform.Rotate(this.vector3s[2], this.space);
			this.postUpdate = this.thisTransform.eulerAngles;
			if (this.physics)
			{
				this.thisTransform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x002BF27C File Offset: 0x002BD47C
		private void ApplyPunchPositionTargets()
		{
			this.preUpdate = this.thisTransform.position;
			Vector3 eulerAngles = default(Vector3);
			if (this.tweenArguments.Contains("looktarget"))
			{
				eulerAngles = this.thisTransform.eulerAngles;
				this.thisTransform.eulerAngles = this.vector3s[4];
			}
			if (this.vector3s[1].x > 0f)
			{
				this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
			}
			else if (this.vector3s[1].x < 0f)
			{
				this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
			}
			if (this.vector3s[1].y > 0f)
			{
				this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
			}
			else if (this.vector3s[1].y < 0f)
			{
				this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
			}
			if (this.vector3s[1].z > 0f)
			{
				this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
			}
			else if (this.vector3s[1].z < 0f)
			{
				this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
			}
			this.thisTransform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			if (this.tweenArguments.Contains("looktarget"))
			{
				this.thisTransform.eulerAngles = eulerAngles;
			}
			this.postUpdate = this.thisTransform.position;
			if (this.physics)
			{
				this.thisTransform.position = this.preUpdate;
				base.GetComponent<Rigidbody>().MovePosition(this.postUpdate);
			}
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x002BF534 File Offset: 0x002BD734
		private void ApplyPunchRotationTargets()
		{
			this.preUpdate = this.thisTransform.eulerAngles;
			if (this.vector3s[1].x > 0f)
			{
				this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
			}
			else if (this.vector3s[1].x < 0f)
			{
				this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
			}
			if (this.vector3s[1].y > 0f)
			{
				this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
			}
			else if (this.vector3s[1].y < 0f)
			{
				this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
			}
			if (this.vector3s[1].z > 0f)
			{
				this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
			}
			else if (this.vector3s[1].z < 0f)
			{
				this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
			}
			this.thisTransform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
			this.vector3s[3] = this.vector3s[2];
			this.postUpdate = this.thisTransform.eulerAngles;
			if (this.physics)
			{
				this.thisTransform.eulerAngles = this.preUpdate;
				base.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(this.postUpdate));
			}
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x002BF794 File Offset: 0x002BD994
		private void ApplyPunchScaleTargets()
		{
			if (this.vector3s[1].x > 0f)
			{
				this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
			}
			else if (this.vector3s[1].x < 0f)
			{
				this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
			}
			if (this.vector3s[1].y > 0f)
			{
				this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
			}
			else if (this.vector3s[1].y < 0f)
			{
				this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
			}
			if (this.vector3s[1].z > 0f)
			{
				this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
			}
			else if (this.vector3s[1].z < 0f)
			{
				this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
			}
			this.thisTransform.localScale = this.vector3s[0] + this.vector3s[2];
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x00053C82 File Offset: 0x00051E82
		private IEnumerator TweenDelay()
		{
			this.delayStarted = Time.time;
			yield return new WaitForSeconds(this.delay);
			if (this.wasPaused)
			{
				this.wasPaused = false;
				this.TweenStart();
			}
			yield break;
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x002BF988 File Offset: 0x002BDB88
		private void TweenStart()
		{
			this.CallBack("onstart");
			if (!this.loop)
			{
				this.ConflictCheck();
				this.GenerateTargets();
			}
			if (this.type == "stab")
			{
				this.audioSource.PlayOneShot(this.audioSource.clip);
			}
			if (this.type == "move" || this.type == "scale" || this.type == "rotate" || this.type == "punch" || this.type == "shake" || this.type == "curve" || this.type == "look")
			{
				this.EnableKinematic();
			}
			this.isRunning = true;
		}

		// Token: 0x06007A91 RID: 31377 RVA: 0x00053C91 File Offset: 0x00051E91
		private IEnumerator TweenRestart()
		{
			if (this.delay > 0f)
			{
				this.delayStarted = Time.time;
				yield return new WaitForSeconds(this.delay);
			}
			this.loop = true;
			this.TweenStart();
			yield break;
		}

		// Token: 0x06007A92 RID: 31378 RVA: 0x00053CA0 File Offset: 0x00051EA0
		private void TweenUpdate()
		{
			this.apply();
			this.CallBack("onupdate");
			this.UpdatePercentage();
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x002BFA68 File Offset: 0x002BDC68
		private void TweenComplete()
		{
			this.isRunning = false;
			if (this.percentage > 0.5f)
			{
				this.percentage = 1f;
			}
			else
			{
				this.percentage = 0f;
			}
			this.apply();
			if (this.type == "value")
			{
				this.CallBack("onupdate");
			}
			if (this.loopType == iTween.LoopType.none)
			{
				this.Dispose();
			}
			else
			{
				this.TweenLoop();
			}
			this.CallBack("oncomplete");
		}

		// Token: 0x06007A94 RID: 31380 RVA: 0x002BFAEC File Offset: 0x002BDCEC
		private void TweenLoop()
		{
			this.DisableKinematic();
			iTween.LoopType loopType = this.loopType;
			if (loopType == iTween.LoopType.loop)
			{
				this.percentage = 0f;
				this.runningTime = 0f;
				this.apply();
				base.StartCoroutine("TweenRestart");
				return;
			}
			if (loopType != iTween.LoopType.pingPong)
			{
				return;
			}
			this.reverse = !this.reverse;
			this.runningTime = 0f;
			base.StartCoroutine("TweenRestart");
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x002BFB64 File Offset: 0x002BDD64
		public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
		{
			return new Rect(iTween.FloatUpdate(currentValue.x, targetValue.x, speed), iTween.FloatUpdate(currentValue.y, targetValue.y, speed), iTween.FloatUpdate(currentValue.width, targetValue.width, speed), iTween.FloatUpdate(currentValue.height, targetValue.height, speed));
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x002BFBC8 File Offset: 0x002BDDC8
		public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
		{
			Vector3 vector = targetValue - currentValue;
			currentValue += vector * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x002BFBF8 File Offset: 0x002BDDF8
		public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
		{
			Vector2 vector = targetValue - currentValue;
			currentValue += vector * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x002BFC28 File Offset: 0x002BDE28
		public static float FloatUpdate(float currentValue, float targetValue, float speed)
		{
			float num = targetValue - currentValue;
			currentValue += num * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x00053CBE File Offset: 0x00051EBE
		public static void FadeUpdate(GameObject target, Hashtable args)
		{
			args["a"] = args["alpha"];
			iTween.ColorUpdate(target, args);
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x00053CDD File Offset: 0x00051EDD
		public static void FadeUpdate(GameObject target, float alpha, float time)
		{
			iTween.FadeUpdate(target, iTween.Hash(new object[]
			{
				"alpha",
				alpha,
				"time",
				time
			}));
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x002BFC48 File Offset: 0x002BDE48
		public static void ColorUpdate(GameObject target, Hashtable args)
		{
			iTween.CleanArgs(args);
			Color[] array = new Color[4];
			if (!args.Contains("includechildren") || (bool)args["includechildren"])
			{
				foreach (object obj in target.transform)
				{
					iTween.ColorUpdate(((Transform)obj).gameObject, args);
				}
			}
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= iTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = iTween.Defaults.updateTime;
			}
			if (target.GetComponent<Renderer>())
			{
				array[0] = (array[1] = target.GetComponent<Renderer>().material.color);
			}
			else if (target.GetComponent<Light>())
			{
				array[0] = (array[1] = target.GetComponent<Light>().color);
			}
			if (args.Contains("color"))
			{
				array[1] = (Color)args["color"];
			}
			else
			{
				if (args.Contains("r"))
				{
					array[1].r = (float)args["r"];
				}
				if (args.Contains("g"))
				{
					array[1].g = (float)args["g"];
				}
				if (args.Contains("b"))
				{
					array[1].b = (float)args["b"];
				}
				if (args.Contains("a"))
				{
					array[1].a = (float)args["a"];
				}
			}
			array[3].r = Mathf.SmoothDamp(array[0].r, array[1].r, ref array[2].r, num);
			array[3].g = Mathf.SmoothDamp(array[0].g, array[1].g, ref array[2].g, num);
			array[3].b = Mathf.SmoothDamp(array[0].b, array[1].b, ref array[2].b, num);
			array[3].a = Mathf.SmoothDamp(array[0].a, array[1].a, ref array[2].a, num);
			if (target.GetComponent<Renderer>())
			{
				target.GetComponent<Renderer>().material.color = array[3];
				return;
			}
			if (target.GetComponent<Light>())
			{
				target.GetComponent<Light>().color = array[3];
			}
		}

		// Token: 0x06007A9C RID: 31388 RVA: 0x00053D12 File Offset: 0x00051F12
		public static void ColorUpdate(GameObject target, Color color, float time)
		{
			iTween.ColorUpdate(target, iTween.Hash(new object[]
			{
				"color",
				color,
				"time",
				time
			}));
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x002BFF48 File Offset: 0x002BE148
		public static void AudioUpdate(GameObject target, Hashtable args)
		{
			iTween.CleanArgs(args);
			Vector2[] array = new Vector2[4];
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= iTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = iTween.Defaults.updateTime;
			}
			AudioSource audioSource;
			if (args.Contains("audiosource"))
			{
				audioSource = (AudioSource)args["audiosource"];
			}
			else
			{
				if (!target.GetComponent<AudioSource>())
				{
					Debug.LogError("iTween Error: AudioUpdate requires an AudioSource.");
					return;
				}
				audioSource = target.GetComponent<AudioSource>();
			}
			array[0] = (array[1] = new Vector2(audioSource.volume, audioSource.pitch));
			if (args.Contains("volume"))
			{
				array[1].x = (float)args["volume"];
			}
			if (args.Contains("pitch"))
			{
				array[1].y = (float)args["pitch"];
			}
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			audioSource.volume = array[3].x;
			audioSource.pitch = array[3].y;
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x002C00D8 File Offset: 0x002BE2D8
		public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
		{
			iTween.AudioUpdate(target, iTween.Hash(new object[]
			{
				"volume",
				volume,
				"pitch",
				pitch,
				"time",
				time
			}));
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x002C012C File Offset: 0x002BE32C
		public static void RotateUpdate(GameObject target, Hashtable args)
		{
			iTween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			Vector3 eulerAngles = target.transform.eulerAngles;
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= iTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = iTween.Defaults.updateTime;
			}
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = iTween.Defaults.isLocal;
			}
			if (flag)
			{
				array[0] = target.transform.localEulerAngles;
			}
			else
			{
				array[0] = target.transform.eulerAngles;
			}
			if (args.Contains("rotation"))
			{
				if (args["rotation"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["rotation"];
					array[1] = transform.eulerAngles;
				}
				else if (args["rotation"].GetType() == typeof(Vector3))
				{
					array[1] = (Vector3)args["rotation"];
				}
			}
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
			if (flag)
			{
				target.transform.localEulerAngles = array[3];
			}
			else
			{
				target.transform.eulerAngles = array[3];
			}
			if (target.GetComponent<Rigidbody>() != null)
			{
				Vector3 eulerAngles2 = target.transform.eulerAngles;
				target.transform.eulerAngles = eulerAngles;
				target.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(eulerAngles2));
			}
		}

		// Token: 0x06007AA0 RID: 31392 RVA: 0x00053D47 File Offset: 0x00051F47
		public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
		{
			iTween.RotateUpdate(target, iTween.Hash(new object[]
			{
				"rotation",
				rotation,
				"time",
				time
			}));
		}

		// Token: 0x06007AA1 RID: 31393 RVA: 0x002C035C File Offset: 0x002BE55C
		public static void ScaleUpdate(GameObject target, Hashtable args)
		{
			iTween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= iTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = iTween.Defaults.updateTime;
			}
			array[0] = (array[1] = target.transform.localScale);
			if (args.Contains("scale"))
			{
				if (args["scale"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["scale"];
					array[1] = transform.localScale;
				}
				else if (args["scale"].GetType() == typeof(Vector3))
				{
					array[1] = (Vector3)args["scale"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					array[1].x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					array[1].y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					array[1].z = (float)args["z"];
				}
			}
			array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
			target.transform.localScale = array[3];
		}

		// Token: 0x06007AA2 RID: 31394 RVA: 0x00053D7C File Offset: 0x00051F7C
		public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
		{
			iTween.ScaleUpdate(target, iTween.Hash(new object[]
			{
				"scale",
				scale,
				"time",
				time
			}));
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x002C0584 File Offset: 0x002BE784
		public static void MoveUpdate(GameObject target, Hashtable args)
		{
			iTween.CleanArgs(args);
			Vector3[] array = new Vector3[4];
			Vector3 position = target.transform.position;
			float num;
			if (args.Contains("time"))
			{
				num = (float)args["time"];
				num *= iTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = iTween.Defaults.updateTime;
			}
			bool flag;
			if (args.Contains("islocal"))
			{
				flag = (bool)args["islocal"];
			}
			else
			{
				flag = iTween.Defaults.isLocal;
			}
			if (flag)
			{
				array[0] = (array[1] = target.transform.localPosition);
			}
			else
			{
				array[0] = (array[1] = target.transform.position);
			}
			if (args.Contains("position"))
			{
				if (args["position"].GetType() == typeof(Transform))
				{
					Transform transform = (Transform)args["position"];
					array[1] = transform.position;
				}
				else if (args["position"].GetType() == typeof(Vector3))
				{
					array[1] = (Vector3)args["position"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					array[1].x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					array[1].y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					array[1].z = (float)args["z"];
				}
			}
			array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
			if (args.Contains("orienttopath") && (bool)args["orienttopath"])
			{
				args["looktarget"] = array[3];
			}
			if (args.Contains("looktarget"))
			{
				iTween.LookUpdate(target, args);
			}
			if (flag)
			{
				target.transform.localPosition = array[3];
			}
			else
			{
				target.transform.position = array[3];
			}
			if (target.GetComponent<Rigidbody>() != null)
			{
				Vector3 position2 = target.transform.position;
				target.transform.position = position;
				target.GetComponent<Rigidbody>().MovePosition(position2);
			}
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x00053DB1 File Offset: 0x00051FB1
		public static void MoveUpdate(GameObject target, Vector3 position, float time)
		{
			iTween.MoveUpdate(target, iTween.Hash(new object[]
			{
				"position",
				position,
				"time",
				time
			}));
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x002C0898 File Offset: 0x002BEA98
		public static void LookUpdate(GameObject target, Hashtable args)
		{
			iTween.CleanArgs(args);
			Vector3[] array = new Vector3[5];
			float num;
			if (args.Contains("looktime"))
			{
				num = (float)args["looktime"];
				num *= iTween.Defaults.updateTimePercentage;
			}
			else if (args.Contains("time"))
			{
				num = (float)args["time"] * 0.15f;
				num *= iTween.Defaults.updateTimePercentage;
			}
			else
			{
				num = iTween.Defaults.updateTime;
			}
			array[0] = target.transform.eulerAngles;
			if (args.Contains("looktarget"))
			{
				if (args["looktarget"].GetType() == typeof(Transform))
				{
					target.transform.LookAt((Transform)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
				}
				else if (args["looktarget"].GetType() == typeof(Vector3))
				{
					target.transform.LookAt((Vector3)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
				}
				array[1] = target.transform.eulerAngles;
				target.transform.eulerAngles = array[0];
				array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
				array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
				array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
				target.transform.eulerAngles = array[3];
				if (args.Contains("axis"))
				{
					array[4] = target.transform.eulerAngles;
					string a = (string)args["axis"];
					if (!(a == "x"))
					{
						if (!(a == "y"))
						{
							if (a == "z")
							{
								array[4].x = array[0].x;
								array[4].y = array[0].y;
							}
						}
						else
						{
							array[4].x = array[0].x;
							array[4].z = array[0].z;
						}
					}
					else
					{
						array[4].y = array[0].y;
						array[4].z = array[0].z;
					}
					target.transform.eulerAngles = array[4];
				}
				return;
			}
			Debug.LogError("iTween Error: LookUpdate needs a 'looktarget' property!");
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x00053DE6 File Offset: 0x00051FE6
		public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
		{
			iTween.LookUpdate(target, iTween.Hash(new object[]
			{
				"looktarget",
				looktarget,
				"time",
				time
			}));
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x002C0BE8 File Offset: 0x002BEDE8
		public static float PathLength(Transform[] path)
		{
			Vector3[] array = new Vector3[path.Length];
			float num = 0f;
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			Vector3[] pts = iTween.PathControlPointGenerator(array);
			Vector3 vector = iTween.Interp(pts, 0f);
			int num2 = path.Length * 20;
			for (int j = 1; j <= num2; j++)
			{
				float t = (float)j / (float)num2;
				Vector3 vector2 = iTween.Interp(pts, t);
				num += Vector3.Distance(vector, vector2);
				vector = vector2;
			}
			return num;
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x002C0C78 File Offset: 0x002BEE78
		public static float PathLength(Vector3[] path)
		{
			float num = 0f;
			Vector3[] pts = iTween.PathControlPointGenerator(path);
			Vector3 vector = iTween.Interp(pts, 0f);
			int num2 = path.Length * 20;
			for (int i = 1; i <= num2; i++)
			{
				float t = (float)i / (float)num2;
				Vector3 vector2 = iTween.Interp(pts, t);
				num += Vector3.Distance(vector, vector2);
				vector = vector2;
			}
			return num;
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x002C0CD8 File Offset: 0x002BEED8
		public static Texture2D CameraTexture(Color color)
		{
			Texture2D texture2D = new Texture2D(Screen.width, Screen.height, 5, false);
			Color[] array = new Color[Screen.width * Screen.height];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = color;
			}
			texture2D.SetPixels(array);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x00053E1B File Offset: 0x0005201B
		public static void PutOnPath(GameObject target, Vector3[] path, float percent)
		{
			target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x00053E34 File Offset: 0x00052034
		public static void PutOnPath(Transform target, Vector3[] path, float percent)
		{
			target.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x002C0D2C File Offset: 0x002BEF2C
		public static void PutOnPath(GameObject target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x002C0D78 File Offset: 0x002BEF78
		public static void PutOnPath(Transform target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x002C0DC0 File Offset: 0x002BEFC0
		public static Vector3 PointOnPath(Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			return iTween.Interp(iTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06007AAF RID: 31407 RVA: 0x00053E48 File Offset: 0x00052048
		public static void DrawLine(Vector3[] line)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007AB0 RID: 31408 RVA: 0x00053E5E File Offset: 0x0005205E
		public static void DrawLine(Vector3[] line, Color color)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06007AB1 RID: 31409 RVA: 0x002C0E00 File Offset: 0x002BF000
		public static void DrawLine(Transform[] line)
		{
			if (line.Length != 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007AB2 RID: 31410 RVA: 0x002C0E48 File Offset: 0x002BF048
		public static void DrawLine(Transform[] line, Color color)
		{
			if (line.Length != 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				iTween.DrawLineHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x00053E48 File Offset: 0x00052048
		public static void DrawLineGizmos(Vector3[] line)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x00053E5E File Offset: 0x0005205E
		public static void DrawLineGizmos(Vector3[] line, Color color)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x002C0E00 File Offset: 0x002BF000
		public static void DrawLineGizmos(Transform[] line)
		{
			if (line.Length != 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x002C0E48 File Offset: 0x002BF048
		public static void DrawLineGizmos(Transform[] line, Color color)
		{
			if (line.Length != 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				iTween.DrawLineHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x00053E70 File Offset: 0x00052070
		public static void DrawLineHandles(Vector3[] line)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, iTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x00053E86 File Offset: 0x00052086
		public static void DrawLineHandles(Vector3[] line, Color color)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, color, "handles");
			}
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x002C0E8C File Offset: 0x002BF08C
		public static void DrawLineHandles(Transform[] line)
		{
			if (line.Length != 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				iTween.DrawLineHelper(array, iTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x002C0ED4 File Offset: 0x002BF0D4
		public static void DrawLineHandles(Transform[] line, Color color)
		{
			if (line.Length != 0)
			{
				Vector3[] array = new Vector3[line.Length];
				for (int i = 0; i < line.Length; i++)
				{
					array[i] = line[i].position;
				}
				iTween.DrawLineHelper(array, color, "handles");
			}
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x00053E98 File Offset: 0x00052098
		public static Vector3 PointOnPath(Vector3[] path, float percent)
		{
			return iTween.Interp(iTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x00053EA6 File Offset: 0x000520A6
		public static void DrawPath(Vector3[] path)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x00053EBC File Offset: 0x000520BC
		public static void DrawPath(Vector3[] path, Color color)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06007ABE RID: 31422 RVA: 0x002C0F18 File Offset: 0x002BF118
		public static void DrawPath(Transform[] path)
		{
			if (path.Length != 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x002C0F60 File Offset: 0x002BF160
		public static void DrawPath(Transform[] path, Color color)
		{
			if (path.Length != 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				iTween.DrawPathHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x00053EA6 File Offset: 0x000520A6
		public static void DrawPathGizmos(Vector3[] path)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x00053EBC File Offset: 0x000520BC
		public static void DrawPathGizmos(Vector3[] path, Color color)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x002C0F18 File Offset: 0x002BF118
		public static void DrawPathGizmos(Transform[] path)
		{
			if (path.Length != 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x002C0F60 File Offset: 0x002BF160
		public static void DrawPathGizmos(Transform[] path, Color color)
		{
			if (path.Length != 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				iTween.DrawPathHelper(array, color, "gizmos");
			}
		}

		// Token: 0x06007AC4 RID: 31428 RVA: 0x00053ECE File Offset: 0x000520CE
		public static void DrawPathHandles(Vector3[] path)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, iTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x00053EE4 File Offset: 0x000520E4
		public static void DrawPathHandles(Vector3[] path, Color color)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, color, "handles");
			}
		}

		// Token: 0x06007AC6 RID: 31430 RVA: 0x002C0FA4 File Offset: 0x002BF1A4
		public static void DrawPathHandles(Transform[] path)
		{
			if (path.Length != 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				iTween.DrawPathHelper(array, iTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x002C0FEC File Offset: 0x002BF1EC
		public static void DrawPathHandles(Transform[] path, Color color)
		{
			if (path.Length != 0)
			{
				Vector3[] array = new Vector3[path.Length];
				for (int i = 0; i < path.Length; i++)
				{
					array[i] = path[i].position;
				}
				iTween.DrawPathHelper(array, color, "handles");
			}
		}

		// Token: 0x06007AC8 RID: 31432 RVA: 0x002C1030 File Offset: 0x002BF230
		public static void CameraFadeDepth(int depth)
		{
			if (iTween.cameraFade)
			{
				iTween.cameraFade.transform.position = new Vector3(iTween.cameraFade.transform.position.x, iTween.cameraFade.transform.position.y, (float)depth);
			}
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x00053EF6 File Offset: 0x000520F6
		public static void CameraFadeDestroy()
		{
			if (iTween.cameraFade)
			{
				Object.Destroy(iTween.cameraFade);
			}
		}

		// Token: 0x06007ACA RID: 31434 RVA: 0x002C1088 File Offset: 0x002BF288
		public static void Resume(GameObject target)
		{
			Component[] array = target.GetComponents<iTween>();
			array = array;
			for (int i = 0; i < array.Length; i++)
			{
				((iTween)array[i]).enabled = true;
			}
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x002C10BC File Offset: 0x002BF2BC
		public static void Resume(GameObject target, bool includechildren)
		{
			iTween.Resume(target);
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.Resume(((Transform)obj).gameObject, true);
				}
			}
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x002C1124 File Offset: 0x002BF324
		public static void Resume(GameObject target, string type)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					iTween.enabled = true;
				}
			}
		}

		// Token: 0x06007ACD RID: 31437 RVA: 0x002C1188 File Offset: 0x002BF388
		public static void Resume(GameObject target, string type, bool includechildren)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					iTween.enabled = true;
				}
			}
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.Resume(((Transform)obj).gameObject, type, true);
				}
			}
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x002C1244 File Offset: 0x002BF444
		public static void Resume()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				iTween.Resume((GameObject)iTween.tweens[i]["target"]);
			}
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x002C1288 File Offset: 0x002BF488
		public static void Resume(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				GameObject value = (GameObject)iTween.tweens[i]["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				iTween.Resume((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x002C12FC File Offset: 0x002BF4FC
		public static void Pause(GameObject target)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if (iTween.delay > 0f)
				{
					iTween.delay -= Time.time - iTween.delayStarted;
					iTween.StopCoroutine("TweenDelay");
				}
				iTween.isPaused = true;
				iTween.enabled = false;
			}
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x002C1368 File Offset: 0x002BF568
		public static void Pause(GameObject target, bool includechildren)
		{
			iTween.Pause(target);
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.Pause(((Transform)obj).gameObject, true);
				}
			}
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		public static void Pause(GameObject target, string type)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					if (iTween.delay > 0f)
					{
						iTween.delay -= Time.time - iTween.delayStarted;
						iTween.StopCoroutine("TweenDelay");
					}
					iTween.isPaused = true;
					iTween.enabled = false;
				}
			}
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x002C1470 File Offset: 0x002BF670
		public static void Pause(GameObject target, string type, bool includechildren)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					if (iTween.delay > 0f)
					{
						iTween.delay -= Time.time - iTween.delayStarted;
						iTween.StopCoroutine("TweenDelay");
					}
					iTween.isPaused = true;
					iTween.enabled = false;
				}
			}
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.Pause(((Transform)obj).gameObject, type, true);
				}
			}
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x002C1564 File Offset: 0x002BF764
		public static void Pause()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				iTween.Pause((GameObject)iTween.tweens[i]["target"]);
			}
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x002C15A8 File Offset: 0x002BF7A8
		public static void Pause(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				GameObject value = (GameObject)iTween.tweens[i]["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				iTween.Pause((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x00053F0E File Offset: 0x0005210E
		public static int Count()
		{
			return iTween.tweens.Count;
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x002C161C File Offset: 0x002BF81C
		public static int Count(string type)
		{
			int num = 0;
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				Hashtable hashtable = iTween.tweens[i];
				if (((string)hashtable["type"] + (string)hashtable["method"]).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x002C1694 File Offset: 0x002BF894
		public static int Count(GameObject target)
		{
			Component[] components = target.GetComponents<iTween>();
			return components.Length;
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x002C16AC File Offset: 0x002BF8AC
		public static int Count(GameObject target, string type)
		{
			int num = 0;
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x002C1710 File Offset: 0x002BF910
		public static void Stop()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				iTween.Stop((GameObject)iTween.tweens[i]["target"]);
			}
			iTween.tweens.Clear();
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x002C175C File Offset: 0x002BF95C
		public static void Stop(string type)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				GameObject value = (GameObject)iTween.tweens[i]["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				iTween.Stop((GameObject)arrayList[j], type);
			}
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x002C17D0 File Offset: 0x002BF9D0
		public static void StopByName(string name)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				GameObject value = (GameObject)iTween.tweens[i]["target"];
				arrayList.Insert(arrayList.Count, value);
			}
			for (int j = 0; j < arrayList.Count; j++)
			{
				iTween.StopByName((GameObject)arrayList[j], name);
			}
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x002C1844 File Offset: 0x002BFA44
		public static void Stop(GameObject target)
		{
			Component[] array = target.GetComponents<iTween>();
			array = array;
			for (int i = 0; i < array.Length; i++)
			{
				((iTween)array[i]).Dispose();
			}
		}

		// Token: 0x06007ADE RID: 31454 RVA: 0x002C1878 File Offset: 0x002BFA78
		public static void Stop(GameObject target, bool includechildren)
		{
			iTween.Stop(target);
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.Stop(((Transform)obj).gameObject, true);
				}
			}
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x002C18E0 File Offset: 0x002BFAE0
		public static void Stop(GameObject target, string type)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					iTween.Dispose();
				}
			}
		}

		// Token: 0x06007AE0 RID: 31456 RVA: 0x002C1944 File Offset: 0x002BFB44
		public static void StopByName(GameObject target, string name)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if (iTween._name == name)
				{
					iTween.Dispose();
				}
			}
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x002C1988 File Offset: 0x002BFB88
		public static void Stop(GameObject target, string type, bool includechildren)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
				{
					iTween.Dispose();
				}
			}
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.Stop(((Transform)obj).gameObject, type, true);
				}
			}
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x002C1A40 File Offset: 0x002BFC40
		public static void StopByName(GameObject target, string name, bool includechildren)
		{
			Component[] array = target.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if (iTween._name == name)
				{
					iTween.Dispose();
				}
			}
			if (includechildren)
			{
				foreach (object obj in target.transform)
				{
					iTween.StopByName(((Transform)obj).gameObject, name, true);
				}
			}
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x002C1AD8 File Offset: 0x002BFCD8
		public static Hashtable Hash(params object[] args)
		{
			Hashtable hashtable = new Hashtable(args.Length / 2);
			if (args.Length % 2 != 0)
			{
				Debug.LogError("Tween Error: Hash requires an even number of arguments!");
				return null;
			}
			for (int i = 0; i < args.Length - 1; i += 2)
			{
				hashtable.Add(args[i], args[i + 1]);
			}
			return hashtable;
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x00053F1A File Offset: 0x0005211A
		private iTween(Hashtable h)
		{
			this.tweenArguments = h;
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x00053F29 File Offset: 0x00052129
		private void Awake()
		{
			this.thisTransform = base.transform;
			this.RetrieveArgs();
			this.lastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x00053F48 File Offset: 0x00052148
		private IEnumerator Start()
		{
			if (this.delay > 0f)
			{
				yield return base.StartCoroutine("TweenDelay");
			}
			this.TweenStart();
			yield break;
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x002C1B24 File Offset: 0x002BFD24
		private void Update()
		{
			if (this.isRunning && !this.physics)
			{
				if (!this.reverse)
				{
					if (this.percentage < 1f)
					{
						this.TweenUpdate();
						return;
					}
					this.TweenComplete();
					return;
				}
				else
				{
					if (this.percentage > 0f)
					{
						this.TweenUpdate();
						return;
					}
					this.TweenComplete();
				}
			}
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x002C1B80 File Offset: 0x002BFD80
		private void FixedUpdate()
		{
			if (this.isRunning && this.physics)
			{
				if (!this.reverse)
				{
					if (this.percentage < 1f)
					{
						this.TweenUpdate();
						return;
					}
					this.TweenComplete();
					return;
				}
				else
				{
					if (this.percentage > 0f)
					{
						this.TweenUpdate();
						return;
					}
					this.TweenComplete();
				}
			}
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x002C1BDC File Offset: 0x002BFDDC
		private void LateUpdate()
		{
			if (this.tweenArguments.Contains("looktarget") && this.isRunning && (this.type == "move" || this.type == "shake" || this.type == "punch"))
			{
				iTween.LookUpdate(base.gameObject, this.tweenArguments);
			}
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x00053F57 File Offset: 0x00052157
		private void OnEnable()
		{
			if (this.isRunning)
			{
				this.EnableKinematic();
			}
			if (this.isPaused)
			{
				this.isPaused = false;
				if (this.delay > 0f)
				{
					this.wasPaused = true;
					this.ResumeDelay();
				}
			}
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x00053F90 File Offset: 0x00052190
		private void OnDisable()
		{
			this.DisableKinematic();
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x002C1C4C File Offset: 0x002BFE4C
		private static void DrawLineHelper(Vector3[] line, Color color, string method)
		{
			Gizmos.color = color;
			for (int i = 0; i < line.Length - 1; i++)
			{
				if (method == "gizmos")
				{
					Gizmos.DrawLine(line[i], line[i + 1]);
				}
				else if (method == "handles")
				{
					Debug.LogError("iTween Error: Drawing a line with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
				}
			}
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x002C1CAC File Offset: 0x002BFEAC
		private static void DrawPathHelper(Vector3[] path, Color color, string method)
		{
			Vector3[] pts = iTween.PathControlPointGenerator(path);
			Vector3 vector = iTween.Interp(pts, 0f);
			Gizmos.color = color;
			int num = path.Length * 20;
			for (int i = 1; i <= num; i++)
			{
				float t = (float)i / (float)num;
				Vector3 vector2 = iTween.Interp(pts, t);
				if (method == "gizmos")
				{
					Gizmos.DrawLine(vector2, vector);
				}
				else if (method == "handles")
				{
					Debug.LogError("iTween Error: Drawing a path with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
				}
				vector = vector2;
			}
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x002C1D28 File Offset: 0x002BFF28
		private static Vector3[] PathControlPointGenerator(Vector3[] path)
		{
			int num = 2;
			Vector3[] array = new Vector3[path.Length + num];
			Array.Copy(path, 0, array, 1, path.Length);
			array[0] = array[1] + (array[1] - array[2]);
			array[array.Length - 1] = array[array.Length - 2] + (array[array.Length - 2] - array[array.Length - 3]);
			if (array[1] == array[array.Length - 2])
			{
				Vector3[] array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
				array2[0] = array2[array2.Length - 3];
				array2[array2.Length - 1] = array2[2];
				array = new Vector3[array2.Length];
				Array.Copy(array2, array, array2.Length);
			}
			return array;
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x002C1E10 File Offset: 0x002C0010
		private static Vector3 Interp(Vector3[] pts, float t)
		{
			int num = pts.Length - 3;
			int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
			float num3 = t * (float)num - (float)num2;
			Vector3 vector = pts[num2];
			Vector3 vector2 = pts[num2 + 1];
			Vector3 vector3 = pts[num2 + 2];
			Vector3 vector4 = pts[num2 + 3];
			return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x002C1F14 File Offset: 0x002C0114
		private static void Launch(GameObject target, Hashtable args)
		{
			if (!args.Contains("id"))
			{
				args["id"] = iTween.GenerateID();
			}
			if (!args.Contains("target"))
			{
				args["target"] = target;
			}
			iTween.tweens.Insert(0, args);
			target.AddComponent<iTween>();
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x002C1F6C File Offset: 0x002C016C
		private static Hashtable CleanArgs(Hashtable args)
		{
			Hashtable hashtable = new Hashtable(args.Count);
			Hashtable hashtable2 = new Hashtable(args.Count);
			foreach (object obj in args)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				hashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			foreach (object obj2 in hashtable)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				if (dictionaryEntry2.Value.GetType() == typeof(int))
				{
					float num = (float)((int)dictionaryEntry2.Value);
					args[dictionaryEntry2.Key] = num;
				}
				if (dictionaryEntry2.Value.GetType() == typeof(double))
				{
					float num2 = (float)((double)dictionaryEntry2.Value);
					args[dictionaryEntry2.Key] = num2;
				}
			}
			foreach (object obj3 in args)
			{
				DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
				hashtable2.Add(dictionaryEntry3.Key.ToString().ToLower(), dictionaryEntry3.Value);
			}
			args = hashtable2;
			return args;
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x002C2110 File Offset: 0x002C0310
		private static string GenerateID()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x002C2130 File Offset: 0x002C0330
		private void RetrieveArgs()
		{
			foreach (Hashtable hashtable in iTween.tweens)
			{
				if ((GameObject)hashtable["target"] == base.gameObject)
				{
					this.tweenArguments = hashtable;
					break;
				}
			}
			this.id = (string)this.tweenArguments["id"];
			this.type = (string)this.tweenArguments["type"];
			this._name = (string)this.tweenArguments["name"];
			this.method = (string)this.tweenArguments["method"];
			if (this.tweenArguments.Contains("time"))
			{
				this.time = (float)this.tweenArguments["time"];
			}
			else
			{
				this.time = iTween.Defaults.time;
			}
			if (base.GetComponent<Rigidbody>() != null)
			{
				this.physics = true;
			}
			if (this.tweenArguments.Contains("delay"))
			{
				this.delay = (float)this.tweenArguments["delay"];
			}
			else
			{
				this.delay = iTween.Defaults.delay;
			}
			if (this.tweenArguments.Contains("namedcolorvalue"))
			{
				if (this.tweenArguments["namedcolorvalue"].GetType() == typeof(iTween.NamedValueColor))
				{
					this.namedcolorvalue = (iTween.NamedValueColor)this.tweenArguments["namedcolorvalue"];
					goto IL_1F3;
				}
				try
				{
					this.namedcolorvalue = (iTween.NamedValueColor)Enum.Parse(typeof(iTween.NamedValueColor), (string)this.tweenArguments["namedcolorvalue"], true);
					goto IL_1F3;
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported namedcolorvalue supplied! Default will be used.");
					this.namedcolorvalue = iTween.NamedValueColor._Color;
					goto IL_1F3;
				}
			}
			this.namedcolorvalue = iTween.Defaults.namedColorValue;
			IL_1F3:
			if (this.tweenArguments.Contains("looptype"))
			{
				if (this.tweenArguments["looptype"].GetType() == typeof(iTween.LoopType))
				{
					this.loopType = (iTween.LoopType)this.tweenArguments["looptype"];
					goto IL_299;
				}
				try
				{
					this.loopType = (iTween.LoopType)Enum.Parse(typeof(iTween.LoopType), (string)this.tweenArguments["looptype"], true);
					goto IL_299;
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported loopType supplied! Default will be used.");
					this.loopType = iTween.LoopType.none;
					goto IL_299;
				}
			}
			this.loopType = iTween.LoopType.none;
			IL_299:
			if (this.tweenArguments.Contains("easetype"))
			{
				if (this.tweenArguments["easetype"].GetType() == typeof(iTween.EaseType))
				{
					this.easeType = (iTween.EaseType)this.tweenArguments["easetype"];
					goto IL_347;
				}
				try
				{
					this.easeType = (iTween.EaseType)Enum.Parse(typeof(iTween.EaseType), (string)this.tweenArguments["easetype"], true);
					goto IL_347;
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported easeType supplied! Default will be used.");
					this.easeType = iTween.Defaults.easeType;
					goto IL_347;
				}
			}
			this.easeType = iTween.Defaults.easeType;
			IL_347:
			if (this.tweenArguments.Contains("space"))
			{
				if (this.tweenArguments["space"].GetType() == typeof(Space))
				{
					this.space = (Space)this.tweenArguments["space"];
					goto IL_3F5;
				}
				try
				{
					this.space = (Space)Enum.Parse(typeof(Space), (string)this.tweenArguments["space"], true);
					goto IL_3F5;
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported space supplied! Default will be used.");
					this.space = iTween.Defaults.space;
					goto IL_3F5;
				}
			}
			this.space = iTween.Defaults.space;
			IL_3F5:
			if (this.tweenArguments.Contains("islocal"))
			{
				this.isLocal = (bool)this.tweenArguments["islocal"];
			}
			else
			{
				this.isLocal = iTween.Defaults.isLocal;
			}
			if (this.tweenArguments.Contains("ignoretimescale"))
			{
				this.useRealTime = (bool)this.tweenArguments["ignoretimescale"];
			}
			else
			{
				this.useRealTime = iTween.Defaults.useRealTime;
			}
			this.GetEasingFunction();
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x002C25EC File Offset: 0x002C07EC
		private void GetEasingFunction()
		{
			switch (this.easeType)
			{
			case iTween.EaseType.easeInQuad:
				this.ease = new iTween.EasingFunction(this.easeInQuad);
				return;
			case iTween.EaseType.easeOutQuad:
				this.ease = new iTween.EasingFunction(this.easeOutQuad);
				return;
			case iTween.EaseType.easeInOutQuad:
				this.ease = new iTween.EasingFunction(this.easeInOutQuad);
				return;
			case iTween.EaseType.easeInCubic:
				this.ease = new iTween.EasingFunction(this.easeInCubic);
				return;
			case iTween.EaseType.easeOutCubic:
				this.ease = new iTween.EasingFunction(this.easeOutCubic);
				return;
			case iTween.EaseType.easeInOutCubic:
				this.ease = new iTween.EasingFunction(this.easeInOutCubic);
				return;
			case iTween.EaseType.easeInQuart:
				this.ease = new iTween.EasingFunction(this.easeInQuart);
				return;
			case iTween.EaseType.easeOutQuart:
				this.ease = new iTween.EasingFunction(this.easeOutQuart);
				return;
			case iTween.EaseType.easeInOutQuart:
				this.ease = new iTween.EasingFunction(this.easeInOutQuart);
				return;
			case iTween.EaseType.easeInQuint:
				this.ease = new iTween.EasingFunction(this.easeInQuint);
				return;
			case iTween.EaseType.easeOutQuint:
				this.ease = new iTween.EasingFunction(this.easeOutQuint);
				return;
			case iTween.EaseType.easeInOutQuint:
				this.ease = new iTween.EasingFunction(this.easeInOutQuint);
				return;
			case iTween.EaseType.easeInSine:
				this.ease = new iTween.EasingFunction(this.easeInSine);
				return;
			case iTween.EaseType.easeOutSine:
				this.ease = new iTween.EasingFunction(this.easeOutSine);
				return;
			case iTween.EaseType.easeInOutSine:
				this.ease = new iTween.EasingFunction(this.easeInOutSine);
				return;
			case iTween.EaseType.easeInExpo:
				this.ease = new iTween.EasingFunction(this.easeInExpo);
				return;
			case iTween.EaseType.easeOutExpo:
				this.ease = new iTween.EasingFunction(this.easeOutExpo);
				return;
			case iTween.EaseType.easeInOutExpo:
				this.ease = new iTween.EasingFunction(this.easeInOutExpo);
				return;
			case iTween.EaseType.easeInCirc:
				this.ease = new iTween.EasingFunction(this.easeInCirc);
				return;
			case iTween.EaseType.easeOutCirc:
				this.ease = new iTween.EasingFunction(this.easeOutCirc);
				return;
			case iTween.EaseType.easeInOutCirc:
				this.ease = new iTween.EasingFunction(this.easeInOutCirc);
				return;
			case iTween.EaseType.linear:
				this.ease = new iTween.EasingFunction(this.linear);
				return;
			case iTween.EaseType.spring:
				this.ease = new iTween.EasingFunction(this.spring);
				return;
			case iTween.EaseType.easeInBounce:
				this.ease = new iTween.EasingFunction(this.easeInBounce);
				return;
			case iTween.EaseType.easeOutBounce:
				this.ease = new iTween.EasingFunction(this.easeOutBounce);
				return;
			case iTween.EaseType.easeInOutBounce:
				this.ease = new iTween.EasingFunction(this.easeInOutBounce);
				return;
			case iTween.EaseType.easeInBack:
				this.ease = new iTween.EasingFunction(this.easeInBack);
				return;
			case iTween.EaseType.easeOutBack:
				this.ease = new iTween.EasingFunction(this.easeOutBack);
				return;
			case iTween.EaseType.easeInOutBack:
				this.ease = new iTween.EasingFunction(this.easeInOutBack);
				return;
			case iTween.EaseType.easeInElastic:
				this.ease = new iTween.EasingFunction(this.easeInElastic);
				return;
			case iTween.EaseType.easeOutElastic:
				this.ease = new iTween.EasingFunction(this.easeOutElastic);
				return;
			case iTween.EaseType.easeInOutElastic:
				this.ease = new iTween.EasingFunction(this.easeInOutElastic);
				return;
			default:
				return;
			}
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x002C28E8 File Offset: 0x002C0AE8
		private void UpdatePercentage()
		{
			if (this.useRealTime)
			{
				this.runningTime += Time.realtimeSinceStartup - this.lastRealTime;
			}
			else
			{
				this.runningTime += Time.deltaTime;
			}
			if (this.reverse)
			{
				this.percentage = 1f - this.runningTime / this.time;
			}
			else
			{
				this.percentage = this.runningTime / this.time;
			}
			this.lastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x002C296C File Offset: 0x002C0B6C
		private void CallBack(string callbackType)
		{
			if (this.tweenArguments.Contains(callbackType) && !this.tweenArguments.Contains("ischild"))
			{
				GameObject gameObject;
				if (this.tweenArguments.Contains(callbackType + "target"))
				{
					gameObject = (GameObject)this.tweenArguments[callbackType + "target"];
				}
				else
				{
					gameObject = base.gameObject;
				}
				if (this.tweenArguments[callbackType].GetType() == typeof(string))
				{
					gameObject.SendMessage((string)this.tweenArguments[callbackType], this.tweenArguments[callbackType + "params"], 1);
					return;
				}
				Debug.LogError("iTween Error: Callback method references must be passed as a String!");
				Object.Destroy(this);
			}
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x002C2A40 File Offset: 0x002C0C40
		private void Dispose()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				if ((string)iTween.tweens[i]["id"] == this.id)
				{
					iTween.tweens.RemoveAt(i);
					break;
				}
			}
			Object.Destroy(this);
		}

		// Token: 0x06007AF8 RID: 31480 RVA: 0x002C2A9C File Offset: 0x002C0C9C
		private void ConflictCheck()
		{
			Component[] array = base.GetComponents<iTween>();
			foreach (iTween iTween in array)
			{
				if (iTween.type == "value")
				{
					return;
				}
				if (iTween.isRunning && iTween.type == this.type)
				{
					if (iTween.method != this.method)
					{
						return;
					}
					if (iTween.tweenArguments.Count != this.tweenArguments.Count)
					{
						iTween.Dispose();
						return;
					}
					foreach (object obj in this.tweenArguments)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!iTween.tweenArguments.Contains(dictionaryEntry.Key))
						{
							iTween.Dispose();
							return;
						}
						if (!iTween.tweenArguments[dictionaryEntry.Key].Equals(this.tweenArguments[dictionaryEntry.Key]) && (string)dictionaryEntry.Key != "id")
						{
							iTween.Dispose();
							return;
						}
					}
					this.Dispose();
				}
			}
		}

		// Token: 0x06007AF9 RID: 31481 RVA: 0x000042DD File Offset: 0x000024DD
		private void EnableKinematic()
		{
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x000042DD File Offset: 0x000024DD
		private void DisableKinematic()
		{
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x00053F98 File Offset: 0x00052198
		private void ResumeDelay()
		{
			base.StartCoroutine("TweenDelay");
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x00053FA6 File Offset: 0x000521A6
		private float linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		// Token: 0x06007AFD RID: 31485 RVA: 0x002C2BEC File Offset: 0x002C0DEC
		private float clerp(float start, float end, float value)
		{
			float num = 0f;
			float num2 = 360f;
			float num3 = Mathf.Abs((num2 - num) * 0.5f);
			float result;
			if (end - start < -num3)
			{
				float num4 = (num2 - start + end) * value;
				result = start + num4;
			}
			else if (end - start > num3)
			{
				float num4 = -(num2 - end + start) * value;
				result = start + num4;
			}
			else
			{
				result = start + (end - start) * value;
			}
			return result;
		}

		// Token: 0x06007AFE RID: 31486 RVA: 0x002C2C58 File Offset: 0x002C0E58
		private float spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x00053FB0 File Offset: 0x000521B0
		private float easeInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x00053FBE File Offset: 0x000521BE
		private float easeOutQuad(float start, float end, float value)
		{
			end -= start;
			return -end * value * (value - 2f) + start;
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x002C2CBC File Offset: 0x002C0EBC
		private float easeInOutQuad(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value + start;
			}
			value -= 1f;
			return -end * 0.5f * (value * (value - 2f) - 1f) + start;
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x00053FD3 File Offset: 0x000521D3
		private float easeInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x00053FE3 File Offset: 0x000521E3
		private float easeOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x002C2D10 File Offset: 0x002C0F10
		private float easeInOutCubic(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value + 2f) + start;
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x00054002 File Offset: 0x00052202
		private float easeInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x00054014 File Offset: 0x00052214
		private float easeOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * (value * value * value * value - 1f) + start;
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x002C2D64 File Offset: 0x002C0F64
		private float easeInOutQuart(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value + start;
			}
			value -= 2f;
			return -end * 0.5f * (value * value * value * value - 2f) + start;
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x00054036 File Offset: 0x00052236
		private float easeInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x0005404A File Offset: 0x0005224A
		private float easeOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x002C2DBC File Offset: 0x002C0FBC
		private float easeInOutQuint(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * value * value * value * value * value + start;
			}
			value -= 2f;
			return end * 0.5f * (value * value * value * value * value + 2f) + start;
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x0005406D File Offset: 0x0005226D
		private float easeInSine(float start, float end, float value)
		{
			end -= start;
			return -end * Mathf.Cos(value * 1.5707964f) + end + start;
		}

		// Token: 0x06007B0C RID: 31500 RVA: 0x00054087 File Offset: 0x00052287
		private float easeOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value * 1.5707964f) + start;
		}

		// Token: 0x06007B0D RID: 31501 RVA: 0x0005409E File Offset: 0x0005229E
		private float easeInOutSine(float start, float end, float value)
		{
			end -= start;
			return -end * 0.5f * (Mathf.Cos(3.1415927f * value) - 1f) + start;
		}

		// Token: 0x06007B0E RID: 31502 RVA: 0x000540C2 File Offset: 0x000522C2
		private float easeInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}

		// Token: 0x06007B0F RID: 31503 RVA: 0x000540E4 File Offset: 0x000522E4
		private float easeOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
		}

		// Token: 0x06007B10 RID: 31504 RVA: 0x002C2E18 File Offset: 0x002C1018
		private float easeInOutExpo(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return end * 0.5f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
			}
			value -= 1f;
			return end * 0.5f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
		}

		// Token: 0x06007B11 RID: 31505 RVA: 0x00054107 File Offset: 0x00052307
		private float easeInCirc(float start, float end, float value)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		// Token: 0x06007B12 RID: 31506 RVA: 0x00054127 File Offset: 0x00052327
		private float easeOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		// Token: 0x06007B13 RID: 31507 RVA: 0x002C2E88 File Offset: 0x002C1088
		private float easeInOutCirc(float start, float end, float value)
		{
			value /= 0.5f;
			end -= start;
			if (value < 1f)
			{
				return -end * 0.5f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
			}
			value -= 2f;
			return end * 0.5f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
		}

		// Token: 0x06007B14 RID: 31508 RVA: 0x002C2EF4 File Offset: 0x002C10F4
		private float easeInBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return end - this.easeOutBounce(0f, end, num - value) + start;
		}

		// Token: 0x06007B15 RID: 31509 RVA: 0x002C2F20 File Offset: 0x002C1120
		private float easeOutBounce(float start, float end, float value)
		{
			value /= 1f;
			end -= start;
			if (value < 0.36363637f)
			{
				return end * (7.5625f * value * value) + start;
			}
			if (value < 0.72727275f)
			{
				value -= 0.54545456f;
				return end * (7.5625f * value * value + 0.75f) + start;
			}
			if ((double)value < 0.9090909090909091)
			{
				value -= 0.8181818f;
				return end * (7.5625f * value * value + 0.9375f) + start;
			}
			value -= 0.95454544f;
			return end * (7.5625f * value * value + 0.984375f) + start;
		}

		// Token: 0x06007B16 RID: 31510 RVA: 0x002C2FBC File Offset: 0x002C11BC
		private float easeInOutBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			if (value < num * 0.5f)
			{
				return this.easeInBounce(0f, end, value * 2f) * 0.5f + start;
			}
			return this.easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
		}

		// Token: 0x06007B17 RID: 31511 RVA: 0x002C3020 File Offset: 0x002C1220
		private float easeInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x002C3054 File Offset: 0x002C1254
		private float easeOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value -= 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x002C3090 File Offset: 0x002C1290
		private float easeInOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value /= 0.5f;
			if (value < 1f)
			{
				num *= 1.525f;
				return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
			}
			value -= 2f;
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x002C310C File Offset: 0x002C130C
		private float punch(float amplitude, float value)
		{
			if (value == 0f)
			{
				return 0f;
			}
			if (value == 1f)
			{
				return 0f;
			}
			float num = 0.3f;
			float num2 = num / 6.2831855f * Mathf.Asin(0f);
			return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num2) * 6.2831855f / num);
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x002C3180 File Offset: 0x002C1380
		private float easeInElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return -(num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x002C3228 File Offset: 0x002C1428
		private float easeOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num) == 1f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 * 0.25f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) + end + start;
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x002C32C8 File Offset: 0x002C14C8
		private float easeInOutElastic(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			float num2 = num * 0.3f;
			float num3 = 0f;
			if (value == 0f)
			{
				return start;
			}
			if ((value /= num * 0.5f) == 2f)
			{
				return start + end;
			}
			float num4;
			if (num3 == 0f || num3 < Mathf.Abs(end))
			{
				num3 = end;
				num4 = num2 / 4f;
			}
			else
			{
				num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
			}
			if (value < 1f)
			{
				return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
			}
			return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) * 0.5f + end + start;
		}

		// Token: 0x0400697C RID: 27004
		public static List<Hashtable> tweens = new List<Hashtable>();

		// Token: 0x0400697D RID: 27005
		private static GameObject cameraFade;

		// Token: 0x0400697E RID: 27006
		public string id;

		// Token: 0x0400697F RID: 27007
		public string type;

		// Token: 0x04006980 RID: 27008
		public string method;

		// Token: 0x04006981 RID: 27009
		public iTween.EaseType easeType;

		// Token: 0x04006982 RID: 27010
		public float time;

		// Token: 0x04006983 RID: 27011
		public float delay;

		// Token: 0x04006984 RID: 27012
		public iTween.LoopType loopType;

		// Token: 0x04006985 RID: 27013
		public bool isRunning;

		// Token: 0x04006986 RID: 27014
		public bool isPaused;

		// Token: 0x04006987 RID: 27015
		public string _name;

		// Token: 0x04006988 RID: 27016
		private float runningTime;

		// Token: 0x04006989 RID: 27017
		private float percentage;

		// Token: 0x0400698A RID: 27018
		private float delayStarted;

		// Token: 0x0400698B RID: 27019
		private bool kinematic;

		// Token: 0x0400698C RID: 27020
		private bool isLocal;

		// Token: 0x0400698D RID: 27021
		private bool loop;

		// Token: 0x0400698E RID: 27022
		private bool reverse;

		// Token: 0x0400698F RID: 27023
		private bool wasPaused;

		// Token: 0x04006990 RID: 27024
		private bool physics;

		// Token: 0x04006991 RID: 27025
		private Hashtable tweenArguments;

		// Token: 0x04006992 RID: 27026
		private Space space;

		// Token: 0x04006993 RID: 27027
		private iTween.EasingFunction ease;

		// Token: 0x04006994 RID: 27028
		private iTween.ApplyTween apply;

		// Token: 0x04006995 RID: 27029
		private AudioSource audioSource;

		// Token: 0x04006996 RID: 27030
		private Vector3[] vector3s;

		// Token: 0x04006997 RID: 27031
		private Vector2[] vector2s;

		// Token: 0x04006998 RID: 27032
		private Color[,] colors;

		// Token: 0x04006999 RID: 27033
		private float[] floats;

		// Token: 0x0400699A RID: 27034
		private Rect[] rects;

		// Token: 0x0400699B RID: 27035
		private iTween.CRSpline path;

		// Token: 0x0400699C RID: 27036
		private Vector3 preUpdate;

		// Token: 0x0400699D RID: 27037
		private Vector3 postUpdate;

		// Token: 0x0400699E RID: 27038
		private iTween.NamedValueColor namedcolorvalue;

		// Token: 0x0400699F RID: 27039
		private float lastRealTime;

		// Token: 0x040069A0 RID: 27040
		private bool useRealTime;

		// Token: 0x040069A1 RID: 27041
		private Transform thisTransform;

		// Token: 0x020013B4 RID: 5044
		// (Invoke) Token: 0x06007B20 RID: 31520
		private delegate float EasingFunction(float start, float end, float Value);

		// Token: 0x020013B5 RID: 5045
		// (Invoke) Token: 0x06007B24 RID: 31524
		private delegate void ApplyTween();

		// Token: 0x020013B6 RID: 5046
		public enum EaseType
		{
			// Token: 0x040069A3 RID: 27043
			easeInQuad,
			// Token: 0x040069A4 RID: 27044
			easeOutQuad,
			// Token: 0x040069A5 RID: 27045
			easeInOutQuad,
			// Token: 0x040069A6 RID: 27046
			easeInCubic,
			// Token: 0x040069A7 RID: 27047
			easeOutCubic,
			// Token: 0x040069A8 RID: 27048
			easeInOutCubic,
			// Token: 0x040069A9 RID: 27049
			easeInQuart,
			// Token: 0x040069AA RID: 27050
			easeOutQuart,
			// Token: 0x040069AB RID: 27051
			easeInOutQuart,
			// Token: 0x040069AC RID: 27052
			easeInQuint,
			// Token: 0x040069AD RID: 27053
			easeOutQuint,
			// Token: 0x040069AE RID: 27054
			easeInOutQuint,
			// Token: 0x040069AF RID: 27055
			easeInSine,
			// Token: 0x040069B0 RID: 27056
			easeOutSine,
			// Token: 0x040069B1 RID: 27057
			easeInOutSine,
			// Token: 0x040069B2 RID: 27058
			easeInExpo,
			// Token: 0x040069B3 RID: 27059
			easeOutExpo,
			// Token: 0x040069B4 RID: 27060
			easeInOutExpo,
			// Token: 0x040069B5 RID: 27061
			easeInCirc,
			// Token: 0x040069B6 RID: 27062
			easeOutCirc,
			// Token: 0x040069B7 RID: 27063
			easeInOutCirc,
			// Token: 0x040069B8 RID: 27064
			linear,
			// Token: 0x040069B9 RID: 27065
			spring,
			// Token: 0x040069BA RID: 27066
			easeInBounce,
			// Token: 0x040069BB RID: 27067
			easeOutBounce,
			// Token: 0x040069BC RID: 27068
			easeInOutBounce,
			// Token: 0x040069BD RID: 27069
			easeInBack,
			// Token: 0x040069BE RID: 27070
			easeOutBack,
			// Token: 0x040069BF RID: 27071
			easeInOutBack,
			// Token: 0x040069C0 RID: 27072
			easeInElastic,
			// Token: 0x040069C1 RID: 27073
			easeOutElastic,
			// Token: 0x040069C2 RID: 27074
			easeInOutElastic,
			// Token: 0x040069C3 RID: 27075
			punch
		}

		// Token: 0x020013B7 RID: 5047
		public enum LoopType
		{
			// Token: 0x040069C5 RID: 27077
			none,
			// Token: 0x040069C6 RID: 27078
			loop,
			// Token: 0x040069C7 RID: 27079
			pingPong
		}

		// Token: 0x020013B8 RID: 5048
		public enum NamedValueColor
		{
			// Token: 0x040069C9 RID: 27081
			_Color,
			// Token: 0x040069CA RID: 27082
			_SpecColor,
			// Token: 0x040069CB RID: 27083
			_Emission,
			// Token: 0x040069CC RID: 27084
			_ReflectColor
		}

		// Token: 0x020013B9 RID: 5049
		public static class Defaults
		{
			// Token: 0x040069CD RID: 27085
			public static float time = 1f;

			// Token: 0x040069CE RID: 27086
			public static float delay = 0f;

			// Token: 0x040069CF RID: 27087
			public static iTween.NamedValueColor namedColorValue = iTween.NamedValueColor._Color;

			// Token: 0x040069D0 RID: 27088
			public static iTween.LoopType loopType = iTween.LoopType.none;

			// Token: 0x040069D1 RID: 27089
			public static iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

			// Token: 0x040069D2 RID: 27090
			public static float lookSpeed = 3f;

			// Token: 0x040069D3 RID: 27091
			public static bool isLocal = false;

			// Token: 0x040069D4 RID: 27092
			public static Space space = 1;

			// Token: 0x040069D5 RID: 27093
			public static bool orientToPath = false;

			// Token: 0x040069D6 RID: 27094
			public static Color color = Color.white;

			// Token: 0x040069D7 RID: 27095
			public static float updateTimePercentage = 0.05f;

			// Token: 0x040069D8 RID: 27096
			public static float updateTime = 1f * iTween.Defaults.updateTimePercentage;

			// Token: 0x040069D9 RID: 27097
			public static int cameraFadeDepth = 999999;

			// Token: 0x040069DA RID: 27098
			public static float lookAhead = 0.05f;

			// Token: 0x040069DB RID: 27099
			public static bool useRealTime = false;

			// Token: 0x040069DC RID: 27100
			public static Vector3 up = Vector3.up;
		}

		// Token: 0x020013BA RID: 5050
		private class CRSpline
		{
			// Token: 0x06007B28 RID: 31528 RVA: 0x00054155 File Offset: 0x00052355
			public CRSpline(params Vector3[] pts)
			{
				this.pts = new Vector3[pts.Length];
				Array.Copy(pts, this.pts, pts.Length);
			}

			// Token: 0x06007B29 RID: 31529 RVA: 0x002C3454 File Offset: 0x002C1654
			public Vector3 Interp(float t)
			{
				int num = this.pts.Length - 3;
				int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
				float num3 = t * (float)num - (float)num2;
				Vector3 vector = this.pts[num2];
				Vector3 vector2 = this.pts[num2 + 1];
				Vector3 vector3 = this.pts[num2 + 2];
				Vector3 vector4 = this.pts[num2 + 3];
				return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
			}

			// Token: 0x040069DD RID: 27101
			public Vector3[] pts;
		}
	}
}
