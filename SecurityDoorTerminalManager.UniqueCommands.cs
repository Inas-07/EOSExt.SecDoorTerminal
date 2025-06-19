using ExtraObjectiveSetup.BaseClasses;
using ExtraObjectiveSetup.BaseClasses.CustomTerminalDefinition;
using SecDoorTerminalInterface;
using GameData;
using ChainedPuzzles;
using GTFO.API.Extensions;
using EOSExt.SecurityDoorTerminal.Definition;
using ExtraObjectiveSetup.Utils;
using LevelGeneration;
using Microsoft.VisualBasic;

namespace EOSExt.SecurityDoorTerminal
{
    public sealed partial class SecurityDoorTerminalManager : ZoneDefinitionManager<SecurityDoorTerminalDefinition>
    {
        private void BuildSDT_UniqueCommands(SecDoorTerminal sdt, SecurityDoorTerminalDefinition def)
        {
            // NOTE: I could have just add TerminalPlacementData at invoke of SecDoorTerminal.Place,
            // but that would perterb existing chained puzzle instance creation order,
            // So i have to put it here
            var tpdata = new TerminalPlacementData() 
            {
                UniqueCommands = def.TerminalSettings.UniqueCommands.ConvertAll(x => x.ToVanillaDataType()).ToIl2Cpp(),
            };

            // TODO: this call is not the one that was used in R7C2 dimension to realize EventBreak
            new LG_TerminalUniqueCommandsSetupJob(sdt.ComputerTerminal, tpdata).Build();

            foreach(var cmd in def.TerminalSettings.UniqueCommands)
            {
                if (sdt.ComputerTerminal.m_command.TryGetCommand(cmd.Command, out var term_cmd, out var _, out var _) 
                    && sdt.ComputerTerminal.GetCommandRule(term_cmd) == TERM_CommandRule.Normal)
                {
                    for(int eventIndex = 0; eventIndex < cmd.CommandEvents.Count; eventIndex++)
                    {
                        if(sdt.ComputerTerminal.TryGetChainPuzzleForCommand(term_cmd, eventIndex, out var cpinstance))
                        {
                            cpinstance.OnPuzzleSolved += new System.Action(cpinstance.ResetProgress);
                        }
                    }
                }
            }

            // NOTE: 'var cmd' is malformed thx to il2cpp
            //foreach (var cmd in sdt.ComputerTerminal.m_commandToChainPuzzleMap.Keys)
            //{
            //    if (sdt.ComputerTerminal.GetCommandRule(cmd.Item1) == TERM_CommandRule.Normal
            //        && sdt.ComputerTerminal.TryGetChainPuzzleForCommand(cmd.Item1, cmd.Item2, out var cpinstance))
            //    {
            //        cpinstance.OnPuzzleSolved += new System.Action(cpinstance.ResetProgress);
            //    }
            //}
        }

        private void BuildLevelSDTs_UniqueCommands()
        {
            levelSDTs.ForEach((tp) => BuildSDT_UniqueCommands(tp.sdt, tp.def));
        }
    }
}
