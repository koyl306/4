using Microsoft.EntityFrameworkCore;
using pract8;
using System;
using System.Collections.ObjectModel;
using System.Linq;

public class MainViewModel : BindableBase
{
    private readonly AppDbContext _db = new();

    public MainViewModel()
    {
        _db.Database.EnsureCreated();
        Reload();
    }

    public ObservableCollection<Experiment> Experiments { get; set; }
        = new();

    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _material;
    public string UsedMaterial
    {
        get => _material;
        set => SetProperty(ref _material, value);
    }

    private string _result;
    public string Result
    {
        get => _result;
        set => SetProperty(ref _result, value);
    }

    public int TotalCount => Experiments.Count;

    public void Reload()
    {
        var items = _db.Experiments.ToList();

        Experiments.Clear();
        items.ForEach(x => Experiments.Add(x));

        OnPropertyChanged(nameof(TotalCount));
    }

    public void Add()
    {
        _db.Experiments.Add(new Experiment
        {
            Name = Name,
            UsedMaterial = UsedMaterial,
            Result = Result,
            Date = DateTime.Now.ToString("yyyy-MM-dd")
        });

        _db.SaveChanges();

        Clear();
        Reload();
    }

    public void Clear()
    {
        Name = "";
        UsedMaterial = "";
        Result = "";
    }

    public void Sort()
    {
        var sorted = Experiments.OrderBy(x => x.Name).ToList();

        Experiments.Clear();
        sorted.ForEach(x => Experiments.Add(x));
    }
}