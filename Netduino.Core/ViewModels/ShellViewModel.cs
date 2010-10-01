using System.ComponentModel.Composition;
using System.IO;
using Caliburn.Micro;

namespace Netduino.Core.ViewModels
{
    /// <summary>
    /// The implementation of the shell
    /// </summary>
    [Export(typeof(IShellViewModel))]
    public class ShellViewModel :Conductor<Screen>,IShellViewModel
    {
        private string _emulatorName;
        private const string KeyBase = @"HKEY_CURRENT_USER\Software\Microsoft\.NETMicroFramework\v4.1\Emulators\{45D406A2-51DD-4662-ABDD-499BD9589AF1}";
        private IWindowManager _windowManager;
        private IEmulatorViewModel _emulatorViewModel;
        private readonly ILog _log = LogManager.GetLog(typeof(ShellViewModel));

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager,IEmulatorViewModel viewModel)
        {
            _log.Info("ShellViewModel Constructor");
            _windowManager = windowManager;
            _emulatorViewModel = viewModel;
            DisplayName = "Netduino Emulator";
        }

        public string EmulatorName
        {
            get { return _emulatorName; }
            set 
            {
                if (_emulatorName != value)
                { 
                    _emulatorName = value;
                    NotifyOfPropertyChange(() => EmulatorName);
                    NotifyOfPropertyChange(() => CanWriteRegistry);
                }
            }
        }

        public IEmulatorViewModel EmulatorViewModel
        {
            get { return _emulatorViewModel; }
            set
            {
                if (_emulatorViewModel != value)
                {
                    _emulatorViewModel = value;
                }
            }
        }

        public bool CanWriteRegistry
        {
            get
            {
                return !string.IsNullOrEmpty(EmulatorName);
            }
            set { }
        }

        public void WriteRegistry()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Netduino.Shell.exe"); ;

            Microsoft.Win32.Registry.SetValue(KeyBase, "Name", EmulatorName);
            Microsoft.Win32.Registry.SetValue(KeyBase, "Path", path);

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            var view = (Screen)IoC.Get<IEmulatorViewModel>();
            ChangeActiveItem(view, true);
        }
    }
}
