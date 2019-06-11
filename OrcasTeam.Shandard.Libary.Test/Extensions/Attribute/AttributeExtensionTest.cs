using OrcasTeam.Shandard.Libary.Extensions;
using OrcasTeam.Shandard.Libary.Test.Extensions.Attribute;
using Xunit;

namespace OrcasTeam.Shandard.Libary.Test.Extensions
{
    public class AttributeExtensionTest
    {
        [Fact]
        public void HasAttributeTest()
        {
           // Assert.Throws<ArgumentException>(() => typeof(Student).IsDefined(typeof(NameAttribute)));
            Assert.False(AttributeExtension.IsDefined<NameAttribute>(typeof(Student)));
            Assert.True(AttributeExtension.IsDefined<NameAttribute>(typeof(Student), inherit: true));
            Assert.False(new Student().IsDefined<NameAttribute>());
            Assert.True(new Student().IsDefined<NameAttribute>(inherit: true));
            Assert.False(typeof(Student).GetMethod("Method1").IsDefined<NameAttribute>());
            Assert.True(AttributeExtension.IsDefined<NoAttribute>(typeof(Student), inherit: true));
        }
    }
}
