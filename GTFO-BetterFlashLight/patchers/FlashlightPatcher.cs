using BepInEx;
using BepInEx.Configuration;
using GameData;
using Gear;
using GTFO_BetterFlashLight.enums;
using HarmonyLib;

namespace GTFO_BetterFlashLight.patchers
{
    [HarmonyPatch]
    public class FlashLightPatcher 
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(GearManager), nameof(GearManager.LoadOfflineGearDatas))]
        private static void PatchFlashLight(ref GearManager __instance)
        {
            var configPath = Path.Combine(Paths.ConfigPath, "BetterFlashLight.cfg");
            ConfigFile config = new(configPath, true);
            ConfigEntry<FlashLightMode> configMode = config.Bind("General",
                "Mode",
                FlashLightMode.MAXIMUM,
                "Flashlight mode.");
            
            FlashlightSettingsDataBlock[] flashlights = GameDataBlockBase<FlashlightSettingsDataBlock>.Wrapper.Blocks.ToArray();
            
            foreach (var flashlight in flashlights)
            {                
                if (flashlight.persistentID != 4)
                {
                    switch (configMode.Value)
                    {
                        case FlashLightMode.MAXIMUM:
                            flashlight.angle = 70.0f;
                            flashlight.intensity = 0.25f;
                            flashlight.range = 25.0f;
                            break;
                        case FlashLightMode.INCREASE_25_FOV:
                            flashlight.angle = LimitFov(flashlight.angle * 1.25f, 70.0f);
                            flashlight.intensity *= 1.25f;
                            break;
                        case FlashLightMode.INCREASE_30_FOV_EXTENDED_RANGE:
                            flashlight.angle = LimitFov(flashlight.angle * 1.3f, 70.0f);
                            flashlight.intensity *= 1.3f;
                            flashlight.range = 20.0f;
                            break;
                    }
                }
            }
        }

        private static float LimitFov(float newValue, float limit)
        {
            if(newValue > limit)
            {
                return limit;
            }
            return newValue;
        }
    }
}
