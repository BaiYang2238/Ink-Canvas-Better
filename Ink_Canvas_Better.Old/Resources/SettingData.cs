using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Resources
{
    public class SettingData
    {
        [JsonProperty("startupAndUpdate")]
        public StartupAndUpdate StartupAndUpdate { get; set; } = new StartupAndUpdate();

        [JsonProperty("appearance")]
        public Appearance Appearance { get; set; } = new Appearance();

        [JsonProperty("ppt")]
        public PPT PPT { get; set; } = new PPT();

        [JsonProperty("experimentalFeatures")]
        public ExperimentalFeatures ExperimentalFeatures { get; set; } = new ExperimentalFeatures();

        [JsonProperty("others")]
        public Others Others { get; set; } = new Others();

        [JsonProperty("runtime")]
        public Runtime Runtime { get; set; } = new Runtime();
    }

    /// <summary>
    /// Startup and update
    /// </summary>
    public class StartupAndUpdate
    {
        [JsonProperty("isAutoUpdate")]//TODO
        public bool IsAutoUpdate { get; set; } = true;

        [JsonProperty("isAutoUpdateWithProxy")]
        public bool IsAutoUpdateWithProxy { get; set; } = false;

        // TODO
        [JsonProperty("autoUpdateProxy")]
        public string AutoUpdateProxy { get; set; } = "https://mirror.ghproxy.com/";

        [JsonProperty("isAutoUpdateWithSilence")]
        public bool IsAutoUpdateWithSilence { get; set; } = false;

        [JsonProperty("isAutoUpdateWithSilenceStartTime")]
        public string AutoUpdateWithSilenceStartTime { get; set; } = "00:00";

        [JsonProperty("isAutoUpdateWithSilenceEndTime")]
        public string AutoUpdateWithSilenceEndTime { get; set; } = "00:00";

        [JsonProperty("isFoldAtStartup")]
        public bool IsFoldAtStartup { get; set; } = false;

        [JsonProperty("isAutoStartup")]
        public bool IsAutoStartup { get; set; } = true;
    }

    public class Appearance
    {
        [JsonProperty("isEnableDisPlayFloatBarText")]
        public bool IsEnableDisPlayFloatBarText { get; set; } = false;

        [JsonProperty("isEnableDisPlayNibModeToggler")]
        public bool IsEnableDisPlayNibModeToggler { get; set; } = true;

        [JsonProperty("FloatingBarScaling")]
        public float FloatingBarScaling { get; set; } = 1.0f;

        [JsonProperty("isShowHideControlButton")]
        public bool IsShowHideControlButton { get; set; } = false;

        [JsonProperty("isShowLRSwitchButton")]
        public bool IsShowLRSwitchButton { get; set; } = false;

        [JsonProperty("isShowModeFingerToggleSwitch")]
        public bool IsShowModeFingerToggleSwitch { get; set; } = true;

        [JsonProperty("theme")]
        public bool? Theme { get; set; } = null;
    }

    /// <summary>
    /// PPT
    /// </summary>
    public class PPT
    {

    }

    /// <summary>
    /// Experimental features
    /// </summary>
    public class ExperimentalFeatures
    {
        
    }

    public class Others
    {
        [JsonProperty("isShowLanguageWindow")]
        public bool IsShowLanguageWindow { get; set; } = true;

        [JsonProperty("isShowWelcomeScreen")]
        public bool IsShowWelcomeScreen { get; set; } = true;

        [JsonProperty("language")]
        public String Language { get; set; } = "en";
    }

    public class Runtime
    {
        [JsonProperty("inkStyle")]
        public InkStyle InkStyle { get; set; } = InkStyle.Default;

        [JsonProperty("bluntnessFactor")]
        public float BluntnessFactor { get; set; } = (49f / 50f);// influent writing style
    }

    public enum OptionalOperation
    {
        Yes,
        No,
        Ask
    }

    public enum InkStyle
    {
        Default,
        Simulative
    }
}
