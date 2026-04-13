using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repositories;

namespace DataDemo.Pages;

public partial class StudentsPage : ContentPage
{
    private readonly StudentRepository studentRepository;

    public StudentsPage()
    {
        InitializeComponent();

        DatabaseService db = new DatabaseService();
        studentRepository = new StudentRepository(db);

        AddButton.Clicked += AddButton_Clicked;
        StudentsCollectionView.SelectionChanged += StudentsCollectionView_SelectionChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadStudents();
    }

    private void LoadStudents()
    {
        List<Student> students = studentRepository.GetAll();
        StudentsCollectionView.ItemsSource = students;
    }

    private async void AddButton_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentPage());
    }

    private async void StudentsCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Student selectedStudent)
        {
            await Navigation.PushAsync(new StudentPage(selectedStudent));

            // Clear selection so it doesn't stay highlighted
            StudentsCollectionView.SelectedItem = null;
        }
    }

    // Edit button inside template
    private async void EditButton_Clicked(object? sender, EventArgs e)
    {
        var button = (Button)sender;
        var student = (Student)button.BindingContext;

        await Navigation.PushAsync(new StudentPage(student));
    }

    // Delete button inside template
    private async void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        var button = (Button)sender;
        var student = (Student)button.BindingContext;

        bool confirm = await DisplayAlert(
            "Delete",
            $"Delete {student.FirstName} {student.LastName}?",
            "Yes",
            "No");

        if (!confirm) return;

        studentRepository.Delete(student.Id);

        LoadStudents();
    }

    private void EditButton_Clicked_1(object sender, EventArgs e)
    {

    }
}