using BepInEx;
using BepInEx.Unity.IL2CPP;
using ExtraObjectiveSetup.Utils;
using ExtraObjectiveSetup.JSON;
using ExtraObjectiveSetup.JSON.MTFOPartialData;

namespace EOSExt.SecurityDoorTerminal
{
    [BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Inas.ExtraObjectiveSetup", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("SecDoorTerminalInterface", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(MTFOPartialDataUtil.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(InjectLibUtil.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(ExtraDoorUtils.PLUGIN_NAME, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(AUTHOR + "." + PLUGIN_NAME, PLUGIN_NAME, VERSION)]
    
    public class EntryPoint: BasePlugin
    {
        public const string AUTHOR = "Inas";
        public const string PLUGIN_NAME = "EOSExt.SecurityDoorTerminal";
        public const string VERSION = "1.1.2";

        public override void Load()
        {
            SetupManagers();
            EOSLogger.Log("ExtraObjectiveSetup.SecurityDoorTerminal loaded.");
        }

        /// <summary>
        /// Explicitly invoke Init() to all managers to eager-load, which in the meantime defines chained puzzle creation order if any
        /// </summary>
        private void SetupManagers()
        {
            SecurityDoorTerminalManager.Current.Init();
        }
    }
}

