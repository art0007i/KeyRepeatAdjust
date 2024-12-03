using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrooxEngine;

namespace KeyRepeatAdjust;

public class KeyRepeatAdjust : ResoniteMod
{
    public const string VERSION = "1.0.0";
    public override string Name => "KeyRepeatAdjust";
    public override string Author => "art0007i";
    public override string Version => VERSION;
    public override string Link => "https://github.com/art0007i/KeyRepeatAdjust/";

    [AutoRegisterConfigKey]
    public static ModConfigurationKey<float> KEY_REPEAT_DELAY = new("repeat_delay", "Amount of time in seconds before a key will be automatically pressed while holding it.", () => 0.25f);
    [AutoRegisterConfigKey]
    public static ModConfigurationKey<float> KEY_REPEAT_INTERVAL = new("repeat_interval", "Amount of presses per second that will be performed while holding a key.", () => 30f);

    public override void OnEngineInit()
    {
        //Harmony harmony = new Harmony("me.art0007i.KeyRepeatAdjust");
        var config = GetConfiguration();

        Engine.Current.OnReady += () =>
        {
            Engine.Current.InputInterface.KeyRepeatWait = config.GetValue(KEY_REPEAT_DELAY);
            Engine.Current.InputInterface.KeyRepeatInterval = 1f/config.GetValue(KEY_REPEAT_INTERVAL);
        };
        
        config.OnThisConfigurationChanged += (e) =>
        {
            if(e.Key == KEY_REPEAT_DELAY) Engine.Current.InputInterface.KeyRepeatWait = config.GetValue(KEY_REPEAT_DELAY);
            if(e.Key == KEY_REPEAT_INTERVAL) Engine.Current.InputInterface.KeyRepeatInterval = 1f / config.GetValue(KEY_REPEAT_INTERVAL);
        };
    }
}
