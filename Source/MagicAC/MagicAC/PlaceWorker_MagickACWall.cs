﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace MagicAC
{
    class PlaceWorker_MagicACWall : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot)
        {
            var vecNorth = center + IntVec3.North.RotatedBy(rot);
            if (!vecNorth.InBounds(this.Map))
            {
                return;
            }

            GenDraw.DrawFieldEdges(new List<IntVec3>() { vecNorth }, GenTemperature.ColorRoomHot);
            var room = vecNorth.GetRoom(this.Map);
            if (room == null || room.UsesOutdoorTemperature)
            {
                return;
            }
            GenDraw.DrawFieldEdges(room.Cells.ToList(), GenTemperature.ColorRoomHot);
        }
    }
}
