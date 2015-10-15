using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media;
using WcfProxies.Contracts.Services;
using WcfProxies.Data.Repositories;
using WcfProxies.Services;

namespace WcfProxies.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost _host = null;
        ContainerBuilder builder = null;
        IContainer container = null;

        public MainWindow()
        {
            InitializeComponent();

            // Init Autofac.WCF container
            builder = new ContainerBuilder();
            builder.RegisterType<BlogPostController>()
                    .As<IBlogPostService>();
            builder.RegisterType<BlogPostRepository>()
                    .As<IBlogPostRepository>();

            container = builder.Build();
        }

        private void btnStartService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _host = new ServiceHost(typeof(BlogPostController));
                _host.AddDependencyInjectionBehavior<IBlogPostService>(container);

                _host.Open();

                this.btnStopService.IsEnabled = true;
                this.btnStartService.IsEnabled = false;
                this.lblStatus.Content = "Service is running..";
                this.lblStatus.Foreground = new SolidColorBrush(Colors.RoyalBlue);
            }
            catch (Exception ex)
            {
                container.Dispose();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStopService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _host.Close();

                this.btnStopService.IsEnabled = false;
                this.btnStartService.IsEnabled = true;
                this.lblStatus.Content = "Service is stopped.";
                this.lblStatus.Foreground = new SolidColorBrush(Colors.Crimson);
            }
            catch (Exception ex)
            {
                container.Dispose();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
