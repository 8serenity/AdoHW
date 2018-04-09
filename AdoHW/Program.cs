using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Создайте приложение по шаблону ConsoleApplication.
//В этом приложении сделайте экземпляр класса DataSet с именем
//ShopDB.Создайте объекты DataTable с именами Orders, Customers,
//Employees, OrderDetails, Products со схемами, идентичными схемам
//таблицам базы данных ShopDB(c ограничениями для столбцов).
//Добавьте созданные таблицы в коллекцию таблиц ShopDB.
//Для всех таблиц в ShopDB создайте ограничения PrimaryKey и
//ForeignKey.


namespace AdoHW
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet shopDb = new DataSet();

            DataTable products = new DataTable("Products");
            {
                DataColumn idColumn = new DataColumn("Id", typeof(int));
                DataColumn name = new DataColumn("Name", typeof(string));
                DataColumn price = new DataColumn("Price", typeof(float));
                products.Columns.AddRange(new DataColumn[] { idColumn, name, price });

                products.PrimaryKey = new DataColumn[] { idColumn };

                DataRow newRow = products.NewRow();
                newRow[idColumn] = 1;
                newRow[name] = "Laptop";
                newRow[price] = 989.99;

                products.Rows.Add(newRow);
            }



            shopDb.Tables.Add(products);

            DataTable customers = new DataTable("Customers");
            {
                DataColumn idColumn = new DataColumn("Id", typeof(int));
                DataColumn name = new DataColumn("Name", typeof(string));
                DataColumn surname = new DataColumn("Surname", typeof(string));
                customers.Columns.AddRange(new DataColumn[] { idColumn, name, surname });

                customers.PrimaryKey = new DataColumn[] { idColumn };

                DataRow newRow = customers.NewRow();
                newRow[idColumn] = 1;
                newRow[name] = "John";
                newRow[surname] = "Smith";

                customers.Rows.Add(newRow);
            }

            shopDb.Tables.Add(customers);

            DataTable employees = new DataTable("Employees");
            {
                DataColumn idColumn = new DataColumn("Id", typeof(int));
                DataColumn name = new DataColumn("Name", typeof(string));
                DataColumn surname = new DataColumn("Surname", typeof(string));
                employees.Columns.AddRange(new DataColumn[] { idColumn, name, surname });

                employees.PrimaryKey = new DataColumn[] { idColumn };

                DataRow newRow = employees.NewRow();
                newRow[idColumn] = 1;
                newRow[name] = "Adam";
                newRow[surname] = "Aaron";

                employees.Rows.Add(newRow);
            }

            shopDb.Tables.Add(employees);

            DataTable orders = new DataTable("Orders");
            {
                DataColumn idColumn = new DataColumn("Id", typeof(int));
                DataColumn customerId = new DataColumn("CustomerId", typeof(int));
                DataColumn employeeId = new DataColumn("EmployeeId", typeof(int));
                DataColumn saleDate = new DataColumn("SaleDate", typeof(DateTime));
                DataColumn totalSum = new DataColumn("TotalSum", typeof(float));
                orders.Columns.AddRange(new DataColumn[] { idColumn, customerId, employeeId, saleDate, totalSum });

                orders.PrimaryKey = new DataColumn[] { idColumn };


                DataRow newRow = orders.NewRow();
                newRow[idColumn] = 1;
                newRow[customerId] = 1;
                newRow[employeeId] = 1;
                newRow[saleDate] = DateTime.Today;
                newRow[totalSum] = 2325235;

                orders.Rows.Add(newRow);

            }

            shopDb.Tables.Add(orders);
            ForeignKeyConstraint fkCustomer = new ForeignKeyConstraint(shopDb.Tables["Customers"].Columns["Id"], shopDb.Tables["Orders"].Columns["CustomerId"]);
            ForeignKeyConstraint fkEmployee = new ForeignKeyConstraint(shopDb.Tables["Employees"].Columns["Id"], shopDb.Tables["Orders"].Columns["EmployeeId"]);
            shopDb.Tables["Orders"].Constraints.AddRange(new Constraint[] { fkCustomer, fkEmployee });



            DataTable orderDetails = new DataTable("OrderDetails");
            {
                DataColumn orderId = new DataColumn("OrderId", typeof(int));
                DataColumn productId = new DataColumn("ProductId", typeof(int));
                orderDetails.Columns.AddRange(new DataColumn[] { orderId, productId, });

                DataRow newRow = orderDetails.NewRow();
                newRow[orderId] = 1;
                newRow[productId] = 1;

                orderDetails.Rows.Add(newRow);
            }

            shopDb.Tables.Add(orderDetails);

            ForeignKeyConstraint fkOrder = new ForeignKeyConstraint(shopDb.Tables["Orders"].Columns["Id"], shopDb.Tables["OrderDetails"].Columns["OrderId"]);
            ForeignKeyConstraint fkProduct = new ForeignKeyConstraint(shopDb.Tables["Products"].Columns["Id"], shopDb.Tables["OrderDetails"].Columns["ProductId"]);
            shopDb.Tables["OrderDetails"].Constraints.AddRange(new Constraint[] { fkOrder, fkProduct });

        }
    }
}
