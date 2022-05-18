using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000026 RID: 38
public class LTDescr
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000182 RID: 386 RVA: 0x00005316 File Offset: 0x00003516
	// (set) Token: 0x06000183 RID: 387 RVA: 0x0000531E File Offset: 0x0000351E
	public Vector3 from
	{
		get
		{
			return this.fromInternal;
		}
		set
		{
			this.fromInternal = value;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000184 RID: 388 RVA: 0x00005327 File Offset: 0x00003527
	// (set) Token: 0x06000185 RID: 389 RVA: 0x0000532F File Offset: 0x0000352F
	public Vector3 to
	{
		get
		{
			return this.toInternal;
		}
		set
		{
			this.toInternal = value;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000186 RID: 390 RVA: 0x00005338 File Offset: 0x00003538
	// (set) Token: 0x06000187 RID: 391 RVA: 0x00005340 File Offset: 0x00003540
	public LTDescr.ActionMethodDelegate easeInternal { get; set; }

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000188 RID: 392 RVA: 0x00005349 File Offset: 0x00003549
	// (set) Token: 0x06000189 RID: 393 RVA: 0x00005351 File Offset: 0x00003551
	public LTDescr.ActionMethodDelegate initInternal { get; set; }

	// Token: 0x0600018A RID: 394 RVA: 0x000628F4 File Offset: 0x00060AF4
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			(this.trans != null) ? ("name:" + this.trans.gameObject.name) : "gameObject:null",
			" toggle:",
			this.toggle.ToString(),
			" passed:",
			this.passed,
			" time:",
			this.time,
			" delay:",
			this.delay,
			" direction:",
			this.direction,
			" from:",
			this.from,
			" to:",
			this.to,
			" diff:",
			this.diff,
			" type:",
			this.type,
			" ease:",
			this.easeType,
			" useEstimatedTime:",
			this.useEstimatedTime.ToString(),
			" id:",
			this.id,
			" hasInitiliazed:",
			this.hasInitiliazed.ToString()
		});
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00005374 File Offset: 0x00003574
	[Obsolete("Use 'LeanTween.cancel( id )' instead")]
	public LTDescr cancel(GameObject gameObject)
	{
		if (gameObject == this.trans.gameObject)
		{
			LeanTween.removeTween((int)this._id, this.uniqueId);
		}
		return this;
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600018D RID: 397 RVA: 0x0000539B File Offset: 0x0000359B
	public int uniqueId
	{
		get
		{
			return (int)(this._id | this.counter << 16);
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600018E RID: 398 RVA: 0x000053AD File Offset: 0x000035AD
	public int id
	{
		get
		{
			return this.uniqueId;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600018F RID: 399 RVA: 0x000053B5 File Offset: 0x000035B5
	// (set) Token: 0x06000190 RID: 400 RVA: 0x000053BD File Offset: 0x000035BD
	public LTDescrOptional optional
	{
		get
		{
			return this._optional;
		}
		set
		{
			this._optional = this.optional;
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00062A70 File Offset: 0x00060C70
	public void reset()
	{
		this.toggle = (this.useRecursion = (this.usesNormalDt = true));
		this.trans = null;
		this.spriteRen = null;
		this.passed = (this.delay = (this.lastVal = 0f));
		this.hasUpdateCallback = (this.useEstimatedTime = (this.useFrames = (this.hasInitiliazed = (this.onCompleteOnRepeat = (this.destroyOnComplete = (this.onCompleteOnStart = (this.useManualTime = (this.hasExtraOnCompletes = false))))))));
		this.easeType = LeanTweenType.linear;
		this.loopType = LeanTweenType.once;
		this.loopCount = 0;
		this.direction = (this.directionLast = (this.overshoot = (this.scale = 1f)));
		this.period = 0.3f;
		this.speed = -1f;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeLinear);
		this.from = (this.to = Vector3.zero);
		this._optional.reset();
	}

	// Token: 0x06000192 RID: 402 RVA: 0x000053CB File Offset: 0x000035CB
	public LTDescr setMoveX()
	{
		this.type = TweenAction.MOVE_X;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.position.x;
		};
		this.easeInternal = delegate()
		{
			this.trans.position = new Vector3(this.easeMethod().x, this.trans.position.y, this.trans.position.z);
		};
		return this;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000053F9 File Offset: 0x000035F9
	public LTDescr setMoveY()
	{
		this.type = TweenAction.MOVE_Y;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.position.y;
		};
		this.easeInternal = delegate()
		{
			this.trans.position = new Vector3(this.trans.position.x, this.easeMethod().x, this.trans.position.z);
		};
		return this;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00005427 File Offset: 0x00003627
	public LTDescr setMoveZ()
	{
		this.type = TweenAction.MOVE_Z;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.position.z;
		};
		this.easeInternal = delegate()
		{
			this.trans.position = new Vector3(this.trans.position.x, this.trans.position.y, this.easeMethod().x);
		};
		return this;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00005455 File Offset: 0x00003655
	public LTDescr setMoveLocalX()
	{
		this.type = TweenAction.MOVE_LOCAL_X;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.localPosition.x;
		};
		this.easeInternal = delegate()
		{
			this.trans.localPosition = new Vector3(this.easeMethod().x, this.trans.localPosition.y, this.trans.localPosition.z);
		};
		return this;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00005483 File Offset: 0x00003683
	public LTDescr setMoveLocalY()
	{
		this.type = TweenAction.MOVE_LOCAL_Y;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.localPosition.y;
		};
		this.easeInternal = delegate()
		{
			this.trans.localPosition = new Vector3(this.trans.localPosition.x, this.easeMethod().x, this.trans.localPosition.z);
		};
		return this;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000054B1 File Offset: 0x000036B1
	public LTDescr setMoveLocalZ()
	{
		this.type = TweenAction.MOVE_LOCAL_Z;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.localPosition.z;
		};
		this.easeInternal = delegate()
		{
			this.trans.localPosition = new Vector3(this.trans.localPosition.x, this.trans.localPosition.y, this.easeMethod().x);
		};
		return this;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x000054DF File Offset: 0x000036DF
	private void initFromInternal()
	{
		this.fromInternal.x = 0f;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000054F1 File Offset: 0x000036F1
	public LTDescr setMoveCurved()
	{
		this.type = TweenAction.MOVE_CURVED;
		this.initInternal = new LTDescr.ActionMethodDelegate(this.initFromInternal);
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			if (!this._optional.path.orientToPath)
			{
				this.trans.position = this._optional.path.point(LTDescr.val);
				return;
			}
			if (this._optional.path.orientToPath2d)
			{
				this._optional.path.place2d(this.trans, LTDescr.val);
				return;
			}
			this._optional.path.place(this.trans, LTDescr.val);
		};
		return this;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000551F File Offset: 0x0000371F
	public LTDescr setMoveCurvedLocal()
	{
		this.type = TweenAction.MOVE_CURVED_LOCAL;
		this.initInternal = new LTDescr.ActionMethodDelegate(this.initFromInternal);
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			if (!this._optional.path.orientToPath)
			{
				this.trans.localPosition = this._optional.path.point(LTDescr.val);
				return;
			}
			if (this._optional.path.orientToPath2d)
			{
				this._optional.path.placeLocal2d(this.trans, LTDescr.val);
				return;
			}
			this._optional.path.placeLocal(this.trans, LTDescr.val);
		};
		return this;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000554D File Offset: 0x0000374D
	public LTDescr setMoveSpline()
	{
		this.type = TweenAction.MOVE_SPLINE;
		this.initInternal = new LTDescr.ActionMethodDelegate(this.initFromInternal);
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			if (!this._optional.spline.orientToPath)
			{
				this.trans.position = this._optional.spline.point(LTDescr.val);
				return;
			}
			if (this._optional.spline.orientToPath2d)
			{
				this._optional.spline.place2d(this.trans, LTDescr.val);
				return;
			}
			this._optional.spline.place(this.trans, LTDescr.val);
		};
		return this;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000557B File Offset: 0x0000377B
	public LTDescr setMoveSplineLocal()
	{
		this.type = TweenAction.MOVE_SPLINE_LOCAL;
		this.initInternal = new LTDescr.ActionMethodDelegate(this.initFromInternal);
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			if (!this._optional.spline.orientToPath)
			{
				this.trans.localPosition = this._optional.spline.point(LTDescr.val);
				return;
			}
			if (this._optional.spline.orientToPath2d)
			{
				this._optional.spline.placeLocal2d(this.trans, LTDescr.val);
				return;
			}
			this._optional.spline.placeLocal(this.trans, LTDescr.val);
		};
		return this;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x000055AA File Offset: 0x000037AA
	public LTDescr setScaleX()
	{
		this.type = TweenAction.SCALE_X;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.localScale.x;
		};
		this.easeInternal = delegate()
		{
			this.trans.localScale = new Vector3(this.easeMethod().x, this.trans.localScale.y, this.trans.localScale.z);
		};
		return this;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000055D9 File Offset: 0x000037D9
	public LTDescr setScaleY()
	{
		this.type = TweenAction.SCALE_Y;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.localScale.y;
		};
		this.easeInternal = delegate()
		{
			this.trans.localScale = new Vector3(this.trans.localScale.x, this.easeMethod().x, this.trans.localScale.z);
		};
		return this;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00005608 File Offset: 0x00003808
	public LTDescr setScaleZ()
	{
		this.type = TweenAction.SCALE_Z;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.localScale.z;
		};
		this.easeInternal = delegate()
		{
			this.trans.localScale = new Vector3(this.trans.localScale.x, this.trans.localScale.y, this.easeMethod().x);
		};
		return this;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00005637 File Offset: 0x00003837
	public LTDescr setRotateX()
	{
		this.type = TweenAction.ROTATE_X;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.eulerAngles.x;
			this.toInternal.x = LeanTween.closestRot(this.fromInternal.x, this.toInternal.x);
		};
		this.easeInternal = delegate()
		{
			this.trans.eulerAngles = new Vector3(this.easeMethod().x, this.trans.eulerAngles.y, this.trans.eulerAngles.z);
		};
		return this;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00005666 File Offset: 0x00003866
	public LTDescr setRotateY()
	{
		this.type = TweenAction.ROTATE_Y;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.eulerAngles.y;
			this.toInternal.x = LeanTween.closestRot(this.fromInternal.x, this.toInternal.x);
		};
		this.easeInternal = delegate()
		{
			this.trans.eulerAngles = new Vector3(this.trans.eulerAngles.x, this.easeMethod().x, this.trans.eulerAngles.z);
		};
		return this;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00005695 File Offset: 0x00003895
	public LTDescr setRotateZ()
	{
		this.type = TweenAction.ROTATE_Z;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.eulerAngles.z;
			this.toInternal.x = LeanTween.closestRot(this.fromInternal.x, this.toInternal.x);
		};
		this.easeInternal = delegate()
		{
			this.trans.eulerAngles = new Vector3(this.trans.eulerAngles.x, this.trans.eulerAngles.y, this.easeMethod().x);
		};
		return this;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000056C4 File Offset: 0x000038C4
	public LTDescr setRotateAround()
	{
		this.type = TweenAction.ROTATE_AROUND;
		this.initInternal = delegate()
		{
			this.fromInternal.x = 0f;
			this._optional.origRotation = this.trans.rotation;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Vector3 localPosition = this.trans.localPosition;
			Vector3 vector = this.trans.TransformPoint(this._optional.point);
			this.trans.RotateAround(vector, this._optional.axis, -this._optional.lastVal);
			Vector3 vector2 = localPosition - this.trans.localPosition;
			this.trans.localPosition = localPosition - vector2;
			this.trans.rotation = this._optional.origRotation;
			vector = this.trans.TransformPoint(this._optional.point);
			this.trans.RotateAround(vector, this._optional.axis, LTDescr.val);
			this._optional.lastVal = LTDescr.val;
		};
		return this;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000056F3 File Offset: 0x000038F3
	public LTDescr setRotateAroundLocal()
	{
		this.type = TweenAction.ROTATE_AROUND_LOCAL;
		this.initInternal = delegate()
		{
			this.fromInternal.x = 0f;
			this._optional.origRotation = this.trans.localRotation;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Vector3 localPosition = this.trans.localPosition;
			this.trans.RotateAround(this.trans.TransformPoint(this._optional.point), this.trans.TransformDirection(this._optional.axis), -this._optional.lastVal);
			Vector3 vector = localPosition - this.trans.localPosition;
			this.trans.localPosition = localPosition - vector;
			this.trans.localRotation = this._optional.origRotation;
			Vector3 vector2 = this.trans.TransformPoint(this._optional.point);
			this.trans.RotateAround(vector2, this.trans.TransformDirection(this._optional.axis), LTDescr.val);
			this._optional.lastVal = LTDescr.val;
		};
		return this;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00005722 File Offset: 0x00003922
	public LTDescr setAlpha()
	{
		this.type = TweenAction.ALPHA;
		this.initInternal = delegate()
		{
			SpriteRenderer component = this.trans.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				this.fromInternal.x = component.color.a;
			}
			else if (this.trans.GetComponent<Renderer>() != null && this.trans.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				this.fromInternal.x = this.trans.GetComponent<Renderer>().material.color.a;
			}
			else if (this.trans.GetComponent<Renderer>() != null && this.trans.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color = this.trans.GetComponent<Renderer>().material.GetColor("_TintColor");
				this.fromInternal.x = color.a;
			}
			else if (this.trans.childCount > 0)
			{
				foreach (object obj in this.trans)
				{
					Transform transform = (Transform)obj;
					if (transform.gameObject.GetComponent<Renderer>() != null)
					{
						Color color2 = transform.gameObject.GetComponent<Renderer>().material.color;
						this.fromInternal.x = color2.a;
						break;
					}
				}
			}
			this.easeInternal = delegate()
			{
				LTDescr.val = this.easeMethod().x;
				if (this.spriteRen != null)
				{
					this.spriteRen.color = new Color(this.spriteRen.color.r, this.spriteRen.color.g, this.spriteRen.color.b, LTDescr.val);
					LTDescr.alphaRecursiveSprite(this.trans, LTDescr.val);
					return;
				}
				LTDescr.alphaRecursive(this.trans, LTDescr.val, this.useRecursion);
			};
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			if (this.spriteRen != null)
			{
				this.spriteRen.color = new Color(this.spriteRen.color.r, this.spriteRen.color.g, this.spriteRen.color.b, LTDescr.val);
				LTDescr.alphaRecursiveSprite(this.trans, LTDescr.val);
				return;
			}
			LTDescr.alphaRecursive(this.trans, LTDescr.val, this.useRecursion);
		};
		return this;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00005751 File Offset: 0x00003951
	public LTDescr setTextAlpha()
	{
		this.type = TweenAction.TEXT_ALPHA;
		this.initInternal = delegate()
		{
			this.uiText = this.trans.GetComponent<Text>();
			this.fromInternal.x = ((this.uiText != null) ? this.uiText.color.a : 1f);
		};
		this.easeInternal = delegate()
		{
			LTDescr.textAlphaRecursive(this.trans, this.easeMethod().x, this.useRecursion);
		};
		return this;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00005780 File Offset: 0x00003980
	public LTDescr setAlphaVertex()
	{
		this.type = TweenAction.ALPHA_VERTEX;
		this.initInternal = delegate()
		{
			this.fromInternal.x = (float)this.trans.GetComponent<MeshFilter>().mesh.colors32[0].a;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Mesh mesh = this.trans.GetComponent<MeshFilter>().mesh;
			Vector3[] vertices = mesh.vertices;
			Color32[] array = new Color32[vertices.Length];
			if (array.Length == 0)
			{
				Color32 color;
				color..ctor(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				array = new Color32[mesh.vertices.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = color;
				}
				mesh.colors32 = array;
			}
			Color32 color2 = mesh.colors32[0];
			color2 = new Color((float)color2.r, (float)color2.g, (float)color2.b, LTDescr.val);
			for (int j = 0; j < vertices.Length; j++)
			{
				array[j] = color2;
			}
			mesh.colors32 = array;
		};
		return this;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x000057AF File Offset: 0x000039AF
	public LTDescr setColor()
	{
		this.type = TweenAction.COLOR;
		this.initInternal = delegate()
		{
			SpriteRenderer component = this.trans.GetComponent<SpriteRenderer>();
			if (component != null)
			{
				this.setFromColor(component.color);
				return;
			}
			if (this.trans.GetComponent<Renderer>() != null && this.trans.GetComponent<Renderer>().material.HasProperty("_Color"))
			{
				Color color = this.trans.GetComponent<Renderer>().material.color;
				this.setFromColor(color);
				return;
			}
			if (this.trans.GetComponent<Renderer>() != null && this.trans.GetComponent<Renderer>().material.HasProperty("_TintColor"))
			{
				Color color2 = this.trans.GetComponent<Renderer>().material.GetColor("_TintColor");
				this.setFromColor(color2);
				return;
			}
			if (this.trans.childCount > 0)
			{
				foreach (object obj in this.trans)
				{
					Transform transform = (Transform)obj;
					if (transform.gameObject.GetComponent<Renderer>() != null)
					{
						Color color3 = transform.gameObject.GetComponent<Renderer>().material.color;
						this.setFromColor(color3);
						break;
					}
				}
			}
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Color color = LTDescr.tweenColor(this, LTDescr.val);
			if (this.spriteRen != null)
			{
				this.spriteRen.color = color;
				LTDescr.colorRecursiveSprite(this.trans, color);
			}
			else if (this.type == TweenAction.COLOR)
			{
				LTDescr.colorRecursive(this.trans, color, this.useRecursion);
			}
			if (LTDescr.dt != 0f && this._optional.onUpdateColor != null)
			{
				this._optional.onUpdateColor(color);
				return;
			}
			if (LTDescr.dt != 0f && this._optional.onUpdateColorObject != null)
			{
				this._optional.onUpdateColorObject(color, this._optional.onUpdateParam);
			}
		};
		return this;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000057DE File Offset: 0x000039DE
	public LTDescr setCallbackColor()
	{
		this.type = TweenAction.CALLBACK_COLOR;
		this.initInternal = delegate()
		{
			this.diff = new Vector3(1f, 0f, 0f);
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Color color = LTDescr.tweenColor(this, LTDescr.val);
			if (this.spriteRen != null)
			{
				this.spriteRen.color = color;
				LTDescr.colorRecursiveSprite(this.trans, color);
			}
			else if (this.type == TweenAction.COLOR)
			{
				LTDescr.colorRecursive(this.trans, color, this.useRecursion);
			}
			if (LTDescr.dt != 0f && this._optional.onUpdateColor != null)
			{
				this._optional.onUpdateColor(color);
				return;
			}
			if (LTDescr.dt != 0f && this._optional.onUpdateColorObject != null)
			{
				this._optional.onUpdateColorObject(color, this._optional.onUpdateParam);
			}
		};
		return this;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000580D File Offset: 0x00003A0D
	public LTDescr setTextColor()
	{
		this.type = TweenAction.TEXT_COLOR;
		this.initInternal = delegate()
		{
			this.uiText = this.trans.GetComponent<Text>();
			this.setFromColor((this.uiText != null) ? this.uiText.color : Color.white);
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Color color = LTDescr.tweenColor(this, LTDescr.val);
			this.uiText.color = color;
			if (LTDescr.dt != 0f && this._optional.onUpdateColor != null)
			{
				this._optional.onUpdateColor(color);
			}
			if (this.useRecursion && this.trans.childCount > 0)
			{
				LTDescr.textColorRecursive(this.trans, color);
			}
		};
		return this;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000583C File Offset: 0x00003A3C
	public LTDescr setCanvasAlpha()
	{
		this.type = TweenAction.CANVAS_ALPHA;
		this.initInternal = delegate()
		{
			this.uiImage = this.trans.GetComponent<Image>();
			if (this.uiImage != null)
			{
				this.fromInternal.x = this.uiImage.color.a;
				return;
			}
			this.rawImage = this.trans.GetComponent<RawImage>();
			if (this.rawImage != null)
			{
				this.fromInternal.x = this.rawImage.color.a;
				return;
			}
			this.fromInternal.x = 1f;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			if (this.uiImage != null)
			{
				Color color = this.uiImage.color;
				color.a = LTDescr.val;
				this.uiImage.color = color;
			}
			else if (this.rawImage != null)
			{
				Color color2 = this.rawImage.color;
				color2.a = LTDescr.val;
				this.rawImage.color = color2;
			}
			if (this.useRecursion)
			{
				LTDescr.alphaRecursive(this.rectTransform, LTDescr.val, 0);
				LTDescr.textAlphaChildrenRecursive(this.rectTransform, LTDescr.val, true);
			}
		};
		return this;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000586B File Offset: 0x00003A6B
	public LTDescr setCanvasGroupAlpha()
	{
		this.type = TweenAction.CANVASGROUP_ALPHA;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.trans.GetComponent<CanvasGroup>().alpha;
		};
		this.easeInternal = delegate()
		{
			this.trans.GetComponent<CanvasGroup>().alpha = this.easeMethod().x;
		};
		return this;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000589A File Offset: 0x00003A9A
	public LTDescr setCanvasColor()
	{
		this.type = TweenAction.CANVAS_COLOR;
		this.initInternal = delegate()
		{
			this.uiImage = this.trans.GetComponent<Image>();
			if (this.uiImage == null)
			{
				this.rawImage = this.trans.GetComponent<RawImage>();
				this.setFromColor((this.rawImage != null) ? this.rawImage.color : Color.white);
				return;
			}
			this.setFromColor(this.uiImage.color);
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			Color color = LTDescr.tweenColor(this, LTDescr.val);
			if (this.uiImage != null)
			{
				this.uiImage.color = color;
			}
			else if (this.rawImage != null)
			{
				this.rawImage.color = color;
			}
			if (LTDescr.dt != 0f && this._optional.onUpdateColor != null)
			{
				this._optional.onUpdateColor(color);
			}
			if (this.useRecursion)
			{
				LTDescr.colorRecursive(this.rectTransform, color);
			}
		};
		return this;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000058C9 File Offset: 0x00003AC9
	public LTDescr setCanvasMoveX()
	{
		this.type = TweenAction.CANVAS_MOVE_X;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.rectTransform.anchoredPosition3D.x;
		};
		this.easeInternal = delegate()
		{
			Vector3 anchoredPosition3D = this.rectTransform.anchoredPosition3D;
			this.rectTransform.anchoredPosition3D = new Vector3(this.easeMethod().x, anchoredPosition3D.y, anchoredPosition3D.z);
		};
		return this;
	}

	// Token: 0x060001AF RID: 431 RVA: 0x000058F8 File Offset: 0x00003AF8
	public LTDescr setCanvasMoveY()
	{
		this.type = TweenAction.CANVAS_MOVE_Y;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.rectTransform.anchoredPosition3D.y;
		};
		this.easeInternal = delegate()
		{
			Vector3 anchoredPosition3D = this.rectTransform.anchoredPosition3D;
			this.rectTransform.anchoredPosition3D = new Vector3(anchoredPosition3D.x, this.easeMethod().x, anchoredPosition3D.z);
		};
		return this;
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00005927 File Offset: 0x00003B27
	public LTDescr setCanvasMoveZ()
	{
		this.type = TweenAction.CANVAS_MOVE_Z;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this.rectTransform.anchoredPosition3D.z;
		};
		this.easeInternal = delegate()
		{
			Vector3 anchoredPosition3D = this.rectTransform.anchoredPosition3D;
			this.rectTransform.anchoredPosition3D = new Vector3(anchoredPosition3D.x, anchoredPosition3D.y, this.easeMethod().x);
		};
		return this;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00005956 File Offset: 0x00003B56
	private void initCanvasRotateAround()
	{
		this.lastVal = 0f;
		this.fromInternal.x = 0f;
		this._optional.origRotation = this.rectTransform.rotation;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00005989 File Offset: 0x00003B89
	public LTDescr setCanvasRotateAround()
	{
		this.type = TweenAction.CANVAS_ROTATEAROUND;
		this.initInternal = new LTDescr.ActionMethodDelegate(this.initCanvasRotateAround);
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			RectTransform rectTransform = this.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			rectTransform.RotateAround(rectTransform.TransformPoint(this._optional.point), this._optional.axis, -LTDescr.val);
			Vector3 vector = localPosition - rectTransform.localPosition;
			rectTransform.localPosition = localPosition - vector;
			rectTransform.rotation = this._optional.origRotation;
			rectTransform.RotateAround(rectTransform.TransformPoint(this._optional.point), this._optional.axis, LTDescr.val);
		};
		return this;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x000059B8 File Offset: 0x00003BB8
	public LTDescr setCanvasRotateAroundLocal()
	{
		this.type = TweenAction.CANVAS_ROTATEAROUND_LOCAL;
		this.initInternal = new LTDescr.ActionMethodDelegate(this.initCanvasRotateAround);
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			RectTransform rectTransform = this.rectTransform;
			Vector3 localPosition = rectTransform.localPosition;
			rectTransform.RotateAround(rectTransform.TransformPoint(this._optional.point), rectTransform.TransformDirection(this._optional.axis), -LTDescr.val);
			Vector3 vector = localPosition - rectTransform.localPosition;
			rectTransform.localPosition = localPosition - vector;
			rectTransform.rotation = this._optional.origRotation;
			rectTransform.RotateAround(rectTransform.TransformPoint(this._optional.point), rectTransform.TransformDirection(this._optional.axis), LTDescr.val);
		};
		return this;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x000059E7 File Offset: 0x00003BE7
	public LTDescr setCanvasPlaySprite()
	{
		this.type = TweenAction.CANVAS_PLAYSPRITE;
		this.initInternal = delegate()
		{
			this.uiImage = this.trans.GetComponent<Image>();
			this.fromInternal.x = 0f;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			LTDescr.val = LTDescr.newVect.x;
			int num = (int)Mathf.Round(LTDescr.val);
			this.uiImage.sprite = this.sprites[num];
		};
		return this;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00005A16 File Offset: 0x00003C16
	public LTDescr setCanvasMove()
	{
		this.type = TweenAction.CANVAS_MOVE;
		this.initInternal = delegate()
		{
			this.fromInternal = this.rectTransform.anchoredPosition3D;
		};
		this.easeInternal = delegate()
		{
			this.rectTransform.anchoredPosition3D = this.easeMethod();
		};
		return this;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00005A45 File Offset: 0x00003C45
	public LTDescr setCanvasScale()
	{
		this.type = TweenAction.CANVAS_SCALE;
		this.initInternal = delegate()
		{
			this.from = this.rectTransform.localScale;
		};
		this.easeInternal = delegate()
		{
			this.rectTransform.localScale = this.easeMethod();
		};
		return this;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00005A74 File Offset: 0x00003C74
	public LTDescr setCanvasSizeDelta()
	{
		this.type = TweenAction.CANVAS_SIZEDELTA;
		this.initInternal = delegate()
		{
			this.from = this.rectTransform.sizeDelta;
		};
		this.easeInternal = delegate()
		{
			this.rectTransform.sizeDelta = this.easeMethod();
		};
		return this;
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00005AA3 File Offset: 0x00003CA3
	private void callback()
	{
		LTDescr.newVect = this.easeMethod();
		LTDescr.val = LTDescr.newVect.x;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00062B94 File Offset: 0x00060D94
	public LTDescr setCallback()
	{
		this.type = TweenAction.CALLBACK;
		this.initInternal = delegate()
		{
		};
		this.easeInternal = new LTDescr.ActionMethodDelegate(this.callback);
		return this;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00062BE4 File Offset: 0x00060DE4
	public LTDescr setValue3()
	{
		this.type = TweenAction.VALUE3;
		this.initInternal = delegate()
		{
		};
		this.easeInternal = new LTDescr.ActionMethodDelegate(this.callback);
		return this;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00005AC4 File Offset: 0x00003CC4
	public LTDescr setMove()
	{
		this.type = TweenAction.MOVE;
		this.initInternal = delegate()
		{
			this.from = this.trans.position;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			this.trans.position = LTDescr.newVect;
		};
		return this;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00005AF3 File Offset: 0x00003CF3
	public LTDescr setMoveLocal()
	{
		this.type = TweenAction.MOVE_LOCAL;
		this.initInternal = delegate()
		{
			this.from = this.trans.localPosition;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			this.trans.localPosition = LTDescr.newVect;
		};
		return this;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00005B22 File Offset: 0x00003D22
	public LTDescr setMoveToTransform()
	{
		this.type = TweenAction.MOVE_TO_TRANSFORM;
		this.initInternal = delegate()
		{
			this.from = this.trans.position;
		};
		this.easeInternal = delegate()
		{
			this.to = this._optional.toTrans.position;
			this.diff = this.to - this.from;
			this.diffDiv2 = this.diff * 0.5f;
			LTDescr.newVect = this.easeMethod();
			this.trans.position = LTDescr.newVect;
		};
		return this;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00005B51 File Offset: 0x00003D51
	public LTDescr setRotate()
	{
		this.type = TweenAction.ROTATE;
		this.initInternal = delegate()
		{
			this.from = this.trans.eulerAngles;
			this.to = new Vector3(LeanTween.closestRot(this.fromInternal.x, this.toInternal.x), LeanTween.closestRot(this.from.y, this.to.y), LeanTween.closestRot(this.from.z, this.to.z));
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			this.trans.eulerAngles = LTDescr.newVect;
		};
		return this;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00005B80 File Offset: 0x00003D80
	public LTDescr setRotateLocal()
	{
		this.type = TweenAction.ROTATE_LOCAL;
		this.initInternal = delegate()
		{
			this.from = this.trans.localEulerAngles;
			this.to = new Vector3(LeanTween.closestRot(this.fromInternal.x, this.toInternal.x), LeanTween.closestRot(this.from.y, this.to.y), LeanTween.closestRot(this.from.z, this.to.z));
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			this.trans.localEulerAngles = LTDescr.newVect;
		};
		return this;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00005BAF File Offset: 0x00003DAF
	public LTDescr setScale()
	{
		this.type = TweenAction.SCALE;
		this.initInternal = delegate()
		{
			this.from = this.trans.localScale;
		};
		this.easeInternal = delegate()
		{
			LTDescr.newVect = this.easeMethod();
			this.trans.localScale = LTDescr.newVect;
		};
		return this;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00005BDE File Offset: 0x00003DDE
	public LTDescr setGUIMove()
	{
		this.type = TweenAction.GUI_MOVE;
		this.initInternal = delegate()
		{
			this.from = new Vector3(this._optional.ltRect.rect.x, this._optional.ltRect.rect.y, 0f);
		};
		this.easeInternal = delegate()
		{
			Vector3 vector = this.easeMethod();
			this._optional.ltRect.rect = new Rect(vector.x, vector.y, this._optional.ltRect.rect.width, this._optional.ltRect.rect.height);
		};
		return this;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00005C0D File Offset: 0x00003E0D
	public LTDescr setGUIMoveMargin()
	{
		this.type = TweenAction.GUI_MOVE_MARGIN;
		this.initInternal = delegate()
		{
			this.from = new Vector2(this._optional.ltRect.margin.x, this._optional.ltRect.margin.y);
		};
		this.easeInternal = delegate()
		{
			Vector3 vector = this.easeMethod();
			this._optional.ltRect.margin = new Vector2(vector.x, vector.y);
		};
		return this;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00005C3C File Offset: 0x00003E3C
	public LTDescr setGUIScale()
	{
		this.type = TweenAction.GUI_SCALE;
		this.initInternal = delegate()
		{
			this.from = new Vector3(this._optional.ltRect.rect.width, this._optional.ltRect.rect.height, 0f);
		};
		this.easeInternal = delegate()
		{
			Vector3 vector = this.easeMethod();
			this._optional.ltRect.rect = new Rect(this._optional.ltRect.rect.x, this._optional.ltRect.rect.y, vector.x, vector.y);
		};
		return this;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00005C6B File Offset: 0x00003E6B
	public LTDescr setGUIAlpha()
	{
		this.type = TweenAction.GUI_ALPHA;
		this.initInternal = delegate()
		{
			this.fromInternal.x = this._optional.ltRect.alpha;
		};
		this.easeInternal = delegate()
		{
			this._optional.ltRect.alpha = this.easeMethod().x;
		};
		return this;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00005C9A File Offset: 0x00003E9A
	public LTDescr setGUIRotate()
	{
		this.type = TweenAction.GUI_ROTATE;
		this.initInternal = delegate()
		{
			if (!this._optional.ltRect.rotateEnabled)
			{
				this._optional.ltRect.rotateEnabled = true;
				this._optional.ltRect.resetForRotation();
			}
			this.fromInternal.x = this._optional.ltRect.rotation;
		};
		this.easeInternal = delegate()
		{
			this._optional.ltRect.rotation = this.easeMethod().x;
		};
		return this;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00005CC9 File Offset: 0x00003EC9
	public LTDescr setDelayedSound()
	{
		this.type = TweenAction.DELAYED_SOUND;
		this.initInternal = delegate()
		{
			this.hasExtraOnCompletes = true;
		};
		this.easeInternal = new LTDescr.ActionMethodDelegate(this.callback);
		return this;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00062C34 File Offset: 0x00060E34
	private void init()
	{
		this.hasInitiliazed = true;
		this.usesNormalDt = (!this.useEstimatedTime && !this.useManualTime && !this.useFrames);
		if (this.useFrames)
		{
			this.optional.initFrameCount = Time.frameCount;
		}
		if (this.time <= 0f)
		{
			this.time = Mathf.Epsilon;
		}
		this.initInternal();
		this.diff = this.to - this.from;
		this.diffDiv2 = this.diff * 0.5f;
		if (this._optional.onStart != null)
		{
			this._optional.onStart();
		}
		if (this.onCompleteOnStart)
		{
			this.callOnCompletes();
		}
		if (this.speed >= 0f)
		{
			this.initSpeed();
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00062D10 File Offset: 0x00060F10
	private void initSpeed()
	{
		if (this.type == TweenAction.MOVE_CURVED || this.type == TweenAction.MOVE_CURVED_LOCAL)
		{
			this.time = this._optional.path.distance / this.speed;
			return;
		}
		if (this.type == TweenAction.MOVE_SPLINE || this.type == TweenAction.MOVE_SPLINE_LOCAL)
		{
			this.time = this._optional.spline.distance / this.speed;
			return;
		}
		this.time = (this.to - this.from).magnitude / this.speed;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00005CF8 File Offset: 0x00003EF8
	public LTDescr updateNow()
	{
		this.updateInternal();
		return this;
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00062DA4 File Offset: 0x00060FA4
	public bool updateInternal()
	{
		float num = this.direction;
		if (this.usesNormalDt)
		{
			LTDescr.dt = LeanTween.dtActual;
		}
		else if (this.useEstimatedTime)
		{
			LTDescr.dt = LeanTween.dtEstimated;
		}
		else if (this.useFrames)
		{
			LTDescr.dt = (float)((this.optional.initFrameCount == 0) ? 0 : 1);
			this.optional.initFrameCount = Time.frameCount;
		}
		else if (this.useManualTime)
		{
			LTDescr.dt = LeanTween.dtManual;
		}
		if (this.delay <= 0f && num != 0f)
		{
			if (this.trans == null)
			{
				return true;
			}
			if (!this.hasInitiliazed)
			{
				this.init();
			}
			LTDescr.dt *= num;
			this.passed += LTDescr.dt;
			this.passed = Mathf.Clamp(this.passed, 0f, this.time);
			this.ratioPassed = this.passed / this.time;
			this.easeInternal();
			if (this.hasUpdateCallback)
			{
				this._optional.callOnUpdate(LTDescr.val, this.ratioPassed);
			}
			if ((num > 0f) ? (this.passed >= this.time) : (this.passed <= 0f))
			{
				this.loopCount--;
				if (this.loopType == LeanTweenType.pingPong)
				{
					this.direction = 0f - num;
				}
				else
				{
					this.passed = Mathf.Epsilon;
				}
				bool flag = this.loopCount == 0 || this.loopType == LeanTweenType.once;
				if (!flag && this.onCompleteOnRepeat && this.hasExtraOnCompletes)
				{
					this.callOnCompletes();
				}
				return flag;
			}
		}
		else
		{
			this.delay -= LTDescr.dt;
		}
		return false;
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00062F74 File Offset: 0x00061174
	public void callOnCompletes()
	{
		if (this.type == TweenAction.GUI_ROTATE)
		{
			this._optional.ltRect.rotateFinished = true;
		}
		if (this.type == TweenAction.DELAYED_SOUND)
		{
			AudioSource.PlayClipAtPoint((AudioClip)this._optional.onCompleteParam, this.to, this.from.x);
		}
		if (this._optional.onComplete != null)
		{
			this._optional.onComplete();
			return;
		}
		if (this._optional.onCompleteObject != null)
		{
			this._optional.onCompleteObject(this._optional.onCompleteParam);
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00063014 File Offset: 0x00061214
	public LTDescr setFromColor(Color col)
	{
		this.from = new Vector3(0f, col.a, 0f);
		this.diff = new Vector3(1f, 0f, 0f);
		this._optional.axis = new Vector3(col.r, col.g, col.b);
		return this;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0006307C File Offset: 0x0006127C
	private static void alphaRecursive(Transform transform, float val, bool useRecursion = true)
	{
		Renderer component = transform.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			foreach (Material material in component.materials)
			{
				if (material.HasProperty("_Color"))
				{
					material.color = new Color(material.color.r, material.color.g, material.color.b, val);
				}
				else if (material.HasProperty("_TintColor"))
				{
					Color color = material.GetColor("_TintColor");
					material.SetColor("_TintColor", new Color(color.r, color.g, color.b, val));
				}
			}
		}
		if (useRecursion && transform.childCount > 0)
		{
			foreach (object obj in transform)
			{
				LTDescr.alphaRecursive((Transform)obj, val, true);
			}
		}
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00063198 File Offset: 0x00061398
	private static void colorRecursive(Transform transform, Color toColor, bool useRecursion = true)
	{
		Renderer component = transform.gameObject.GetComponent<Renderer>();
		if (component != null)
		{
			Material[] materials = component.materials;
			for (int i = 0; i < materials.Length; i++)
			{
				materials[i].color = toColor;
			}
		}
		if (useRecursion && transform.childCount > 0)
		{
			foreach (object obj in transform)
			{
				LTDescr.colorRecursive((Transform)obj, toColor, true);
			}
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00063230 File Offset: 0x00061430
	private static void alphaRecursive(RectTransform rectTransform, float val, int recursiveLevel = 0)
	{
		if (rectTransform.childCount > 0)
		{
			foreach (object obj in rectTransform)
			{
				RectTransform rectTransform2 = (RectTransform)obj;
				MaskableGraphic component = rectTransform2.GetComponent<Image>();
				if (component != null)
				{
					Color color = component.color;
					color.a = val;
					component.color = color;
				}
				else
				{
					component = rectTransform2.GetComponent<RawImage>();
					if (component != null)
					{
						Color color2 = component.color;
						color2.a = val;
						component.color = color2;
					}
				}
				LTDescr.alphaRecursive(rectTransform2, val, recursiveLevel + 1);
			}
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x000632E8 File Offset: 0x000614E8
	private static void alphaRecursiveSprite(Transform transform, float val)
	{
		if (transform.childCount > 0)
		{
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				SpriteRenderer component = transform2.GetComponent<SpriteRenderer>();
				if (component != null)
				{
					component.color = new Color(component.color.r, component.color.g, component.color.b, val);
				}
				LTDescr.alphaRecursiveSprite(transform2, val);
			}
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00063380 File Offset: 0x00061580
	private static void colorRecursiveSprite(Transform transform, Color toColor)
	{
		if (transform.childCount > 0)
		{
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				SpriteRenderer component = transform.gameObject.GetComponent<SpriteRenderer>();
				if (component != null)
				{
					component.color = toColor;
				}
				LTDescr.colorRecursiveSprite(transform2, toColor);
			}
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x000633F8 File Offset: 0x000615F8
	private static void colorRecursive(RectTransform rectTransform, Color toColor)
	{
		if (rectTransform.childCount > 0)
		{
			foreach (object obj in rectTransform)
			{
				RectTransform rectTransform2 = (RectTransform)obj;
				MaskableGraphic component = rectTransform2.GetComponent<Image>();
				if (component != null)
				{
					component.color = toColor;
				}
				else
				{
					component = rectTransform2.GetComponent<RawImage>();
					if (component != null)
					{
						component.color = toColor;
					}
				}
				LTDescr.colorRecursive(rectTransform2, toColor);
			}
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00063488 File Offset: 0x00061688
	private static void textAlphaChildrenRecursive(Transform trans, float val, bool useRecursion = true)
	{
		if (useRecursion && trans.childCount > 0)
		{
			foreach (object obj in trans)
			{
				Transform transform = (Transform)obj;
				Text component = transform.GetComponent<Text>();
				if (component != null)
				{
					Color color = component.color;
					color.a = val;
					component.color = color;
				}
				LTDescr.textAlphaChildrenRecursive(transform, val, true);
			}
		}
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00063510 File Offset: 0x00061710
	private static void textAlphaRecursive(Transform trans, float val, bool useRecursion = true)
	{
		Text component = trans.GetComponent<Text>();
		if (component != null)
		{
			Color color = component.color;
			color.a = val;
			component.color = color;
		}
		if (useRecursion && trans.childCount > 0)
		{
			foreach (object obj in trans)
			{
				LTDescr.textAlphaRecursive((Transform)obj, val, true);
			}
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00063598 File Offset: 0x00061798
	private static void textColorRecursive(Transform trans, Color toColor)
	{
		if (trans.childCount > 0)
		{
			foreach (object obj in trans)
			{
				Transform transform = (Transform)obj;
				Text component = transform.GetComponent<Text>();
				if (component != null)
				{
					component.color = toColor;
				}
				LTDescr.textColorRecursive(transform, toColor);
			}
		}
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0006360C File Offset: 0x0006180C
	private static Color tweenColor(LTDescr tween, float val)
	{
		Vector3 vector = tween._optional.point - tween._optional.axis;
		float num = tween.to.y - tween.from.y;
		return new Color(tween._optional.axis.x + vector.x * val, tween._optional.axis.y + vector.y * val, tween._optional.axis.z + vector.z * val, tween.from.y + num * val);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00005D02 File Offset: 0x00003F02
	public LTDescr pause()
	{
		if (this.direction != 0f)
		{
			this.directionLast = this.direction;
			this.direction = 0f;
		}
		return this;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00005D29 File Offset: 0x00003F29
	public LTDescr resume()
	{
		this.direction = this.directionLast;
		return this;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00005D38 File Offset: 0x00003F38
	public LTDescr setAxis(Vector3 axis)
	{
		this._optional.axis = axis;
		return this;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00005D47 File Offset: 0x00003F47
	public LTDescr setDelay(float delay)
	{
		this.delay = delay;
		return this;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x000636AC File Offset: 0x000618AC
	public LTDescr setEase(LeanTweenType easeType)
	{
		switch (easeType)
		{
		case LeanTweenType.linear:
			this.setEaseLinear();
			break;
		case LeanTweenType.easeOutQuad:
			this.setEaseOutQuad();
			break;
		case LeanTweenType.easeInQuad:
			this.setEaseInQuad();
			break;
		case LeanTweenType.easeInOutQuad:
			this.setEaseInOutQuad();
			break;
		case LeanTweenType.easeInCubic:
			this.setEaseInCubic();
			break;
		case LeanTweenType.easeOutCubic:
			this.setEaseOutCubic();
			break;
		case LeanTweenType.easeInOutCubic:
			this.setEaseInOutCubic();
			break;
		case LeanTweenType.easeInQuart:
			this.setEaseInQuart();
			break;
		case LeanTweenType.easeOutQuart:
			this.setEaseOutQuart();
			break;
		case LeanTweenType.easeInOutQuart:
			this.setEaseInOutQuart();
			break;
		case LeanTweenType.easeInQuint:
			this.setEaseInQuint();
			break;
		case LeanTweenType.easeOutQuint:
			this.setEaseOutQuint();
			break;
		case LeanTweenType.easeInOutQuint:
			this.setEaseInOutQuint();
			break;
		case LeanTweenType.easeInSine:
			this.setEaseInSine();
			break;
		case LeanTweenType.easeOutSine:
			this.setEaseOutSine();
			break;
		case LeanTweenType.easeInOutSine:
			this.setEaseInOutSine();
			break;
		case LeanTweenType.easeInExpo:
			this.setEaseInExpo();
			break;
		case LeanTweenType.easeOutExpo:
			this.setEaseOutExpo();
			break;
		case LeanTweenType.easeInOutExpo:
			this.setEaseInOutExpo();
			break;
		case LeanTweenType.easeInCirc:
			this.setEaseInCirc();
			break;
		case LeanTweenType.easeOutCirc:
			this.setEaseOutCirc();
			break;
		case LeanTweenType.easeInOutCirc:
			this.setEaseInOutCirc();
			break;
		case LeanTweenType.easeInBounce:
			this.setEaseInBounce();
			break;
		case LeanTweenType.easeOutBounce:
			this.setEaseOutBounce();
			break;
		case LeanTweenType.easeInOutBounce:
			this.setEaseInOutBounce();
			break;
		case LeanTweenType.easeInBack:
			this.setEaseInBack();
			break;
		case LeanTweenType.easeOutBack:
			this.setEaseOutBack();
			break;
		case LeanTweenType.easeInOutBack:
			this.setEaseInOutBack();
			break;
		case LeanTweenType.easeInElastic:
			this.setEaseInElastic();
			break;
		case LeanTweenType.easeOutElastic:
			this.setEaseOutElastic();
			break;
		case LeanTweenType.easeInOutElastic:
			this.setEaseInOutElastic();
			break;
		case LeanTweenType.easeSpring:
			this.setEaseSpring();
			break;
		case LeanTweenType.easeShake:
			this.setEaseShake();
			break;
		case LeanTweenType.punch:
			this.setEasePunch();
			break;
		default:
			this.setEaseLinear();
			break;
		}
		return this;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00005D51 File Offset: 0x00003F51
	public LTDescr setEaseLinear()
	{
		this.easeType = LeanTweenType.linear;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeLinear);
		return this;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00005D6D File Offset: 0x00003F6D
	public LTDescr setEaseSpring()
	{
		this.easeType = LeanTweenType.easeSpring;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeSpring);
		return this;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00005D8A File Offset: 0x00003F8A
	public LTDescr setEaseInQuad()
	{
		this.easeType = LeanTweenType.easeInQuad;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInQuad);
		return this;
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00005DA6 File Offset: 0x00003FA6
	public LTDescr setEaseOutQuad()
	{
		this.easeType = LeanTweenType.easeOutQuad;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutQuad);
		return this;
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00005DC2 File Offset: 0x00003FC2
	public LTDescr setEaseInOutQuad()
	{
		this.easeType = LeanTweenType.easeInOutQuad;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutQuad);
		return this;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00005DDE File Offset: 0x00003FDE
	public LTDescr setEaseInCubic()
	{
		this.easeType = LeanTweenType.easeInCubic;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInCubic);
		return this;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00005DFA File Offset: 0x00003FFA
	public LTDescr setEaseOutCubic()
	{
		this.easeType = LeanTweenType.easeOutCubic;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutCubic);
		return this;
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00005E16 File Offset: 0x00004016
	public LTDescr setEaseInOutCubic()
	{
		this.easeType = LeanTweenType.easeInOutCubic;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutCubic);
		return this;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00005E32 File Offset: 0x00004032
	public LTDescr setEaseInQuart()
	{
		this.easeType = LeanTweenType.easeInQuart;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInQuart);
		return this;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00005E4E File Offset: 0x0000404E
	public LTDescr setEaseOutQuart()
	{
		this.easeType = LeanTweenType.easeOutQuart;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutQuart);
		return this;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00005E6B File Offset: 0x0000406B
	public LTDescr setEaseInOutQuart()
	{
		this.easeType = LeanTweenType.easeInOutQuart;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutQuart);
		return this;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00005E88 File Offset: 0x00004088
	public LTDescr setEaseInQuint()
	{
		this.easeType = LeanTweenType.easeInQuint;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInQuint);
		return this;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00005EA5 File Offset: 0x000040A5
	public LTDescr setEaseOutQuint()
	{
		this.easeType = LeanTweenType.easeOutQuint;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutQuint);
		return this;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00005EC2 File Offset: 0x000040C2
	public LTDescr setEaseInOutQuint()
	{
		this.easeType = LeanTweenType.easeInOutQuint;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutQuint);
		return this;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00005EDF File Offset: 0x000040DF
	public LTDescr setEaseInSine()
	{
		this.easeType = LeanTweenType.easeInSine;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInSine);
		return this;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00005EFC File Offset: 0x000040FC
	public LTDescr setEaseOutSine()
	{
		this.easeType = LeanTweenType.easeOutSine;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutSine);
		return this;
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00005F19 File Offset: 0x00004119
	public LTDescr setEaseInOutSine()
	{
		this.easeType = LeanTweenType.easeInOutSine;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutSine);
		return this;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00005F36 File Offset: 0x00004136
	public LTDescr setEaseInExpo()
	{
		this.easeType = LeanTweenType.easeInExpo;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInExpo);
		return this;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00005F53 File Offset: 0x00004153
	public LTDescr setEaseOutExpo()
	{
		this.easeType = LeanTweenType.easeOutExpo;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutExpo);
		return this;
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00005F70 File Offset: 0x00004170
	public LTDescr setEaseInOutExpo()
	{
		this.easeType = LeanTweenType.easeInOutExpo;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutExpo);
		return this;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00005F8D File Offset: 0x0000418D
	public LTDescr setEaseInCirc()
	{
		this.easeType = LeanTweenType.easeInCirc;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInCirc);
		return this;
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00005FAA File Offset: 0x000041AA
	public LTDescr setEaseOutCirc()
	{
		this.easeType = LeanTweenType.easeOutCirc;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutCirc);
		return this;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00005FC7 File Offset: 0x000041C7
	public LTDescr setEaseInOutCirc()
	{
		this.easeType = LeanTweenType.easeInOutCirc;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutCirc);
		return this;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00005FE4 File Offset: 0x000041E4
	public LTDescr setEaseInBounce()
	{
		this.easeType = LeanTweenType.easeInBounce;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInBounce);
		return this;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00006001 File Offset: 0x00004201
	public LTDescr setEaseOutBounce()
	{
		this.easeType = LeanTweenType.easeOutBounce;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutBounce);
		return this;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000601E File Offset: 0x0000421E
	public LTDescr setEaseInOutBounce()
	{
		this.easeType = LeanTweenType.easeInOutBounce;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutBounce);
		return this;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000603B File Offset: 0x0000423B
	public LTDescr setEaseInBack()
	{
		this.easeType = LeanTweenType.easeInBack;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInBack);
		return this;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00006058 File Offset: 0x00004258
	public LTDescr setEaseOutBack()
	{
		this.easeType = LeanTweenType.easeOutBack;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutBack);
		return this;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00006075 File Offset: 0x00004275
	public LTDescr setEaseInOutBack()
	{
		this.easeType = LeanTweenType.easeInOutBack;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutBack);
		return this;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00006092 File Offset: 0x00004292
	public LTDescr setEaseInElastic()
	{
		this.easeType = LeanTweenType.easeInElastic;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInElastic);
		return this;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x000060AF File Offset: 0x000042AF
	public LTDescr setEaseOutElastic()
	{
		this.easeType = LeanTweenType.easeOutElastic;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeOutElastic);
		return this;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x000060CC File Offset: 0x000042CC
	public LTDescr setEaseInOutElastic()
	{
		this.easeType = LeanTweenType.easeInOutElastic;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.easeInOutElastic);
		return this;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000638C4 File Offset: 0x00061AC4
	public LTDescr setEasePunch()
	{
		this._optional.animationCurve = LeanTween.punch;
		this.toInternal.x = this.from.x + this.to.x;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.tweenOnCurve);
		return this;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00063918 File Offset: 0x00061B18
	public LTDescr setEaseShake()
	{
		this._optional.animationCurve = LeanTween.shake;
		this.toInternal.x = this.from.x + this.to.x;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.tweenOnCurve);
		return this;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0006396C File Offset: 0x00061B6C
	private Vector3 tweenOnCurve()
	{
		return new Vector3(this.from.x + this.diff.x * this._optional.animationCurve.Evaluate(this.ratioPassed), this.from.y + this.diff.y * this._optional.animationCurve.Evaluate(this.ratioPassed), this.from.z + this.diff.z * this._optional.animationCurve.Evaluate(this.ratioPassed));
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00063A08 File Offset: 0x00061C08
	private Vector3 easeInOutQuad()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			LTDescr.val *= LTDescr.val;
			return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
		}
		LTDescr.val = (1f - LTDescr.val) * (LTDescr.val - 3f) + 1f;
		return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00063B20 File Offset: 0x00061D20
	private Vector3 easeInQuad()
	{
		LTDescr.val = this.ratioPassed * this.ratioPassed;
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000060E9 File Offset: 0x000042E9
	private Vector3 easeOutQuad()
	{
		LTDescr.val = this.ratioPassed;
		LTDescr.val = -LTDescr.val * (LTDescr.val - 2f);
		return this.diff * LTDescr.val + this.from;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00063B9C File Offset: 0x00061D9C
	private Vector3 easeLinear()
	{
		LTDescr.val = this.ratioPassed;
		return new Vector3(this.from.x + this.diff.x * LTDescr.val, this.from.y + this.diff.y * LTDescr.val, this.from.z + this.diff.z * LTDescr.val);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00063C10 File Offset: 0x00061E10
	private Vector3 easeSpring()
	{
		LTDescr.val = Mathf.Clamp01(this.ratioPassed);
		LTDescr.val = (Mathf.Sin(LTDescr.val * 3.1415927f * (0.2f + 2.5f * LTDescr.val * LTDescr.val * LTDescr.val)) * Mathf.Pow(1f - LTDescr.val, 2.2f) + LTDescr.val) * (1f + 1.2f * (1f - LTDescr.val));
		return this.from + this.diff * LTDescr.val;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00063CB0 File Offset: 0x00061EB0
	private Vector3 easeInCubic()
	{
		LTDescr.val = this.ratioPassed * this.ratioPassed * this.ratioPassed;
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00063D34 File Offset: 0x00061F34
	private Vector3 easeOutCubic()
	{
		LTDescr.val = this.ratioPassed - 1f;
		LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val + 1f;
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00063DCC File Offset: 0x00061FCC
	private Vector3 easeInOutCubic()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val;
			return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
		}
		LTDescr.val -= 2f;
		LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val + 2f;
		return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00006128 File Offset: 0x00004328
	private Vector3 easeInQuart()
	{
		LTDescr.val = this.ratioPassed * this.ratioPassed * this.ratioPassed * this.ratioPassed;
		return this.diff * LTDescr.val + this.from;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00063EF4 File Offset: 0x000620F4
	private Vector3 easeOutQuart()
	{
		LTDescr.val = this.ratioPassed - 1f;
		LTDescr.val = -(LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val - 1f);
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00063F94 File Offset: 0x00062194
	private Vector3 easeInOutQuart()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val;
			return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
		}
		LTDescr.val -= 2f;
		return -this.diffDiv2 * (LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val - 2f) + this.from;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00064080 File Offset: 0x00062280
	private Vector3 easeInQuint()
	{
		LTDescr.val = this.ratioPassed;
		LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val;
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00064118 File Offset: 0x00062318
	private Vector3 easeOutQuint()
	{
		LTDescr.val = this.ratioPassed - 1f;
		LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val + 1f;
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x0600020C RID: 524 RVA: 0x000641BC File Offset: 0x000623BC
	private Vector3 easeInOutQuint()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val;
			return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
		}
		LTDescr.val -= 2f;
		LTDescr.val = LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val * LTDescr.val + 2f;
		return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
	}

	// Token: 0x0600020D RID: 525 RVA: 0x000642FC File Offset: 0x000624FC
	private Vector3 easeInSine()
	{
		LTDescr.val = -Mathf.Cos(this.ratioPassed * LeanTween.PI_DIV2);
		return new Vector3(this.diff.x * LTDescr.val + this.diff.x + this.from.x, this.diff.y * LTDescr.val + this.diff.y + this.from.y, this.diff.z * LTDescr.val + this.diff.z + this.from.z);
	}

	// Token: 0x0600020E RID: 526 RVA: 0x000643A0 File Offset: 0x000625A0
	private Vector3 easeOutSine()
	{
		LTDescr.val = Mathf.Sin(this.ratioPassed * LeanTween.PI_DIV2);
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00064420 File Offset: 0x00062620
	private Vector3 easeInOutSine()
	{
		LTDescr.val = -(Mathf.Cos(3.1415927f * this.ratioPassed) - 1f);
		return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000210 RID: 528 RVA: 0x000644A8 File Offset: 0x000626A8
	private Vector3 easeInExpo()
	{
		LTDescr.val = Mathf.Pow(2f, 10f * (this.ratioPassed - 1f));
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00064534 File Offset: 0x00062734
	private Vector3 easeOutExpo()
	{
		LTDescr.val = -Mathf.Pow(2f, -10f * this.ratioPassed) + 1f;
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000645C0 File Offset: 0x000627C0
	private Vector3 easeInOutExpo()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			return this.diffDiv2 * Mathf.Pow(2f, 10f * (LTDescr.val - 1f)) + this.from;
		}
		LTDescr.val -= 1f;
		return this.diffDiv2 * (-Mathf.Pow(2f, -10f * LTDescr.val) + 2f) + this.from;
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00064660 File Offset: 0x00062860
	private Vector3 easeInCirc()
	{
		LTDescr.val = -(Mathf.Sqrt(1f - this.ratioPassed * this.ratioPassed) - 1f);
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000214 RID: 532 RVA: 0x000646F0 File Offset: 0x000628F0
	private Vector3 easeOutCirc()
	{
		LTDescr.val = this.ratioPassed - 1f;
		LTDescr.val = Mathf.Sqrt(1f - LTDescr.val * LTDescr.val);
		return new Vector3(this.diff.x * LTDescr.val + this.from.x, this.diff.y * LTDescr.val + this.from.y, this.diff.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00064788 File Offset: 0x00062988
	private Vector3 easeInOutCirc()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			LTDescr.val = -(Mathf.Sqrt(1f - LTDescr.val * LTDescr.val) - 1f);
			return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
		}
		LTDescr.val -= 2f;
		LTDescr.val = Mathf.Sqrt(1f - LTDescr.val * LTDescr.val) + 1f;
		return new Vector3(this.diffDiv2.x * LTDescr.val + this.from.x, this.diffDiv2.y * LTDescr.val + this.from.y, this.diffDiv2.z * LTDescr.val + this.from.z);
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000648C0 File Offset: 0x00062AC0
	private Vector3 easeInBounce()
	{
		LTDescr.val = this.ratioPassed;
		LTDescr.val = 1f - LTDescr.val;
		return new Vector3(this.diff.x - LeanTween.easeOutBounce(0f, this.diff.x, LTDescr.val) + this.from.x, this.diff.y - LeanTween.easeOutBounce(0f, this.diff.y, LTDescr.val) + this.from.y, this.diff.z - LeanTween.easeOutBounce(0f, this.diff.z, LTDescr.val) + this.from.z);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00064984 File Offset: 0x00062B84
	private Vector3 easeOutBounce()
	{
		LTDescr.val = this.ratioPassed;
		float num;
		float num2;
		if (LTDescr.val < (num = 1f - 1.75f * this.overshoot / 2.75f))
		{
			LTDescr.val = 1f / num / num * LTDescr.val * LTDescr.val;
		}
		else if (LTDescr.val < (num2 = 1f - 0.75f * this.overshoot / 2.75f))
		{
			LTDescr.val -= (num + num2) / 2f;
			LTDescr.val = 7.5625f * LTDescr.val * LTDescr.val + 1f - 0.25f * this.overshoot * this.overshoot;
		}
		else if (LTDescr.val < (num = 1f - 0.25f * this.overshoot / 2.75f))
		{
			LTDescr.val -= (num + num2) / 2f;
			LTDescr.val = 7.5625f * LTDescr.val * LTDescr.val + 1f - 0.0625f * this.overshoot * this.overshoot;
		}
		else
		{
			LTDescr.val -= (num + 1f) / 2f;
			LTDescr.val = 7.5625f * LTDescr.val * LTDescr.val + 1f - 0.015625f * this.overshoot * this.overshoot;
		}
		return this.diff * LTDescr.val + this.from;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00064B10 File Offset: 0x00062D10
	private Vector3 easeInOutBounce()
	{
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			return new Vector3(LeanTween.easeInBounce(0f, this.diff.x, LTDescr.val) * 0.5f + this.from.x, LeanTween.easeInBounce(0f, this.diff.y, LTDescr.val) * 0.5f + this.from.y, LeanTween.easeInBounce(0f, this.diff.z, LTDescr.val) * 0.5f + this.from.z);
		}
		LTDescr.val -= 1f;
		return new Vector3(LeanTween.easeOutBounce(0f, this.diff.x, LTDescr.val) * 0.5f + this.diffDiv2.x + this.from.x, LeanTween.easeOutBounce(0f, this.diff.y, LTDescr.val) * 0.5f + this.diffDiv2.y + this.from.y, LeanTween.easeOutBounce(0f, this.diff.z, LTDescr.val) * 0.5f + this.diffDiv2.z + this.from.z);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00064C84 File Offset: 0x00062E84
	private Vector3 easeInBack()
	{
		LTDescr.val = this.ratioPassed;
		LTDescr.val /= 1f;
		float num = 1.70158f * this.overshoot;
		return this.diff * LTDescr.val * LTDescr.val * ((num + 1f) * LTDescr.val - num) + this.from;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00064CF4 File Offset: 0x00062EF4
	private Vector3 easeOutBack()
	{
		float num = 1.70158f * this.overshoot;
		LTDescr.val = this.ratioPassed / 1f - 1f;
		LTDescr.val = LTDescr.val * LTDescr.val * ((num + 1f) * LTDescr.val + num) + 1f;
		return this.diff * LTDescr.val + this.from;
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00064D68 File Offset: 0x00062F68
	private Vector3 easeInOutBack()
	{
		float num = 1.70158f * this.overshoot;
		LTDescr.val = this.ratioPassed * 2f;
		if (LTDescr.val < 1f)
		{
			num *= 1.525f * this.overshoot;
			return this.diffDiv2 * (LTDescr.val * LTDescr.val * ((num + 1f) * LTDescr.val - num)) + this.from;
		}
		LTDescr.val -= 2f;
		num *= 1.525f * this.overshoot;
		LTDescr.val = LTDescr.val * LTDescr.val * ((num + 1f) * LTDescr.val + num) + 2f;
		return this.diffDiv2 * LTDescr.val + this.from;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00064E40 File Offset: 0x00063040
	private Vector3 easeInElastic()
	{
		return new Vector3(LeanTween.easeInElastic(this.from.x, this.to.x, this.ratioPassed, this.overshoot, this.period), LeanTween.easeInElastic(this.from.y, this.to.y, this.ratioPassed, this.overshoot, this.period), LeanTween.easeInElastic(this.from.z, this.to.z, this.ratioPassed, this.overshoot, this.period));
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00064EDC File Offset: 0x000630DC
	private Vector3 easeOutElastic()
	{
		return new Vector3(LeanTween.easeOutElastic(this.from.x, this.to.x, this.ratioPassed, this.overshoot, this.period), LeanTween.easeOutElastic(this.from.y, this.to.y, this.ratioPassed, this.overshoot, this.period), LeanTween.easeOutElastic(this.from.z, this.to.z, this.ratioPassed, this.overshoot, this.period));
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00064F78 File Offset: 0x00063178
	private Vector3 easeInOutElastic()
	{
		return new Vector3(LeanTween.easeInOutElastic(this.from.x, this.to.x, this.ratioPassed, this.overshoot, this.period), LeanTween.easeInOutElastic(this.from.y, this.to.y, this.ratioPassed, this.overshoot, this.period), LeanTween.easeInOutElastic(this.from.z, this.to.z, this.ratioPassed, this.overshoot, this.period));
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00006165 File Offset: 0x00004365
	public LTDescr setOvershoot(float overshoot)
	{
		this.overshoot = overshoot;
		return this;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0000616F File Offset: 0x0000436F
	public LTDescr setPeriod(float period)
	{
		this.period = period;
		return this;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00006179 File Offset: 0x00004379
	public LTDescr setScale(float scale)
	{
		this.scale = scale;
		return this;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00006183 File Offset: 0x00004383
	public LTDescr setEase(AnimationCurve easeCurve)
	{
		this._optional.animationCurve = easeCurve;
		this.easeMethod = new LTDescr.EaseTypeDelegate(this.tweenOnCurve);
		this.easeType = LeanTweenType.animationCurve;
		return this;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x000061AC File Offset: 0x000043AC
	public LTDescr setTo(Vector3 to)
	{
		if (this.hasInitiliazed)
		{
			this.to = to;
			this.diff = to - this.from;
		}
		else
		{
			this.to = to;
		}
		return this;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x000061D9 File Offset: 0x000043D9
	public LTDescr setTo(Transform to)
	{
		this._optional.toTrans = to;
		return this;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00065014 File Offset: 0x00063214
	public LTDescr setFrom(Vector3 from)
	{
		if (this.trans)
		{
			this.init();
		}
		this.from = from;
		this.diff = this.to - this.from;
		this.diffDiv2 = this.diff * 0.5f;
		return this;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000061E8 File Offset: 0x000043E8
	public LTDescr setFrom(float from)
	{
		return this.setFrom(new Vector3(from, 0f, 0f));
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00006200 File Offset: 0x00004400
	public LTDescr setDiff(Vector3 diff)
	{
		this.diff = diff;
		return this;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000620A File Offset: 0x0000440A
	public LTDescr setHasInitialized(bool has)
	{
		this.hasInitiliazed = has;
		return this;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00006214 File Offset: 0x00004414
	public LTDescr setId(uint id, uint global_counter)
	{
		this._id = id;
		this.counter = global_counter;
		return this;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00006225 File Offset: 0x00004425
	public LTDescr setPassed(float passed)
	{
		this.passed = passed;
		return this;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0006506C File Offset: 0x0006326C
	public LTDescr setTime(float time)
	{
		float num = this.passed / this.time;
		this.passed = time * num;
		this.time = time;
		return this;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000622F File Offset: 0x0000442F
	public LTDescr setSpeed(float speed)
	{
		this.speed = speed;
		if (this.hasInitiliazed)
		{
			this.initSpeed();
		}
		return this;
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00065098 File Offset: 0x00063298
	public LTDescr setRepeat(int repeat)
	{
		this.loopCount = repeat;
		if ((repeat > 1 && this.loopType == LeanTweenType.once) || (repeat < 0 && this.loopType == LeanTweenType.once))
		{
			this.loopType = LeanTweenType.clamp;
		}
		if (this.type == TweenAction.CALLBACK || this.type == TweenAction.CALLBACK_COLOR)
		{
			this.setOnCompleteOnRepeat(true);
		}
		return this;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00006247 File Offset: 0x00004447
	public LTDescr setLoopType(LeanTweenType loopType)
	{
		this.loopType = loopType;
		return this;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00006251 File Offset: 0x00004451
	public LTDescr setUseEstimatedTime(bool useEstimatedTime)
	{
		this.useEstimatedTime = useEstimatedTime;
		this.usesNormalDt = false;
		return this;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00006251 File Offset: 0x00004451
	public LTDescr setIgnoreTimeScale(bool useUnScaledTime)
	{
		this.useEstimatedTime = useUnScaledTime;
		this.usesNormalDt = false;
		return this;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00006262 File Offset: 0x00004462
	public LTDescr setUseFrames(bool useFrames)
	{
		this.useFrames = useFrames;
		this.usesNormalDt = false;
		return this;
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00006273 File Offset: 0x00004473
	public LTDescr setUseManualTime(bool useManualTime)
	{
		this.useManualTime = useManualTime;
		this.usesNormalDt = false;
		return this;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00006284 File Offset: 0x00004484
	public LTDescr setLoopCount(int loopCount)
	{
		this.loopType = LeanTweenType.clamp;
		this.loopCount = loopCount;
		return this;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00006296 File Offset: 0x00004496
	public LTDescr setLoopOnce()
	{
		this.loopType = LeanTweenType.once;
		return this;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x000062A1 File Offset: 0x000044A1
	public LTDescr setLoopClamp()
	{
		this.loopType = LeanTweenType.clamp;
		if (this.loopCount == 0)
		{
			this.loopCount = -1;
		}
		return this;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x000062BB File Offset: 0x000044BB
	public LTDescr setLoopClamp(int loops)
	{
		this.loopCount = loops;
		return this;
	}

	// Token: 0x06000237 RID: 567 RVA: 0x000062C5 File Offset: 0x000044C5
	public LTDescr setLoopPingPong()
	{
		this.loopType = LeanTweenType.pingPong;
		if (this.loopCount == 0)
		{
			this.loopCount = -1;
		}
		return this;
	}

	// Token: 0x06000238 RID: 568 RVA: 0x000062DF File Offset: 0x000044DF
	public LTDescr setLoopPingPong(int loops)
	{
		this.loopType = LeanTweenType.pingPong;
		this.loopCount = ((loops == -1) ? loops : (loops * 2));
		return this;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000062FA File Offset: 0x000044FA
	public LTDescr setOnComplete(Action onComplete)
	{
		this._optional.onComplete = onComplete;
		this.hasExtraOnCompletes = true;
		return this;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00006310 File Offset: 0x00004510
	public LTDescr setOnComplete(Action<object> onComplete)
	{
		this._optional.onCompleteObject = onComplete;
		this.hasExtraOnCompletes = true;
		return this;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00006326 File Offset: 0x00004526
	public LTDescr setOnComplete(Action<object> onComplete, object onCompleteParam)
	{
		this._optional.onCompleteObject = onComplete;
		this.hasExtraOnCompletes = true;
		if (onCompleteParam != null)
		{
			this._optional.onCompleteParam = onCompleteParam;
		}
		return this;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000634B File Offset: 0x0000454B
	public LTDescr setOnCompleteParam(object onCompleteParam)
	{
		this._optional.onCompleteParam = onCompleteParam;
		this.hasExtraOnCompletes = true;
		return this;
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00006361 File Offset: 0x00004561
	public LTDescr setOnUpdate(Action<float> onUpdate)
	{
		this._optional.onUpdateFloat = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00006377 File Offset: 0x00004577
	public LTDescr setOnUpdateRatio(Action<float, float> onUpdate)
	{
		this._optional.onUpdateFloatRatio = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000638D File Offset: 0x0000458D
	public LTDescr setOnUpdateObject(Action<float, object> onUpdate)
	{
		this._optional.onUpdateFloatObject = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x000063A3 File Offset: 0x000045A3
	public LTDescr setOnUpdateVector2(Action<Vector2> onUpdate)
	{
		this._optional.onUpdateVector2 = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x000063B9 File Offset: 0x000045B9
	public LTDescr setOnUpdateVector3(Action<Vector3> onUpdate)
	{
		this._optional.onUpdateVector3 = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x000063CF File Offset: 0x000045CF
	public LTDescr setOnUpdateColor(Action<Color> onUpdate)
	{
		this._optional.onUpdateColor = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x000063E5 File Offset: 0x000045E5
	public LTDescr setOnUpdateColor(Action<Color, object> onUpdate)
	{
		this._optional.onUpdateColorObject = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x000063CF File Offset: 0x000045CF
	public LTDescr setOnUpdate(Action<Color> onUpdate)
	{
		this._optional.onUpdateColor = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x000063E5 File Offset: 0x000045E5
	public LTDescr setOnUpdate(Action<Color, object> onUpdate)
	{
		this._optional.onUpdateColorObject = onUpdate;
		this.hasUpdateCallback = true;
		return this;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x000063FB File Offset: 0x000045FB
	public LTDescr setOnUpdate(Action<float, object> onUpdate, object onUpdateParam = null)
	{
		this._optional.onUpdateFloatObject = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this._optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00006420 File Offset: 0x00004620
	public LTDescr setOnUpdate(Action<Vector3, object> onUpdate, object onUpdateParam = null)
	{
		this._optional.onUpdateVector3Object = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this._optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00006445 File Offset: 0x00004645
	public LTDescr setOnUpdate(Action<Vector2> onUpdate, object onUpdateParam = null)
	{
		this._optional.onUpdateVector2 = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this._optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000646A File Offset: 0x0000466A
	public LTDescr setOnUpdate(Action<Vector3> onUpdate, object onUpdateParam = null)
	{
		this._optional.onUpdateVector3 = onUpdate;
		this.hasUpdateCallback = true;
		if (onUpdateParam != null)
		{
			this._optional.onUpdateParam = onUpdateParam;
		}
		return this;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000648F File Offset: 0x0000468F
	public LTDescr setOnUpdateParam(object onUpdateParam)
	{
		this._optional.onUpdateParam = onUpdateParam;
		return this;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x000650F0 File Offset: 0x000632F0
	public LTDescr setOrientToPath(bool doesOrient)
	{
		if (this.type == TweenAction.MOVE_CURVED || this.type == TweenAction.MOVE_CURVED_LOCAL)
		{
			if (this._optional.path == null)
			{
				this._optional.path = new LTBezierPath();
			}
			this._optional.path.orientToPath = doesOrient;
		}
		else
		{
			this._optional.spline.orientToPath = doesOrient;
		}
		return this;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00065154 File Offset: 0x00063354
	public LTDescr setOrientToPath2d(bool doesOrient2d)
	{
		this.setOrientToPath(doesOrient2d);
		if (this.type == TweenAction.MOVE_CURVED || this.type == TweenAction.MOVE_CURVED_LOCAL)
		{
			this._optional.path.orientToPath2d = doesOrient2d;
		}
		else
		{
			this._optional.spline.orientToPath2d = doesOrient2d;
		}
		return this;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000649E File Offset: 0x0000469E
	public LTDescr setRect(LTRect rect)
	{
		this._optional.ltRect = rect;
		return this;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000064AD File Offset: 0x000046AD
	public LTDescr setRect(Rect rect)
	{
		this._optional.ltRect = new LTRect(rect);
		return this;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000064C1 File Offset: 0x000046C1
	public LTDescr setPath(LTBezierPath path)
	{
		this._optional.path = path;
		return this;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000064D0 File Offset: 0x000046D0
	public LTDescr setPoint(Vector3 point)
	{
		this._optional.point = point;
		return this;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x000064DF File Offset: 0x000046DF
	public LTDescr setDestroyOnComplete(bool doesDestroy)
	{
		this.destroyOnComplete = doesDestroy;
		return this;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x000064E9 File Offset: 0x000046E9
	public LTDescr setAudio(object audio)
	{
		this._optional.onCompleteParam = audio;
		return this;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x000064F8 File Offset: 0x000046F8
	public LTDescr setOnCompleteOnRepeat(bool isOn)
	{
		this.onCompleteOnRepeat = isOn;
		return this;
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00006502 File Offset: 0x00004702
	public LTDescr setOnCompleteOnStart(bool isOn)
	{
		this.onCompleteOnStart = isOn;
		return this;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000650C File Offset: 0x0000470C
	public LTDescr setRect(RectTransform rect)
	{
		this.rectTransform = rect;
		return this;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00006516 File Offset: 0x00004716
	public LTDescr setSprites(Sprite[] sprites)
	{
		this.sprites = sprites;
		return this;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00006520 File Offset: 0x00004720
	public LTDescr setFrameRate(float frameRate)
	{
		this.time = (float)this.sprites.Length / frameRate;
		return this;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00006534 File Offset: 0x00004734
	public LTDescr setOnStart(Action onStart)
	{
		this._optional.onStart = onStart;
		return this;
	}

	// Token: 0x06000259 RID: 601 RVA: 0x000651A0 File Offset: 0x000633A0
	public LTDescr setDirection(float direction)
	{
		if (this.direction != -1f && this.direction != 1f)
		{
			Debug.LogWarning("You have passed an incorrect direction of '" + direction + "', direction must be -1f or 1f");
			return this;
		}
		if (this.direction != direction)
		{
			if (this.hasInitiliazed)
			{
				this.direction = direction;
			}
			else if (this._optional.path != null)
			{
				this._optional.path = new LTBezierPath(LTUtility.reverse(this._optional.path.pts));
			}
			else if (this._optional.spline != null)
			{
				this._optional.spline = new LTSpline(LTUtility.reverse(this._optional.spline.pts));
			}
		}
		return this;
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00006543 File Offset: 0x00004743
	public LTDescr setRecursive(bool useRecursion)
	{
		this.useRecursion = useRecursion;
		return this;
	}

	// Token: 0x04000134 RID: 308
	public bool toggle;

	// Token: 0x04000135 RID: 309
	public bool useEstimatedTime;

	// Token: 0x04000136 RID: 310
	public bool useFrames;

	// Token: 0x04000137 RID: 311
	public bool useManualTime;

	// Token: 0x04000138 RID: 312
	public bool usesNormalDt;

	// Token: 0x04000139 RID: 313
	public bool hasInitiliazed;

	// Token: 0x0400013A RID: 314
	public bool hasExtraOnCompletes;

	// Token: 0x0400013B RID: 315
	public bool hasPhysics;

	// Token: 0x0400013C RID: 316
	public bool onCompleteOnRepeat;

	// Token: 0x0400013D RID: 317
	public bool onCompleteOnStart;

	// Token: 0x0400013E RID: 318
	public bool useRecursion;

	// Token: 0x0400013F RID: 319
	public float ratioPassed;

	// Token: 0x04000140 RID: 320
	public float passed;

	// Token: 0x04000141 RID: 321
	public float delay;

	// Token: 0x04000142 RID: 322
	public float time;

	// Token: 0x04000143 RID: 323
	public float speed;

	// Token: 0x04000144 RID: 324
	public float lastVal;

	// Token: 0x04000145 RID: 325
	private uint _id;

	// Token: 0x04000146 RID: 326
	public int loopCount;

	// Token: 0x04000147 RID: 327
	public uint counter = uint.MaxValue;

	// Token: 0x04000148 RID: 328
	public float direction;

	// Token: 0x04000149 RID: 329
	public float directionLast;

	// Token: 0x0400014A RID: 330
	public float overshoot;

	// Token: 0x0400014B RID: 331
	public float period;

	// Token: 0x0400014C RID: 332
	public float scale;

	// Token: 0x0400014D RID: 333
	public bool destroyOnComplete;

	// Token: 0x0400014E RID: 334
	public Transform trans;

	// Token: 0x0400014F RID: 335
	internal Vector3 fromInternal;

	// Token: 0x04000150 RID: 336
	internal Vector3 toInternal;

	// Token: 0x04000151 RID: 337
	internal Vector3 diff;

	// Token: 0x04000152 RID: 338
	internal Vector3 diffDiv2;

	// Token: 0x04000153 RID: 339
	public TweenAction type;

	// Token: 0x04000154 RID: 340
	private LeanTweenType easeType;

	// Token: 0x04000155 RID: 341
	public LeanTweenType loopType;

	// Token: 0x04000156 RID: 342
	public bool hasUpdateCallback;

	// Token: 0x04000157 RID: 343
	public LTDescr.EaseTypeDelegate easeMethod;

	// Token: 0x0400015A RID: 346
	public SpriteRenderer spriteRen;

	// Token: 0x0400015B RID: 347
	public RectTransform rectTransform;

	// Token: 0x0400015C RID: 348
	public Text uiText;

	// Token: 0x0400015D RID: 349
	public Image uiImage;

	// Token: 0x0400015E RID: 350
	public RawImage rawImage;

	// Token: 0x0400015F RID: 351
	public Sprite[] sprites;

	// Token: 0x04000160 RID: 352
	public LTDescrOptional _optional = new LTDescrOptional();

	// Token: 0x04000161 RID: 353
	public static float val;

	// Token: 0x04000162 RID: 354
	public static float dt;

	// Token: 0x04000163 RID: 355
	public static Vector3 newVect;

	// Token: 0x02000027 RID: 39
	// (Invoke) Token: 0x060002B6 RID: 694
	public delegate Vector3 EaseTypeDelegate();

	// Token: 0x02000028 RID: 40
	// (Invoke) Token: 0x060002BA RID: 698
	public delegate void ActionMethodDelegate();
}
