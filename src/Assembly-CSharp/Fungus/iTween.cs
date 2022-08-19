using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F0A RID: 3850
	[AddComponentMenu("")]
	public class iTween : MonoBehaviour
	{
		// Token: 0x06006C59 RID: 27737 RVA: 0x00298927 File Offset: 0x00296B27
		public static void Init(GameObject target)
		{
			iTween.MoveBy(target, Vector3.zero, 0f);
			iTween.cameraFade = null;
		}

		// Token: 0x06006C5A RID: 27738 RVA: 0x00298940 File Offset: 0x00296B40
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

		// Token: 0x06006C5B RID: 27739 RVA: 0x00298996 File Offset: 0x00296B96
		public static void CameraFadeFrom(Hashtable args)
		{
			if (iTween.cameraFade)
			{
				iTween.ColorFrom(iTween.cameraFade, args);
				return;
			}
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}

		// Token: 0x06006C5C RID: 27740 RVA: 0x002989BC File Offset: 0x00296BBC
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

		// Token: 0x06006C5D RID: 27741 RVA: 0x00298A12 File Offset: 0x00296C12
		public static void CameraFadeTo(Hashtable args)
		{
			if (iTween.cameraFade)
			{
				iTween.ColorTo(iTween.cameraFade, args);
				return;
			}
			Debug.LogError("iTween Error: You must first add a camera fade object with CameraFadeAdd() before atttempting to use camera fading.");
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x00298A38 File Offset: 0x00296C38
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

		// Token: 0x06006C5F RID: 27743 RVA: 0x00298BC5 File Offset: 0x00296DC5
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

		// Token: 0x06006C60 RID: 27744 RVA: 0x00298BFA File Offset: 0x00296DFA
		public static void FadeFrom(GameObject target, Hashtable args)
		{
			iTween.ColorFrom(target, args);
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x00298C03 File Offset: 0x00296E03
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

		// Token: 0x06006C62 RID: 27746 RVA: 0x00298C38 File Offset: 0x00296E38
		public static void FadeTo(GameObject target, Hashtable args)
		{
			iTween.ColorTo(target, args);
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x00298C41 File Offset: 0x00296E41
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

		// Token: 0x06006C64 RID: 27748 RVA: 0x00298C78 File Offset: 0x00296E78
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

		// Token: 0x06006C65 RID: 27749 RVA: 0x00298F0C File Offset: 0x0029710C
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

		// Token: 0x06006C66 RID: 27750 RVA: 0x00298F44 File Offset: 0x00297144
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

		// Token: 0x06006C67 RID: 27751 RVA: 0x0029902C File Offset: 0x0029722C
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

		// Token: 0x06006C68 RID: 27752 RVA: 0x00299080 File Offset: 0x00297280
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

		// Token: 0x06006C69 RID: 27753 RVA: 0x002991D8 File Offset: 0x002973D8
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

		// Token: 0x06006C6A RID: 27754 RVA: 0x0029922C File Offset: 0x0029742C
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

		// Token: 0x06006C6B RID: 27755 RVA: 0x00299287 File Offset: 0x00297487
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

		// Token: 0x06006C6C RID: 27756 RVA: 0x002992B7 File Offset: 0x002974B7
		public static void Stab(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "stab";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x002992D8 File Offset: 0x002974D8
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

		// Token: 0x06006C6E RID: 27758 RVA: 0x00299310 File Offset: 0x00297510
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

		// Token: 0x06006C6F RID: 27759 RVA: 0x002994E7 File Offset: 0x002976E7
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

		// Token: 0x06006C70 RID: 27760 RVA: 0x0029951C File Offset: 0x0029771C
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

		// Token: 0x06006C71 RID: 27761 RVA: 0x00299606 File Offset: 0x00297806
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

		// Token: 0x06006C72 RID: 27762 RVA: 0x0029963C File Offset: 0x0029783C
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

		// Token: 0x06006C73 RID: 27763 RVA: 0x0029975F File Offset: 0x0029795F
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

		// Token: 0x06006C74 RID: 27764 RVA: 0x00299794 File Offset: 0x00297994
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

		// Token: 0x06006C75 RID: 27765 RVA: 0x00299AA4 File Offset: 0x00297CA4
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

		// Token: 0x06006C76 RID: 27766 RVA: 0x00299AD9 File Offset: 0x00297CD9
		public static void MoveAdd(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "add";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x00299B0A File Offset: 0x00297D0A
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

		// Token: 0x06006C78 RID: 27768 RVA: 0x00299B3F File Offset: 0x00297D3F
		public static void MoveBy(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "move";
			args["method"] = "by";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x00299B70 File Offset: 0x00297D70
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

		// Token: 0x06006C7A RID: 27770 RVA: 0x00299BA8 File Offset: 0x00297DA8
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

		// Token: 0x06006C7B RID: 27771 RVA: 0x00299CCB File Offset: 0x00297ECB
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

		// Token: 0x06006C7C RID: 27772 RVA: 0x00299D00 File Offset: 0x00297F00
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

		// Token: 0x06006C7D RID: 27773 RVA: 0x00299E50 File Offset: 0x00298050
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

		// Token: 0x06006C7E RID: 27774 RVA: 0x00299E85 File Offset: 0x00298085
		public static void ScaleAdd(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "add";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C7F RID: 27775 RVA: 0x00299EB6 File Offset: 0x002980B6
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

		// Token: 0x06006C80 RID: 27776 RVA: 0x00299EEB File Offset: 0x002980EB
		public static void ScaleBy(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "scale";
			args["method"] = "by";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x00299F1C File Offset: 0x0029811C
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

		// Token: 0x06006C82 RID: 27778 RVA: 0x00299F54 File Offset: 0x00298154
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

		// Token: 0x06006C83 RID: 27779 RVA: 0x0029A077 File Offset: 0x00298277
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

		// Token: 0x06006C84 RID: 27780 RVA: 0x0029A0AC File Offset: 0x002982AC
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

		// Token: 0x06006C85 RID: 27781 RVA: 0x0029A246 File Offset: 0x00298446
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

		// Token: 0x06006C86 RID: 27782 RVA: 0x0029A27B File Offset: 0x0029847B
		public static void RotateAdd(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "add";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x0029A2AC File Offset: 0x002984AC
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

		// Token: 0x06006C88 RID: 27784 RVA: 0x0029A2E1 File Offset: 0x002984E1
		public static void RotateBy(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "rotate";
			args["method"] = "by";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x0029A312 File Offset: 0x00298512
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

		// Token: 0x06006C8A RID: 27786 RVA: 0x0029A347 File Offset: 0x00298547
		public static void ShakePosition(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "position";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x0029A378 File Offset: 0x00298578
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

		// Token: 0x06006C8C RID: 27788 RVA: 0x0029A3AD File Offset: 0x002985AD
		public static void ShakeScale(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "scale";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x0029A3DE File Offset: 0x002985DE
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

		// Token: 0x06006C8E RID: 27790 RVA: 0x0029A413 File Offset: 0x00298613
		public static void ShakeRotation(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "shake";
			args["method"] = "rotation";
			iTween.Launch(target, args);
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x0029A444 File Offset: 0x00298644
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

		// Token: 0x06006C90 RID: 27792 RVA: 0x0029A47C File Offset: 0x0029867C
		public static void PunchPosition(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "punch";
			args["method"] = "position";
			args["easetype"] = iTween.EaseType.punch;
			iTween.Launch(target, args);
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x0029A4CA File Offset: 0x002986CA
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

		// Token: 0x06006C92 RID: 27794 RVA: 0x0029A500 File Offset: 0x00298700
		public static void PunchRotation(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "punch";
			args["method"] = "rotation";
			args["easetype"] = iTween.EaseType.punch;
			iTween.Launch(target, args);
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x0029A54E File Offset: 0x0029874E
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

		// Token: 0x06006C94 RID: 27796 RVA: 0x0029A584 File Offset: 0x00298784
		public static void PunchScale(GameObject target, Hashtable args)
		{
			args = iTween.CleanArgs(args);
			args["type"] = "punch";
			args["method"] = "scale";
			args["easetype"] = iTween.EaseType.punch;
			iTween.Launch(target, args);
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x0029A5D4 File Offset: 0x002987D4
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

		// Token: 0x06006C96 RID: 27798 RVA: 0x0029AAFC File Offset: 0x00298CFC
		private void GenerateRectTargets()
		{
			this.rects = new Rect[3];
			this.rects[0] = (Rect)this.tweenArguments["from"];
			this.rects[1] = (Rect)this.tweenArguments["to"];
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x0029AB58 File Offset: 0x00298D58
		private void GenerateColorTargets()
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (Color)this.tweenArguments["from"];
			this.colors[0, 1] = (Color)this.tweenArguments["to"];
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x0029ABB8 File Offset: 0x00298DB8
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

		// Token: 0x06006C99 RID: 27801 RVA: 0x0029AC68 File Offset: 0x00298E68
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

		// Token: 0x06006C9A RID: 27802 RVA: 0x0029AD5C File Offset: 0x00298F5C
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

		// Token: 0x06006C9B RID: 27803 RVA: 0x0029ADF8 File Offset: 0x00298FF8
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

		// Token: 0x06006C9C RID: 27804 RVA: 0x0029B144 File Offset: 0x00299344
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

		// Token: 0x06006C9D RID: 27805 RVA: 0x0029B260 File Offset: 0x00299460
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

		// Token: 0x06006C9E RID: 27806 RVA: 0x0029B384 File Offset: 0x00299584
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

		// Token: 0x06006C9F RID: 27807 RVA: 0x0029B70C File Offset: 0x0029990C
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

		// Token: 0x06006CA0 RID: 27808 RVA: 0x0029B9EC File Offset: 0x00299BEC
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

		// Token: 0x06006CA1 RID: 27809 RVA: 0x0029BC58 File Offset: 0x00299E58
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

		// Token: 0x06006CA2 RID: 27810 RVA: 0x0029BECC File Offset: 0x0029A0CC
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

		// Token: 0x06006CA3 RID: 27811 RVA: 0x0029C0BC File Offset: 0x0029A2BC
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

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0029C24C File Offset: 0x0029A44C
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

		// Token: 0x06006CA5 RID: 27813 RVA: 0x0029C3DC File Offset: 0x0029A5DC
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

		// Token: 0x06006CA6 RID: 27814 RVA: 0x0029C694 File Offset: 0x0029A894
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

		// Token: 0x06006CA7 RID: 27815 RVA: 0x0029C830 File Offset: 0x0029AA30
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

		// Token: 0x06006CA8 RID: 27816 RVA: 0x0029C9F8 File Offset: 0x0029ABF8
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

		// Token: 0x06006CA9 RID: 27817 RVA: 0x0029CB1C File Offset: 0x0029AD1C
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

		// Token: 0x06006CAA RID: 27818 RVA: 0x0029CC28 File Offset: 0x0029AE28
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

		// Token: 0x06006CAB RID: 27819 RVA: 0x0029CD34 File Offset: 0x0029AF34
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

		// Token: 0x06006CAC RID: 27820 RVA: 0x0029CE78 File Offset: 0x0029B078
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

		// Token: 0x06006CAD RID: 27821 RVA: 0x0029CFA4 File Offset: 0x0029B1A4
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

		// Token: 0x06006CAE RID: 27822 RVA: 0x0029D0C4 File Offset: 0x0029B2C4
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

		// Token: 0x06006CAF RID: 27823 RVA: 0x0029D230 File Offset: 0x0029B430
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

		// Token: 0x06006CB0 RID: 27824 RVA: 0x0029D3AC File Offset: 0x0029B5AC
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

		// Token: 0x06006CB1 RID: 27825 RVA: 0x0029D4D4 File Offset: 0x0029B6D4
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

		// Token: 0x06006CB2 RID: 27826 RVA: 0x0029D5B8 File Offset: 0x0029B7B8
		private void ApplyFloatTargets()
		{
			this.floats[2] = this.ease(this.floats[0], this.floats[1], this.percentage);
			this.tweenArguments["onupdateparams"] = this.floats[2];
			if (this.percentage == 1f)
			{
				this.tweenArguments["onupdateparams"] = this.floats[1];
			}
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x0029D638 File Offset: 0x0029B838
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

		// Token: 0x06006CB4 RID: 27828 RVA: 0x0029D880 File Offset: 0x0029BA80
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

		// Token: 0x06006CB5 RID: 27829 RVA: 0x00004095 File Offset: 0x00002295
		private void ApplyStabTargets()
		{
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x0029D994 File Offset: 0x0029BB94
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

		// Token: 0x06006CB7 RID: 27831 RVA: 0x0029DB18 File Offset: 0x0029BD18
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

		// Token: 0x06006CB8 RID: 27832 RVA: 0x0029DCBC File Offset: 0x0029BEBC
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

		// Token: 0x06006CB9 RID: 27833 RVA: 0x0029DE84 File Offset: 0x0029C084
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

		// Token: 0x06006CBA RID: 27834 RVA: 0x0029DF98 File Offset: 0x0029C198
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

		// Token: 0x06006CBB RID: 27835 RVA: 0x0029E0B4 File Offset: 0x0029C2B4
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

		// Token: 0x06006CBC RID: 27836 RVA: 0x0029E270 File Offset: 0x0029C470
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

		// Token: 0x06006CBD RID: 27837 RVA: 0x0029E3E0 File Offset: 0x0029C5E0
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

		// Token: 0x06006CBE RID: 27838 RVA: 0x0029E624 File Offset: 0x0029C824
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

		// Token: 0x06006CBF RID: 27839 RVA: 0x0029E754 File Offset: 0x0029C954
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

		// Token: 0x06006CC0 RID: 27840 RVA: 0x0029E8D4 File Offset: 0x0029CAD4
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

		// Token: 0x06006CC1 RID: 27841 RVA: 0x0029EB8C File Offset: 0x0029CD8C
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

		// Token: 0x06006CC2 RID: 27842 RVA: 0x0029EDEC File Offset: 0x0029CFEC
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

		// Token: 0x06006CC3 RID: 27843 RVA: 0x0029EFDD File Offset: 0x0029D1DD
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

		// Token: 0x06006CC4 RID: 27844 RVA: 0x0029EFEC File Offset: 0x0029D1EC
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

		// Token: 0x06006CC5 RID: 27845 RVA: 0x0029F0CB File Offset: 0x0029D2CB
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

		// Token: 0x06006CC6 RID: 27846 RVA: 0x0029F0DA File Offset: 0x0029D2DA
		private void TweenUpdate()
		{
			this.apply();
			this.CallBack("onupdate");
			this.UpdatePercentage();
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x0029F0F8 File Offset: 0x0029D2F8
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

		// Token: 0x06006CC8 RID: 27848 RVA: 0x0029F17C File Offset: 0x0029D37C
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

		// Token: 0x06006CC9 RID: 27849 RVA: 0x0029F1F4 File Offset: 0x0029D3F4
		public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
		{
			return new Rect(iTween.FloatUpdate(currentValue.x, targetValue.x, speed), iTween.FloatUpdate(currentValue.y, targetValue.y, speed), iTween.FloatUpdate(currentValue.width, targetValue.width, speed), iTween.FloatUpdate(currentValue.height, targetValue.height, speed));
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x0029F258 File Offset: 0x0029D458
		public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
		{
			Vector3 vector = targetValue - currentValue;
			currentValue += vector * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x0029F288 File Offset: 0x0029D488
		public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
		{
			Vector2 vector = targetValue - currentValue;
			currentValue += vector * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x0029F2B8 File Offset: 0x0029D4B8
		public static float FloatUpdate(float currentValue, float targetValue, float speed)
		{
			float num = targetValue - currentValue;
			currentValue += num * speed * Time.deltaTime;
			return currentValue;
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x0029F2D7 File Offset: 0x0029D4D7
		public static void FadeUpdate(GameObject target, Hashtable args)
		{
			args["a"] = args["alpha"];
			iTween.ColorUpdate(target, args);
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x0029F2F6 File Offset: 0x0029D4F6
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

		// Token: 0x06006CCF RID: 27855 RVA: 0x0029F32C File Offset: 0x0029D52C
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

		// Token: 0x06006CD0 RID: 27856 RVA: 0x0029F62C File Offset: 0x0029D82C
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

		// Token: 0x06006CD1 RID: 27857 RVA: 0x0029F664 File Offset: 0x0029D864
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

		// Token: 0x06006CD2 RID: 27858 RVA: 0x0029F7F4 File Offset: 0x0029D9F4
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

		// Token: 0x06006CD3 RID: 27859 RVA: 0x0029F848 File Offset: 0x0029DA48
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

		// Token: 0x06006CD4 RID: 27860 RVA: 0x0029FA78 File Offset: 0x0029DC78
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

		// Token: 0x06006CD5 RID: 27861 RVA: 0x0029FAB0 File Offset: 0x0029DCB0
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

		// Token: 0x06006CD6 RID: 27862 RVA: 0x0029FCD5 File Offset: 0x0029DED5
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

		// Token: 0x06006CD7 RID: 27863 RVA: 0x0029FD0C File Offset: 0x0029DF0C
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

		// Token: 0x06006CD8 RID: 27864 RVA: 0x002A001F File Offset: 0x0029E21F
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

		// Token: 0x06006CD9 RID: 27865 RVA: 0x002A0054 File Offset: 0x0029E254
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

		// Token: 0x06006CDA RID: 27866 RVA: 0x002A03A1 File Offset: 0x0029E5A1
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

		// Token: 0x06006CDB RID: 27867 RVA: 0x002A03D8 File Offset: 0x0029E5D8
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

		// Token: 0x06006CDC RID: 27868 RVA: 0x002A0468 File Offset: 0x0029E668
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

		// Token: 0x06006CDD RID: 27869 RVA: 0x002A04C8 File Offset: 0x0029E6C8
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

		// Token: 0x06006CDE RID: 27870 RVA: 0x002A051C File Offset: 0x0029E71C
		public static void PutOnPath(GameObject target, Vector3[] path, float percent)
		{
			target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x002A0535 File Offset: 0x0029E735
		public static void PutOnPath(Transform target, Vector3[] path, float percent)
		{
			target.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x002A054C File Offset: 0x0029E74C
		public static void PutOnPath(GameObject target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x002A0598 File Offset: 0x0029E798
		public static void PutOnPath(Transform target, Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			target.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x002A05E0 File Offset: 0x0029E7E0
		public static Vector3 PointOnPath(Transform[] path, float percent)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			return iTween.Interp(iTween.PathControlPointGenerator(array), percent);
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x002A061F File Offset: 0x0029E81F
		public static void DrawLine(Vector3[] line)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x002A0635 File Offset: 0x0029E835
		public static void DrawLine(Vector3[] line, Color color)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x002A0648 File Offset: 0x0029E848
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

		// Token: 0x06006CE6 RID: 27878 RVA: 0x002A0690 File Offset: 0x0029E890
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

		// Token: 0x06006CE7 RID: 27879 RVA: 0x002A061F File Offset: 0x0029E81F
		public static void DrawLineGizmos(Vector3[] line)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x002A0635 File Offset: 0x0029E835
		public static void DrawLineGizmos(Vector3[] line, Color color)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, color, "gizmos");
			}
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x002A06D4 File Offset: 0x0029E8D4
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

		// Token: 0x06006CEA RID: 27882 RVA: 0x002A071C File Offset: 0x0029E91C
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

		// Token: 0x06006CEB RID: 27883 RVA: 0x002A075F File Offset: 0x0029E95F
		public static void DrawLineHandles(Vector3[] line)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, iTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x002A0775 File Offset: 0x0029E975
		public static void DrawLineHandles(Vector3[] line, Color color)
		{
			if (line.Length != 0)
			{
				iTween.DrawLineHelper(line, color, "handles");
			}
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x002A0788 File Offset: 0x0029E988
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

		// Token: 0x06006CEE RID: 27886 RVA: 0x002A07D0 File Offset: 0x0029E9D0
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

		// Token: 0x06006CEF RID: 27887 RVA: 0x002A0813 File Offset: 0x0029EA13
		public static Vector3 PointOnPath(Vector3[] path, float percent)
		{
			return iTween.Interp(iTween.PathControlPointGenerator(path), percent);
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x002A0821 File Offset: 0x0029EA21
		public static void DrawPath(Vector3[] path)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x002A0837 File Offset: 0x0029EA37
		public static void DrawPath(Vector3[] path, Color color)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x002A084C File Offset: 0x0029EA4C
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

		// Token: 0x06006CF3 RID: 27891 RVA: 0x002A0894 File Offset: 0x0029EA94
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

		// Token: 0x06006CF4 RID: 27892 RVA: 0x002A0821 File Offset: 0x0029EA21
		public static void DrawPathGizmos(Vector3[] path)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
			}
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x002A0837 File Offset: 0x0029EA37
		public static void DrawPathGizmos(Vector3[] path, Color color)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, color, "gizmos");
			}
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x002A08D8 File Offset: 0x0029EAD8
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

		// Token: 0x06006CF7 RID: 27895 RVA: 0x002A0920 File Offset: 0x0029EB20
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

		// Token: 0x06006CF8 RID: 27896 RVA: 0x002A0963 File Offset: 0x0029EB63
		public static void DrawPathHandles(Vector3[] path)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, iTween.Defaults.color, "handles");
			}
		}

		// Token: 0x06006CF9 RID: 27897 RVA: 0x002A0979 File Offset: 0x0029EB79
		public static void DrawPathHandles(Vector3[] path, Color color)
		{
			if (path.Length != 0)
			{
				iTween.DrawPathHelper(path, color, "handles");
			}
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x002A098C File Offset: 0x0029EB8C
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

		// Token: 0x06006CFB RID: 27899 RVA: 0x002A09D4 File Offset: 0x0029EBD4
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

		// Token: 0x06006CFC RID: 27900 RVA: 0x002A0A18 File Offset: 0x0029EC18
		public static void CameraFadeDepth(int depth)
		{
			if (iTween.cameraFade)
			{
				iTween.cameraFade.transform.position = new Vector3(iTween.cameraFade.transform.position.x, iTween.cameraFade.transform.position.y, (float)depth);
			}
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x002A0A6F File Offset: 0x0029EC6F
		public static void CameraFadeDestroy()
		{
			if (iTween.cameraFade)
			{
				Object.Destroy(iTween.cameraFade);
			}
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x002A0A88 File Offset: 0x0029EC88
		public static void Resume(GameObject target)
		{
			Component[] array = target.GetComponents<iTween>();
			array = array;
			for (int i = 0; i < array.Length; i++)
			{
				((iTween)array[i]).enabled = true;
			}
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x002A0ABC File Offset: 0x0029ECBC
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

		// Token: 0x06006D00 RID: 27904 RVA: 0x002A0B24 File Offset: 0x0029ED24
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

		// Token: 0x06006D01 RID: 27905 RVA: 0x002A0B88 File Offset: 0x0029ED88
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

		// Token: 0x06006D02 RID: 27906 RVA: 0x002A0C44 File Offset: 0x0029EE44
		public static void Resume()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				iTween.Resume((GameObject)iTween.tweens[i]["target"]);
			}
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x002A0C88 File Offset: 0x0029EE88
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

		// Token: 0x06006D04 RID: 27908 RVA: 0x002A0CFC File Offset: 0x0029EEFC
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

		// Token: 0x06006D05 RID: 27909 RVA: 0x002A0D68 File Offset: 0x0029EF68
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

		// Token: 0x06006D06 RID: 27910 RVA: 0x002A0DD0 File Offset: 0x0029EFD0
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

		// Token: 0x06006D07 RID: 27911 RVA: 0x002A0E70 File Offset: 0x0029F070
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

		// Token: 0x06006D08 RID: 27912 RVA: 0x002A0F64 File Offset: 0x0029F164
		public static void Pause()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				iTween.Pause((GameObject)iTween.tweens[i]["target"]);
			}
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x002A0FA8 File Offset: 0x0029F1A8
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

		// Token: 0x06006D0A RID: 27914 RVA: 0x002A101B File Offset: 0x0029F21B
		public static int Count()
		{
			return iTween.tweens.Count;
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x002A1028 File Offset: 0x0029F228
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

		// Token: 0x06006D0C RID: 27916 RVA: 0x002A10A0 File Offset: 0x0029F2A0
		public static int Count(GameObject target)
		{
			Component[] components = target.GetComponents<iTween>();
			return components.Length;
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x002A10B8 File Offset: 0x0029F2B8
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

		// Token: 0x06006D0E RID: 27918 RVA: 0x002A111C File Offset: 0x0029F31C
		public static void Stop()
		{
			for (int i = 0; i < iTween.tweens.Count; i++)
			{
				iTween.Stop((GameObject)iTween.tweens[i]["target"]);
			}
			iTween.tweens.Clear();
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x002A1168 File Offset: 0x0029F368
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

		// Token: 0x06006D10 RID: 27920 RVA: 0x002A11DC File Offset: 0x0029F3DC
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

		// Token: 0x06006D11 RID: 27921 RVA: 0x002A1250 File Offset: 0x0029F450
		public static void Stop(GameObject target)
		{
			Component[] array = target.GetComponents<iTween>();
			array = array;
			for (int i = 0; i < array.Length; i++)
			{
				((iTween)array[i]).Dispose();
			}
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x002A1284 File Offset: 0x0029F484
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

		// Token: 0x06006D13 RID: 27923 RVA: 0x002A12EC File Offset: 0x0029F4EC
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

		// Token: 0x06006D14 RID: 27924 RVA: 0x002A1350 File Offset: 0x0029F550
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

		// Token: 0x06006D15 RID: 27925 RVA: 0x002A1394 File Offset: 0x0029F594
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

		// Token: 0x06006D16 RID: 27926 RVA: 0x002A144C File Offset: 0x0029F64C
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

		// Token: 0x06006D17 RID: 27927 RVA: 0x002A14E4 File Offset: 0x0029F6E4
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

		// Token: 0x06006D18 RID: 27928 RVA: 0x002A152E File Offset: 0x0029F72E
		private iTween(Hashtable h)
		{
			this.tweenArguments = h;
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x002A153D File Offset: 0x0029F73D
		private void Awake()
		{
			this.thisTransform = base.transform;
			this.RetrieveArgs();
			this.lastRealTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x002A155C File Offset: 0x0029F75C
		private IEnumerator Start()
		{
			if (this.delay > 0f)
			{
				yield return base.StartCoroutine("TweenDelay");
			}
			this.TweenStart();
			yield break;
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x002A156C File Offset: 0x0029F76C
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

		// Token: 0x06006D1C RID: 27932 RVA: 0x002A15C8 File Offset: 0x0029F7C8
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

		// Token: 0x06006D1D RID: 27933 RVA: 0x002A1624 File Offset: 0x0029F824
		private void LateUpdate()
		{
			if (this.tweenArguments.Contains("looktarget") && this.isRunning && (this.type == "move" || this.type == "shake" || this.type == "punch"))
			{
				iTween.LookUpdate(base.gameObject, this.tweenArguments);
			}
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x002A1692 File Offset: 0x0029F892
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

		// Token: 0x06006D1F RID: 27935 RVA: 0x002A16CB File Offset: 0x0029F8CB
		private void OnDisable()
		{
			this.DisableKinematic();
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x002A16D4 File Offset: 0x0029F8D4
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

		// Token: 0x06006D21 RID: 27937 RVA: 0x002A1734 File Offset: 0x0029F934
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

		// Token: 0x06006D22 RID: 27938 RVA: 0x002A17B0 File Offset: 0x0029F9B0
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

		// Token: 0x06006D23 RID: 27939 RVA: 0x002A1898 File Offset: 0x0029FA98
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

		// Token: 0x06006D24 RID: 27940 RVA: 0x002A199C File Offset: 0x0029FB9C
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

		// Token: 0x06006D25 RID: 27941 RVA: 0x002A19F4 File Offset: 0x0029FBF4
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

		// Token: 0x06006D26 RID: 27942 RVA: 0x002A1B98 File Offset: 0x0029FD98
		private static string GenerateID()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06006D27 RID: 27943 RVA: 0x002A1BB8 File Offset: 0x0029FDB8
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

		// Token: 0x06006D28 RID: 27944 RVA: 0x002A2074 File Offset: 0x002A0274
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

		// Token: 0x06006D29 RID: 27945 RVA: 0x002A2370 File Offset: 0x002A0570
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

		// Token: 0x06006D2A RID: 27946 RVA: 0x002A23F4 File Offset: 0x002A05F4
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

		// Token: 0x06006D2B RID: 27947 RVA: 0x002A24C8 File Offset: 0x002A06C8
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

		// Token: 0x06006D2C RID: 27948 RVA: 0x002A2524 File Offset: 0x002A0724
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

		// Token: 0x06006D2D RID: 27949 RVA: 0x00004095 File Offset: 0x00002295
		private void EnableKinematic()
		{
		}

		// Token: 0x06006D2E RID: 27950 RVA: 0x00004095 File Offset: 0x00002295
		private void DisableKinematic()
		{
		}

		// Token: 0x06006D2F RID: 27951 RVA: 0x002A2674 File Offset: 0x002A0874
		private void ResumeDelay()
		{
			base.StartCoroutine("TweenDelay");
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x002A2682 File Offset: 0x002A0882
		private float linear(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value);
		}

		// Token: 0x06006D31 RID: 27953 RVA: 0x002A268C File Offset: 0x002A088C
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

		// Token: 0x06006D32 RID: 27954 RVA: 0x002A26F8 File Offset: 0x002A08F8
		private float spring(float start, float end, float value)
		{
			value = Mathf.Clamp01(value);
			value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
			return start + (end - start) * value;
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x002A275C File Offset: 0x002A095C
		private float easeInQuad(float start, float end, float value)
		{
			end -= start;
			return end * value * value + start;
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x002A276A File Offset: 0x002A096A
		private float easeOutQuad(float start, float end, float value)
		{
			end -= start;
			return -end * value * (value - 2f) + start;
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x002A2780 File Offset: 0x002A0980
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

		// Token: 0x06006D36 RID: 27958 RVA: 0x002A27D4 File Offset: 0x002A09D4
		private float easeInCubic(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value + start;
		}

		// Token: 0x06006D37 RID: 27959 RVA: 0x002A27E4 File Offset: 0x002A09E4
		private float easeOutCubic(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value + 1f) + start;
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x002A2804 File Offset: 0x002A0A04
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

		// Token: 0x06006D39 RID: 27961 RVA: 0x002A2855 File Offset: 0x002A0A55
		private float easeInQuart(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value + start;
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x002A2867 File Offset: 0x002A0A67
		private float easeOutQuart(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return -end * (value * value * value * value - 1f) + start;
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x002A288C File Offset: 0x002A0A8C
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

		// Token: 0x06006D3C RID: 27964 RVA: 0x002A28E2 File Offset: 0x002A0AE2
		private float easeInQuint(float start, float end, float value)
		{
			end -= start;
			return end * value * value * value * value * value + start;
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x002A28F6 File Offset: 0x002A0AF6
		private float easeOutQuint(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * (value * value * value * value * value + 1f) + start;
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x002A291C File Offset: 0x002A0B1C
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

		// Token: 0x06006D3F RID: 27967 RVA: 0x002A2975 File Offset: 0x002A0B75
		private float easeInSine(float start, float end, float value)
		{
			end -= start;
			return -end * Mathf.Cos(value * 1.5707964f) + end + start;
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x002A298F File Offset: 0x002A0B8F
		private float easeOutSine(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Sin(value * 1.5707964f) + start;
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x002A29A6 File Offset: 0x002A0BA6
		private float easeInOutSine(float start, float end, float value)
		{
			end -= start;
			return -end * 0.5f * (Mathf.Cos(3.1415927f * value) - 1f) + start;
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x002A29CA File Offset: 0x002A0BCA
		private float easeInExpo(float start, float end, float value)
		{
			end -= start;
			return end * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x002A29EC File Offset: 0x002A0BEC
		private float easeOutExpo(float start, float end, float value)
		{
			end -= start;
			return end * (-Mathf.Pow(2f, -10f * value) + 1f) + start;
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x002A2A10 File Offset: 0x002A0C10
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

		// Token: 0x06006D45 RID: 27973 RVA: 0x002A2A80 File Offset: 0x002A0C80
		private float easeInCirc(float start, float end, float value)
		{
			end -= start;
			return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x002A2AA0 File Offset: 0x002A0CA0
		private float easeOutCirc(float start, float end, float value)
		{
			value -= 1f;
			end -= start;
			return end * Mathf.Sqrt(1f - value * value) + start;
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x002A2AC4 File Offset: 0x002A0CC4
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

		// Token: 0x06006D48 RID: 27976 RVA: 0x002A2B30 File Offset: 0x002A0D30
		private float easeInBounce(float start, float end, float value)
		{
			end -= start;
			float num = 1f;
			return end - this.easeOutBounce(0f, end, num - value) + start;
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x002A2B5C File Offset: 0x002A0D5C
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

		// Token: 0x06006D4A RID: 27978 RVA: 0x002A2BF8 File Offset: 0x002A0DF8
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

		// Token: 0x06006D4B RID: 27979 RVA: 0x002A2C5C File Offset: 0x002A0E5C
		private float easeInBack(float start, float end, float value)
		{
			end -= start;
			value /= 1f;
			float num = 1.70158f;
			return end * value * value * ((num + 1f) * value - num) + start;
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x002A2C90 File Offset: 0x002A0E90
		private float easeOutBack(float start, float end, float value)
		{
			float num = 1.70158f;
			end -= start;
			value -= 1f;
			return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x002A2CCC File Offset: 0x002A0ECC
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

		// Token: 0x06006D4E RID: 27982 RVA: 0x002A2D48 File Offset: 0x002A0F48
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

		// Token: 0x06006D4F RID: 27983 RVA: 0x002A2DBC File Offset: 0x002A0FBC
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

		// Token: 0x06006D50 RID: 27984 RVA: 0x002A2E64 File Offset: 0x002A1064
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

		// Token: 0x06006D51 RID: 27985 RVA: 0x002A2F04 File Offset: 0x002A1104
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

		// Token: 0x04005AFA RID: 23290
		public static List<Hashtable> tweens = new List<Hashtable>();

		// Token: 0x04005AFB RID: 23291
		private static GameObject cameraFade;

		// Token: 0x04005AFC RID: 23292
		public string id;

		// Token: 0x04005AFD RID: 23293
		public string type;

		// Token: 0x04005AFE RID: 23294
		public string method;

		// Token: 0x04005AFF RID: 23295
		public iTween.EaseType easeType;

		// Token: 0x04005B00 RID: 23296
		public float time;

		// Token: 0x04005B01 RID: 23297
		public float delay;

		// Token: 0x04005B02 RID: 23298
		public iTween.LoopType loopType;

		// Token: 0x04005B03 RID: 23299
		public bool isRunning;

		// Token: 0x04005B04 RID: 23300
		public bool isPaused;

		// Token: 0x04005B05 RID: 23301
		public string _name;

		// Token: 0x04005B06 RID: 23302
		private float runningTime;

		// Token: 0x04005B07 RID: 23303
		private float percentage;

		// Token: 0x04005B08 RID: 23304
		private float delayStarted;

		// Token: 0x04005B09 RID: 23305
		private bool kinematic;

		// Token: 0x04005B0A RID: 23306
		private bool isLocal;

		// Token: 0x04005B0B RID: 23307
		private bool loop;

		// Token: 0x04005B0C RID: 23308
		private bool reverse;

		// Token: 0x04005B0D RID: 23309
		private bool wasPaused;

		// Token: 0x04005B0E RID: 23310
		private bool physics;

		// Token: 0x04005B0F RID: 23311
		private Hashtable tweenArguments;

		// Token: 0x04005B10 RID: 23312
		private Space space;

		// Token: 0x04005B11 RID: 23313
		private iTween.EasingFunction ease;

		// Token: 0x04005B12 RID: 23314
		private iTween.ApplyTween apply;

		// Token: 0x04005B13 RID: 23315
		private AudioSource audioSource;

		// Token: 0x04005B14 RID: 23316
		private Vector3[] vector3s;

		// Token: 0x04005B15 RID: 23317
		private Vector2[] vector2s;

		// Token: 0x04005B16 RID: 23318
		private Color[,] colors;

		// Token: 0x04005B17 RID: 23319
		private float[] floats;

		// Token: 0x04005B18 RID: 23320
		private Rect[] rects;

		// Token: 0x04005B19 RID: 23321
		private iTween.CRSpline path;

		// Token: 0x04005B1A RID: 23322
		private Vector3 preUpdate;

		// Token: 0x04005B1B RID: 23323
		private Vector3 postUpdate;

		// Token: 0x04005B1C RID: 23324
		private iTween.NamedValueColor namedcolorvalue;

		// Token: 0x04005B1D RID: 23325
		private float lastRealTime;

		// Token: 0x04005B1E RID: 23326
		private bool useRealTime;

		// Token: 0x04005B1F RID: 23327
		private Transform thisTransform;

		// Token: 0x0200171A RID: 5914
		// (Invoke) Token: 0x0600892D RID: 35117
		private delegate float EasingFunction(float start, float end, float Value);

		// Token: 0x0200171B RID: 5915
		// (Invoke) Token: 0x06008931 RID: 35121
		private delegate void ApplyTween();

		// Token: 0x0200171C RID: 5916
		public enum EaseType
		{
			// Token: 0x040074D5 RID: 29909
			easeInQuad,
			// Token: 0x040074D6 RID: 29910
			easeOutQuad,
			// Token: 0x040074D7 RID: 29911
			easeInOutQuad,
			// Token: 0x040074D8 RID: 29912
			easeInCubic,
			// Token: 0x040074D9 RID: 29913
			easeOutCubic,
			// Token: 0x040074DA RID: 29914
			easeInOutCubic,
			// Token: 0x040074DB RID: 29915
			easeInQuart,
			// Token: 0x040074DC RID: 29916
			easeOutQuart,
			// Token: 0x040074DD RID: 29917
			easeInOutQuart,
			// Token: 0x040074DE RID: 29918
			easeInQuint,
			// Token: 0x040074DF RID: 29919
			easeOutQuint,
			// Token: 0x040074E0 RID: 29920
			easeInOutQuint,
			// Token: 0x040074E1 RID: 29921
			easeInSine,
			// Token: 0x040074E2 RID: 29922
			easeOutSine,
			// Token: 0x040074E3 RID: 29923
			easeInOutSine,
			// Token: 0x040074E4 RID: 29924
			easeInExpo,
			// Token: 0x040074E5 RID: 29925
			easeOutExpo,
			// Token: 0x040074E6 RID: 29926
			easeInOutExpo,
			// Token: 0x040074E7 RID: 29927
			easeInCirc,
			// Token: 0x040074E8 RID: 29928
			easeOutCirc,
			// Token: 0x040074E9 RID: 29929
			easeInOutCirc,
			// Token: 0x040074EA RID: 29930
			linear,
			// Token: 0x040074EB RID: 29931
			spring,
			// Token: 0x040074EC RID: 29932
			easeInBounce,
			// Token: 0x040074ED RID: 29933
			easeOutBounce,
			// Token: 0x040074EE RID: 29934
			easeInOutBounce,
			// Token: 0x040074EF RID: 29935
			easeInBack,
			// Token: 0x040074F0 RID: 29936
			easeOutBack,
			// Token: 0x040074F1 RID: 29937
			easeInOutBack,
			// Token: 0x040074F2 RID: 29938
			easeInElastic,
			// Token: 0x040074F3 RID: 29939
			easeOutElastic,
			// Token: 0x040074F4 RID: 29940
			easeInOutElastic,
			// Token: 0x040074F5 RID: 29941
			punch
		}

		// Token: 0x0200171D RID: 5917
		public enum LoopType
		{
			// Token: 0x040074F7 RID: 29943
			none,
			// Token: 0x040074F8 RID: 29944
			loop,
			// Token: 0x040074F9 RID: 29945
			pingPong
		}

		// Token: 0x0200171E RID: 5918
		public enum NamedValueColor
		{
			// Token: 0x040074FB RID: 29947
			_Color,
			// Token: 0x040074FC RID: 29948
			_SpecColor,
			// Token: 0x040074FD RID: 29949
			_Emission,
			// Token: 0x040074FE RID: 29950
			_ReflectColor
		}

		// Token: 0x0200171F RID: 5919
		public static class Defaults
		{
			// Token: 0x040074FF RID: 29951
			public static float time = 1f;

			// Token: 0x04007500 RID: 29952
			public static float delay = 0f;

			// Token: 0x04007501 RID: 29953
			public static iTween.NamedValueColor namedColorValue = iTween.NamedValueColor._Color;

			// Token: 0x04007502 RID: 29954
			public static iTween.LoopType loopType = iTween.LoopType.none;

			// Token: 0x04007503 RID: 29955
			public static iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

			// Token: 0x04007504 RID: 29956
			public static float lookSpeed = 3f;

			// Token: 0x04007505 RID: 29957
			public static bool isLocal = false;

			// Token: 0x04007506 RID: 29958
			public static Space space = 1;

			// Token: 0x04007507 RID: 29959
			public static bool orientToPath = false;

			// Token: 0x04007508 RID: 29960
			public static Color color = Color.white;

			// Token: 0x04007509 RID: 29961
			public static float updateTimePercentage = 0.05f;

			// Token: 0x0400750A RID: 29962
			public static float updateTime = 1f * iTween.Defaults.updateTimePercentage;

			// Token: 0x0400750B RID: 29963
			public static int cameraFadeDepth = 999999;

			// Token: 0x0400750C RID: 29964
			public static float lookAhead = 0.05f;

			// Token: 0x0400750D RID: 29965
			public static bool useRealTime = false;

			// Token: 0x0400750E RID: 29966
			public static Vector3 up = Vector3.up;
		}

		// Token: 0x02001720 RID: 5920
		private class CRSpline
		{
			// Token: 0x06008935 RID: 35125 RVA: 0x002EA39C File Offset: 0x002E859C
			public CRSpline(params Vector3[] pts)
			{
				this.pts = new Vector3[pts.Length];
				Array.Copy(pts, this.pts, pts.Length);
			}

			// Token: 0x06008936 RID: 35126 RVA: 0x002EA3C4 File Offset: 0x002E85C4
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

			// Token: 0x0400750F RID: 29967
			public Vector3[] pts;
		}
	}
}
