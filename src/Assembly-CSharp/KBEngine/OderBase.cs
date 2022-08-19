using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C55 RID: 3157
	public abstract class OderBase : Entity
	{
		// Token: 0x060055C2 RID: 21954 RVA: 0x00220682 File Offset: 0x0021E882
		public OderBase()
		{
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x0023A0D4 File Offset: 0x002382D4
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_OderBase(this.id, this.className);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x0023A0ED File Offset: 0x002382ED
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_OderBase(this.id, this.className);
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x0023A106 File Offset: 0x00238306
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0023A10F File Offset: 0x0023830F
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x0023A117 File Offset: 0x00238317
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0023A120 File Offset: 0x00238320
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Oder"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x0023A1A0 File Offset: 0x002383A0
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Oder"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				switch (property.properUtype)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				}
			}
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x0023A2D0 File Offset: 0x002384D0
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Oder"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			Vector3 position = this.position;
			Property property2 = idpropertys[1];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
					return;
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
		}

		// Token: 0x040050C6 RID: 20678
		public EntityBaseEntityCall_OderBase baseEntityCall;

		// Token: 0x040050C7 RID: 20679
		public EntityCellEntityCall_OderBase cellEntityCall;
	}
}
