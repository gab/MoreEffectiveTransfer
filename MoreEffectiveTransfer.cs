﻿using ICities;
using MoreEffectiveTransfer.Util;
using System.IO;


namespace MoreEffectiveTransfer
{
    public class MoreEffectiveTransfer : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool fixUnRouteTransfer = false;
        public static bool debugMode = false;
        public static bool considerProirity = false;

        public string Name
        {
            get { return "More Effective Transfer Manager"; }
        }

        public string Description
        {
            get { return "Optimize transfer manager in vanilla game. match the shortest transfer bettween offers"; }
        }

        public void OnEnabled()
        {
            IsEnabled = true;
            FileStream fs = File.Create("MoreEffectiveTransfer.txt");
            fs.Close();
        }

        public void OnDisabled()
        {
            IsEnabled = false;
        }

        public static void SaveSetting()
        {
            FileStream fs = File.Create("MoreEffectiveTransfer_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(fixUnRouteTransfer);
            streamWriter.WriteLine(debugMode);
            streamWriter.WriteLine(considerProirity);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("MoreEffectiveTransfer_setting.txt"))
            {
                FileStream fs = new FileStream("MoreEffectiveTransfer_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();
                if (strLine == "True")
                {
                    fixUnRouteTransfer = true;
                }
                else
                {
                    fixUnRouteTransfer = false;
                }

                strLine = sr.ReadLine();
                if (strLine == "True")
                {
                    debugMode = true;
                }
                else
                {
                    debugMode = false;
                }

                strLine = sr.ReadLine();
                if (strLine == "True")
                {
                    considerProirity = true;
                }
                else
                {
                    considerProirity = false;
                }

                sr.Close();
                fs.Close();
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            LoadSetting();
            UIHelperBase group1 = helper.AddGroup(Localization.Get("FIX_UNROUTED_TRANSFER_MATCH_DESCRIPTION"));
            group1.AddCheckbox(Localization.Get("FIX_UNROUTED_TRANSFER_MATCH_ENALBE"), fixUnRouteTransfer, (index) => fixUnRouteTransferEnable(index));
            UIHelperBase group2 = helper.AddGroup(Localization.Get("DEBUG_MODE_DESCRIPTION"));
            group2.AddCheckbox(Localization.Get("DEBUG_MODE_DESCRIPTION_ENALBE"), debugMode, (index) => debugModeEnable(index));
            UIHelperBase group3 = helper.AddGroup(Localization.Get("EXPERIMENTAL_FEATURES"));
            group3.AddCheckbox(Localization.Get("CONSIDER_PROIRITY"), considerProirity, (index) => considerProirityEnable(index));
            SaveSetting();
        }

        public void fixUnRouteTransferEnable(bool index)
        {
            fixUnRouteTransfer = index;
            SaveSetting();
        }

        public void debugModeEnable(bool index)
        {
            debugMode = index;
            SaveSetting();
        }

        public void considerProirityEnable(bool index)
        {
            considerProirity = index;
            SaveSetting();
        }
    }
}
