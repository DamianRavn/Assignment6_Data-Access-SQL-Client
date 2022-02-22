using Microsoft.Data.SqlClient;
using ProjectB.Models;
using System;
using System.Collections.Generic;

namespace ProjectB.Repositories
{
    class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customers";

            try
            {
                //Connect
                using (SqlConnection connection  = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
                {
                    connection.Open();
                    //Make a command
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                //Handle result
                                Customer customer = new Customer();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Country = reader.GetString(3);
                                customer.PostalCode = reader.GetString(4);
                                customer.Phone = reader.GetString(5);
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

        public Customer GetCustomer(int id)
        {
            Customer customer = new Customer();
            string sql = "SELECT CustomerID, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customers WHERE CustomerID = @CustomerID";

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
                                customer.Country = reader.GetString(3);
                                customer.PostalCode = reader.GetString(4);
                                customer.Phone = reader.GetString(5);
                                customer.Email = reader.GetString(6);

                                customer.Add(customer);
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

        public bool AddNewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
