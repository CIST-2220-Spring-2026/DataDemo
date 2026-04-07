using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repositories;

namespace DataDemo.Pages;

public partial class MajorsPage : ContentPage
{
    private readonly DatabaseService db;
    private readonly MajorRepository repo;
    private Major? selectedMajor; 
    
    public MajorsPage()
    {
        InitializeComponent(); 
        db = new DatabaseService();
        repo = new MajorRepository(db); 
        LoadMajores();
    }

    private void LoadMajores()
    {
        cvMajors.ItemsSource = repo.GetAll();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string title = txtMajorTitle.Text?.Trim() ?? ""; 
        if (title == "")
        {
            lblStatus.Text = "Enter a Major title before adding.";
            return;
        }

        Major Major = new Major
        {
            Title = title
        }; 
        
        repo.Add(Major); 
        LoadMajores();
        ClearForm(); 
        lblStatus.Text = "Major added.";
    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {
        if (selectedMajor == null)
        {
            lblStatus.Text = "Select a Major before updating.";
            return;
        }

        string title = txtMajorTitle.Text?.Trim() ?? ""; if (title == "")
        {
            lblStatus.Text = "Enter a Major title before updating.";
            return;
        }

        selectedMajor.Title = title;
        repo.Update(selectedMajor); 
        LoadMajores();
        ClearForm(); lblStatus.Text = "Major updated.";
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (selectedMajor == null)
        {
            lblStatus.Text = "Select a Major before deleting.";
            return;
        }

        repo.Delete(selectedMajor.Id); 
        LoadMajores();
        ClearForm(); 
        lblStatus.Text = "Major deleted.";
    }
    private void OnClearClicked(object sender, EventArgs e)
    {
        ClearForm();
        lblStatus.Text = "Form cleared.";
    }

    private void OnMajorSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedMajor = e.CurrentSelection.FirstOrDefault() as Major; 
        
        if (selectedMajor != null)
        {
            txtMajorTitle.Text = selectedMajor.Title;
            lblStatus.Text = $"Selected Major Id {selectedMajor.Id}.";
        }
    }

    private void ClearForm()
    {
        selectedMajor = null;
        txtMajorTitle.Text = "";
        cvMajors.SelectedItem = null;
    }
}