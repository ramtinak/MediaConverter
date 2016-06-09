///////////////////////////////////////////////////////////////////////
/////////////////////// DOWNLOAD FROM WIN NEVIS ///////////////////////
////////////////////// http://www.win-nevis.com ///////////////////////
//////////////////////////// Ramtin Jokar /////////////////////////////

using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Media_Converter
{

    class cc
    {
        private BitmapImage r_thumbnail = new BitmapImage(new Uri("ms-appx:///WinNevisLogo.png"));
        public BitmapImage Thumbnail { get { return r_thumbnail; } set { r_thumbnail = value; } }
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Duration { get; set; }
    }

    public class VideoCustomSettings
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Bitrate { get; set; }
    }
    public class AudioCustomSettings
    {
        public double Bitrate { get; set; }
    }
    public enum ConverterFormat
    {
        MP4=-1,
        WMV,
        WMA,
        AVI,
        MP3,
        WAV,
        M4A
    }
    public enum ConverterMode
    {
        Audio,
        Video = -1
    }
    public enum VideoProfile
    {
        HD1080p,
        HD720p,
        Wvga,
        Ntsc,
        Pal,
        Vga,
        Qvga,
        Custom = -1
    }
    public enum AudioProfile
    {
        High,
        Medium,
        Low,
        Auto = -1
    }
    public class CS
    {
        public static string Extension = "MP4";
        public static uint Width = 1280;
        public static uint Height = 720;
        public static uint VideoBitRate = 4500000;
        public static uint FrameRate = 30;

        public static uint SampleRate = 44100;
        public static uint AudioBitRate = 128000;
    }
}
