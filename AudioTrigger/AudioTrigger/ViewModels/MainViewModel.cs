using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace AudioTrigger.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private Tuple<int, string> m_SelectedAudioInput;
        private ObservableCollection<Tuple<int, string>> m_AudioInputOptions;
        //private float m_AudioLevel;
        //private bool m_IsRecording;
        //private float m_MaxSample;
        //private bool m_ResetMax;

        public ObservableCollection<Tuple<int, string>> AudioInputOptions
        {
            get { return m_AudioInputOptions; }
            set
            {
                m_AudioInputOptions = value;
                OnPropertyChanged(nameof(AudioInputOptions));
            }
        }

        public Tuple<int, string> SelectedAudioInput
        {
            get { return m_SelectedAudioInput; }
            set
            {
                m_SelectedAudioInput = value;
                OnPropertyChanged(nameof(SelectedAudioInput));
                //StartListening();
            }
        }

        //public float AudioLevel
        //{
        //    get { return m_AudioLevel; }
        //    set
        //    {
        //        m_AudioLevel = value;
        //        OnPropertyChanged(nameof(AudioLevel));
        //    }
        //}

        public MainViewModel()
        {
            InitialiseAudioDevices();
        }

        private void InitialiseAudioDevices()
        {
            m_AudioInputOptions = new ObservableCollection<Tuple<int, string>>();
            int waveInDevices = WaveIn.DeviceCount;
            for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                m_AudioInputOptions.Add(new Tuple<int, string>(waveInDevice, deviceInfo.ProductName));
            }

            SelectedAudioInput = m_AudioInputOptions.FirstOrDefault();
        }

        //private void StartListening()
        //{
        //    if (SelectedAudioInput == null)
        //    {
        //        return;
        //    }
        //    var waveIn = new WaveInEvent();
        //    waveIn.DeviceNumber = SelectedAudioInput.Item1;
        //    waveIn.DataAvailable += OnDataAvailable;
        //    waveIn.WaveFormat = new WaveFormat(8000, 1);
        //    waveIn.StartRecording();

        //    System.Timers.Timer timer = new System.Timers.Timer();
        //    timer.Interval = 100;
        //    timer.Elapsed += UpdateMaxAudioLevel;
        //    timer.AutoReset = true;
        //    timer.Enabled = true;

        //    //m_IsRecording = true;
        //}

        //private void UpdateMaxAudioLevel(object sender, ElapsedEventArgs e)
        //{
        //    Application.Current?.Dispatcher.Invoke(() => AudioLevel = m_MaxSample * 100);
        //    m_ResetMax = true;
        //}

        //void OnDataAvailable(object sender, WaveInEventArgs e)
        //{
        //    if (m_ResetMax)
        //    {
        //        m_MaxSample = 0;
        //        m_ResetMax = false;
        //    }
        //    for (int index = 0; index < e.BytesRecorded; index += 2)
        //    {
        //        short sample = (short)((e.Buffer[index + 1] << 8) |
        //                                e.Buffer[index + 0]);
        //        float sample32 = Math.Abs(sample / 32768f);
        //        m_MaxSample = sample32 > m_MaxSample ? sample32 : m_MaxSample;
        //    }
        //}
    }
}
