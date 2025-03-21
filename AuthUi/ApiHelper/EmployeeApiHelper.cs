using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AuthUi.Models;
public class EmployeeApiHelper
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _jwtToken;

    public EmployeeApiHelper(string baseUrl, string jwtToken)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
        _jwtToken = jwtToken;
    }

    // Helper method to add Authorization Header
    private void AddAuthorizationHeader()
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_jwtToken}");
    }

    // Get all employees
    public async Task<string> GetAllEmployeesAsync()
    {
        try
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/Employee/AllEmployees");
            response.EnsureSuccessStatusCode();  // Throws exception for non-success status codes
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // Add a new employee
    public async Task<string> AddEmployeeAsync(Employee employee)
    {
        try
        {
            AddAuthorizationHeader();
            var jsonContent = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Employee/AddEmployee", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }



    // Update an existing employee
    public async Task<string> UpdateEmployeeAsync(Employee employee)
    {
        try
        {
            AddAuthorizationHeader();
            var jsonContent = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/Employee/UpdateEmployee", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> GetEmployeeByIdAsync(int id)
    {
        try
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/Employee/GetEmployeeById/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }


    // Delete an employee

    public async Task<string> DeleteEmployeeAsync(int employeeId)
    {
        try
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/Employee/DeleteEmployee/{employeeId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

}
