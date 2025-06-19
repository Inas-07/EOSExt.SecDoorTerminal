using BepInEx.Unity.IL2CPP;
using ExtraObjectiveSetup.Utils;
using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOSExt.SecurityDoorTerminal
{
    public static class ExtraDoorUtils
    {
        public const string PLUGIN_NAME = "Inas.EOSExt.ExtraDoor";

        public static bool PluginLoaded => IL2CPPChainloader.Instance.Plugins.ContainsKey(PLUGIN_NAME);

        public static LG_SecurityDoor GetFCDoor(string worldEventObjectFilter)
        {
            if(!PluginLoaded)
            {
                EOSLogger.Error("ExtraDoorUtils: plugin not loaded");
                return null;
            }

            return DoGetFCDoor(worldEventObjectFilter);
        }

        private static LG_SecurityDoor DoGetFCDoor(string worldEventObjectFilter) 
        {
            return ExtraDoor.ForceConnectManager.Current.GetFCDoor(worldEventObjectFilter);
        }
    }
}
