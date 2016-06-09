///////////////////////////////////////////////////////////////////////
/////////////////////// DOWNLOAD FROM WIN NEVIS ///////////////////////
////////////////////// http://www.win-nevis.com ///////////////////////
//////////////////////////// Ramtin Jokar /////////////////////////////

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;


namespace Media_Converter
{
    public sealed partial class MainPage : Page
    {
        #region Variables
        Stopwatch stopwatch = new Stopwatch();
        CoreDispatcher _dispatcher = Window.Current.Dispatcher;
        CancellationTokenSource _cts;
        string _OutputFileName = string.Empty;
        MediaEncodingProfile _Profile;
        StorageFile _InputFile = null;
        StorageFile _OutputFile = null;
        MediaTranscoder _Transcoder = new MediaTranscoder();
        private ApplicationViewTitleBar titleBar;
        bool IsDisplayed { get; set; }
        #endregion

        #region Constructor
        public MainPage()
        {
            this.InitializeComponent();
            _cts = new CancellationTokenSource();
            titleBar = ApplicationView.GetForCurrentView().TitleBar;
            Loaded += MainPage_Loaded;
        }
        #endregion

        #region Slide control with animation
        public void Display()
        {
            MASettings.Visibility = Windows.UI.Xaml.Visibility.Visible;

            try
            {
                SlideAnimation(false);
            }
            catch (Exception) { }
        }

        public void Hide()
        {
            try
            {
                SlideAnimation(true);
            }
            catch (Exception) { }
        }

        private void SlideAnimation(bool hide)
        {
            Storyboard s = new Storyboard();

            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));

            if (hide)
                da.To = +1500;
            else
                da.To = 0;

            s.Children.Add(da);

            Storyboard.SetTarget(da, transform);
            Storyboard.SetTargetProperty(da, "TranslateX");

            s.Completed += s_Completed;
            s.Begin();
        }

        void s_Completed(object sender, object e)
        {
            IsDisplayed = !IsDisplayed;
        }
        #endregion

        #region Button Click Event Handlers
        private void HamburgerRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        async private void StartRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (_InputFile == null)
            {
                await new Windows.UI.Popups.MessageDialog("Please import a video or music file to convert").ShowAsync();
                return;
            }
            VideoEncodingQuality videoQuallity = VideoEncodingQuality.Wvga;
            if (CS.Width == 1920 && CS.Height == 1080)
                videoQuallity = VideoEncodingQuality.HD1080p;
            else if(CS.Width == 1280 && CS.Height == 720)
                videoQuallity = VideoEncodingQuality.HD720p;
            else if (CS.Width == 800 && CS.Height == 480)
                videoQuallity = VideoEncodingQuality.Wvga;
            else if (CS.Width == 720 && CS.Height == 480)
                videoQuallity = VideoEncodingQuality.Ntsc;
            else if (CS.Width == 720 && CS.Height == 576)
                videoQuallity = VideoEncodingQuality.Pal;
            else if (CS.Width == 640 && CS.Height == 480)
                videoQuallity = VideoEncodingQuality.Vga;
            else if (CS.Width == 320 && CS.Height == 240)
                videoQuallity = VideoEncodingQuality.Qvga;
            if (CS.Extension.ToLower().Equals("mp4"))
                _Profile = MediaEncodingProfile.CreateMp4(videoQuallity);
            else if (CS.Extension.ToLower().Equals("avi"))
                _Profile = MediaEncodingProfile.CreateAvi(videoQuallity);
            else if (CS.Extension.ToLower().Equals("wmv"))
                _Profile = MediaEncodingProfile.CreateWmv(videoQuallity);

            else if (CS.Extension.ToLower().Equals("wma"))
                _Profile = MediaEncodingProfile.CreateWma(AudioEncodingQuality.High);
            else if (CS.Extension.ToLower().Equals("wav"))
                _Profile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
            else if (CS.Extension.ToLower().Equals("mp3"))
                _Profile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            else if (CS.Extension.ToLower().Equals("m4a"))
                _Profile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.High);
            try
            {
                if (CS.Extension.ToLower().Equals("mp4")||
                    CS.Extension.ToLower().Equals("avi") ||
                    CS.Extension.ToLower().Equals("wmv"))
                {
                    _Profile.Video.Width = CS.Width;
                    _Profile.Video.Height = CS.Height;
                    _Profile.Video.Bitrate = CS.VideoBitRate;
                }
                _Profile.Audio.Bitrate = CS.AudioBitRate;
                _Profile.Audio.SampleRate = CS.SampleRate;
            }
            catch (Exception exception)
            {
                vars.Output("StartRadioButton_Click ex: " + exception.Message);
                _Profile = null;
                return;
            }
            _cts = new CancellationTokenSource();

            try
            {
                if (_InputFile != null && _Profile != null)
                {
                    _OutputFileName = System.IO.Path.GetFileNameWithoutExtension(_InputFile.Path);
                    string extension = CS.Extension;
                    _OutputFileName = string.Format("[MC] {0}.{1}", _OutputFileName, extension);
                    _OutputFile = await KnownFolders.VideosLibrary.CreateFileAsync(_OutputFileName, CreationCollisionOption.GenerateUniqueName);

                    var preparedTranscodeResult = await _Transcoder.PrepareFileTranscodeAsync(_InputFile, _OutputFile, _Profile);

                    _Transcoder.VideoProcessingAlgorithm = MediaVideoProcessingAlgorithm.Default;


                    if (preparedTranscodeResult.CanTranscode)
                    {
                        var progress = new Progress<double>(TranscodeProgress);
                        stopwatch.Reset();
                        stopwatch.Start();
                        MASettings.GPVisibility = Visibility.Visible;
                        StopRadioButton.Visibility = Visibility.Visible;
                        await preparedTranscodeResult.TranscodeAsync().AsTask(_cts.Token, progress);
                    }
                    else
                    {
                        vars.Output(preparedTranscodeResult.FailureReason);
                        MASettings.GPVisibility = Visibility.Collapsed;
                        await new Windows.UI.Popups.MessageDialog("Failed to start.\r\nError: " + preparedTranscodeResult.FailureReason).ShowAsync();
                        StopRadioButton.Visibility = Visibility.Collapsed;

                    }
                }
            }
            catch (TaskCanceledException)
            {
                MASettings.GPVisibility = Visibility.Collapsed;

                StopRadioButton.Visibility = Visibility.Collapsed;

                vars.Output("Transcode Canceled");
            }
            catch (Exception exception)
            {
                MASettings.GPVisibility = Visibility.Collapsed;

                StopRadioButton.Visibility = Visibility.Collapsed;
                await new Windows.UI.Popups.MessageDialog("Failed to convert.\r\nError: " + exception.Message).ShowAsync();

                vars.Output(exception.Message);
            }
        }

        private void StopRadioButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResult.Text = "Convert stopped.";
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
            catch (Exception ex) { vars.Output("StopRadioButton_Click ex: " + ex.Message); }
        }

        async private void AddRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".wmv");
            picker.FileTypeFilter.Add(".mp4");
            picker.FileTypeFilter.Add(".avi");
            picker.FileTypeFilter.Add(".3gp");
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".mp4");
            picker.FileTypeFilter.Add(".flac");
            picker.FileTypeFilter.Add(".mkv");
            picker.FileTypeFilter.Add(".wma");
            picker.FileTypeFilter.Add(".m4a");
            picker.FileTypeFilter.Add(".m4v");


            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                cc c = new cc();
                _InputFile = file;
                _OutputFileName = System.IO.Path.GetFileNameWithoutExtension(file.Path);
                var t = await file.Properties.GetVideoPropertiesAsync();

                try
                {
                    var v = await file.GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.VideosView);
                    BitmapImage img = new BitmapImage();
                    img.SetSource(v);
                    c.Thumbnail = img;
                }
                catch (Exception Exception) { vars.Output("VideoUC.Load().Thumbnail ex: " + Exception.Message); }

                c.Profile = file.FileType.ToUpper().Replace(".", "");
                c.Duration = string.Format("{0}:{1}:{2}",
                    t.Duration.Hours.ToString("00"),
                    t.Duration.Minutes.ToString("00"),
                    t.Duration.Seconds.ToString("00"));
                c.Name = file.Name;
                lv.Items.Clear();
                lv.Items.Add(c);
            }

        }

        private void SettingsRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (IsDisplayed)
                Hide();
            else
                Display();

        }
        #endregion
        
        #region Other functions and events
        async void TranscodeProgress(double percent)
        {
            int secondsremaining = (int)(stopwatch.Elapsed.TotalSeconds / pb.Value * (pb.Maximum - pb.Value));

            TimeSpan span = new TimeSpan(0, 0, 0, secondsremaining);
            string remainTime = string.Format("{0}:{1}:{2}",
  span.Hours.ToString("00"),
  span.Minutes.ToString("00"),
  span.Seconds.ToString("00"));
            string elapsedTime = string.Format("{0}:{1}:{2}",
                stopwatch.Elapsed.Hours.ToString("00"),
                stopwatch.Elapsed.Minutes.ToString("00"),
                stopwatch.Elapsed.Seconds.ToString("00"));

            try
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    string st = string.Format("Completed: {0}% | Time remaining: {1} | Time elapsed: {2}",
                        pb.Value, remainTime, elapsedTime);
                    txtResult.Text = st;
                });
            }
            catch (Exception ex) { vars.Output("ExXX " + ex.Message); }
            pb.Value = double.Parse(percent.ToString().Split('.')[0]);

            if (double.Parse(percent.ToString().Split('.','/','\\',',')[0]) == 100)
            {
                try
                {
                    pb.Value = 100;

                    string st = string.Format("Completed: {0}%",
                       pb.Value);
                    txtResult.Text = st;
                    StopRadioButton.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex) { vars.Output("ExXX " + ex.Message); }
                MASettings.GPVisibility = Visibility.Collapsed;
            }
            else
            {
                MASettings.GPVisibility = Visibility.Visible;
                StopRadioButton.Visibility = Visibility.Visible;
            }
        }

        TimeSpan Subtract(TimeSpan dt1, TimeSpan dt2)
        {
            TimeSpan span = dt1 - dt2;
            return span;
        }



        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeTitleBar();
        }

        void ChangeTitleBar()
        {
            titleBar.BackgroundColor = new Color() { A = 100, R = 127, G = 220, B = 224 };
            titleBar.ForegroundColor = Colors.Black;
            titleBar.InactiveBackgroundColor = new Color() { A = 100, R = 127, G = 220, B = 224 };
            titleBar.InactiveForegroundColor = Colors.DarkGray;

            byte buttonAlpha = 255;
            titleBar.ButtonBackgroundColor = new Color() { A = 100, R = 127, G = 220, B = 224 };
            titleBar.ButtonHoverBackgroundColor = new Color() { A = buttonAlpha, R = 19, G = 21, B = 40 };
            titleBar.ButtonPressedBackgroundColor = Colors.White;
            titleBar.ButtonInactiveBackgroundColor = new Color() { A = 100, R = 127, G = 220, B = 224 };

            titleBar.ButtonForegroundColor = Colors.Black;
            titleBar.ButtonHoverForegroundColor = Colors.Black;
            titleBar.ButtonPressedForegroundColor = Colors.DarkGray;
            titleBar.ButtonInactiveForegroundColor = Colors.Cyan;
        }
        #endregion
    }
}
