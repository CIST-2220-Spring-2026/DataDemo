using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repositories;

namespace DataDemo.Pages;

public partial class StudentPage : ContentPage
{
    private readonly StudentRepository studentRepository;
    private readonly MajorRepository majorRepository;

    private Student student;
    private List<Major> majors = new List<Major>();

    public StudentPage() : this(new Student())
    {
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

        if (student.MajorId != null)
        {
            MajorPicker.SelectedItem =
                majors.FirstOrDefault(m => m.Id == student.MajorId);
        }
    }

    private void LoadMajors()
    {
        majors = majorRepository.GetAll();
        MajorPicker.ItemsSource = majors;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var selectedMajor = MajorPicker.SelectedItem as Major;

        student.Major = selectedMajor;
        student.MajorId = selectedMajor?.Id;

        if (student.Id == 0)
            studentRepository.Add(student);
        else
            studentRepository.Update(student);

        await Navigation.PopAsync();
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}