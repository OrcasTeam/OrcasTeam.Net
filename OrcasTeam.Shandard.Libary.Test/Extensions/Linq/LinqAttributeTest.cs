using System.Collections.Generic;
using OrcasTeam.Shandard.Libary.Extensions;
using Xunit;

namespace OrcasTeam.Shandard.Libary.Test.Extensions
{
    public class LinqAttributeTest
    {
        [Fact]
        public void AddRangeTest()
        {
            IList<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
            List<string> list2 = new List<string>();
            list.AddRange(new List<string>
            {
                "4",
                "5"
            });


            Assert.Equal<int>(5, list.Count);
        }
    }
}
