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
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadStudents();
    }

    private void LoadStudents()
    {
        StudentsCollectionView.ItemsSource = studentRepository.GetAll();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentPage());
    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var student = (Student)button.BindingContext;

        await Navigation.PushAsync(new StudentPage(student));
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var student = (Student)button.BindingContext;

        bool confirm = await DisplayAlertAsync(
            "Delete",
            $"Delete {student.FirstName} {student.LastName}?",
            "Yes",
            "No");

        if (!confirm) return;

        studentRepository.Delete(student.Id);
        LoadStudents();
    }
}