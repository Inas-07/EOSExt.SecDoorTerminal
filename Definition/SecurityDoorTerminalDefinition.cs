using System.Collections.Generic;
using ExtraObjectiveSetup.BaseClasses.CustomTerminalDefinition;
using GameData;

namespace EOSExt.SecurityDoorTerminal.Definition
{
    public class SecurityDoorTerminalDefinition : ExtraObjectiveSetup.BaseClasses.GlobalZoneIndex
    {
        public string FCDoorWorldEventObjectFilter { get; set; } = string.Empty;

        public SDTStateSetting StateSettings { get; set; } = new();

        public TerminalDefinition TerminalSettings { get; set; } = new();
    }
}
