﻿using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Netduino.Core.ViewModels;
using Netduino.Core.Services;
using System.IO;

namespace Netduino.Core
{
    /// <summary>
    /// The bootstrapper for initializing the application
    /// </summary>
	public class EmulatorBootstrapper : Bootstrapper<IShellViewModel>
	{
		private CompositionContainer _container;

        static EmulatorBootstrapper()
        {
            LogManager.GetLog = type => new Log4NetLogger(type);
        }

        /// <summary>
        /// Configure the IOC Container and get the emulator started
        /// </summary>
        protected override void Configure()
		{

            var catalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());
			_container = new CompositionContainer(catalog);

			var batch = new CompositionBatch();

			batch.AddExportedValue<IWindowManager>(new WindowManager());

            IEventAggregator aggregator = new EventAggregator();
			batch.AddExportedValue(aggregator);
			
            var emulatorService = new EmulatorService(aggregator);
			batch.AddExportedValue<IEmulatorService>(emulatorService);
			
            batch.AddExportedValue(_container);

			_container.Compose(batch);

            // Start the emulator
			emulatorService.StartEmulator();
		}

        /// <summary>
        /// Resolve an instance of a contract
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
		protected override object GetInstance(Type serviceType, string key)
		{
			string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = _container.GetExportedValues<object>(contract);

			if (exports.Count() > 0)
				return exports.First();

			throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		protected override void BuildUp(object instance)
		{
			_container.SatisfyImportsOnce(instance);
		}
		
        /// <summary>
        /// Determine what assemblies are part of the application
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
		{
            return new[] { Assembly.GetExecutingAssembly(), Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Extensions\NetduinoEmulator.dll")) };
        }
		
        /// <summary>
        /// Display the main windows
        /// </summary>
        protected override void DisplayRootView()
		{
			var viewModel = IoC.Get<IShellViewModel>();
			new WindowManager().Show(viewModel);
		}  
	}
}
