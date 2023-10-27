﻿using practical_task_17;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для BuyerWindow.xaml
    /// </summary>
    public partial class BuyerWindow : Window
    {
        
        public BuyerWindow(int id=0)
        {
            InitializeComponent();
            DataContext = new Interaction(id);
        }
    }
}
