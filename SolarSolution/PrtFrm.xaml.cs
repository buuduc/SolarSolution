using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Mvvm.POCO;

namespace SolarSolution
{
    /// <summary>
    /// Interaction logic for PrtFrm.xaml
    /// </summary>
    public partial class PrtFrm : Window
    {
        public PrtFrm()
        {
            InitializeComponent();
        }

        private void loaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ReportDE a = new ReportDE();
            a.CreateDocument();
           DocumentPreviewControl.DocumentSource= a;
        }
    }
}
