using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SCDObjectDefinitions.Special
{
	public class UFO : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private ReadOnlyCollection<byte> subtypes = new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2 });

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Special/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 1, 80, 56), -40, -28);
			sprites[1] = new Sprite(sheet.GetSection(82, 1, 80, 56), -40, -28);
			sprites[2] = new Sprite(sheet.GetSection(163, 1, 80, 56), -40, -28);
		}

		public override Sprite Image { get { return sprites[0]; } }

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			List<ObjectEntry> nodes = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj) + 1).TakeWhile(a => a.Name == "UFO Node").ToList();
			if (nodes.Count == 0)
				return null;
			short xmin = Math.Min(obj.X, nodes.Min(a => a.X));
			short ymin = Math.Min(obj.Y, nodes.Min(a => a.Y));
			short xmax = Math.Max(obj.X, nodes.Max(a => a.X));
			short ymax = Math.Max(obj.Y, nodes.Max(a => a.Y));
			BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			if (obj.X != nodes[0].X || obj.Y != nodes[0].Y)
				bmp.DrawLine(LevelData.ColorWhite, obj.X - xmin, obj.Y - ymin, nodes[0].X - xmin, nodes[0].Y - ymin);
			for (int i = 0; i < nodes.Count - 1; i++)
				bmp.DrawLine(LevelData.ColorYellow, nodes[i].X - xmin, nodes[i].Y - ymin, nodes[i + 1].X - xmin, nodes[i + 1].Y - ymin);
			if (nodes.Count > 2)
				bmp.DrawLine(LevelData.ColorYellow, nodes[nodes.Count - 1].X - xmin, nodes[nodes.Count - 1].Y - ymin, nodes[0].X - xmin, nodes[0].Y - ymin);
			return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
		}

		public override ReadOnlyCollection<byte> Subtypes { get { return subtypes; } }

		public override Sprite SubtypeImage(byte subtype)
		{
			if (subtype < sprites.Length)
				return sprites[subtype];
			return new Sprite(LevelData.UnknownSprite);
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
					return "Rings";
				case 1:
					return "Shoes";
				case 2:
					return "Time";
				default:
					return "Unknown";
			}
		}
	}
}