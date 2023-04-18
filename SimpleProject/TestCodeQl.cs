namespace SimpleProject
{
    //https://codeql.github.com/codeql-query-help/csharp/cs-invalid-dynamic-call/
    public class TestCodeQl
    {
        class WithFoo
        {
            public void Foo(int i) { }
        }

        class WithoutFoo { }

        public void DoSomething()
        {
            dynamic o = new WithoutFoo();
            o.Foo(3);
        }
    }
}
