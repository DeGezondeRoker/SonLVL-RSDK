using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class HPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 1, 64, 32), -32, -10);
			
			// tagging this area withLevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(193, 33);
			bitmap.DrawRectangle(6, 0, 0, 63, 31); // left box
			bitmap.DrawRectangle(6, 128, 0, 63, 31); // right box
			bitmap.DrawLine(6, 32, 10, 160, 10);
			debug = new Sprite(bitmap, -96, -10);
			
			properties[0] = new PropertySpec("Start Direction", typeof(int), "Extended",
				"The starting direction of this Platform.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (byte)((int)value)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Start Right" : "Start Left";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}