<%@ WebService Language="C#" Class="ProductsService" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class ProductsService : WebService {

    [WebMethod]
    public Product[] GetTopTenProducts()
    {
        return GetProducts();
    }

    private Product[] GetProducts()
    {
        List<Product> products = new List<Product>();
        try
        {
            using (SqlConnection connection = new SqlConnection())
            using (SqlCommand command = new SqlCommand())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT TOP 10 * FROM Production.Product";

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    product.ID = (int)reader["ProductID"];
                    product.Name = (string)reader["Name"];

                    products.Add(product);
                }
            }
        }
        finally
        {
        }

        return products.ToArray();
    }

    public class Product
    {
        private int id;
        private string name;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }     
}

