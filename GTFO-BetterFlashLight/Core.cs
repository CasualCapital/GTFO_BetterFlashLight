using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace GTFO_BetterFlashLight
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    [BepInProcess("GTFO.exe")]
    public class Core : BasePlugin
    {
        public const string
            NAME = "BetterFlashLight Plugin",
            MODNAME = "BetterFlashLight",
            AUTHOR = "Henry0012",
            GUID =  AUTHOR + "." + MODNAME,
            VERSION = "7.3.0";

        public static ManualLogSource log;


        private Harmony HarmonyPatches { get; set; }


        public override void Load()
        {
            log = Log;
            HarmonyPatches = new Harmony(GUID);
            HarmonyPatches.PatchAll();
        }
    }
}