namespace CaptureTheFlag {
	using System;
	using System.Collections.Generic;
	using System.Windows.Controls;
	using Microsoft.Phone.Controls;
	using Caliburn.Micro;
    using CaptureTheFlag.ViewModels;
    using CaptureTheFlag.Services;
    using Caliburn.Micro.BindableAppBar;
    using CaptureTheFlag.ViewModels.GameVVMs;
    using CaptureTheFlag.ViewModels.GameMapVVMs;

	public class AppBootstrapper : PhoneBootstrapperBase
	{
		PhoneContainer container;

		public AppBootstrapper()
		{
			Start();
		}

		protected override void Configure()
		{
			container = new PhoneContainer();
			if (!Execute.InDesignMode)
				container.RegisterPhoneServices(RootFrame);

            //Pre Game view models:
            container.PerRequest<ListGamesViewModel>();
            container.PerRequest<CreateGameViewModel>();
            container.PerRequest<SearchGameViewModel>();

            container.PerRequest<GameEditScreenViewModel>();
            container.PerRequest<GameEditAppBarViewModel>();

            container.PerRequest<GameDetailsScreenViewModel>();
            container.PerRequest<GameDetailsAppBarViewModel>();

            container.PerRequest<GameFieldsViewModel>();

            container.PerRequest<GamesListViewModel>();
            container.PerRequest<GamesListScreenViewModel>();
            container.PerRequest<GamesListAppBarViewModel>();
            
            
            //User authentication view models
            container.PerRequest<UserAccessPivotViewModel>();
            container.PerRequest<UserLoginViewModel>();
            container.PerRequest<UserRegistrationViewModel>();

            //Game Map view models:
            container.PerRequest<CreateGameMapViewModel>();

            container.PerRequest<InGameMapViewModel>();

            //Other view models:
            container.PerRequest<MainAppPivotViewModel>();
            container.PerRequest<GameItemViewModel>();
            container.PerRequest<CharacterViewModel>();
            container.PerRequest<UserViewModel>();

            //Services:
            container.PerRequest<FilterService>();
            container.PerRequest<CommunicationService>();
            container.PerRequest<LocationService>();
            container.Singleton<GlobalStorageService>();

			AddCustomConventions();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = container.GetInstance(service, key);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}

		static void AddCustomConventions()
		{
			ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
				(viewModelType, path, property, element, convention) => {
					if (ConventionManager
						.GetElementConvention(typeof(ItemsControl))
						.ApplyBinding(viewModelType, path, property, element, convention))
					{
						ConventionManager
							.ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
						ConventionManager
							.ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
						return true;
					}

					return false;
				};

			ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
				(viewModelType, path, property, element, convention) => {
					if (ConventionManager
						.GetElementConvention(typeof(ItemsControl))
						.ApplyBinding(viewModelType, path, property, element, convention))
					{
						ConventionManager
							.ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
						ConventionManager
							.ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
						return true;
					}

					return false;
				};

            // App Bar Conventions
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");
		}
	}
}