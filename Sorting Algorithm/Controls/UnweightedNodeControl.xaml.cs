﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathFinding
{
    /// <summary>
    /// Interaction logic for NodeControl.xaml
    /// </summary>
    public partial class UnweightedNodeControl : UserControl
    {
        public Node Node
        {
            get { return (Node)GetValue(NodeProperty); }
            set
            {
                SetValue(NodeProperty, value);
            }
        }
        
        public static readonly DependencyProperty NodeProperty =
            DependencyProperty.Register("Node", typeof(Node),
              typeof(UnweightedNodeControl), new PropertyMetadata(null));


        public UnweightedNodeControl()
        {
            InitializeComponent();
        }
    }
}
