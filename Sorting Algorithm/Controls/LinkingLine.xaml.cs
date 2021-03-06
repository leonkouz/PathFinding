﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for LinkingLine.xaml
    /// </summary>
    public partial class LinkingLine : UserControl
    {
        private Timer timer = new Timer();

        private WeightedNodeControl source;

        private WeightedNodeControl destination;

        public bool IsFollowingMouse { get; private set; } = false;

        private WeightedNodeControl nodeToFollow;

        public bool RequiresDestination { get; private set; } = false;

        public double CostXPosition
        {
            get { return (double)GetValue(CostXPositionProperty) / 2; }
            set
            {
                SetValue(CostXPositionProperty, value);
            }
        }

        public static readonly DependencyProperty CostXPositionProperty =
           DependencyProperty.Register("CostXPosition", typeof(double),
             typeof(LinkingLine), new PropertyMetadata(null));

        public double CostYPosition
        {
            get { return (double)GetValue(CostYPositionProperty) / 2; }
            set
            {
                SetValue(CostYPositionProperty, value);
            }
        }

        public static readonly DependencyProperty CostYPositionProperty =
           DependencyProperty.Register("CostYPosition", typeof(double),
             typeof(LinkingLine), new PropertyMetadata(null));




        public double X1
        {
            get { return (double)GetValue(X1Property) / 2; }
            set
            {
                SetValue(X1Property, value);
            }
        }

        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set
            {
                SetValue(X2Property, value);
            }
        }

        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set
            {
                SetValue(Y1Property, value);
            }
        }

        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set
            {
                SetValue(Y2Property, value);
            }
        }

        #region Dependency Properties

        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2", typeof(double),
       typeof(LinkingLine), new PropertyMetadata(null));

        public static readonly DependencyProperty X1Property =
           DependencyProperty.Register("X1", typeof(double),
             typeof(LinkingLine), new PropertyMetadata(null));

        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2", typeof(double),
              typeof(LinkingLine), new PropertyMetadata(null));


        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1", typeof(double),
              typeof(LinkingLine), new PropertyMetadata(null));


        #endregion

        /// <summary>
        /// Creates a LinkingLine that which already has a destination.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public LinkingLine(WeightedNodeControl source, WeightedNodeControl destination)
        {
            InitializeComponent();

            this.source = source;
            this.destination = destination;

            source.AddLinkingLine(this);
            destination.AddLinkingLine(this);

            destination.PreviewMouseLeftButtonDown += Destination_PreviewMouseLeftButtonDown;

            timer.Interval = 5;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Creates a LinkingLine that only has a source and can be linked to a new destination.
        /// </summary>
        /// <param name=""></param>
        public LinkingLine(WeightedNodeControl source)
        {
            InitializeComponent();

            this.source = source;

            RequiresDestination = true;

            source.AddLinkingLine(this);

            timer.Interval = 5;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void LinkingLine_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (RequiresDestination)
            {
                if (ParentIsOfType((FrameworkElement)Mouse.DirectlyOver, typeof(WeightedNodeControl)))
                {
                    WeightedNodeControl node = (WeightedNodeControl)GetParentControl((FrameworkElement)Mouse.DirectlyOver, typeof(WeightedNodeControl));
                    node.WasLinkedTo = true;
                    node.AddLinkingLine(this);
                    this.destination = node;
                    RequiresDestination = false;

                    ((UIElement)Parent).PreviewMouseLeftButtonDown -= LinkingLine_PreviewMouseLeftButtonDown;
                }
            }
        }

        private FrameworkElement GetParentControl(FrameworkElement element, Type type)
        {
            if (element == null)
            {
                return null;
            }

            if (element.GetType() == type)
            {
                return element;
            }
            else
            {
                return GetParentControl((FrameworkElement)element.Parent, type);
            }
        }

        private bool ParentIsOfType(FrameworkElement element, Type type)
        {
            if (element == null)
            {
                return false;
            }

            if (element.GetType() == type)
            {
                return true;
            }
            else
            {
                return ParentIsOfType((FrameworkElement)element.Parent, type);
            }
        }

        private void Destination_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StopFollowingMouse();

            destination.PreviewMouseLeftButtonDown -= Destination_PreviewMouseLeftButtonDown;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (RequiresDestination)
            {
                ((UIElement)Parent).PreviewMouseLeftButtonDown += LinkingLine_PreviewMouseLeftButtonDown;
            }

            FollowMouse(destination);
        }

        public void FollowMouse(WeightedNodeControl selectedNode)
        {
            nodeToFollow = selectedNode;

            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            IsFollowingMouse = true;
        }

        public void StopFollowingMouse()
        {
            nodeToFollow = null;

            timer.Stop();
            timer.Elapsed -= Timer_Elapsed;

            IsFollowingMouse = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Point mousePosition = Mouse.GetPosition(WeightedGraphWindow.MainCanvas);

                    if (nodeToFollow == source)
                    {
                        X1 = mousePosition.X;
                        Y1 = mousePosition.Y;
                    }
                    else if (nodeToFollow == destination)
                    {
                        X2 = mousePosition.X;
                        Y2 = mousePosition.Y;
                    }

                    if( source != null && destination != null)
                    {
                        // https://stackoverflow.com/questions/17195055/calculate-a-perpendicular-offset-from-a-diagonal-line/17195324#17195324
                        // Convulated way of getting the center of the source node and center of the destination node because using X1 and X2 does not 
                        // seem to give the correct positions.
                        Point sourcePoint = new Point((Canvas.GetLeft(source) + (source.Width / 2)), (Canvas.GetTop(source) + (source.Height / 2)));
                        Point destinationPoint = new Point((Canvas.GetLeft(destination) + (source.Width / 2)), (Canvas.GetTop(destination) + (source.Height / 2)));

                        Point M = GetCenterPoint(sourcePoint, destinationPoint);
                        Vector p = Point.Subtract(sourcePoint, destinationPoint);
                        Point n = new Point(-p.Y, p.X);
                        int normLength = (int)Math.Sqrt((n.X * n.X) + (n.Y * n.Y));
                        n.X = n.X / normLength;
                        n.Y = n.Y / normLength;

                        Point final = new Point(M.X + (30 * n.X), M.Y + (30 * n.Y));

                        Canvas.SetTop(costTextBlock, final.Y);
                        Canvas.SetLeft(costTextBlock, final.X);
                    }
                });
            }
        }
        private Point GetCenterPoint(Point p1, Point p2)
        {
            return new Point((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
        }
    }
}
