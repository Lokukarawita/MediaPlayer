﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MediaLibrary _ml;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void btnML_Click(object sender, RoutedEventArgs e)
        {
            if (_ml != null && !_ml.IsActive)
            {
                _ml.Focus();

            }
            else
            {
                _ml = new MediaLibrary();
                _ml.Show();

            }
        }
    }
}
