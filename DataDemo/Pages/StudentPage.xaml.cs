using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repositories;

namespace DataDemo.Pages;

public partial class StudentPage : ContentPage
{
    private readonly StudentRepository studentRepository;
    private readonly MajorRepository majorRepository;

    private Student student = new Student();
    private List<Major> majors = new List<Major>();

    public StudentPage()
    {
        InitializeComponent();

        DatabaseService db = new DatabaseService();
        studentRepository = new StudentRepository(db);
        majorRepository = new MajorRepository(db);

        LoadMajors();

        student = new Student();
        BindingContext = student;

        SaveButton.Clicked += SaveButton_Clicked;
        CancelButton.Clicked += CancelButton_Clicked;
    }

    public StudentPage(Student existingStudent)
    {
        InitializeComponent();

        DatabaseService db = new DatabaseService();
        studentRepository = new StudentRepository(db);
        majorRepository = new MajorRepository(db);

        LoadMajors();

        student = existingStudent;
        BindingContext = student;

        SelectStudentMajor();

        SaveButton.Clicked += SaveButton_Clicked;
        CancelButton.Clicked += CancelButton_Clicked;
    }

    private void LoadMajors()
    {
        majors = majorRepository.GetAll();
        MajorPicker.ItemsSource = majors;
        MajorPicker.ItemDisplayBinding = new Binding("Title");
    }

    private void SelectStudentMajor()
    {
        if (student.MajorId == null)
        {
            MajorPicker.SelectedItem = null;
            return;
        }

        Major? selectedMajor = majors.FirstOrDefault(m => m.Id == student.MajorId);

        if (selectedMajor != null)
        {
            MajorPicker.SelectedItem = selectedMajor;
        }
    }

    private async void SaveButton_Clicked(object? sender, EventArgs e)
    {
        Major? selectedMajor = MajorPicker.SelectedItem as Major;

        student.Major = selectedMajor;
        student.MajorId = selectedMajor?.Id;

        if (student.Id == 0)
        {
            studentRepository.Add(student);
        }
        else
        {
            studentRepository.Update(student);
        }

        await DisplayAlert("Saved", "Student saved successfully.", "OK");
        await Navigation.PopAsync();
    }

    private async void CancelButton_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}