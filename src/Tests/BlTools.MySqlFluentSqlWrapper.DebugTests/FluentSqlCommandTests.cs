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
                .AddParameter("param1", "value1")
                .AddParameter("param2", "value2")
                .ExecuteReadRecordsList(ReadData);
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