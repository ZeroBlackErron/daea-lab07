using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Semana07
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            /*With out Lambda*/
            IntroToLinQ();
            DataSource();
            Filtering();
            Ordering();
            Grouping();
            Grouping2();
            Joining();

            /*With Lambda*/
            IntroToLinQWithLambda();
            DataSourceWithLambda();
            FilteringWithLambda();
            OrderingWithLambda();
            GroupingWithLambda();
            Grouping2WithLambda();
            JoiningWithLambda();

            Console.Read();
        }

        static void IntroToLinQ()
        {
            int[] numbers = new int[7] {0, 1, 2, 3, 4, 5, 6};

            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            Console.WriteLine("-----IntroToLinQ-----");
            foreach (int num in numQuery)
            {
                Console.WriteLine("{0, 1} ", num);
            }
            Console.WriteLine("---------------------");
        }

        static void IntroToLinQWithLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = numbers.Where(n => n % 2 == 0);

            Console.WriteLine("-----IntroToLinQ (Lambda)-----");
            foreach (int num in numQuery)
            {
                Console.WriteLine("{0, 1} ", num);
            }
            Console.WriteLine("---------------------");
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                           select cust;

            Console.WriteLine("-----DataSource-----");
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
            Console.WriteLine("---------------------");
        }

        static void DataSourceWithLambda()
        {
            var queryAllCustomers = context.clientes;

            Console.WriteLine("-----DataSource (Lambda)-----");
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
            Console.WriteLine("---------------------");
        }

        static void Filtering()
        {
            var queryLondomCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            Console.WriteLine("-----Filtering-----");
            foreach (var item in queryLondomCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
            Console.WriteLine("---------------------");
        }

        static void FilteringWithLambda()
        {
            var queryLondomCustomers = context.clientes.Where(c => c.Ciudad == "Londres");

            Console.WriteLine("-----Filtering (Lambda)-----");
            foreach (var item in queryLondomCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
            Console.WriteLine("---------------------");
        }

        static void Ordering()
        {
            var queryLondomCustomersOrder = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       orderby cust.NombreCompañia ascending
                                       select cust;

            Console.WriteLine("-----Ordering-----");
            foreach (var item in queryLondomCustomersOrder)
            {
                Console.WriteLine(item.NombreCompañia);
            }
            Console.WriteLine("---------------------");
        }

        static void OrderingWithLambda()
        {
            var queryLondomCustomersOrder = context.clientes
                .Where(c => c.Ciudad == "Londres")
                .OrderBy(c => c.NombreCompañia);

            Console.WriteLine("-----Ordering (Lambda)-----");
            foreach (var item in queryLondomCustomersOrder)
            {
                Console.WriteLine(item.NombreCompañia);
            }
            Console.WriteLine("---------------------");
        }

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            Console.WriteLine("-----Grouping-----");
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }
            Console.WriteLine("---------------------");
        }

        static void GroupingWithLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad);

            Console.WriteLine("-----Grouping (Lambda)-----");
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("     {0}", customer.NombreCompañia);
                }
            }
            Console.WriteLine("---------------------");
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 1
                            orderby custGroup.Key
                            select custGroup;

            Console.WriteLine("-----Grouping2-----");
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
            Console.WriteLine("---------------------");
        }

        static void Grouping2WithLambda()
        {
            var custQuery = context.clientes.GroupBy(c => c.Ciudad).Where(c => c.Count() > 1).OrderBy(c => c.Key);

            Console.WriteLine("-----Grouping2 (Lambda)-----");
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
            Console.WriteLine("---------------------");
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            Console.WriteLine("-----Joining-----");
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
            Console.WriteLine("---------------------");
        }

        static void JoiningWithLambda()
        {
            var innerJoinQuery = context.clientes.Join(
                context.Pedidos,
                cliente => cliente.idCliente,
                pedido => pedido.IdCliente,
                (cliente, pedido) => new { CustomerName = cliente.NombreCompañia, DistributorName = pedido.PaisDestinatario }
                );

            Console.WriteLine("-----Joining (Lambda)-----");
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
            Console.WriteLine("---------------------");
        }
    }
}
