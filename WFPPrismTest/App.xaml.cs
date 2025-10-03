using Prism.Ioc;
using Prism.Unity;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using WFPPrismTest.Views;

namespace WFPPrismTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 設定ファイルからCSVの場所を取得
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("appsettings.json"));
            var csvPath = config["CustomerCsvPath"];

            // DI登録
            containerRegistry.RegisterInstance<ICustomerRepository>(new CsvCustomerRepository(csvPath));
            containerRegistry.Register<CustomerService>();
        }
    }
}
