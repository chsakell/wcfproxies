using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using WcfProxies.Contracts.Data;
using WcfProxies.Proxies.Shared;

namespace WcfProxies.Clients
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ContainerBuilder builder = null;
        IContainer container = null;

        public MainWindow()
        {
            InitializeComponent();

            // Init Autofac.WCF container
            builder = new ContainerBuilder();
            // Register the channel factory for the service. Make it
            // SingleInstance since you don't need a new one each time.
            builder
              .Register(c => new ChannelFactory<WcfProxies.Proxies.External.IBlogPostService>(
                new WSHttpBinding(),
                new EndpointAddress("http://localhost:9002/BlogPostService")))
              .SingleInstance();
            // Register the service interface using a lambda that creates
            // a channel from the factory. Include the UseWcfSafeRelease()
            // helper to handle proper disposal.
            builder
              .Register(c => c.Resolve<ChannelFactory<WcfProxies.Proxies.External.IBlogPostService>>()
              .CreateChannel())
              .As<WcfProxies.Proxies.External.IBlogPostService>()
              .UseWcfSafeRelease();

            // You can also register other dependencies.
            builder.RegisterType<WcfProxies.Proxies.External.BlogPostClientExt>();

            container = builder.Build();
        }

        private void btnSharedClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Instance per endpoint name
                //BlogPostClient sharedProxy = new BlogPostClient("tcp");

                EndpointAddress address = new EndpointAddress("http://localhost:9002/BlogPostService");
                Binding binding = new WSHttpBinding();
                BlogPostClient sharedProxy = new BlogPostClient(binding, address);

                PostData _firstPost = sharedProxy.GetPost(1);

                if (_firstPost != null)
                {
                    lbxResult.Items.Clear();
                    lbxResult.Items.Add(_firstPost.Title + " by " + _firstPost.Author);
                }

                sharedProxy.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNonSharedClient_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<WcfProxies.Proxies.External.PostData> _posts = null;

            try
            {
                //ChannelFactory<WcfProxies.Proxies.External.IBlogPostService> factory
                //    = new ChannelFactory<WcfProxies.Proxies.External.IBlogPostService>("tcpExt");

                EndpointAddress address = new EndpointAddress("http://localhost:9002/BlogPostService");
                Binding binding = new WSHttpBinding();
                ChannelFactory<WcfProxies.Proxies.External.IBlogPostService> factory
                    = new ChannelFactory<WcfProxies.Proxies.External.IBlogPostService>(binding, address);

                WcfProxies.Proxies.External.IBlogPostService _externalProxy = factory.CreateChannel();
                _posts = _externalProxy.GetBlogPosts(2);

                if (_posts != null)
                {
                    lbxResult.Items.Clear();
                    foreach (var post in _posts)
                    {
                        lbxResult.Items.Add(post.Title + " by " + post.Author);
                    }

                    factory.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNonSharedClientInjected_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<WcfProxies.Proxies.External.PostData> _posts = null;

            try
            {
                using (var lifetime = container.BeginLifetimeScope())
                {
                    var _externalProxyInjected = lifetime.Resolve<WcfProxies.Proxies.External.BlogPostClientExt>();
                    _posts = _externalProxyInjected.GetBlogPosts(1);
                }

                if (_posts != null)
                {
                    lbxResult.Items.Clear();
                    foreach (var post in _posts)
                    {
                        lbxResult.Items.Add(post.Title + " by " + post.Author);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
