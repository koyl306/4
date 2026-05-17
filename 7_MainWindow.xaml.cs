using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using pract7.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace pract7
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly DBContext _db = new DBContext();

        public MainWindow()
        {
            this.InitializeComponent();

            _db.Database.EnsureCreated();

            _db.Experiments.Load();

            listView.ItemsSource = _db.Experiments.Local.ToObservableCollection();
        }

        private void listView_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem is not Experiment exp)
                return;

            TextBoxName.Text = exp.Name;
            TextBoxMaterial.Text = exp.UsedMaterial;
            TextBoxResult.Text = exp.Result;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            _db.Experiments.Add(new Experiment
            {
                Name = TextBoxName.Text,

                // автоматична поточна дата
                Date = DateTime.Now.ToString("yyyy-MM-dd"),

                UsedMaterial = TextBoxMaterial.Text,
                Result = TextBoxResult.Text
            });

            _db.SaveChanges();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is not Experiment selected)
                return;

            var entity = _db.Experiments.Find(selected.ID);

            if (entity == null)
                return;

            entity.Name = TextBoxName.Text;

            entity.Date = DateTime.Now.ToString("yyyy-MM-dd");

            entity.UsedMaterial = TextBoxMaterial.Text;
            entity.Result = TextBoxResult.Text;

            _db.SaveChanges();

            listView.ItemsSource = null;

            listView.ItemsSource = _db.Experiments.Local.ToObservableCollection();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is not Experiment selected)
                return;

            var entity = _db.Experiments.Find(selected.ID);

            if (entity == null)
                return;

            _db.Experiments.Remove(entity);

            _db.SaveChanges();

            listView.ItemsSource = null;

            listView.ItemsSource = _db.Experiments.Local.ToObservableCollection();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxName.Text = "";
            TextBoxMaterial.Text = "";
            TextBoxResult.Text = "";
        }

        private async void ButtonDialog_Click(
            object sender,
            RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Інформація",
                Content = "Система працює",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}
