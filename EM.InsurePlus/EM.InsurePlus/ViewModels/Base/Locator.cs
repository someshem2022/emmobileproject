
namespace EM.InsurePlus.ViewModels.Base
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EM.InsurePlus.Services;
    using EM.InsurePlus.Services.ApiServices;
    using EM.InsurePlus.Services.Contacts;
    using EM.InsurePlus.Services.Navigation;
    using EM.InsurePlus.Data.Repositories;
    using EM.InsurePlus.Data.Repositories.Contracts;

    public class Locator
    {
        private IContainer _container;
        private readonly ContainerBuilder _containerBuilder;

        private static readonly Locator _instance = new Locator();

        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }

        public Locator()
        {
            _containerBuilder = new ContainerBuilder();

            //ViewModels
            
                ;
            _containerBuilder.RegisterType<RegisterViewModel>();
            //_containerBuilder.RegisterType<RegisterGuestViewModel>();
            //_containerBuilder.RegisterType<ReserveParkingViewModel>();
            //_containerBuilder.RegisterType<ReservationDetailViewModel>();
            //_containerBuilder.RegisterType<RegisterVehicleViewModel>();
            //_containerBuilder.RegisterType<LotViewModel>();
            //_containerBuilder.RegisterType<MyParkingViewModel>();


            //Insure
            _containerBuilder.RegisterType<MenuViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<HomeViewModel>();
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<VehiclePolicyViewModel>();
            _containerBuilder.RegisterType<VehicleOfflinePoliciesViewModel>();



            _containerBuilder.RegisterType<VehiclePolicyRepository>().As<IVehiclePolicyRepository>();

            // Services 
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>(); 
            _containerBuilder.RegisterType<UserService>().As<IUserService>();
            _containerBuilder.RegisterType<PolicyService>().As<IPolicyService>();
            _containerBuilder.RegisterType<HelperService>().As<IHelperService>();

            //_containerBuilder.RegisterType<VehicleService>().As<IVehicleService>();

            //_containerBuilder.RegisterType<LotService>().As<ILotService>();
            //_containerBuilder.RegisterType<ServiceClient>().As<IServiceClient>();
            //_containerBuilder.RegisterType<AuthorizeService>().As<IAuthorizeService>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            _container = _containerBuilder.Build();
        }
    }
}
