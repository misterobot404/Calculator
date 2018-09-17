using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Main
{
    public partial class MainWindow : Window
    {
        string leftop = "";
        string operation = "";
        string rightop = "";
        bool minus = false;
        bool root = false;
        bool first=true;
        int count = 0;
        bool isApplicationActive;

        ObservableCollection<string> MainList;
              
        public MainWindow()
        {
            InitializeComponent();
            MainList = new ObservableCollection<string>();
            MainList.Add(" ");
            MainList.Add(" ");
            DataContext = MainList;

            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender == ButtonMinimized)
                {
                    WindowState = WindowState.Minimized;
                }
                if (sender == ButtonExit)
                {
                    this.Close();
                }

                string s = (string)((Button)e.OriginalSource).Content;

                if (s == "H")
                {
                    return;
                }
                else if (s == "×²")
                {
                    double num1 = Double.Parse(leftop);
                    textBlock.Text = "";
                    textBlock.Text += (num1 * num1).ToString();
                    return;
                }
                else if (s == "√")
                {
                    textBlock.Text += "√";
                    root = true;
                    return;
                }
                else if (s == "±")
                {
                    textBlock.Text += "-";
                    minus = true;
                    return;
                }
                else
                {
                    textBlock.Text += s;
                    if (minus)
                    {
                        s = textBlock.Text;
                        minus = false;
                    }
                }

                double num;
                bool result = Double.TryParse(s, out num);

                if (result == true || s == ".")
                {
                    if (operation == "")
                    {
                        leftop += s;
                    }
                    else
                    {
                        rightop += s;
                    }
                }
                else
                {
                    if (root)
                    {
                        double l;
                        if (operation == "")
                        {
                            l = Double.Parse(leftop);
                            l = Math.Sqrt(l);
                            s = l.ToString();
                            leftop = s;
                            s = "";
                        }
                        if (operation != "")
                        {
                            l = Double.Parse(rightop);
                            l = Math.Sqrt(l);
                            s = l.ToString();
                            rightop = s;
                            s = "";
                        }
                        Update_RightOp();
                        textBlock.Text = rightop;
                        operation = "";
                        root = false;
                    }
                    if (s == "=")
                    {
                        if(first)
                        {
                            MainList.RemoveAt(1);
                            MainList.RemoveAt(0);
                            first = false;
                        }
                        if (count > 2)
                        {
                            MainList.RemoveAt(1);
                            MainList.Add(textBlock.Text);
                            Update_RightOp();
                            MainList.RemoveAt(0);
                            MainList.Add(rightop);
                        }
                        else
                        {
                            MainList.Add(textBlock.Text);
                            Update_RightOp();
                            MainList.Add(rightop);
                        }
                        ++count;

                        textBlock.Text = rightop;                       
                        leftop = rightop;
                        rightop = "";
                        operation = "";
                    }
                    else if (s == "CLEAR")
                    {
                        leftop = "";
                        rightop = "";
                        operation = "";
                        textBlock.Text = "";
                    }
                    else
                    {
                        if (rightop != "")
                        {
                            Update_RightOp();
                            leftop = rightop;
                            rightop = "";
                        }
                        operation = s;
                    }
                }
            }
            catch (Exception)
            {
                textBlock.Text = "";
                leftop = "";
                rightop = "";
                operation = "";
            }
        }

        private void Update_RightOp()
        {
            try
            {
                double num1 = Double.Parse(leftop);
                double num2 = 0;
                if (rightop != "") num2 = Double.Parse(rightop);

                switch (operation)
                {
                    case "+":
                        rightop = (num1 + num2).ToString();
                        break;
                    case "-":
                        rightop = (num1 - num2).ToString();
                        break;
                    case "×":
                        rightop = (num1 * num2).ToString();
                        break;
                    case "÷":
                        rightop = (num1 / num2).ToString();
                        break;
                    default:
                        rightop = leftop;
                        break;
                }
            }
            catch (Exception)
            {
                textBlock.Text = "";
                leftop = "";
                rightop = "";
                operation = "";
            }
        }

        private void Window_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        void App_Activated(object sender, EventArgs e)
        {
            this.isApplicationActive = true;
            this.BorderBrush = new SolidColorBrush(Color.FromRgb(24, 131, 215));
        }

        void App_Deactivated(object sender, EventArgs e)
        {
            this.isApplicationActive = false;
            this.BorderBrush = Brushes.Gray;
        }

    }
}

