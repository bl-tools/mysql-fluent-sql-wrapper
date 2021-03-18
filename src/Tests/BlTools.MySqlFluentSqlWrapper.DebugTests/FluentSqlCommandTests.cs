using System.Data;
using System.Linq;
using NUnit.Framework;

namespace BlTools.MySqlFluentSqlWrapper.DebugTests
{
    internal sealed class FluentSqlCommandTests
    {
        private const string ConnectionString = "";

        [Test]
        [Ignore("just example")]
        public void ExampleExecuteStoredProcedureCallReturnsData()
        {
            var products = new FluentSqlCommand(ConnectionString)
                .Procedure("procedureName")
                .AddParam("param1", "value1")
                .AddParam("param2", "value2")
                .ExecReadList(ReadData);
            Assert.IsNotEmpty(products);
            var firstProduct = products.First();
            Assert.IsNotNull(firstProduct.ValueFromDb);
        }

        private static DataModel ReadData(IDataReader record)
        {
            return new DataModel
            {
                ValueFromDb = record.GetStringNull("valueName")
            };
        }

        private sealed class DataModel
        {
            public string ValueFromDb { get; set; }
        }
    }
}