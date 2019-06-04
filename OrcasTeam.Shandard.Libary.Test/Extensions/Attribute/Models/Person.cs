using System;
using System.Collections.Generic;
using System.Text;

namespace OrcasTeam.Shandard.Libary.Test.Extensions.Attribute
{
    [Name("zhangsan")]
    internal class Person
    {
    }

    [No]
    internal class Student : Person
    {
        public void Method1()
        {
        }
    }
}
