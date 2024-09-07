using System;
using System.Collections.Generic;
using System.Text;

namespace bbpfer.CustomContent.Structures.Builders
{
    public class ClockBuilder : ObjectBuilder
    {
		public override void Build(EnvironmentController ec, LevelBuilder builder, RoomController room, System.Random cRng)
		{
			foreach (var c in room.GetTilesOfShape(allowedShapes, false))
				if (c.HasFreeWall && cRng.NextDouble() <= chance)
					ec.BuildPoster(clock, c, c.RandomUncoveredDirection(cRng));
		}


		public List<TileShape> allowedShapes = new List<TileShape>()
		{
			TileShape.Corner,
			TileShape.Single,
			TileShape.End,
			TileShape.Straight
		};
		public PosterObject clock = Posters.clockPoster;
		public float chance = 0.07f;
	}
}
