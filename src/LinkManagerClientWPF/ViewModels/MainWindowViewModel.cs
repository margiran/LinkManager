using BespokeFusion;
using LinkManagerClientWPF.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace LinkManagerClientWPF.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private readonly ObservableCollection<LinksViewModel> _linksViewModel;

    public IEnumerable<LinksViewModel> LinkCollection => _linksViewModel;

    // private readonly LinkRepository _repository;
    public override string WindowTitle => "WPF Sample Project";


    #region commands

    public Command<Guid> OpenLinkCommand { get; set; }
    public Command UpdateLocalDatabaseCommand { get; set; }
    public Command ExitCommand { get; set; }
    public Command AboutCommand { get; set; }

    public Command ChangeSettingVisibilityCommand { get; set; }


    #endregion

    #region constructor

    public MainWindowViewModel()
    {
        // _repository = new LinkRepository();
        OpenLinkCommand = new Command<Guid>(OpenLink);
        ExitCommand = new Command(() => Application.Current.Shutdown());
        AboutCommand = new Command(() => MaterialMessageBox.Show($"Wpf sample By Mohammad Reza Margiran") );
        ChangeSettingVisibilityCommand = new Command(()=> { SettingsVisibility = ChangeSettingVisibility(); });
        _linksViewModel = new ObservableCollection<LinksViewModel>();

        _linksViewModel.Add(new LinksViewModel(new Models.LinkModel { Title = "google", FileName = "chrome", Argument = "google.com", Id = Guid.NewGuid(), Order = 1, ShortDescription = "google" }));
        _linksViewModel.Add(new LinksViewModel(new Models.LinkModel { Title = "yahoo", FileName = "chrome", Argument = "yahoo.com", Id = Guid.NewGuid(), Order = 2, ShortDescription = "yahoo" }));
    }

    private Visibility ChangeSettingVisibility()
    {
        if (SettingsVisibility == Visibility.Collapsed)
        {
            return Visibility.Visible;
        }
            return Visibility.Collapsed;

    }

    #endregion

    #region methods
    private void OpenLink(Guid id )
        {
            var link = _linksViewModel.FirstOrDefault(l => l.Id == id);
            if(link !=null )
            {
            RunProcess(link);
            }
        }

    private void RunProcess( LinksViewModel link )
    {
        NetworkControl(link.InternetNeeded);
        try
        {
            Process npd = new Process();
            npd.StartInfo.FileName = link.FileName;
            npd.StartInfo.Arguments = link.Argument;
            npd.Start();
        }
        catch
        {
            MaterialMessageBox.ShowError($"Application {link.FileName} Not Found");
        }
    }
    private void NetworkControl(bool net)
    {
        try
        {
            Process npd = new Process();
            npd.StartInfo.FileName = "rasphone";

            if (net)
            {
                npd.StartInfo.Arguments = " -d internet";

            }
            else
            {
                npd.StartInfo.Arguments = " -h internet";
            }
            npd.Start();

        }
        catch
        {
            MaterialMessageBox.ShowError("Internet Connection not found!");

        }

    }

    //    private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //    {
    //        if (sender != null)
    //        {
    //            var x = (sender as StackPanel)?.Tag;
    //            using (var db = new AppDbContext())
    //            {
    //                var item = db.Links.FirstOrDefault(s => s.Argument == x.ToString());
    //                SiteOpen(item.FileName, item.Argument, item.InternetNeeded);

    //            }


    //        }


    //    }

    //    private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
    //    {
    //        try
    //        {
    //            if (ButtonCloseMenu.Visibility == Visibility.Collapsed)
    //            {
    //                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
    //                //Storyboard.SetTarget(sb, this.btn);
    //                sb.Begin();
    //                //           ButtonCloseMenu.Visibility = Visibility.Visible ;
    //                RunOpenMenu();

    //                var x = (sender as Button).Tag;
    //                ShowDetail((Guid)x);

    //            }
    //            else
    //            {
    //                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
    //                //Storyboard.SetTarget(sb, this.btn);
    //                sb.Begin();
    //                // ButtonCloseMenu.Visibility = Visibility.Collapsed;
    //                RunCloseMenu();

    //            }
    //        }
    //        catch { }
    //    }

    //    private void ShowDetail(Guid itemId)
    //    {
    //        using (var db = new AppDbContext())
    //        {
    //            var item = db.Links.FirstOrDefault(s => s.Id == itemId);

    //            //     var item =listOfItems.FirstOrDefault(s => s.ItemId == itemId);
    //            TxtTitle.Text = item.Title;
    //            TxtShortDescription.Text = item.ShortDescription;
    //            TxtUser.Text = item.UserName;
    //            TxtPassword.Text = item.DefaultPassword;
    //            TxtDescription.Text = item.Description;

    //        }
    //    }
    //    private void RunOpenMenu()
    //    {
    //        ButtonOpenMenu.Visibility = Visibility.Collapsed;
    //        ButtonCloseMenu.Visibility = Visibility.Visible;
    //    }
    //    private void RunCloseMenu()
    //    {
    //        ButtonOpenMenu.Visibility = Visibility.Visible;
    //        ButtonCloseMenu.Visibility = Visibility.Collapsed;

    //    }

    //    private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
    //    {
    //        RunCloseMenu();
    //    }

    //    private void OpenBrowser_Click(object sender, RoutedEventArgs e)
    //    {
    //        var x = (sender as Button).Tag;

    //        using (var db = new AppDbContext())
    //        {
    //            var item = db.Links.FirstOrDefault(s => s.Id == (Guid)x);
    //            //  var item = listOfItems.FirstOrDefault(s => s.ItemId == (int) x);

    //            SiteOpen(item.FileName, item.Argument, item.InternetNeeded);
    //            // item.Visited += 1;
    //            // db.SaveChanges();
    //        }
    //    }

    //    private void Button_Click_1(object sender, RoutedEventArgs e)
    //    {
    //        string msg = "Wpf sample application";


    //        MaterialMessageBox.Show(msg, "Link List");

    //    }

    #endregion
    #region Property
    private Visibility _settingsVisibility;

    public Visibility SettingsVisibility
    {
        get { return _settingsVisibility; }
        set
        {
            _settingsVisibility = value;
            Notify(nameof(SettingsVisibility));
        }
    }
    private bool _sortByVisitedCount;

    public bool SortByVisitedCount
    {
        get { return _sortByVisitedCount; }
        set { _sortByVisitedCount = value;
            Notify(nameof( SortByVisitedCount));
        }
    }
    private string _filter;

    public string Filter
    {
        get { return _filter; }
        set
        {
            _filter = value;
            Notify(nameof(Filter));
        }
    }

    #endregion

}
