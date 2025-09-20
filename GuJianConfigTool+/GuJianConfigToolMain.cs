using LXCustomTools.Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuJianConfigTool_
{
    public partial class GuJianConfigToolMain : Form
    {
        string radioLabel = null;
        string dmpels = "";
        List<string> dpiList = null;
        InitializationFileConfig ini_cfg = null;
        string[] shadowMapSize = {"512","1024","2048","4096"};
        string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public GuJianConfigToolMain()
        {
            InitializeComponent();
            InitGameConfig();
            InitDpiList();
        }

        private void InitDpiList()
        {
            dmpels = $"{ini_cfg.Read("Renderer", "ScreenWidth")} x {ini_cfg.Read("Renderer", "ScreenHeight")} ";
            dpiList = EnumDisplayInfo.GetDisplaySizeList();
            if (dpiList != null)
                comboBox_Dpi.Items.AddRange(dpiList.ToArray());
            int index = -1;
            for (int i = 0; i < dpiList.Count; i++)
            {
                if (dpiList[i] == dmpels)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                dmpels = dmpels + "(自定义)";
                comboBox_Dpi.Items.Add(dmpels);
                index = comboBox_Dpi.Items.Count - 1;
            }
            comboBox_Dpi.SelectedIndex = index;
        }

        private void InitGameConfig()
        {
            ini_cfg = new InitializationFileConfig($"{localApplicationData}\\Aurogon Games\\GuJian\\Config\\config.ini");
            label_path.Text = $"{localApplicationData}\\Aurogon Games\\GuJian\\Config\\config.ini";
            //全屏
            checkBox_Fullscreen.Checked = ini_cfg.ReadBoolean("Renderer", "Fullscreen");
            //图像质量 1 - 4
            string value = ini_cfg.Read("AppUserData", "GraphicsQuality");
            foreach (var btn in lxGroupBox_GraphicsQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //垂直同步 0 - 1
            value = ini_cfg.Read("Renderer", "VSync");
            foreach (var btn in lxGroupBox_VSync.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //抗锯齿质量 0 - 3
            value = ini_cfg.Read("Renderer", "AAQuality");
            foreach (var btn in lxGroupBox_AAQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //半透明抗锯齿 0 - 1
            value = ini_cfg.Read("Renderer", "AlphaAA");
            foreach (var btn in lxGroupBox_AlphaAA.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //对话显示 0 - 1
            value = ini_cfg.Read("AppUserData", "Dialogue");
            foreach (var btn in lxGroupBox_Dialogue.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //新手教学 0 - 1
            value = ini_cfg.Read("AppUserData", "NewbieEnable");
            foreach (var btn in lxGroupBox_NewbieEnable.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //烹饪动画 0 - 1
            value = ini_cfg.Read("AppUserData", "CookingAnimation");
            foreach (var btn in lxGroupBox_CookingAnimation.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;

            //视野距离 1 - 3
            value = ini_cfg.Read("Renderer", "ClipRange");
            foreach (var btn in lxGroupBox_ClipRange.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //粒子质量 1 - 3
            value = ini_cfg.Read("Renderer", "ParticleQuality");
            foreach (var btn in lxGroupBox_ParticleQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //阴影质量 0 - 3
            value = ini_cfg.Read("Renderer", "ShadowQuality");
            foreach (var btn in lxGroupBox_ShadowQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //贴图质量 1 - 3
            value = ini_cfg.Read("Renderer", "TextureQuality");
            foreach (var btn in lxGroupBox_TextureQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //HDR质量 0-2
            value = ini_cfg.Read("Renderer", "HDRQuality");
            foreach (var btn in lxGroupBox_HDRQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //景深 0-1
            value = ini_cfg.Read("Renderer", "DepthOfField");
            foreach (var btn in lxGroupBox_DepthOfField.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //光晕 0-1
            value = ini_cfg.Read("Renderer", "LensFlare");
            foreach (var btn in lxGroupBox_LensFlare.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //技能动画 0-1
            value = ini_cfg.Read("AppUserData", "SkillAnimation");
            foreach (var btn in lxGroupBox_SkillAnimation.Controls.OfType<RadioButton>().ToList())
                if (btn.Name.Substring(btn.Name.Length - 1, 1) == value)
                    btn.Checked = true;
            //音乐调节 0-100
            trackBar_MusicVolume.Value = ini_cfg.ReadInt("AppUserData", "MusicVolume", 0);
            //音效调节 0-100
            trackBar_SoundVolume.Value = ini_cfg.ReadInt("AppUserData", "SoundVolume", 0);
            //语音调节 0-100
            trackBar_VoiceVolume.Value = ini_cfg.ReadInt("AppUserData", "VoiceVolume", 0);
        }

        private void OnTrackBarValueChanged(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            if (trackBar.Name.Contains("MusicVolume"))
                label_MusicVolume.Text = trackBar.Value.ToString();
            if (trackBar.Name.Contains("SoundVolume"))
                label_SoundVolume.Text = trackBar.Value.ToString();
            if (trackBar.Name.Contains("VoiceVolume"))
                label_VoiceVolume.Text = trackBar.Value.ToString();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name.Contains("Confirm"))
            {
                SaveIniConfig();
                Close();
            }
            if (button.Name.Contains("Cancel"))
            {
                Close();
            }
            if (button.Name.Contains("Custom"))
            {
                panel_Custom.Visible = !panel_Custom.Visible;
            }
            if (button.Name.Contains("SaveData"))
            {
                ShellExecuteEx.Open( $"{localApplicationData}\\Aurogon Games\\GuJian\\SaveData");
            }
            if (button.Name.Contains("ScreenShots"))
            {
                if (!Directory.Exists($"{localApplicationData}\\Aurogon Games\\GuJian\\ScreenShots"))
                {
                    Directory.CreateDirectory($"{localApplicationData}\\Aurogon Games\\GuJian\\ScreenShots");
                }
                ShellExecuteEx.Open($"{localApplicationData}\\Aurogon Games\\GuJian\\ScreenShots");
            }
            if (button.Name.Contains("BackUps"))
            {
                FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string SaveData = $"{localApplicationData}\\Aurogon Games\\GuJian\\SaveData";
                    string backupPath = saveFileDialog.SelectedPath + "\\SaveData";
                    IOManager.CopyDir(SaveData, backupPath);
                    ShellExecuteEx.Open(backupPath);
                }
            }
            if (button.Name.Contains("About"))
            {
                GuJianConfigToolAbout toolAbout = new GuJianConfigToolAbout();
                toolAbout.ShowDialog();
            }
        }

        private void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string[] split = comboBox.SelectedItem.ToString().Replace("(自定义)","").Split('x');
            textBox_ScreenWidth.Text = split[0].Trim();
            textBox_ScreenHeight.Text = split[1].Trim();
        }

        public void SaveIniConfig()
        {
            //设置分辨率
            ini_cfg.Write("Renderer", "ScreenWidth", textBox_ScreenWidth.Text);
            ini_cfg.Write("Renderer", "ScreenHeight", textBox_ScreenHeight.Text);
            //设置全屏
            ini_cfg.Write("Renderer", "Fullscreen", checkBox_Fullscreen.Checked);
            //设置图像质量
            foreach (var btn in lxGroupBox_GraphicsQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("AppUserData", "GraphicsQuality", btn.Name.Substring(btn.Name.Length - 1, 1));
            //垂直同步 0 - 1
            foreach (var btn in lxGroupBox_VSync.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "VSync", btn.Name.Substring(btn.Name.Length - 1, 1));
            //抗锯齿质量 0 - 3
            foreach (var btn in lxGroupBox_AAQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "AAQuality", btn.Name.Substring(btn.Name.Length - 1, 1));
            //半透明抗锯齿 0 - 1
            foreach (var btn in lxGroupBox_AlphaAA.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "AlphaAA", btn.Name.Substring(btn.Name.Length - 1, 1));
            //对话显示 0 - 1
            foreach (var btn in lxGroupBox_Dialogue.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("AppUserData", "Dialogue", btn.Name.Substring(btn.Name.Length - 1, 1));
            //新手教学 0 - 1
            foreach (var btn in lxGroupBox_NewbieEnable.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("AppUserData", "NewbieEnable", btn.Name.Substring(btn.Name.Length - 1, 1));
            //烹饪动画 0 - 1
            foreach (var btn in lxGroupBox_CookingAnimation.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("AppUserData", "CookingAnimation", btn.Name.Substring(btn.Name.Length - 1, 1));

            //视野距离 1 - 3
            foreach (var btn in lxGroupBox_ClipRange.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "ClipRange", btn.Name.Substring(btn.Name.Length - 1, 1));
            //粒子质量 1 - 3
            foreach (var btn in lxGroupBox_ParticleQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "ParticleQuality", btn.Name.Substring(btn.Name.Length - 1, 1));
            //阴影质量 0 - 3
            foreach (var btn in lxGroupBox_ShadowQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                {
                    int value = int.Parse(btn.Name.Substring(btn.Name.Length - 1, 1));
                    ini_cfg.Write("Renderer", "ShadowQuality", value);
                    ini_cfg.Write("Renderer", "ShadowMapSize", shadowMapSize[value]);
                }
            //贴图质量 1 - 3
            foreach (var btn in lxGroupBox_TextureQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                {
                    int value = int.Parse(btn.Name.Substring(btn.Name.Length - 1, 1));
                    ini_cfg.Write("Renderer", "TextureQuality", value);
                    ini_cfg.Write("Renderer", "MipMapRange", value);
                }
            //HDR质量 0-2
            foreach (var btn in lxGroupBox_HDRQuality.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "HDRQuality", btn.Name.Substring(btn.Name.Length - 1, 1));
            //景深 0-1
            foreach (var btn in lxGroupBox_DepthOfField.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "DepthOfField", btn.Name.Substring(btn.Name.Length - 1, 1));
            //光晕 0-1
            foreach (var btn in lxGroupBox_LensFlare.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("Renderer", "LensFlare", btn.Name.Substring(btn.Name.Length - 1, 1));
            //技能动画 0-1
            foreach (var btn in lxGroupBox_SkillAnimation.Controls.OfType<RadioButton>().ToList())
                if (btn.Checked)
                    ini_cfg.Write("AppUserData", "SkillAnimation", btn.Name.Substring(btn.Name.Length - 1, 1));

            //音乐调节 0-100
            ini_cfg.Write("AppUserData", "MusicVolume", trackBar_MusicVolume.Value);
            //音效调节 0-100
            ini_cfg.Write("AppUserData", "SoundVolume", trackBar_SoundVolume.Value);
            //语音调节 0-100
            ini_cfg.Write("AppUserData", "VoiceVolume", trackBar_VoiceVolume.Value);
        }


        private void OnRadioButtonChecked(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            radioLabel = radioButton.Name;

            if (radioButton.Name.Contains("GraphicsQuality"))
            {
                foreach (var btn in lxGroupBox_GraphicsQuality.Controls.OfType<RadioButton>().ToList())
                {
                    if (btn.Name == radioButton.Name)
                    {
                        string index = btn.Name.Substring(btn.Name.Length - 1, 1);
                        if (int.Parse(index) != 4)
                        {
                            //视野距离
                            foreach (var btnSub in lxGroupBox_ClipRange.Controls.OfType<RadioButton>().ToList())
                                if (btnSub.Name.Substring(btnSub.Name.Length - 1, 1) == index)
                                    btnSub.Checked = true;
                            //粒子质量
                            foreach (var btnSub in lxGroupBox_ParticleQuality.Controls.OfType<RadioButton>().ToList())
                                if (btnSub.Name.Substring(btnSub.Name.Length - 1, 1) == index)
                                    btnSub.Checked = true;
                            //阴影质量
                            foreach (var btnSub in lxGroupBox_ShadowQuality.Controls.OfType<RadioButton>().ToList())
                                if (btnSub.Name.Substring(btnSub.Name.Length - 1, 1) == index)
                                    btnSub.Checked = true;
                            //阴影质量
                            foreach (var btnSub in lxGroupBox_ShadowQuality.Controls.OfType<RadioButton>().ToList())
                                if (btnSub.Name.Substring(btnSub.Name.Length - 1, 1) == (int.Parse(index) - 1).ToString())
                                    btnSub.Checked = true;
                            //贴图质量
                            foreach (var btnSub in lxGroupBox_TextureQuality.Controls.OfType<RadioButton>().ToList())
                                if (btnSub.Name.Substring(btnSub.Name.Length - 1, 1) == index)
                                    btnSub.Checked = true;
                        }
                        //HDR质量
                        Console.WriteLine(int.Parse(index));
                        rBtn_HDRQuality1.Checked = int.Parse(index) < 3;
                        rBtn_HDRQuality2.Checked = !rBtn_HDRQuality1.Checked;
                        //景深
                        rBtn_DepthOfField0.Checked = int.Parse(index) == 1;
                        rBtn_DepthOfField1.Checked = !rBtn_DepthOfField0.Checked;
                        //光晕
                        rBtn_LensFlare0.Checked = int.Parse(index) == 1;
                        rBtn_LensFlare1.Checked = !rBtn_LensFlare0.Checked;
                    }
                }
            }
            else
            {
                rBtn_GraphicsQuality4.Checked = true;
            }
            
        }

        private void GuJianConfigToolMain_Load(object sender, EventArgs e)
        {

        }

        private void lxGroupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void label_path_Click(object sender, EventArgs e)
        {

        }
    }
}
