using BespokeFusion;
using LinkManagerClientWPF.Common;
using LinkManagerClientWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace LinkManagerClientWPF.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private readonly ObservableCollection<LinksViewModel> _linksViewModel;

    public IEnumerable<LinksViewModel> LinkCollection => _linksViewModel;

    // private readonly LinkRepository _repository;
    public override string WindowTitle => "WPF Sample Project";


    #region commands

    public Command<Guid> OpenLinkCommand { get; set; }
    public Command<Guid> SelectALinkCommand { get; set; }
    public Command UpdateLocalDatabaseCommand { get; set; }
    public Command ExitCommand { get; set; }
    public Command AboutCommand { get; set; }

    public Command ChangeSettingVisibilityCommand { get; set; }
    public Command OpenMenuCommand { get; set; }
    public Command CloseMenuCommand { get; set; }


    #endregion

    #region constructor

    public MainWindowViewModel()
    {
        // _repository = new LinkRepository();
        OpenLinkCommand = new Command<Guid>(OpenLink);
        SelectALinkCommand = new Command<Guid>(ShowLinkDetails);
        ExitCommand = new Command(() => Application.Current.Shutdown());
        AboutCommand = new Command(() => MaterialMessageBox.Show($"Wpf sample By Mohammad Reza Margiran") );
        ChangeSettingVisibilityCommand = new Command(()=> { SettingsVisibility = ChangeSettingVisibility(); });
        OpenMenuCommand = new Command(OpenMenu);
        CloseMenuCommand = new Command(CloseMenu);
        OpenMenuVisibility = true;
        _linksViewModel = new ObservableCollection<LinksViewModel>();

        _linksViewModel.Add(new LinksViewModel(new Models.LinkModel { Title = "google", FileName = "chrome", Argument = "google.com", Id = Guid.NewGuid(), Order = 1, ShortDescription = "google" }));
        _linksViewModel.Add(new LinksViewModel(new Models.LinkModel { Title = "yahoo", FileName = "chrome", Argument = "yahoo.com", Id = Guid.NewGuid(), Order = 2, ShortDescription = "yahoo" }));
    }

    private void ShowLinkDetails(Guid id)
    {
        var link = _linksViewModel.FirstOrDefault(l => l.Id == id);
        if (link == null)
            return;
        SelectedLink = link;
        try
        {
            if (OpenMenuVisibility)
            {
                Storyboard? sb = Application.Current.MainWindow.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
                OpenMenu();
            }
            else
            {
                Storyboard? sb = Application.Current.MainWindow.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
                CloseMenu();
            }
        }
        catch { }


        
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

    private void OpenMenu()
    {
        OpenMenuVisibility = false;
        CloseMenuVisibility = true;
    }

    private void CloseMenu()
    {
        OpenMenuVisibility = true;
        CloseMenuVisibility = false;
    }

 

    #endregion
    #region Property

    private LinksViewModel _selectedLink;

    public LinksViewModel SelectedLink
    {
        get { return _selectedLink; }
        set { 
            _selectedLink = value; 
            Notify(nameof(SelectedLink));

        }
    }


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

    private bool _openMenuVisibility ;

    public bool OpenMenuVisibility
    {
        get { return _openMenuVisibility; }
        set { _openMenuVisibility = value;
            Notify(nameof(OpenMenuVisibility));
        }
    }

    private bool _closeMenuVisibility;

    public bool CloseMenuVisibility
    {
        get { return _closeMenuVisibility; }
        set { _closeMenuVisibility = value;
            Notify(nameof(CloseMenuVisibility));
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
