﻿using EOSExt.SecurityDoorTerminal.Definition;
using ExtraObjectiveSetup.BaseClasses;
using ExtraObjectiveSetup.Utils;
using GameData;

namespace EOSExt.SecurityDoorTerminal
{
    public sealed partial class SecurityDoorTerminalManager : ZoneDefinitionManager<SecurityDoorTerminalDefinition>
    {
        private enum SDTWardenEvents
        {
            ADD_OVERRIDE_COMMAND = 1000, 
        }

        private const string OVERRIDE_COMMAND = "ACCESS_OVERRIDE";

        private void WardenEvent_AddOverrideCommand(WardenObjectiveEventData e)
        {
            int i = levelSDTs.FindIndex((tp) => {
                var linkToNode = tp.sdt.LinkedDoor.Gate.m_linksTo.m_courseNode;
                return linkToNode.m_dimension.DimensionIndex == e.DimensionIndex && linkToNode.LayerType == e.Layer && linkToNode.m_zone.LocalIndex == e.LocalIndex;
            });

            if (i == -1)
            {
                EOSLogger.Error($"SDT_AddOverrideCommand: SDT not found on door to {(e.DimensionIndex, e.Layer, e.LocalIndex)}");
                return;
            }

            var targetSDT = levelSDTs[i].sdt;

            AddOverrideCommandWithAlarmText(targetSDT);
            EOSLogger.Debug($"SDT_AddOverrideCommand: added for SDT {(e.DimensionIndex, e.Layer, e.LocalIndex)}");
        }
    }
}
