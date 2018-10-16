using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Main
{
    public partial class MainWindow : Window
    {
        string leftop = "";
        string operation = "";
        string rightop = "";
        bool minus = false;
        bool root = false;
        public static bool History = true;
        bool isApplicationActive;

        public MainWindow()
        {
            InitializeComponent();
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
                    if (History)
                    {
                        Storyboard sb = Resources["Open"] as Storyboard;
                        sb.Begin();
                        History = false;
                        return;
                    }
                    else
                    {
                        Storyboard sb = Resources["Close"] as Storyboard;
                        sb.Begin();
                        History = true;
                        return;
                    }
                    
                }

                else if (s == "×²")
                {
                    double num1 = Double.Parse(leftop);
                    HistoryWindow.AddingNewItem(leftop + "*" + leftop + "=" + (num1 * num1).ToString());
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
                else if (s == "CLEAR")
                {
                    throw new IndexOutOfRangeException();
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
                        HistoryWindow.AddingNewItem(textBlock.Text + rightop);
                        textBlock.Text = rightop;
                        operation = "";
                        root = false;
                    }
                    if (s == "=")
                    {                        
                        Update_RightOp();
                        if (operation != "")
                            HistoryWindow.AddingNewItem(textBlock.Text + rightop);                    
                        textBlock.Text = rightop;                       
                        leftop = rightop;
                        rightop = "";
                        operation = "";
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
                if (rightop != "")
                    num2 = Double.Parse(rightop);

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

