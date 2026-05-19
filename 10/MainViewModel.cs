using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract10
{
    public class MainViewModel : BindableBase
    {
        private readonly RestService _restService;

        public MainViewModel()
        {
            _restService = new RestService();

            ReloadExperiments();
        }

        public ObservableCollection<Experiment> Experiments
        { get; set; }
            = new ObservableCollection<Experiment>();

        private async Task ReloadExperiments()
        {
            var items =
                await _restService.GetExperimentsAsync();

            var result = items.OrderBy(x => x.Id).ToList();

            Experiments.Clear();

            result.ForEach(x =>
            {
                Experiments.Add(x);
            });
        }

        private string _newTitle;

        public string NewTitle
        {
            get => _newTitle;
            set => SetProperty(ref _newTitle, value);
        }

        private string _newEquipment;

        public string NewEquipment
        {
            get => _newEquipment;
            set => SetProperty(ref _newEquipment, value);
        }

        private string _newResult;

        public string NewResult
        {
            get => _newResult;
            set => SetProperty(ref _newResult, value);
        }

        public async Task SaveNewExperiment()
        {
            await _restService.CreateExperimentAsync(
                new Experiment
                {
                    Name = NewTitle,
                    UsedMaterial = NewEquipment,
                    Result = NewResult,
                });

            await ReloadExperiments();

            ClearNewExperiment();
        }

        public void ClearNewExperiment()
        {
            NewTitle = "";
            NewEquipment = "";
            NewResult = "";
        }
    }
}
