using People.Models;
using System.Collections.Generic;

namespace People;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        string[] people = newPerson.Text.Replace(" ", "").Split(',');

        if (people.Length <= 1)
            await App.PersonRepo.AddNewPerson(newPerson.Text);
        else
            await App.PersonRepo.AddPeople(people);

        statusMessage.Text = App.PersonRepo.StatusMessage;
    }

    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Person> people = await App.PersonRepo.GetAllPeople();
        peopleList.ItemsSource = people;
    }

}

