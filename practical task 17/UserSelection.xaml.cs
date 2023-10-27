using practical_task_17;
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

namespace online_store
{
    /// <summary>
    /// Логика взаимодействия для UserSelection.xaml
    /// </summary>
    public partial class UserSelection : Window
    {
        public UserSelection()
        {
            InitializeComponent();
            Interaction interaction = new Interaction();
            this.DataContext = interaction; 
            if (interaction.CloseAction == null)
                interaction.CloseAction = new Action(() => this.Close());
        }
    }
}
