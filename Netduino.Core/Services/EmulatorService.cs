using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Microsoft.SPOT.Emulator.Gpio;

namespace Netduino.Core.Services
{
	[Export(typeof(IEmulatorService))]
	public class EmulatorService:Emulator2,IEmulatorService,IHandle<InputGpioEventArgs>,IHandle<OutputGpioEventArgs>
	{
		//private readonly Emulator _emulator;
		private GpioPort _onBoardLedPort;
		private GpioPort _onBoardSwitch1;
		private GpioPort _gpioD0Port;
		private GpioPort _gpioD1Port;
		private GpioPort _gpioD2Port;
		private GpioPort _gpioD3Port;
		private GpioPort _gpioD4Port;
		private GpioPort _gpioD5Port;
		private GpioPort _gpioD6Port;
		private GpioPort _gpioD7Port;
		private GpioPort _gpioD8Port;
		private GpioPort _gpioD9Port;
		private GpioPort _gpioD10Port;
		private GpioPort _gpioD11Port;
		private GpioPort _gpioD12Port;
		private GpioPort _gpioD13Port;
		//private GpioPort _gpio_a0Port;
		private readonly IEventAggregator _eventAggregator;
        private readonly ILog _log = LogManager.GetLog(typeof(EmulatorService));


		public EmulatorService(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

        public override void Configure(System.Xml.XmlReader reader)
        {
            try
            {
                base.Configure(reader);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

		/// <summary>Registers default components and settings for this emulator</summary>
		protected override void LoadDefaultComponents()
		{
			base.LoadDefaultComponents();

		}

		public override void InitializeComponent()
		{
			base.InitializeComponent();
            _gpioD0Port = RegisterOutput("GPIO_PIN_D0");
            _gpioD1Port = RegisterOutput("GPIO_PIN_D1");
            _gpioD2Port = RegisterOutput("GPIO_PIN_D2");
            _gpioD3Port = RegisterOutput("GPIO_PIN_D3");
            _gpioD4Port = RegisterOutput("GPIO_PIN_D4");
            _gpioD5Port = RegisterOutput("GPIO_PIN_D5");
            _gpioD6Port = RegisterOutput("GPIO_PIN_D6");
            _gpioD7Port = RegisterOutput("GPIO_PIN_D7");
            _gpioD8Port = RegisterOutput("GPIO_PIN_D8");
            _gpioD9Port = RegisterOutput("GPIO_PIN_D9");
            _gpioD10Port = RegisterOutput("GPIO_PIN_D10");
            _gpioD11Port = RegisterOutput("GPIO_PIN_D11");
            _gpioD12Port = RegisterOutput("GPIO_PIN_D12");
            _gpioD13Port = RegisterOutput("GPIO_PIN_D13");
            _onBoardLedPort = RegisterOutput("ONBOARD_LED");
            _onBoardSwitch1 = FindComponentById("ONBOARD_SW1") as GpioPort;

            _eventAggregator.Subscribe(this);
		}

        private GpioPort RegisterOutput(string id)
        {
            var gpioPort = FindComponentById(id) as GpioPort;
            if (gpioPort != null)
            {
                if (gpioPort.ModesAllowed == GpioPortMode.InputOutputPort || gpioPort.ModesAllowed == GpioPortMode.OutputPort)
                {
                    _log.Info("Id={0} is an output pin so register it for events", id);
                    gpioPort.OnGpioActivity += Port_OnGpioActivity;
                }
                else
                {
                    _log.Warn("Id={0} must not be an output pin", id);
                }
            }
            else
            {
                _log.Warn("Output was not registered Id={0}", id);
            }
            return gpioPort;
        }

		void Port_OnGpioActivity(GpioPort sender, bool edge)
		{
            _log.Info("Emulator Service GpioPort Fired {0} {1}",sender.Pin,edge);
            _eventAggregator.Publish(new OutputGpioEventArgs((int)sender.Pin, edge));
		}

        public void Handle(InputGpioEventArgs message)
        {
            
            if (message != null)
            {
                _log.Info("Emulator Service: Handle Input {0} {1}",message.Pin,message.Edge);
                if (message.Pin == Pins.ONBOARD_SW1 && _onBoardSwitch1 != null)
                {
                    _onBoardSwitch1.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D1 && _gpioD1Port != null)
                {
                    _gpioD1Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D2 && _gpioD2Port != null)
                {
                    _gpioD2Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D3 && _gpioD3Port != null)
                {
                    _gpioD3Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D4 && _gpioD4Port != null)
                {
                    _gpioD4Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D5 && _gpioD5Port != null)
                {
                    _gpioD5Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D6 && _gpioD6Port != null)
                {
                    _gpioD6Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D7 && _gpioD7Port != null)
                {
                    _gpioD7Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D8 && _gpioD8Port != null)
                {
                    _gpioD8Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D9 && _gpioD9Port != null)
                {
                    _gpioD9Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D10 && _gpioD10Port != null)
                {
                    _gpioD10Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D11 && _gpioD11Port != null)
                {
                    _gpioD11Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D12 && _gpioD12Port != null)
                {
                    _gpioD12Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D13 && _gpioD13Port != null)
                {
                    _gpioD13Port.Write(message.Edge);
                }
            }
        }

        public void Handle(OutputGpioEventArgs message)
        {
            _log.Info("Emulator Service Handle Output ");

        }
    }
}
