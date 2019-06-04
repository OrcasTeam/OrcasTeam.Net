using System;
using System.Collections.Generic;
using System.Text;
using OrcasTeam.Shandard.Libary.Extensions.Attribute;
using OrcasTeam.Shandard.Libary.Test.Extensions.Attribute;
using Xunit;

namespace OrcasTeam.Shandard.Libary.Test.Extensions
{
    public class AttributeExtensionTest
    {
        [Fact]
        public void HasArrtibuteTest()
        {
            Assert.False(typeof(Student).HasAttribute<NameAttribute>());
            Assert.True(typeof(Student).HasAttribute<NameAttribute>(inherit: true));
            Assert.False(new Student().HasAttribute<NameAttribute>());
            Assert.True(new Student().HasAttribute<NameAttribute>(inherit: true));
            Assert.False(typeof(Student).GetMethod("Method1").HasAttribute<NameAttribute>());
            Assert.True(typeof(Student).HasAttribute<NoAttribute>(inherit: true));
        }
    }
}
