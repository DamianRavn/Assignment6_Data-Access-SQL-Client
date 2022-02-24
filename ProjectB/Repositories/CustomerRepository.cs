using Microsoft.Data.SqlClient;
using ProjectB.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProjectB.Repositories
{
    class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// ICustomerRepository specific. Get a customer by name
        /// </summary>
        /// <param name="name">the name of the customer. Can be first or last name</param>
        /// <returns>A list of customers matching the name</returns>
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

        /// <summary>
        /// ICustomerRepository specific. Gets customers from the database, with the correct offset and amount.
        /// </summary>
        /// <param name="offset">how many customers are we skipping</param>
        /// <param name="limit">how many customers are wanted</param>
        /// <returns>a 'page' of customers</returns>
        public List<Customer> GetCustomerPage(int offset, int limit)
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

        /// <summary>
        /// ICustomerRepository specific. How many customers per country?
        /// </summary>
        /// <returns>A list of CustomerCountry objects, representing the population per country</returns>
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

        /// <summary>
        /// ICustomerRepository specific. Goes through the invoice table to find the customers who spent the most
        /// </summary>
        /// <returns>A sorted list of customers and how much they have spent, highest first</returns>
        public List<CustomerSpender> GetHighestSpenders()
        {
            List<CustomerSpender> customerSpenderList = new List<CustomerSpender>();
            string sql = 
                "SELECT Customer.CustomerID, Customer.FirstName, Customer.LastName, Customer.Country, Customer.PostalCode, Customer.Phone, Customer.Email, SUM(Invoice.Total) " +
                "FROM Customer INNER JOIN Invoice " +
                "ON Customer.CustomerID = Invoice.CustomerID " +
                "GROUP BY Customer.CustomerID, Customer.FirstName, Customer.LastName, Customer.Country, Customer.PostalCode, Customer.Phone, Customer.Email " +
                "ORDER BY SUM(Invoice.Total) DESC";

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
                                customer.Phone = reader.IsDBNull(5) ? "undefined" : reader.GetString(5);
                                customer.Email = reader.GetString(6);

                                CustomerSpender spender = new CustomerSpender();
                                spender.Customer = customer;
                                spender.Amount = reader.GetDecimal(7);
                                customerSpenderList.Add(spender);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return customerSpenderList;
        }

        /// <summary>
        /// ICustomerRepository specific. Looks thought the invoice, invoiceLine, Track and Genre table to find the customers favorite genres (2 if tied. Never more)
        /// </summary>
        /// <param name="customer">A customer object. Has to have CustomerId</param>
        /// <returns>A customergenre object with the needed information</returns>
        public CustomerGenre GetCustomersPopularGenre(Customer customer)
        {
            List<CustomerGenre> customerGenreList = new List<CustomerGenre>(2);

            //Pretty much speaks for itself.
            //Starting with the customerid, which is given as a paramater, we know which invoices we need, then we use those invoiceIds to find the trackIds, then we use those trackIds to find the genreId,
            //Then we use that genreId to find genre, so we can group it by genre name and get the count of the same genre.
            //Easy
            string sql =
                "SELECT TOP 1 WITH TIES COUNT(Genre.GenreId), Genre.Name " +
                "FROM Invoice " +
                "INNER JOIN InvoiceLine ON Invoice.InvoiceID = InvoiceLine.InvoiceID " +
                "INNER JOIN Track ON InvoiceLine.TrackId = Track.TrackId " +
                "INNER JOIN Genre ON Track.GenreId = Genre.GenreId " +
                "WHERE Invoice.CustomerId = @CustomerId " +
                "GROUP BY Genre.Name " +
                "ORDER BY COUNT(Genre.GenreId) DESC;";

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
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Handle result
                                CustomerGenre customerGenre = new CustomerGenre();
                                customerGenre.Customer = customer;

                                customerGenre.BoughtTracks = reader.GetInt32(0);
                                customerGenre.Genre = reader.GetString(1);

                                customerGenreList.Add(customerGenre);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            //Making sure to catch posible errors
            //If the customer has not bought any tracks
            if (customerGenreList.Count == 0)
            {
                CustomerGenre customerGenre = new CustomerGenre();
                customerGenre.Customer = customer;
                customerGenre.BoughtTracks = 0;
                customerGenre.Genre = "Nothing";

                return customerGenre;
            }

            //If count is not 1, it means that a genre is tied. Did not add support for more than 1 tie.
            if (customerGenreList.Count != 1)
            {
                customerGenreList[0].Genre += " and " + customerGenreList[1].Genre;
            }

            return customerGenreList[0];
        }

        /// <summary>
        /// IRepository. Goes though the Customers table and gets the information needed for the customer object
        /// </summary>
        /// <returns>List of all Customer objects</returns>
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

        /// <summary>
        /// IRepository. Use the a customer id to get the rest of the customer information
        /// </summary>
        /// <param name="id">has to correspond to a customer in the customer table</param>
        /// <returns>the customer the id corresponds to</returns>
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

        /// <summary>
        /// IRepository. Add a customer to the table. Only the information from the customer object will be added, even though the table has more columns.
        /// </summary>
        /// <param name="customer">the customer to be added</param>
        /// <returns>Did the insert succeed?</returns>
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

        /// <summary>
        /// Delete a customer from the customer table. Uses the customerId
        /// </summary>
        /// <param name="customer">a customer object. Only customerId is important</param>
        /// <returns>Did the delete succeed</returns>
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

        /// <summary>
        /// Updates a customer in the customer table to that of the customer object, using the customerId
        /// </summary>
        /// <param name="customer">a customer object, which has an id corresponding to a customer in the customer table</param>
        /// <returns>Did the Update succeed</returns>
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
