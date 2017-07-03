using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace MagicAC
{
    public class PlaceWorker_WallChecker : PlaceWorker
    {
        public static string WallAlreadyOccupied = "MagicAC_WallAlreadyOccupied".Translate();

        public override AcceptanceReport AllowsPlacing(BuildableDef def, IntVec3 center, Rot4 rot, Thing thingToIgnore = null)
        {
            var things = Find.VisibleMap.thingGrid.ThingsListAt(center);
            if (things.Exists(s => s is IWallAttachable))
            {
                return WallAlreadyOccupied;
            }
            return true;
        }
    }
}
