﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace MagicAC
{
    class PlaceWorker_WallObject : PlaceWorker
    {
        public static string NeedsWall = "MagicAC_NeedsWall".Translate();

        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 center, Rot4 rot, Thing thingToIgnore = null)
        {
            IntVec3 c = center;
            Building wall = c.GetEdifice(this.Map);

            if (wall == null)
            {
                return NeedsWall;
            }

            if ((wall.def == null) || (wall.def.graphicData == null))
            {
                return NeedsWall;
            }

            return (wall.def.graphicData.linkFlags & LinkFlags.Wall) != 0 ? AcceptanceReport.WasAccepted : NeedsWall;
        }
    }
}
