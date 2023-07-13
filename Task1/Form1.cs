using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class Patient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Diagnosis { get; set; }
    public string Treatment { get; set; }
}

public class DataAnalysisModule
{
    private string connectionString; // Connection string for your database

    public DataAnalysisModule(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Patient> RetrievePatientData()
    {
        List<Patient> patients = new List<Patient>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT PatientId, Name, Age, Gender, Diagnosis, Treatment FROM Patients";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Patient patient = new Patient();
                        patient.PatientId = Convert.ToInt32(reader["PatientId"]);
                        patient.Name = reader["Name"].ToString();
                        patient.Age = Convert.ToInt32(reader["Age"]);
                        patient.Gender = reader["Gender"].ToString();
                        patient.Diagnosis = reader["Diagnosis"].ToString();
                        patient.Treatment = reader["Treatment"].ToString();

                        patients.Add(patient);
                    }
                }
            }
        }

        return patients;
    }

    public double GetAverageAge()
    {
        List<Patient> patients = RetrievePatientData();
        int totalAge = 0;

        foreach (Patient patient in patients)
        {
            totalAge += patient.Age;
        }

        return (double)totalAge / patients.Count;
    }

    public List<Patient> GetMalePatients()
    {
        List<Patient> patients = RetrievePatientData();
        List<Patient> malePatients = new List<Patient>();

        foreach (Patient patient in patients)
        {
            if (patient.Gender == "Male")
            {
                malePatients.Add(patient);
            }
        }

        return malePatients;
    }

    public List<Patient> GetPatientsWithDiagnosis(string diagnosis)
    {
        List<Patient> patients = RetrievePatientData();
        List<Patient> diagnosedPatients = new List<Patient>();

        foreach (Patient patient in patients)
        {
            if (patient.Diagnosis == diagnosis)
            {
                diagnosedPatients.Add(patient);
            }
        }

        return diagnosedPatients;
    }

    public List<Patient> GetPatientsByAgeRange(int minAge, int maxAge)
    {
        List<Patient> patients = RetrievePatientData();
        List<Patient> filteredPatients = new List<Patient>();

        foreach (Patient patient in patients)
        {
            if (patient.Age >= minAge && patient.Age <= maxAge)
            {
                filteredPatients.Add(patient);
            }
        }

        return filteredPatients;
    }
}

// Unit test example
public class DataAnalysisModuleTests
{
    public void RunTests()
    {
        DataAnalysisModule module = new DataAnalysisModule("YourConnectionString");

        // Test RetrievePatientData()
        List<Patient> allPatients = module.RetrievePatientData();
        Console.WriteLine("All Patients:");
        foreach (Patient patient in allPatients)
        {
            Console.WriteLine(patient.Name);
        }

        // Test GetAverageAge()
        double averageAge = module.GetAverageAge();
        Console.WriteLine("Average Age: " + averageAge);

        // Test GetMalePatients()
        List<Patient> malePatients = module.GetMalePatients();
        Console.WriteLine("Male Patients:");
        foreach (Patient patient in malePatients)
        {
            Console.WriteLine(patient.Name);
        }

        // Test GetPatientsWithDiagnosis()
        List<Patient> patientsWithDiagnosis = module.GetPatientsWithDiagnosis("Some Diagnosis");
        Console.WriteLine("Patients with Diagnosis:");
        foreach (Patient patient in patientsWithDiagnosis)
        {
            Console.WriteLine(patient.Name);
        }

        // Test GetPatientsByAgeRange()
        List<Patient> patientsInAgeRange = module.GetPatientsByAgeRange(20, 30);
        Console.WriteLine("Patients in Age Range:");
        foreach (Patient patient in patientsInAgeRange)
        {
            Console.WriteLine(patient.Name);
        }
    }
}

// Usage example
public class Program
{
    public static void Main(string[] args)
    {
        DataAnalysisModuleTests tests = new DataAnalysisModuleTests();
        tests.RunTests();
    }
}
