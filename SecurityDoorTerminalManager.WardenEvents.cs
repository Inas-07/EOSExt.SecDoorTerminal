using EOSExt.SecurityDoorTerminal.Definition;
using ExtraObjectiveSetup.BaseClasses;
using ExtraObjectiveSetup.Utils;
using GameData;
using SecDoorTerminalInterface;
using System;

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
            Predicate<(SecDoorTerminal sdt, SecurityDoorTerminalDefinition def)> p = null;

            if(e.WorldEventObjectFilter != null && e.WorldEventObjectFilter.Length > 0)
            {
                p = (tp) => e.WorldEventObjectFilter.Equals(tp.def.FCDoorWorldEventObjectFilter);
            }
            else
            {
                p = (tp) =>
                {
                    var linkToNode = tp.sdt.LinkedDoor.Gate.m_linksTo.m_courseNode;
                    return linkToNode.m_dimension.DimensionIndex == e.DimensionIndex && linkToNode.LayerType == e.Layer && linkToNode.m_zone.LocalIndex == e.LocalIndex;
                };
            }

            int i = levelSDTs.FindIndex(p);

            if (i == -1)
            {
                if(e.WorldEventObjectFilter != null && e.WorldEventObjectFilter.Length > 0)
                {
                    EOSLogger.Error($"SDT_AddOverrideCommand: SDT not found on ExtraDoor(WorldEventObjectFilter) '{e.WorldEventObjectFilter}'");
                }
                else
                {
                    EOSLogger.Error($"SDT_AddOverrideCommand: SDT not found on door to {(e.DimensionIndex, e.Layer, e.LocalIndex)}");
                }
                return;
            }

            var targetSDT = levelSDTs[i].sdt;

            AddOverrideCommandWithAlarmText(targetSDT);

            if (e.WorldEventObjectFilter != null && e.WorldEventObjectFilter.Length > 0)
            {
                EOSLogger.Debug($"SDT_AddOverrideCommand: added for ExtraDoor(WorldEventObjectFilter) '{e.WorldEventObjectFilter}'");
            }
            else
            {
                EOSLogger.Debug($"SDT_AddOverrideCommand: added for SDT {(e.DimensionIndex, e.Layer, e.LocalIndex)}");
            }
        }
    }
}
