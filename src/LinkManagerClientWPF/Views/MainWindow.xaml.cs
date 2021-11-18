using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BespokeFusion;
using LinkManagerClientWPF.Data;

namespace LinkManagerClientWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateLocalDatabase();
        }
        private void UpdateLocalDatabase(string filter = "")
        {
            bool HaveChange = false;
            // try
            // {
            //     using (var db = new Model())
            //     {
            //         List<Item> listOfItems = db.Links.ToList();
            //         using (var LocalDb = new AppDbContext())
            //         {
            //             foreach (var item in listOfItems)
            //             {
            //                 Item LocalItem = LocalDb.Links.Find( item.ItemId);
            //                 if (LocalItem !=null )
            //                 {
            //                     if (item.UpDate != LocalItem.UpDate)
            //                     {
            //                         LocalItem.Order = item.Order;
            //                         LocalItem.UpDate = item.UpDate;
            //                         LocalItem.DefaultPassword = item.DefaultPassword;
            //                         LocalItem.Description = item.Description;
            //                         LocalItem.FileName = item.FileName;
            //                         LocalItem.InternetNeeded = item.InternetNeeded;
            //                         LocalItem.IsDelete = item.IsDelete;
            //                         LocalItem.ShortDescription = item.ShortDescription;
            //                         LocalItem.Title = item.Title;
            //                         LocalItem.Argument = item.Argument;
            //                         LocalItem.UserName = item.UserName;
            //                         HaveChange = true;
            //                         LocalDb.Update(LocalItem );
            //                     }
            //                 }
            //                 else
            //                 {
            //                     HaveChange = true;
            //                     LocalDb.Add(item);
            //                 }
            //             }
            //            if (HaveChange ) LocalDb.SaveChanges();

            //             if (string.IsNullOrEmpty(filter))
            //             {
            //                 if (NormalOrder.IsChecked != null && NormalOrder.IsChecked.Value)
            //                 {
            //                     ListViewItems.ItemsSource = LocalDb.Links.Where(d => !d.IsDelete).ToList().OrderBy(o => o.Order);
            //                 }
            //                 else
            //                     ListViewItems.ItemsSource = LocalDb.Links.Where(d => !d.IsDelete).ToList().OrderByDescending(o => o.Visited);
            //             }
            //             else
            //             {
            //                 if (NormalOrder.IsChecked != null && NormalOrder.IsChecked.Value)
            //                 {
            //                     ListViewItems.ItemsSource = LocalDb.Links.Where(d => !d.IsDelete && (d.Title.Contains (filter)  || d.ShortDescription.Contains(filter))  ).ToList().OrderBy(o => o.Order);
            //                 }
            //                 else
            //                     ListViewItems.ItemsSource = LocalDb.Links.Where(d => !d.IsDelete && (d.Title.Contains(filter) || d.ShortDescription.Contains(filter))).ToList().OrderByDescending(o => o.Visited);
            //             }
            //         }

            //     }

            //     //foreach (var item in listOfItems)
            //     //{
            //     //   // item.ImageName = "e:\\" + item.ImageName;

            //     //}
            // }
            // catch(Exception e)
            // {
            //     MaterialMessageBox.ShowError("برنامه قادر به اتصال به سرور نمی باشد" + e.Message );
            //     Application.Current.Shutdown();
            // }


        }

        private void WPFClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void SiteOpen(string explname, string siteaddress, Boolean net)
        {
            string exname = explname;
            NetworkControl(net);
            try
            {
                Process npd = new Process();
                // if (explname == "firefox") exname = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
                npd.StartInfo.FileName = exname;
                npd.StartInfo.Arguments = siteaddress;
                npd.Start();
            }
            catch
            {
                MaterialMessageBox.ShowError("مرورگر پیشفرض این وب سایت  " + explname + " نصب نیست");
            }
        }
        private void NetworkControl(bool net)
        {
            try
            {
                // RASDisplay ras = new RASDisplay();
                // RAS ff = new RAS();
                Process npd = new Process();
                npd.StartInfo.FileName = "rasphone";

                if (net)
                {
                    //  if (!ff.IsConnected()) ras.Connect("internet");
                    npd.StartInfo.Arguments = " -d internet";

                }
                else
                {
                    npd.StartInfo.Arguments = " -h internet";
                    //ras.Disconnect();
                }
                npd.Start();

            }
            catch
            {
                MaterialMessageBox.ShowError("کانکشن اینترنت نصب نیست");

            }

        }

        //private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        //{
        //    using (var db = new Model())
        //    {
        //        var item = db.Links.FirstOrDefault(s => s.Argument == e.Uri.ToString());
        //        SiteOpen(item.FileName, item.Argument, item.InternetNeeded);

        //        e.Handled = true;
        //    }
        //}

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                var x = (sender as StackPanel)?.Tag;
                using (var db = new AppDbContext())
                {
                    var item = db.Links.FirstOrDefault(s => s.Argument == x.ToString());
                    SiteOpen(item.FileName, item.Argument, item.InternetNeeded);

                }


            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //this.Close();

        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ButtonCloseMenu.Visibility == Visibility.Collapsed)
                {
                    Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                    //Storyboard.SetTarget(sb, this.btn);
                    sb.Begin();
                    //           ButtonCloseMenu.Visibility = Visibility.Visible ;
                    RunOpenMenu();

                    var x = (sender as Button).Tag;
                    ShowDetail((Guid)x);

                }
                else
                {
                    Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                    //Storyboard.SetTarget(sb, this.btn);
                    sb.Begin();
                    // ButtonCloseMenu.Visibility = Visibility.Collapsed;
                    RunCloseMenu();

                }
            }
            catch { }
        }

        private void ShowDetail(Guid itemId)
        {
            using (var db = new AppDbContext())
            {
                var item = db.Links.FirstOrDefault(s => s.Id == itemId);

                //     var item =listOfItems.FirstOrDefault(s => s.ItemId == itemId);
                TxtTitle.Text = item.Title;
                TxtShortDescription.Text = item.ShortDescription;
                TxtUser.Text = item.UserName;
                TxtPassword.Text = item.DefaultPassword;
                TxtDescription.Text = item.Description;

            }
        }
        private void RunOpenMenu()
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void RunCloseMenu()
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            RunCloseMenu();
        }

        private void OpenBrowser_Click(object sender, RoutedEventArgs e)
        {
            var x = (sender as Button).Tag;

            using (var db = new AppDbContext())
            {
                var item = db.Links.FirstOrDefault(s => s.Id == (Guid)x);
                //  var item = listOfItems.FirstOrDefault(s => s.ItemId == (int) x);

                SiteOpen(item.FileName, item.Argument, item.InternetNeeded);
                // item.Visited += 1;
                // db.SaveChanges();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string msg = "برنامه دسترسی سریع  طراحی و اجرا دایره انفورماتیک خوزستان \n برنامه نویس : محمدرضا مارگیران";


            MaterialMessageBox.Show(msg, "دسترسی سریع");

        }

        private void Refreshbtn_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateLocalDatabase();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (SortSetting.Visibility == Visibility.Visible)
            {
                //var so = "view";
                //if (NormalOrder.IsChecked != null)
                //    if (NormalOrder.IsChecked.Value) so = "Normal";
                //using (var db = new AppDbContext())
                //{
                //    if (db.AppSettings.Any())
                //    {
                //        var item = db.AppSettings.FirstOrDefault();

                //        item.OrderBy =so;
                //        db.Update(item);
                //    }
                //    else
                //    {
                //        var item = new AppSetting { OrderBy= so };
                //        db.Add(item);
                //    }
                //    db.SaveChanges();

                //}               
                UpdateLocalDatabase();
            }
            //var item = (sender as ToggleButton).Tag.ToString();
            //OrderDetail = item;
        }

        private void SortSetting_OnClick(object sender, RoutedEventArgs e)
        {
            if (SortSetting.Visibility == Visibility.Collapsed)
            {
                SortSetting.Visibility = Visibility.Visible;
            }
            else
            {
                SortSetting.Visibility = Visibility.Collapsed;

            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = sender as TextBox;
            UpdateLocalDatabase(filter.Text);
        }

    }
}
