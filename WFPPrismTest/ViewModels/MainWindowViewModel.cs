using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

public class MainWindowViewModel : BindableBase
{
    private readonly CustomerService _service;

    public MainWindowViewModel(CustomerService service)
    {
        _service = service;
        SearchCommand = new DelegateCommand(Search);
        Customers = new ObservableCollection<Customer>();
    }

    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ObservableCollection<Customer> Customers { get; }

    public DelegateCommand SearchCommand { get; }

    private void Search()
    {
        Customers.Clear();
        foreach (var c in _service.SearchByNamePrefix(SearchText))
        {
            Customers.Add(c);
        }
    }
}