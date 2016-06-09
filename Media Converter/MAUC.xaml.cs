///////////////////////////////////////////////////////////////////////
/////////////////////// DOWNLOAD FROM WIN NEVIS ///////////////////////
////////////////////// http://www.win-nevis.com ///////////////////////
//////////////////////////// Ramtin Jokar /////////////////////////////

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Media_Converter
{
    public sealed partial class MAUC : UserControl
    {
        public MAUC()
        {
            this.InitializeComponent();
        }

        public Visibility GPVisibility
        {
            get { return gp.Visibility; }
            set { gp.Visibility = value; }
        }

        private void comboExtension_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboExtension != null && comboExtension.SelectedIndex != -1
                && video != null && videoSize != null && videoBitRate != null &&
                videoFrameRate != null)
            {
                CS.Extension = ((ComboBoxItem)comboExtension.SelectedItem).Content.ToString();
                if (comboExtension.SelectedIndex == 3 ||
                    comboExtension.SelectedIndex == 4 ||
                    comboExtension.SelectedIndex == 5 ||
                    comboExtension.SelectedIndex == 6)
                {
                    video.Visibility = Visibility.Collapsed;
                    videoSize.Visibility = Visibility.Collapsed;
                    videoBitRate.Visibility = Visibility.Collapsed;
                    videoFrameRate.Visibility = Visibility.Collapsed;
                }
                else
                {
                    video.Visibility = Visibility.Visible;
                    videoSize.Visibility = Visibility.Visible;
                    videoBitRate.Visibility = Visibility.Visible;
                    videoFrameRate.Visibility = Visibility.Visible;
                }
            }
        }

        private void startTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {

        }

        private void endTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {

        }

        private void comboVideoSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboVideoSize!= null && comboVideoSize.SelectedIndex!=-1)
            {
                switch (comboVideoSize.SelectedIndex)
                {
                    case 0:
                        CS.Width = 1920;
                        CS.Height = 1080;
                        break;
                    case 1:
                        CS.Width = 1280;
                        CS.Height = 720;
                        break;
                    case 2:
                        CS.Width = 800;
                        CS.Height = 480;
                        break;
                    case 3:
                        CS.Width = 720;
                        CS.Height = 480;
                        break;
                    case 4:
                        CS.Width = 720;
                        CS.Height = 576;
                        break;
                    case 5:
                        CS.Width = 640;
                        CS.Height = 480;
                        break;
                    case 6:
                        CS.Width = 320;
                        CS.Height = 240;
                        break;
                }
                //switch (comboVideoSize.SelectedIndex)
                //{
                //    case 0:
                //        break;
                //    case 1:
                //        break;
                //    case 2:
                //        break;
                //    case 3:
                //        break;
                //    case 4:
                //        break;
                //    case 5:
                //        break;
                //    case 6:
                //        break;
                //}
            }
        }

        private void comboVideoBitRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboVideoBitRate != null && comboVideoBitRate.SelectedIndex != -1)
            {
                switch (comboVideoBitRate.SelectedIndex)
                {
                    case 0:
                        CS.VideoBitRate = 6000000;
                        break;
                    case 1:
                        CS.VideoBitRate = 5500000;
                        break;
                    case 2:
                        CS.VideoBitRate = 5000000;
                        break;
                    case 3:
                        CS.VideoBitRate = 4500000;
                        break;
                    case 4:
                        CS.VideoBitRate = 4000000;
                        break;
                    case 5:
                        CS.VideoBitRate = 3500000;
                        break;
                    case 6:
                        CS.VideoBitRate = 3000000;
                        break;
                    case 7:
                        CS.VideoBitRate = 2500000;
                        break;
                    case 8:
                        CS.VideoBitRate = 2000000;
                        break;
                    case 9:
                        CS.VideoBitRate = 1800000;
                        break;
                    case 10:
                        CS.VideoBitRate = 1600000;
                        break;
                    case 11:
                        CS.VideoBitRate = 1500000;
                        break;
                    case 12:
                        CS.VideoBitRate = 1200000;
                        break;
                    case 13:
                        CS.VideoBitRate = 1000000;
                        break;
                    case 14:
                        CS.VideoBitRate = 800000;
                        break;
                    case 15:
                        CS.VideoBitRate = 600000;
                        break;
                    case 16:
                        CS.VideoBitRate = 500000;
                        break;
                    case 17:
                        CS.VideoBitRate = 400000;
                        break;
                    case 18:
                        CS.VideoBitRate = 300000;
                        break;
                    case 19:
                        CS.VideoBitRate = 256000;
                        break;
                    case 20:
                        CS.VideoBitRate = 192000;
                        break;
                    case 21:
                        CS.VideoBitRate = 128000;
                        break;
                    case 22:
                        CS.VideoBitRate = 96000;
                        break;
                }
            }
        }

        private void comboVideoFrameRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboVideoFrameRate!= null && comboVideoFrameRate.SelectedIndex!=-1)
            {
                switch (comboVideoFrameRate.SelectedIndex)
                {
                    case 0:
                        CS.FrameRate = 8;
                        break;
                    case 1:
                        CS.FrameRate = 10;
                        break;
                    case 2:
                        CS.FrameRate = 12;
                        break;
                    case 3:
                        CS.FrameRate = 15;
                        break;
                    case 4:
                        CS.FrameRate = 20;
                        break;
                    case 5:
                        CS.FrameRate = 23;
                        break;
                    case 6:
                        CS.FrameRate = 24;
                        break;
                    case 7:
                        CS.FrameRate = 25;
                        break;
                    case 8:
                        CS.FrameRate = 29;
                        break;
                    case 9:
                        CS.FrameRate = 30;
                        break;
                    case 10:
                        CS.FrameRate = 50;
                        break;
                    case 11:
                        CS.FrameRate = 60;
                        break;
                }
            }
        }

        private void comboAudioSampleRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboAudioSampleRate != null && comboAudioSampleRate.SelectedIndex != -1)
            {
                try
                {
                    CS.SampleRate = uint.Parse(((ComboBoxItem)comboAudioSampleRate.SelectedItem).Content.ToString());
                }
                catch (Exception ex) { vars.Output("comboAudioSampleRate_SelectionChanged ex: " + ex.Message); }
            }
        }

        private void comboAudioBitRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboAudioBitRate != null && comboAudioBitRate.SelectedIndex != -1)
            {
                switch (comboAudioBitRate.SelectedIndex)
                {
                    case 0:
                        CS.AudioBitRate = 32000;
                        break;
                    case 1:
                        CS.AudioBitRate = 64000;
                        break;
                    case 2:
                        CS.AudioBitRate = 96000;
                        break;
                    case 3:
                        CS.AudioBitRate = 112000;
                        break;
                    case 4:
                        CS.AudioBitRate = 128000;
                        break;
                    case 5:
                        CS.AudioBitRate = 160000;
                        break;
                    case 6:
                        CS.AudioBitRate = 192000;
                        break;
                    case 7:
                        CS.AudioBitRate = 256000;
                        break;
                    case 8:
                        CS.AudioBitRate = 320000;
                        break;
                    case 9:
                        CS.AudioBitRate = 448000;
                        break;
                    case 10:
                        CS.AudioBitRate = 512000;
                        break;
                    case 11:
                        CS.AudioBitRate = 640000;
                        break;
                }
            }
        }
    }
}
