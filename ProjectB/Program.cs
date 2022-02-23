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

            IRepository<Customer> repository = new CustomerRepository();
            //TestSelectAll(repository);
            //TestSelect(repository);
            //TestInsert(repository);
            //TestUpdate(repository);
            //TestDelete(repository);

            ICustomerRepository customerRepository = new CustomerRepository();
            //TestSelectName(customerRepository);
            //TestPage(customerRepository);
            //TestCountry(customerRepository);
            TestSpenders(customerRepository);
        }

        static void TestSelectAll(IRepository<Customer> repository)
        {
            PrintCustomers(repository.GetAll());
        }
        static void TestSelect(IRepository<Customer> repository)
        {
            PrintCustomer(repository.GetById(34));
        }
        static void TestInsert(IRepository<Customer> repository)
        {
            Customer customer = new Customer()
            {
                FirstName = "Bjorgen",
                LastName = "Hansen",
                Country = "DK",
                PostalCode = "1111",
                Phone = "88888888",
                Email = "Test@Test.dk"

            };

            if (repository.Add(customer))
            {
                Console.WriteLine($"{customer.FirstName} was inserted successfully");
                PrintCustomer(repository.GetById(60));
            }
            else
            {
                Console.Error.WriteLine($"{customer.FirstName} not inserted");
            }
        }
        static void TestUpdate(IRepository<Customer> repository)
        {
            Customer customer = new Customer()
            {
                CustomerId = 60,
                FirstName = "Tester",
                LastName = "Testerson",
                Country = "DK",
                PostalCode = "1111",
                Phone = "88888888",
                Email = "Test@Test.dk"

            };

            if (repository.Update(customer))
            {
                Console.WriteLine($"{customer.FirstName} was updated successfully");
                PrintCustomer(repository.GetById(60));
            }
            else
            {
                Console.Error.WriteLine($"{customer.FirstName} not inserted");
            }
        }
        static void TestDelete(IRepository<Customer> repository)
        {
            Customer customer = new Customer()
            {
                CustomerId = 60,
                FirstName = "Tester",
                LastName = "Testerson",
                Country = "DK",
                PostalCode = "1111",
                Phone = "88888888",
                Email = "Test@Test.dk"

            };
            PrintCustomers(repository.GetAll());
            if (repository.Delete(customer))
            {
                PrintCustomers(repository.GetAll());
            }
            else
            {
                Console.Error.WriteLine($"60 not deleted");
            }
        }

        private static void TestSelectName(ICustomerRepository customerRepository)
        {
            PrintCustomers(customerRepository.GetByName("Hansen"));
        }

        private static void TestPage(ICustomerRepository customerRepository)
        {
            PrintCustomers(customerRepository.GetCustomerPage(30, 30));
        }

        private static void TestCountry(ICustomerRepository customerRepository)
        {
            PrintALLCountryPopulation(customerRepository.GetCountryCustomerPopulation());
        }

        private static void TestSpenders(ICustomerRepository customerRepository)
        {
            PrintAllSpenders(customerRepository.GetHighestSpenders());
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
            Console.WriteLine($"---- {customer.CustomerId}, {customer.FirstName} {customer.LastName}, {customer.Country}, {customer.PostalCode}, {customer.Phone}, {customer.Email} ----");
        }

        static void PrintALLCountryPopulation(List<CustomerCountry> populationObjectList)
        {
            foreach (var populationObject in populationObjectList)
            {
                PrintCountryPopulation(populationObject);
            }
        }

        static void PrintCountryPopulation(CustomerCountry populationObject)
        {
            Console.WriteLine($"---- {populationObject.Country}, {populationObject.Population} ----");
        }

        static void PrintAllSpenders(List<CustomerSpender> spendersList)
        {
            foreach (var spender in spendersList)
            {
                PrintSpender(spender);
            }
        }
        static void PrintSpender(CustomerSpender spender)
        {
            Console.WriteLine($"---- {spender.Customer.CustomerId} - {spender.Customer.FirstName} {spender.Customer.LastName} spent total: {spender.Amount} ----");
        }
    }
}
