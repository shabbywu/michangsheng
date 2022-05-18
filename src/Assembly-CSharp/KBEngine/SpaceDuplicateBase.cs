using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FEF RID: 4079
	public abstract class SpaceDuplicateBase : Entity
	{
		// Token: 0x06006092 RID: 24722 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelIDChanged(uint oldValue)
		{
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onModelScaleChanged(byte oldValue)
		{
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onNameChanged(string oldValue)
		{
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x0004305D File Offset: 0x0004125D
		public SpaceDuplicateBase()
		{
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x00043078 File Offset: 0x00041278
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_SpaceDuplicateBase(this.id, this.className);
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x00043091 File Offset: 0x00041291
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_SpaceDuplicateBase(this.id, this.className);
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x000430AA File Offset: 0x000412AA
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x000430B3 File Offset: 0x000412B3
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x000430BB File Offset: 0x000412BB
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x00269E0C File Offset: 0x0026800C
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["SpaceDuplicate"];
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

		// Token: 0x060060A2 RID: 24738 RVA: 0x00269E8C File Offset: 0x0026808C
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["SpaceDuplicate"];
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
				ushort properUtype2 = property.properUtype;
				switch (properUtype2)
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
				default:
					switch (properUtype2)
					{
					case 41003:
					{
						string oldValue = this.name;
						this.name = stream.readUnicode();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onNameChanged(oldValue);
							}
						}
						else if (this.inWorld)
						{
							this.onNameChanged(oldValue);
						}
						break;
					}
					case 41004:
					{
						uint oldValue2 = this.uid;
						this.uid = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUidChanged(oldValue2);
							}
						}
						else if (this.inWorld)
						{
							this.onUidChanged(oldValue2);
						}
						break;
					}
					case 41005:
					{
						uint oldValue3 = this.utype;
						this.utype = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onUtypeChanged(oldValue3);
							}
						}
						else if (this.inWorld)
						{
							this.onUtypeChanged(oldValue3);
						}
						break;
					}
					case 41006:
					{
						uint oldValue4 = this.modelID;
						this.modelID = stream.readUint32();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelIDChanged(oldValue4);
							}
						}
						else if (this.inWorld)
						{
							this.onModelIDChanged(oldValue4);
						}
						break;
					}
					case 41007:
					{
						byte oldValue5 = this.modelScale;
						this.modelScale = stream.readUint8();
						if (property.isBase())
						{
							if (this.inited)
							{
								this.onModelScaleChanged(oldValue5);
							}
						}
						else if (this.inWorld)
						{
							this.onModelScaleChanged(oldValue5);
						}
						break;
					}
					}
					break;
				}
			}
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x0026A160 File Offset: 0x00268360
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["SpaceDuplicate"].idpropertys;
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
			uint oldValue = this.modelID;
			Property property2 = idpropertys[4];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelIDChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelIDChanged(oldValue);
			}
			byte oldValue2 = this.modelScale;
			Property property3 = idpropertys[5];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onModelScaleChanged(oldValue2);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onModelScaleChanged(oldValue2);
			}
			string oldValue3 = this.name;
			Property property4 = idpropertys[6];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onNameChanged(oldValue3);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onNameChanged(oldValue3);
			}
			Vector3 position = this.position;
			Property property5 = idpropertys[1];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			uint oldValue4 = this.uid;
			Property property6 = idpropertys[7];
			if (property6.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUidChanged(oldValue4);
				}
			}
			else if (this.inWorld && (!property6.isOwnerOnly() || base.isPlayer()))
			{
				this.onUidChanged(oldValue4);
			}
			uint oldValue5 = this.utype;
			Property property7 = idpropertys[8];
			if (property7.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUtypeChanged(oldValue5);
					return;
				}
			}
			else if (this.inWorld && (!property7.isOwnerOnly() || base.isPlayer()))
			{
				this.onUtypeChanged(oldValue5);
			}
		}

		// Token: 0x04005BCB RID: 23499
		public EntityBaseEntityCall_SpaceDuplicateBase baseEntityCall;

		// Token: 0x04005BCC RID: 23500
		public EntityCellEntityCall_SpaceDuplicateBase cellEntityCall;

		// Token: 0x04005BCD RID: 23501
		public uint modelID;

		// Token: 0x04005BCE RID: 23502
		public byte modelScale = 30;

		// Token: 0x04005BCF RID: 23503
		public string name = "";

		// Token: 0x04005BD0 RID: 23504
		public uint uid;

		// Token: 0x04005BD1 RID: 23505
		public uint utype;
	}
}
