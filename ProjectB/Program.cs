using ProjectB.Models;
using ProjectB.Repositories;
using System;
using System.Collections.Generic;

namespace ProjectB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ICustomerRepository repository = new CustomerRepository();
            TestSelectAll(repository);
        }

        static void TestSelectAll(ICustomerRepository repository)
        {
            PrintCustomers(repository.GetAllCustomers());
        }
        static void TestSelect(ICustomerRepository repository)
        {
            PrintCustomer(repository.GetCustomer(0));
        }
        static void TestInsert(ICustomerRepository repository)
        {
            Customer customer = new Customer()
            {
                CustomerId = 0,
                FirstName = "Test",
                LastName = "Testerson",
                Country = "DK",
                Phone = "88888888",
                Email = "Test@Test.dk"

            };

            if (repository.AddNewCustomer(customer))
            {
                Console.WriteLine($"{customer.FirstName} was inserted successfully");
            }
            else
            {
                Console.Error.WriteLine($"{customer.FirstName} not inserted");
            }
        }
        static void TestUpdate(ICustomerRepository repository)
        {

        }
        static void TestDelete(ICustomerRepository repository)
        {

        }

        static void PrintCustomers(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                PrintCustomer(customer);
            }
        }

        static void PrintCustomer(Customer customer)
        {
            Console.WriteLine($"---- {customer.CustomerId}  {customer.FirstName}    {customer.LastName} {customer.Country}  {customer.Phone}    {customer.Email}");
        }
    }
}
