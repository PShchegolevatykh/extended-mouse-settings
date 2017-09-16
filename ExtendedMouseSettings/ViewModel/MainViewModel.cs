using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using ExtendedMouseSettings.Model;
using GalaSoft.MvvmLight.CommandWpf;

namespace ExtendedMouseSettings.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMouseSettingsService _dataService;

        private int _sensitivity;

        public int Sensitivity
        {
            get => _sensitivity;
            set => _dataService.SetSensitivity(UpdateSensitivity, value);
        }

        private int _initialSensitivity;

        private void UpdateSensitivity(MouseSettings settings, Exception error)
        {
            if (error != null)
            {
                // Report error here
                return;
            }
            Set(ref _sensitivity, settings.Sensitivity);
            RaisePropertyChanged(() => SensitivityPercentage);
        }

        public string SensitivityPercentage => $"Sensitivity: {(uint)(_sensitivity / 20.0 * 100)} %";

        public MainViewModel(IMouseSettingsService dataService)
        {
            _dataService = dataService;
            _dataService.GetSensitivity((settings, error) =>
            {
                UpdateSensitivity(settings, error);
                _initialSensitivity = settings.Sensitivity;
            });
        }

        public ICommand CloseCommand => new RelayCommand(Close);
        private void Close()
        {
            Application.Current.MainWindow.Close();
        }

        public ICommand SaveAndExitCommand => new RelayCommand(SaveAndExit);

        private void SaveAndExit()
        {
            _dataService.SaveSensitivity((settings, error) =>
            {
                if (error != null)
                {
                    // Report error here
                    return;
                }
                _initialSensitivity = Sensitivity;
            }, Sensitivity);
            Close();
        }

        public override void Cleanup()
        {
            _dataService.SetSensitivity(UpdateSensitivity, _initialSensitivity);
            base.Cleanup();
        }
    }
}