using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EB0 RID: 3760
	public abstract class CDKUserUsedBase : Entity
	{
		// Token: 0x06005ADC RID: 23260 RVA: 0x00040112 File Offset: 0x0003E312
		public CDKUserUsedBase()
		{
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x00040165 File Offset: 0x0003E365
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_CDKUserUsedBase(this.id, this.className);
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0004017E File Offset: 0x0003E37E
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_CDKUserUsedBase(this.id, this.className);
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x00040197 File Offset: 0x0003E397
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x000401A0 File Offset: 0x0003E3A0
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x000401A8 File Offset: 0x0003E3A8
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x002501D0 File Offset: 0x0024E3D0
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDKUserUsed"];
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

		// Token: 0x06005AE7 RID: 23271 RVA: 0x00250250 File Offset: 0x0024E450
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["CDKUserUsed"];
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

		// Token: 0x06005AE8 RID: 23272 RVA: 0x00250380 File Offset: 0x0024E580
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["CDKUserUsed"].idpropertys;
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

		// Token: 0x040059FE RID: 23038
		public EntityBaseEntityCall_CDKUserUsedBase baseEntityCall;

		// Token: 0x040059FF RID: 23039
		public EntityCellEntityCall_CDKUserUsedBase cellEntityCall;
	}
}
