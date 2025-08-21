using System;
using System.Collections.Generic;
using System.Text;
using Wedency;
using WebGen.Controls;

namespace SharedUnitTest
{
    /// <summary>
    /// 带有<see cref="WedencyProperty"/>的类
    /// </summary>
    internal class Class1:Control
    {

        public string Foo
        {
            get { return (string)GetValue(FooProperty); }
            set { SetValue(FooProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foo.  This enables animation, styling, binding, etc...
        public static readonly WedencyProperty FooProperty;
        //=
           // WedencyProperty.Register<Class1,string>("Foo","foodefualt");

        
    }
}
