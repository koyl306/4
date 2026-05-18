using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace pract9;

public class MainViewModel : BindableBase
{
    private readonly DbService _db = new();

    public ObservableCollection<Experiment> Experiments { get; set; }
        = new();

    public MainViewModel()
    {
        Load();
    }

    public void Load()
    {
        var items = _db.GetAll();

        Experiments.Clear();

        foreach (var item in items)
            Experiments.Add(item);
    }

    public string? Name { get; set; }
    public string? UsedMaterial { get; set; }
    public string? Result { get; set; }

    public void Add()
    {
        _db.Insert(new Experiment
        {
            Name = Name,
            UsedMaterial = UsedMaterial,
            Result = Result
        });

        Load();

        Name = "";
        UsedMaterial = "";
        Result = "";
    }

    public void Delete(int id)
    {
        _db.Delete(id);
        Load();
    }
}