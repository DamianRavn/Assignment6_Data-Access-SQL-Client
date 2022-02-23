using Microsoft.Data.SqlClient;
using ProjectB.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjectB.Repositories
{
    class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetByName(string name)
        {
            List<Customer> customerList = new List<Customer>();
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE CONCAT('%',@Name,'%') OR LastName LIKE CONCAT('%',@Name,'%') ";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Handle result
                                Customer customer = new Customer();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = reader.IsDBNull(3) ? "undefined" : reader.GetString(3);
                                customer.PostalCode = reader.IsDBNull(4) ? "undefined" : reader.GetString(4);
                                customer.Phone = reader.IsDBNull(5) ? "undefined" : reader.GetString(5);
                                customer.Email = reader.GetString(6);
                                customerList.Add(customer);
                            }

                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                Console.Error.WriteLine(ex.Message);
            }

            return customerList;
        }

        public List<Customer> GetCustomerPage(int limit, int offset)
        {
            List<Customer> customerList = new List<Customer>();
            //Limit was a lie. wasted a lot of time trying to use LIMIT. use FETCH NEXT instead
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer ORDER BY CustomerID OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Limit", limit);
                        command.Parameters.AddWithValue("@Offset", offset);
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Handle result
                                Customer customer = new Customer();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = reader.IsDBNull(3) ? "undefined" : reader.GetString(3);
                                customer.PostalCode = reader.IsDBNull(4) ? "undefined" : reader.GetString(4);
                                customer.Phone = reader.IsDBNull(5) ? "undefined" : reader.GetString(5);
                                customer.Email = reader.GetString(6);

                                customerList.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return customerList;
        }

        public List<CustomerCountry> GetCountryCustomerPopulation()
        {
            List<CustomerCountry> customerList = new List<CustomerCountry>();
            string sql = "SELECT COUNT(CustomerID), Country FROM Customer GROUP BY Country ORDER BY COUNT(CustomerID) DESC";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Handle result
                                CustomerCountry populationObject = new CustomerCountry();
                                populationObject.Population = reader.GetInt32(0);
                                populationObject.Country = reader.GetString(1);

                                customerList.Add(populationObject);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return customerList;
        }

        public List<CustomerSpender> GetHighestSpenders()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            List<Customer> customerList = new List<Customer>();
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Handle result
                                Customer customer = new Customer();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = reader.IsDBNull(3) ? "undefined" : reader.GetString(3);
                                customer.PostalCode = reader.IsDBNull(4) ? "undefined" : reader.GetString(4);
                                customer.Phone = reader.IsDBNull(5) ? "undefined": reader.GetString(5);
                                customer.Email = reader.GetString(6);

                                customerList.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return customerList;
        }

        public Customer GetById(int id)
        {
            Customer customer = new Customer();
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerID = @CustomerID";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", id);
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //Handle result
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = reader.IsDBNull(3) ? "undefined" : reader.GetString(3);
                                customer.PostalCode = reader.IsDBNull(4) ? "undefined" : reader.GetString(4);
                                customer.Phone = reader.IsDBNull(5) ? "undefined" : reader.GetString(5);
                                customer.Email = reader.GetString(6);
                            }
                            else
                            {
                                Console.Error.WriteLine("No customer returned from query");
                            }
                            
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                Console.Error.WriteLine(ex.Message);
            }

            return customer;
        }

        public bool Add(Customer customer)
        {
            bool success = false;
            string sql = "INSERT INTO Customer(FirstName, LastName, Country, PostalCode, Phone, Email) VALUES(@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Country", customer.Country);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        success = command.ExecuteNonQuery() >  0 ? true : false;
                    }
                }
            }
            catch (SqlException ex)
            {

                Console.Error.WriteLine(ex.Message);
            }

            return success;
        }

        public bool Delete(Customer customer)
        {
            bool success = false;
            string sql = "DELETE FROM Customer WHERE CustomerId = @CustomerId";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                        success = command.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
            }
            catch (SqlException ex)
            {

                Console.Error.WriteLine(ex.Message);
            }

            return success;
        }

        public bool Update(Customer customer)
        {
            bool success = false;
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Country = @Country, PostalCode = @PostalCode, Phone = @Phone, Email = @Email WHERE CustomerId = @CustomerId";

            try
            {
                //Connect
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Country", customer.Country);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        success = command.ExecuteNonQuery() > 0 ? true : false;
                    }
                }
            }
            catch (SqlException ex)
            {

                Console.Error.WriteLine(ex.Message);
            }

            return success;
        }
    }
}
