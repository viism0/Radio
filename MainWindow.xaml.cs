using System;
using System.Windows;
using NAudio.Wave;

namespace SimpleRadio
{
    public partial class MainWindow : Window
    {
        private WaveOutEvent _waveOut;
        private AudioFileReader _audioFile;
        private bool _isPlaying = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stop();

                string url = UrlTextBox.Text.Trim();

                _audioFile = new AudioFileReader(url);
                _waveOut = new WaveOutEvent();
                _waveOut.Init(_audioFile);
                _waveOut.Play();

                _isPlaying = true;
                StartButton.Background = System.Windows.Media.Brushes.Yellow;
                StartButton.Content = "▶ PLAYING";
            }
            catch (Exception ex)
            {
                StartButton.Background = System.Windows.Media.Brushes.LightGreen;
                StartButton.Content = "▶ START";
                _isPlaying = false;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            StartButton.Background = System.Windows.Media.Brushes.LightGreen;
            StartButton.Content = "▶ START";
        }

        private void Stop()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
            }

            if (_audioFile != null)
            {
                _audioFile.Dispose();
                _audioFile = null;
            }

            _isPlaying = false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Stop();
            base.OnClosing(e);
        }
    }
}