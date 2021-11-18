using LinkManagerClientWPF.Common;

namespace LinkManagerClientWPF.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    #region filed

    // private readonly SnackMachineRepository _repository;
    public override string WindowTitle => "Snack Machine Page Title";

    // public IReadOnlyList<SnackPileViewModel> Piles
    // {
    //     get
    //     {
    //         return _snackMachine.GetAllSnackPiles()
    //             .Select(s => new SnackPileViewModel(s))
    //             .ToList();
    //     }
    // }

    #endregion

    #region commands

    public Command OpenLinkCommand { get; set; }

    // public Command<string> BuySnackCommand { get; set; }

    #endregion

    #region constructor

    public MainWindowViewModel()
    {
        // _repository = new SnackMachineRepository();
        OpenLinkCommand = new Command(() => OpenLink());
        // BuySnackCommand = new Command<string>(BuySnack);


        // using (ISession session = SessionFactory.OpenSession())
        // {
        //     _snackMachine = session.Get<SnackMachine>(1L);
        //     NotifyClient();
        // }
    }

    #endregion

    #region methods
        private void OpenLink()
        {

        }
    #endregion

}
