using SQLite;
using People.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace People;

public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public async Task AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            await Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = await conn.InsertAsync(new Person { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public async Task AddPeople(string[] people)
    {
        int result = 0;

        try
        {
            await Init();

            // basic validation to ensure a name was entered
            if (people.Count() < 1)
                throw new Exception("Valid name required");

            foreach (var item in people)
            {
                await conn.InsertAsync(new Person()
                {
                    Name = item
                });

                result++;
            }

            StatusMessage = string.Format("{0} record(s) added", result);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add entries. Error: {1}", ex.Message);
        }
    }

    public async Task<List<Person>> GetAllPeople()
    {
        List<Person> result = new List<Person>();

        try
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            await Init();

            return result = await conn.Table<Person>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return result;
    }
}
